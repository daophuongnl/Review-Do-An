using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class Temp_CurveUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static ViewData viewData => ViewData.Instance;

        public static DetailCurve CreateDetail(this Curve curve, View view = null, ElementId styleId = null)
        {
            if (view == null)
            {
                view = viewData.ActiveView;
            }

            var dc = revitData.Document.Create.NewDetailCurve(view, curve);
            if (styleId != null)
            {
                dc.ParameterSet("Line Style", styleId);
            }
            return dc;
        }

        public static ModelCurve CreateModel(this Curve curve)
        {
            var dir = (curve as Line)!.Direction;
            var pnt = curve.GetEndPoint(0);
            XYZ? normal = null;

            var doc = revitData.Document;
            SketchPlane? sp = null;
            if (dir.IsPerpendicular(XYZ.BasisZ))
            {
                normal = XYZ.BasisZ;
            }
            else if (dir.IsParallel(XYZ.BasisZ))
            {
                normal = XYZ.BasisX;
            }
            else
            {
                normal = dir.CrossProduct(XYZ.BasisZ);
            }

            sp = SketchPlane.Create(doc, Plane.CreateByNormalAndOrigin(normal, pnt));

            var ml = revitData.Document.Create.NewModelCurve(curve, sp);
            return ml;
        }
    }
}
