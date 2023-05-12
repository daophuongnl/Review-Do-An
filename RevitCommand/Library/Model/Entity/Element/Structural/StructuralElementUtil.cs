using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;

namespace Utility
{
    public static class StructuralElementUtil
    {
        private static RevitData revitData => RevitData.Instance;

        private static RebarData rebarData => RebarData.Instance;

        #region Property

        public static IEnumerable<Autodesk.Revit.DB.Structure.Rebar> GetRevitRebars(this StructuralElement structuralElement)
        {
            var elemId = structuralElement.ElementId;
            return rebarData.Rebars.Where(x => x.GetHostId().IntegerValue == elemId);
        }

        public static IEnumerable<RebarExport> GetRebarExports(this StructuralElement structuralElement)
        {
            return structuralElement.RevitRebars.Select(x => x.GetRebarExport());
        }

        public static string? GetElementName(this StructuralElement structuralElement)
        {
            return structuralElement.ParameterDict[ElementParameterName.HB_ElementName]?.ValueString;
        }

        #endregion

        #region Method



        #endregion
    }
}
