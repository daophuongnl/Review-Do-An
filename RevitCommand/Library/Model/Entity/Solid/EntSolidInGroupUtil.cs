using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class EntSolidInGroupUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static IOData ioData => IOData.Instance;

        public static EntSolidInGroup GetEntSolidInGroup(this EntSolid entSolid, EntSolidGroup entSolidGroup)
        {
            var entSolidInGroup = new EntSolidInGroup
            {
                EntSolidGroup = entSolidGroup,
                Solid = entSolid.Solid,
            };

            if (entSolid is ElementEntSolid)
            {
                entSolidInGroup.FirstElementId = (entSolid as ElementEntSolid)!.ElementId!.Value;
            }

            return entSolidInGroup;
        }

        public static IEnumerable<EntSolidInGroup> GetEntSolidInGroups(this EntSolid entSolid, EntSolidGroup entSolidGroup)
        {
            int? elemId = null;
            if (entSolid is ElementEntSolid)
            {
                elemId = (entSolid as ElementEntSolid)!.ElementId;
            }

            var ESIGs = entSolid.SplitSolids.Select(x => x.GetEntSolidInGroup(entSolidGroup, entSolid.PurgeTransform, elemId));

            return ESIGs;
        }

        public static EntSolidInGroup GetEntSolidInGroup(this Solid solid, EntSolidGroup entSolidGroup, Transform? purgeTransform = null, int? elementId = null)
        {
            var purgeTf = purgeTransform == null ? Transform.Identity : purgeTransform;

            var entSolidInGroup = new EntSolidInGroup
            {
                EntSolidGroup = entSolidGroup,
                Solid = solid,
                PurgeTransform = purgeTf
            };

            if (elementId != null)
            {
                entSolidInGroup.FirstElementId = elementId.Value;
            }

            return entSolidInGroup;
        }

        #region Property
        public static string? GetIndex(this EntSolidInGroup entSolidInGroup)
        {
            if (entSolidInGroup.EntSolidGroup is null) return null;

            var entSolidInGroups = entSolidInGroup.EntSolidGroup.EntSolidInGroups;
            var index = entSolidInGroups.IndexOf(entSolidInGroup).ToString();

            return index;
        }
        #endregion

        #region Method
        public static bool TryMerge(this EntSolidInGroup es1, EntSolidInGroup es2)
        {
            var mergeSuccess = false;

#pragma warning disable CS8604 // Possible null reference argument.
            if (es1.InvalidMergeIndexs.Contains(es2.Index))
            {
            }
            else
            {
                Solid? mergeSolid;

                try
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    mergeSolid = (es1 as EntSolid).TryMerge((es2 as EntSolid));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                }
                catch (Autodesk.Revit.Exceptions.InvalidOperationException)
                {
                    var errorString = $"{es1.ElementIds.CombineString(x => x.ToString(),"\n")}\n\n{es2.ElementIds.CombineString(x => x.ToString(), "\n")}";
                    File_Util.WriteTxtFileAndOpen(ioData.TempFilePath, errorString);
                    throw;
                }

                if (mergeSolid != null)
                {
                    mergeSuccess = true;

                    var esg = es1.EntSolidGroup;
                    es1.Solid = mergeSolid;

                    es1.InvalidMergeIndexs.Clear();
                    es1.Index = $"{es1.Index}_{es2.Index}";
                    es1.ElementIds.AddRange(es2.ElementIds);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    esg.EntSolidInGroups.Remove(es2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    var excludeIndexs = new List<string> { es1.Index, es2.Index };
                    esg.ClearInvalidMergeIndexs(excludeIndexs);
                }
                else
                {
                    es1.InvalidMergeIndexs.Add(es2.Index);
                    es2.InvalidMergeIndexs.Add(es1.Index);
                }
            }
#pragma warning restore CS8604 // Possible null reference argument.

            return mergeSuccess;
        }
        #endregion
    }
}
