using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static class EntOriginalUtil
    {
        private static PartData partData => PartData.Instance;

        // Instance


        // Property

        public static List<EntPart>? GetMainParts(this EntOriginal original)
        {
            if (original.IsGet_MainParts) return null;
            original.IsGet_MainParts = true;

            var setting = partData.Setting;
            if (!setting.IsCreateSystem)
            {
                setting.IsCreateSystem = true;
            }

            if (!setting.IsDone)
            {
                // Nếu chưa chạy lặp tập hợp Part tiến hành truy xuất
                var qI = partData.EntParts;
            }

            // kiểm tra đối tượng thõa mãn
            var list = setting.Handled_Originals;

            var item = list.FirstOrDefault(x => x == original || x.ElementId == original.ElementId);
            return item?.MainPart_StorageList;
        }

        public static List<EntPart>? GetValueParts(this EntOriginal original)
        {
            if (original.IsGet_ValueParts) return null;
            original.IsGet_ValueParts = true;

            if (original.MainParts == null)
            {
                return null;
            }

            var list = new List<EntPart>();
            foreach (var item in original.MainParts)
            {
                list.AddRange(item.ValueParts);
            }
            return list;
        }

        // Method

    }
}
