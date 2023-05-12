using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DisciplineTypeUtil
    {
        public static List<DisciplineType> GetList()
        {
            return new List<DisciplineType>
            {
                DisciplineType.All, DisciplineType.Structural, DisciplineType.Architect, DisciplineType.MEP
            };
        }

        public static DisciplineType GetDisciplineType(this EntCategoryType entCategoryType)
        {
            DisciplineType disciplineType = DisciplineType.Structural;
            switch (entCategoryType)
            {
                case EntCategoryType.Beam:
                case EntCategoryType.StrFloor:
                case EntCategoryType.Column:
                case EntCategoryType.StrWall:
                case EntCategoryType.Foundation:
                case EntCategoryType.Ramp:
                case EntCategoryType.StrStair:
                    disciplineType = DisciplineType.Structural;
                    break;
                case EntCategoryType.ArcFloor:
                case EntCategoryType.ArcWall:
                case EntCategoryType.ArcStair:
                case EntCategoryType.ArcCeiling:
                case EntCategoryType.Generic:
                    disciplineType = DisciplineType.Architect;
                    break;
                default:
                    break;
            }
            return disciplineType;
        }

        // Method
        public static string GetName(this DisciplineType disciplineType)
        {
            string? name = null;
            switch (disciplineType)
            {
                case DisciplineType.Structural:
                    name = "Kết cấu";
                    break;
                case DisciplineType.Architect:
                    name = "Kiến trúc";
                    break;
                case DisciplineType.MEP:
                    name = "MEP";
                    break;
            }
            return name!;
        }
    }
}
