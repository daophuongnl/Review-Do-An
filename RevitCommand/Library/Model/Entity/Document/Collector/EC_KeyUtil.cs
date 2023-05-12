using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Model.Entity;

namespace Utility
{
    public static class EC_KeyUtil
    {
        // Instance
        public static EC_Key Get(Type type)
        {
            return new EC_Key
            {
                Types = new List<Type> { type }
            };
        }

        public static EC_Key Get(IEnumerable<Type> types)
        {
            return new EC_Key
            {
                Types = types.ToList()
            };
        }

        public static EC_Key Get(DisciplineType disciplineType, string filterType = ElementFilterType.Instance)
        {
            return new EC_Key
            {
                BuiltInCategories = BuiltInCategoryUtil.List(disciplineType),
                FilterType = filterType
            };
        }

        public static EC_Key Get(BuiltInCategory bic, string filterType = ElementFilterType.Instance)
        {
            return new EC_Key
            {
                BuiltInCategories = new List<BuiltInCategory> { bic },
                FilterType = filterType
            };
        }

        public static EC_Key Get(IEnumerable<BuiltInCategory> bics, string filterType = ElementFilterType.Instance)
        {
            return new EC_Key
            {
                BuiltInCategories = bics.ToList(),
                FilterType = filterType
            };
        }

        public static EC_Key Get(Type type, BuiltInCategory bic)
        {
            return new EC_Key
            {
                Types = new List<Type> { type },
                BuiltInCategories = new List<BuiltInCategory> { bic }
            };
        }

        public static EC_Key Get(Type type, IEnumerable<BuiltInCategory> bics)
        {
            return new EC_Key
            {
                Types = new List<Type> { type },
                BuiltInCategories = bics.ToList()
            };
        }

        public static EC_Key Get(IEnumerable<Type> types, BuiltInCategory bic)
        {
            return new EC_Key
            {
                Types = types.ToList(),
                BuiltInCategories = new List<BuiltInCategory> { bic }
            };
        }

        public static EC_Key Get(IEnumerable<Type> types, IEnumerable<BuiltInCategory> bics)
        {
            return new EC_Key
            {
                Types = types.ToList(),
                BuiltInCategories = bics.ToList()
            };
        }

        // Method
        public static bool IsEqual(this EC_Key q1, EC_Key q2)
        {
            if (q1.FilterType != q2.FilterType)
            {
                return false;
            }

            var t1 = q1.Types!;
            var t2 = q2.Types!;

            if (t1 != t2) // t1 == t2 khi bằng null
            {
                if (t1.Count != t2.Count || !t1.All(t2.Contains))
                {
                    return false;
                }
            }

            var b1 = q1.BuiltInCategories!;
            var b2 = q2.BuiltInCategories!;

            if (b1 != b2)
            {
                if (b1.Count != b2.Count || !b1.All(b2.Contains))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
