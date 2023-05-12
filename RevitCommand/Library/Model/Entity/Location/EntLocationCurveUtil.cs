using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using System.IO;

namespace Utility
{
    public static class EntLocationCurveUtil
    {
        private static RevitData revitData => RevitData.Instance;

        // Property
        public static Curve GetCurve(this EntLocationCurve entLocationCurve)
        {
            var curve = (entLocationCurve.RevitLocation as LocationCurve)!.Curve;
            return curve;
        }

        public static XYZ GetDirection(this EntLocationCurve entLocationCurve)
        {
            return (entLocationCurve.Curve as Line)!.Direction;
        }

        public static XYZ GetStartPoint(this EntLocationCurve entLocationCurve)
        {
            return entLocationCurve.Curve.GetEndPoint(0);
        }

        public static XYZ GetEndPoint(this EntLocationCurve entLocationCurve)
        {
            return entLocationCurve.Curve.GetEndPoint(1);
        }

        public static XYZ GetPurgeOffset(this EntLocationCurve entLocationCurve)
        {
            return entLocationCurve.StartPoint.GetPurgeOffset();
        }
    }
}
