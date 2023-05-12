using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Key_ElementsUtil
    {
        // Instance
        public static EC_Key_Elements GetKey_Elements(this ElementCollector col, EC_Key key)
        {
            var list = col.Key_Elements_Dict;
            var item = list.FirstOrDefault(x => x.Key!.IsEqual(key));
            if (item == null)
            {
                item = new EC_Key_Elements
                {
                    Dict = col,
                    Key = key
                };
                list.Add(item);
            }
            return item;
        }

        // Property
        public static FilteredElementCollector GetElements(this EC_Key_Elements q)
        {
            var key = q.Key!;
            var filter = q.Dict!.FilteredElementCollector;

            switch (key.FilterType)
            {
                case ElementFilterType.Instance:
                    filter = filter.WhereElementIsNotElementType();
                    break;
                case ElementFilterType.Type:
                    filter = filter.WhereElementIsElementType();
                    break;
            }

            ElementFilter? ef = null;

            if (key.Types != null)
            {
                ef = ElementFilterUtil.Get(key.Types);
            }
            if (key.BuiltInCategories != null)
            {
                var temp = ElementFilterUtil.Get(key.BuiltInCategories);
                ef = ef == null ? temp : ef.And(temp);
            }

            return filter.WherePasses(ef);
        }
    }
}
