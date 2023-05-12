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
    public static class EntSolidGroupUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static EntSolidGroup GetEntSolidGroup(this IEnumerable<EntSolid> entSolids)
        {
            var esg = new EntSolidGroup
            {
                EntSolids = entSolids
            };

            return esg;
        }

        public static EntSolidGroup GetEntSolidGroup(this IEnumerable<Solid> solids)
        {
            var esg = new EntSolidGroup
            {
                Solids = solids
            };

            return esg;
        }

        #region Property
        public static List<EntSolidInGroup> GetEntSolidInGroups(this EntSolidGroup entSolidGroup)
        {
            List<EntSolidInGroup>? ESIGs = null;
            var solids = entSolidGroup.Solids;
            var ESs = entSolidGroup.EntSolids;
            if (solids != null)
            {
                ESIGs = solids.Select(x => x.GetEntSolidInGroup(entSolidGroup)).ToList();
            }
            else if (ESs != null)
            {
                ESIGs = new List<EntSolidInGroup>();

                foreach (var es in ESs)
                {
                    ESIGs.AddRange(es.GetEntSolidInGroups(entSolidGroup));
                }
            }
            else
            {
                throw new Exception("This case haven't been checked yet!");
            }

            return ESIGs;
        }

        public static List<EntSolidInGroup> GetResultEntSolids(this EntSolidGroup entSolidGroup)
        {
            entSolidGroup.TryMerge();
            return entSolidGroup.EntSolidInGroups;
        }
        #endregion

        #region Method
        public static void ClearInvalidMergeIndexs(this EntSolidGroup entSolidGroup, List<string> targetIndexs)
        {
            var targetES = entSolidGroup.EntSolidInGroups.Where(x => x.InvalidMergeIndexs.Any(y => targetIndexs.Contains(y)));

            if (targetES.Any())
            {
                foreach (var es in targetES)
                {
                    foreach (var index in targetIndexs)
                    {
                        es.InvalidMergeIndexs.Remove(index);
                    }
                }
            }
        }

        public static bool CanDoMerge(this EntSolidGroup entSolidGroup)
        {
            var canDoMerge = false;

            var ESs = entSolidGroup.EntSolidInGroups;
            var count = ESs.Count;

            if (ESs.Any(x => x.InvalidMergeIndexs.Count < count - 1))
            {
                canDoMerge = true;
            }

            return canDoMerge;
        }

        public static void TryMerge(this EntSolidGroup entSolidGroup)
        {
            var canDoMerge = entSolidGroup.CanDoMerge();
            while (canDoMerge)
            {
                var ESs = entSolidGroup.EntSolidInGroups;
                for (int i = 0; i < ESs.Count-1; i++)
                {
                    for (int j = i+1; j < ESs.Count; j++)
                    {
                        if (ESs[i].TryMerge(ESs[j]))
                        {
                            break;
                        }
                    }
                }

                canDoMerge = entSolidGroup.CanDoMerge();
            }
        }
        #endregion
    }
}
