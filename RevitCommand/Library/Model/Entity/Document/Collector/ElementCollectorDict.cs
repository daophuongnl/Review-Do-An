using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ElementCollectorDict
    {
        public EntDocument? Document { get; set; }

        public ElementCollector this[ElementId viewId] => this.GetElementCollector(viewId);
    }
}
