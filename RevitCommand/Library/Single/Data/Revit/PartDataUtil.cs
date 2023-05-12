using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;

namespace Utility
{
    public static class PartDataUtil
    {
        private static PartData gridData => PartData.Instance;

        #region Property

        public static List<EntPart> GetEntParts(this PartData data)
        {
            var setting = data.Setting;
            if (setting.IsCreateSystem)
            {
                foreach (var item in data.Parts)
                {
                    var qI = setting.RetrievePart(item);
                    var q1 = qI.SupItem;
                }
                setting.IsDone = true;
                return setting.Handled_Parts;
            }
            else
            {
                return data.Parts.Select(x => setting.RetrievePart(x)).ToList();
            }
        }

        #endregion

        #region Method

        public static void Dispose()
        {
            PartData.Instance = null;
        }

        #endregion
    }
}
