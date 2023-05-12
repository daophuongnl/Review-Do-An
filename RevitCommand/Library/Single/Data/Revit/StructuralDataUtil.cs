using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class StructuralDataUtil
    {
        private static StructuralData structuralData => StructuralData.Instance;
        private static ViewData viewData => ViewData.Instance;

        public static void RefreshDocument()
        {
            structuralData.AllStructuralTypes = null;
            structuralData.StrFamilyInstances = null;
            structuralData.WallTypes = null;
            structuralData.FloorTypes = null;
            structuralData.FoundationSlabTypes = null;
            structuralData.ColumnTypes = null;
            structuralData.BeamTypes = null;
            structuralData.FoundationTypes = null;

            structuralData.AllStructuralInstances = null;
            structuralData.StrFamilyInstances = null;
            structuralData.Walls = null;
            structuralData.Floors = null;
            structuralData.FoundationSlabs = null;
            structuralData.Columns = null;
            structuralData.Beams = null;
            structuralData.FoundationTypes = null;

            (structuralData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (structuralData as DataBase).RefreshDocument();
        }

        public static void Dispose()
        {
            StructuralData.Instance = null;
        }

        public static IEnumerable<Autodesk.Revit.DB.ViewSchedule> GetAllRebarRevitViewSchedules()
        {
            return viewData.ViewSchedules.Where(x => x.Definition.CategoryId.IntegerValue == (int)BuiltInCategory.OST_Rebar);
        }
    }
}
