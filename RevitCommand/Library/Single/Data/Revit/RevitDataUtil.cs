using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using SingleData;

namespace Utility
{
    public static class RevitDataUtil
    {
        private static RevitData revitData => RevitData.Instance;

        //
        // property
        //

        public static IEnumerable<EntDocument> GetLinkEntDocuments(this RevitData data)
        {
            return data.LinkDocuments.Select(x => x.GetEntDocument());
        }

        //
        // method
        //

        public static void RefreshDocument()
        {
            ViewDataUtil.RefreshDocument();
            WorksetDataUtil.RefreshDocument();
            StructuralDataUtil.RefreshDocument();

            revitData.Transaction = null;
            revitData.InstanceElements = null;
            revitData.TypeElements = null;
            revitData.AllElements = null;
            revitData.Families = null;
            revitData.FamilySymbols = null;
            revitData.FamilyInstances = null;
            revitData.Categories = null;

            revitData.Levels = null;
            revitData.EntLevels = null;

            revitData.ParameterBindings = null;
            revitData.RevitLinkInstances = null;
            revitData.LinkDocuments = null;
            revitData.ProjectInfo = null;
            revitData.InstanceElements = null;

            revitData.FailureDefinitionRegistry = null;
            revitData.DirectShapeTypes = null;
            revitData.Materials = null;
            revitData.DirectShapeLibrary = null;
            revitData.GeometryOptions = null;

            revitData.Document = null;
            revitData.EntDocument = null;
        }

        public static void RefreshUIDocument()
        {
            ViewDataUtil.RefreshUIDocument();
            WorksetDataUtil.RefreshUIDocument();
            StructuralDataUtil.RefreshUIDocument();

            RefreshDocument();

            revitData.UIDocument = null;
            revitData.Selection = null;
        }

        public static void Dispose()
        {
            StructuralDataUtil.Dispose();
            ArchitectDataUtil.Dispose();
            MEPDataUtil.Dispose();
            RebarDataUtil.Dispose();

            ViewDataUtil.Dispose();
            WorksetDataUtil.Dispose();

            DimensionDataUtil.Dispose();
            AnnotationDataUtil.Dispose();
            GraphicsStyleDataUtil.Dispose();

            IODataUtil.Dispose();
            JsonDataUtil.Dispose();
            //LogDataUtil.Dispose();

            LevelDataUtil.Dispose();
            GridDataUtil.Dispose();

            PartDataUtil.Dispose();

            RevitData.Instance = new RevitData { UIApplication = RevitData.Instance.UIApplication };
        }
    }
}
