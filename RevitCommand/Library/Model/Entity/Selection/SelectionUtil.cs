using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Autodesk.Revit.DB;
using SingleData;
using System.Collections;
using Autodesk.Revit.UI.Selection;
using Model.Entity;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Electrical;

namespace Utility
{
    public static class SelectionUtil
    {
        private static ViewData viewData => ViewData.Instance;

        public static Autodesk.Revit.DB.Element PickElement(this Autodesk.Revit.UI.Selection.Selection sel,
            Autodesk.Revit.DB.BuiltInCategory? bic = null,
            Func<Autodesk.Revit.DB.Element, bool> elemFilter = null)
        {
            Func<Autodesk.Revit.DB.Element, bool> filter = x => true;
            if (bic != null && elemFilter != null)
            {
                filter = x => x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic && elemFilter(x);
            }
            if (bic != null && elemFilter == null)
            {
                filter = x => x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic;
            }
            if (bic == null && elemFilter != null)
            {
                filter = x => elemFilter(x);
            }
            return sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new Model.Entity.SelectionFilter { FuncElement = filter }).GetElement();
        }

        public static IEnumerable<Autodesk.Revit.DB.Element> PickElements(this Autodesk.Revit.UI.Selection.Selection sel,
            Autodesk.Revit.DB.BuiltInCategory? bic = null,
            Func<Autodesk.Revit.DB.Element, bool> elemFilter = null)
        {
            Func<Autodesk.Revit.DB.Element, bool> filter = x => true;
            if (bic != null && elemFilter != null)
            {
                filter = x => x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic && elemFilter(x);
            }
            if (bic != null && elemFilter == null)
            {
                filter = x => x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic;
            }
            if (bic == null && elemFilter != null)
            {
                filter = x => elemFilter(x);
            }

            return sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new Model.Entity.SelectionFilter { FuncElement = filter }).Select(x => x.GetElement());
        }

        public static T PickElement<T>(this Autodesk.Revit.UI.Selection.Selection sel,
            Autodesk.Revit.DB.BuiltInCategory? bic = null,
            Func<Autodesk.Revit.DB.Element, bool> elemFilter = null) where T : Autodesk.Revit.DB.Element
        {
            Func<Autodesk.Revit.DB.Element, bool> filter = x => x is T;
            if (bic != null && elemFilter != null)
            {
                filter = x => x is T && x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic && elemFilter(x);
            }
            if (bic != null && elemFilter == null)
            {
                filter = x => x is T && x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic;
            }
            if (bic == null && elemFilter != null)
            {
                filter = x => x is T && elemFilter(x);
            }

            var elem = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new Model.Entity.SelectionFilter { FuncElement = filter }).GetElement();
            return (T)elem;
        }

        public static Element PickElement(this Selection sel, DisciplineType disciplineType)
        {
            Func<Element, bool>? filter = null;

            switch (disciplineType)
            {
                case DisciplineType.Structural:
                    filter = x => x is Wall || x is Floor || x is Stairs ||
                        x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming) || x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming) || x.Category.IsEqual(BuiltInCategory.OST_Ramps);
                    break;
                case DisciplineType.Architect:
                    filter = x => x is Wall || x is Floor ||
                        x.Category.IsEqual(BuiltInCategory.OST_Windows) || x.Category.IsEqual(BuiltInCategory.OST_Doors);
                    break;
                case DisciplineType.MEP:
                    filter = x => x is MEPCurve || x is CableTray || x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment);
                    break;
            }

            return sel.PickObject(ObjectType.Element, new SelectionFilter { FuncElement = filter! }).GetElement();
        }

        public static IEnumerable<T> PickElements<T>(this Autodesk.Revit.UI.Selection.Selection sel,
            Autodesk.Revit.DB.BuiltInCategory? bic = null,
            Func<Autodesk.Revit.DB.Element, bool> elemFilter = null) where T : Autodesk.Revit.DB.Element
        {
            Func<Autodesk.Revit.DB.Element, bool> filter = x => x is T;
            if (bic != null && elemFilter != null)
            {
                filter = x => x is T && x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic && elemFilter(x);
            }
            if (bic != null && elemFilter == null)
            {
                filter = x => x is T && x.Category != null && (Autodesk.Revit.DB.BuiltInCategory)x.Category.Id.IntegerValue == bic;
            }
            if (bic == null && elemFilter != null)
            {
                filter = x => x is T && elemFilter(x);
            }

            return sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new Model.Entity.SelectionFilter { FuncElement = filter })
                .Select(x => x.GetElement()).Cast<T>();
        }

        public static IEnumerable<Element> PickElements(this Selection sel, DisciplineType disciplineType)
        {
            Func<Element, bool>? filter = null;

            switch (disciplineType)
            {
                case DisciplineType.Structural:
                    filter = x => x is Wall || x is Floor || x is Stairs ||
                        x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming) || x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns) || x.Category.IsEqual(BuiltInCategory.OST_Ramps);
                    break;
                case DisciplineType.Architect:
                    filter = x => x is Wall || x is Floor ||
                        x.Category.IsEqual(BuiltInCategory.OST_Windows) || x.Category.IsEqual(BuiltInCategory.OST_Doors);
                    break;
                case DisciplineType.MEP:
                    filter = x => x is MEPCurve || x is CableTray || x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment);
                    break;
            }

            return sel.PickObjects(ObjectType.Element, new SelectionFilter { FuncElement = filter! }).Select(x => x.GetElement());
        }

        public static IEnumerable<Element> GetElements(this Autodesk.Revit.UI.Selection.Selection sel)
        {
            return sel.GetElementIds().Select(x => x.GetElement());
        }

        public static void SetElement(this Autodesk.Revit.UI.Selection.Selection sel, Autodesk.Revit.DB.Element element)
        {
            sel.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { element.Id });
        }

        public static void SetElement(this Autodesk.Revit.UI.Selection.Selection sel, IEnumerable<Autodesk.Revit.DB.Element> elements)
        {
            sel.SetElementIds(elements.Select(x => x.Id).ToList());
        }

        public static void ClearSelection(this Autodesk.Revit.UI.Selection.Selection sel)
        {
            sel.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { });
        }

        public static XYZ SafePickPoint(this Autodesk.Revit.UI.Selection.Selection sel)
        {
            try
            {
                return sel.PickPoint();
            }
            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
            {
                viewData.ActiveView.SketchPlane = viewData.ActiveSketchPlane;
                return sel.PickPoint();
            }
        }


    }
}
