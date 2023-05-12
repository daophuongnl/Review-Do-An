using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// /// <summary>
    /// Tập hợp các công cụ để xử lý Category
    /// </summary>
    /// </summary>
    public static partial class CategoryUtil
    {
        private static RevitData revitData => RevitData.Instance;

        /// <summary>
        /// Truy xuất đối tượng BuildInCategory từ đối tượng Category tương ứng
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.BuiltInCategory GetBuiltInCategory
            (this Autodesk.Revit.DB.Category category)
        {
            return (Autodesk.Revit.DB.BuiltInCategory)category.Id.IntegerValue;
        }

        /// <summary>
        /// Kiểm tra một đối tượng Category có trùng với đối tượng BuildInCategory truyền vào hay không
        /// </summary>
        /// <param name="category"></param>
        /// <param name="bic"></param>
        /// <returns></returns>
        public static bool IsEqual(this Autodesk.Revit.DB.Category category,
            Autodesk.Revit.DB.BuiltInCategory bic)
        {
            return category != null && category.GetBuiltInCategory() == bic;
        }

        public static bool IsEqual(this Autodesk.Revit.DB.CategorySet categorySet1, Autodesk.Revit.DB.CategorySet categorySet2)
        {
            if (categorySet1.Size != categorySet2.Size) return false;

            foreach (Autodesk.Revit.DB.Category cate in categorySet1)
            {
                if (!categorySet2.Contains(cate)) return false;
            }
            return true;
        }

        public static Autodesk.Revit.DB.Category GetCategory(this Autodesk.Revit.DB.BuiltInCategory builtInCategory)
        {
            return revitData.Categories.Single(x => x.IsEqual(builtInCategory));
        }

        public static string? GetName(this Autodesk.Revit.DB.BuiltInCategory builtInCategory)
        {
            string? name = null;
            switch (builtInCategory)
            {
                case BuiltInCategory.OST_StructuralFraming:
                    name = "Structural Framing";
                    break;
                case BuiltInCategory.OST_StructuralColumns:
                    name = "Structural Columnn";
                    break;
                case BuiltInCategory.OST_Walls:
                    name = "Wall";
                    break;
                case BuiltInCategory.OST_StackedWalls:
                    name = "Stacked Wall";
                    break;
                case BuiltInCategory.OST_Floors:
                    name = "Floor";
                    break;
                case BuiltInCategory.OST_StructuralFoundation:
                    name = "Foundation";
                    break;
                case BuiltInCategory.OST_Stairs:
                    name = "Stair";
                    break;
                case BuiltInCategory.OST_Ramps:
                    name = "Ramp";
                    break;
                case BuiltInCategory.OST_GenericModel:
                    name = "Generic Model";
                    break;
                default:
                    break;
            }
            return name;
        }
    }
}
