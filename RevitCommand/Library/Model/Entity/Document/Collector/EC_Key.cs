using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EC_Key
    {
        public string? FilterType { get; set; }

        public List<Type>? Types { get; set; }

        public List<BuiltInCategory>? BuiltInCategories { get; set; }
    }
}
