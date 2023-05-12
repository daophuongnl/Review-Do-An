using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static partial class RebarCurveUtil
    {
        public static bool IsBendSegment(Curve curve)
        {
            if (curve is Autodesk.Revit.DB.Arc)
            {
                var arc = (curve as Autodesk.Revit.DB.Arc)!;
                if (arc.Radius.IsEqualOrSmaller(100.0.milimeter2Feet())) return true;
            }
            return false;
        }
    }
}