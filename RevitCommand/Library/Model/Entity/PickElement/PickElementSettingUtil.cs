using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class PickElementSettingUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static StructuralData structuralData => StructuralData.Instance;
        private static ArchitectData architectData => ArchitectData.Instance;
        private static MEPData mepData => MEPData.Instance;

        #region Property

        public static int GetCount(this PickElementSetting pes)
        {
            var pickedElems = pes.PickedElements;
            return pickedElems == null ? 0 : pickedElems.Count();
        }

        public static IEnumerable<Element>? GetResultElements
            (this PickElementSetting pes)
        {
            IEnumerable<Element>? elements = null;
            var disiplineType = pes.DisciplineType;

            var pickElemType = pes.PickElementType;
            switch (pickElemType)
            {
                case PickElementType.EntireProject:
                    {
                        switch (disiplineType)
                        {
                            case DisciplineType.All:
                                //System.Windows.MessageBox.Show(
                                //    $"{structuralData.AllStructuralInstances.Count()} - " +
                                //    $"{architectData.AllArchitectInstances.Count()} - " +
                                //    $"{mepData.AllMEPInstances.Count()}");
                                elements = structuralData.AllStructuralInstances.Concat(architectData.AllArchitectInstances).Concat(mepData.AllMEPInstances);
                                break;
                            case DisciplineType.Structural:
                                elements = structuralData.AllStructuralInstances;
                                break;
                            case DisciplineType.Architect:
                                elements = architectData.AllArchitectInstances;
                                break;
                            case DisciplineType.MEP:
                                elements = mepData.AllMEPInstances;
                                break;
                        }
                    }
                    break;
                case PickElementType.EntireView:
                    {
                        var doc = revitData.Document;
                        var viewId = doc.ActiveView.Id;
                        Func<Element, bool>? filterElement = null;

                        switch (disiplineType)
                        {
                            case DisciplineType.All:
                                filterElement = (e) => structuralData.StructuralInstanceFilter(e) || architectData.ArchitectInstanceFilter(e) || mepData.MEPInstanceFilter(e);
                                break;
                            case DisciplineType.Structural:
                                filterElement = structuralData.StructuralInstanceFilter;
                                break;
                            case DisciplineType.Architect:
                                filterElement = architectData.ArchitectInstanceFilter;
                                break;
                            case DisciplineType.MEP:
                                filterElement = mepData.MEPInstanceFilter;
                                break;
                        }

                        elements = new FilteredElementCollector(doc, viewId)
                            .WhereElementIsNotElementType().Where(filterElement!).ToList();
                    }
                    break;
                case PickElementType.PickElementsInProject:
                    elements = pes.PickedElements;
                    break;
            }
            return elements;
        }

        #endregion
    }
}
