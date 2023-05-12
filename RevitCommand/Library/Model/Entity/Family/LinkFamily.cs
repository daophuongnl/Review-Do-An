using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;  

namespace Model.Entity
{
    public class LinkFamily : EntFamily
    {
        public override IEnumerable<Element>? AllElements
        {
            get=>allElements;
            set => allElements = value;
        }
    }
}
