using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using System.IO;
using System.Windows.Media.Animation;

namespace Utility
{
    public static class SolidFamilyUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static SolidFamily GetSolidFamily(this IEnumerable<Solid> inputSolids, bool isMerge, string name = null)
        {
            var solidFam = new SolidFamily
            {
                InputSolids = inputSolids,
                IsMerged = isMerge,
                Name = name,
            };
            return solidFam;
        }

        public static SolidFamily GetSolidFamily(this Solid singleSolid, string name = null)
        {
            var solidFam = new SolidFamily
            {
                SingleSolid = singleSolid,
                Name = name
            };
            return solidFam;
        }

        #region Property of EntFamily

        public static string GetName(this SolidFamily solidFamily)
        {
            var name = $"Solid-{Guid.NewGuid()}";
            return name;
        }

        public static void GetEntComponentFamilyForCreate_Action(this SolidFamily solidFamily)
        {
            var singleSolid = solidFamily.SingleSolid;
            var doc = solidFamily.FamilyDocument;
            if (singleSolid != null)
            {
                FreeFormElement.Create(doc, solidFamily.SingleSolid);
            }
            else
            {
                var inputSolids = solidFamily.InputSolids!;
                if (solidFamily.IsMerged)
                {
                    FreeFormElement.Create(doc, inputSolids.MergeSolid());
                }
                else
                {
                    foreach (var inputSolid in inputSolids)
                    {
                        FreeFormElement.Create(doc, inputSolid);
                    }
                }
            }
        }

        #endregion

        #region Method of EntFamily

        #endregion
    }
}
