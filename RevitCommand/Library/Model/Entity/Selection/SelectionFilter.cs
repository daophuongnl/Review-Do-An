using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI.Selection;

namespace Model.Entity
{
    public class SelectionFilter : ISelectionFilter
    {
        public Func<Autodesk.Revit.DB.Element, bool>? FuncElement { get; set; }
        public Func<Reference, bool>? FuncReference { get; set; }

        public bool AllowElement(Autodesk.Revit.DB.Element elem)
        {
            return FuncElement!(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return FuncReference!(reference);
        }
    }
}
