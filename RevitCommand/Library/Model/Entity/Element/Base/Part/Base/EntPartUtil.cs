using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntPartUtil
    {
        private static PartData partData => PartData.Instance;

        // Instance


        // Property

        public static EntPart? GetSupItem(this EntPart part)
        {
            if (part.IsGet_SupItem) return null;
            part.IsGet_SupItem = true;

            var setting = partData.Setting;
            var qI = setting.RetrieveSource(part);
            if (qI is EntPart)
            {
                return qI as EntPart;
            }
            else
            {
                part.Original = (qI as EntOriginal)!;
                return null;
            }
        }

        public static EntOriginal? GetOriginal(this EntPart part)
        {
            if (part.IsGet_Original) return null;
            part.IsGet_Original = true;

            var setting = partData.Setting;
            var qI = setting.RetrieveSource(part);
            if (qI is EntPart)
            {
                var q1 = part.SupItem = qI as EntPart;
                return q1!.Original;
            }
            else
            {
                return qI as EntOriginal;
            }
        }

        public static List<EntPart> GetValueParts(this EntPart part)
        {
            var sub = part.SubItems;
            if (sub.Count == 0)
            {
                return new List<EntPart> { part };
            }

            var list = new List<EntPart>();
            foreach (var item in sub)
            {
                list.AddRange(item.ValueParts);
            }
            return list;
        }

        // Method

    }
}
