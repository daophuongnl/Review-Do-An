using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ElementCollector
    {
        public ElementCollectorDict? Dict { get; set; }

        public EntDocument Document => Dict!.Document!;

        public ElementId? ViewId { get; set; }

        public FilteredElementCollector FilteredElementCollector => this.GetFilteredElementCollector();

        private List<EC_Key_Elements>? key_Elements_Dict;
        public List<EC_Key_Elements> Key_Elements_Dict => key_Elements_Dict ??= new List<EC_Key_Elements>();

        public FilteredElementCollector this[Type type] => this.GetKey_Elements(EC_KeyUtil.Get(type)).Elements;

        public FilteredElementCollector this[IEnumerable<Type> types] => this.GetKey_Elements(EC_KeyUtil.Get(types)).Elements;

        public FilteredElementCollector this[BuiltInCategory bic, string filterType = ElementFilterType.Instance] => this.GetKey_Elements(EC_KeyUtil.Get(bic, filterType)).Elements;

        public FilteredElementCollector this[IEnumerable<BuiltInCategory> bics, string filterType = ElementFilterType.Instance] => this.GetKey_Elements(EC_KeyUtil.Get(bics, filterType)).Elements;

        public FilteredElementCollector this[DisciplineType disciplineType, string filterType = ElementFilterType.Instance] 
            => this.GetKey_Elements(EC_KeyUtil.Get(disciplineType, filterType)).Elements;

        public FilteredElementCollector this[Type type, BuiltInCategory bic] => this.GetKey_Elements(EC_KeyUtil.Get(type, bic)).Elements;

        public FilteredElementCollector this[Type type, IEnumerable<BuiltInCategory> bics] => this.GetKey_Elements(EC_KeyUtil.Get(type, bics)).Elements;

        public FilteredElementCollector this[IEnumerable<Type> types, BuiltInCategory bic] => this.GetKey_Elements(EC_KeyUtil.Get(types, bic)).Elements;

        public FilteredElementCollector this[IEnumerable<Type> types, IEnumerable<BuiltInCategory> bics] => this.GetKey_Elements(EC_KeyUtil.Get(types, bics)).Elements;
    }
}
