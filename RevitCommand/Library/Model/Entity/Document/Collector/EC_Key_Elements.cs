using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EC_Key_Elements
    {
        public ElementCollector? Dict { get; set; }

        public EC_Key? Key { get; set; }

        private FilteredElementCollector? elements;
        public FilteredElementCollector Elements => elements ??= this.GetElements();
    }
}
