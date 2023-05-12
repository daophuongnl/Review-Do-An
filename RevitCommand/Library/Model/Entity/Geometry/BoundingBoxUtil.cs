using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class BoundingBoxUtil
    {
        public static BoundingBoxXYZ Scale(this BoundingBoxXYZ bb, double factor)
        {
            var min = bb.Min;
            var max = bb.Max;
            var center = (min + max) / 2.0;

            var newMin = center + (min - center) * factor;
            var newMax = center + (max - center) * factor;

            return new BoundingBoxXYZ { Min = newMin, Max = newMax };
        }

        public static bool IsOverlap(this BoundingBoxXYZ bb1, BoundingBoxXYZ bb2, bool isTangentValue = true)
        {
            var min1 = bb1.Min;
            var max1 = bb1.Max;
            var min2 = bb2.Min;
            var max2 = bb2.Max;

            return IsOverlap1D(min1.X, max1.X, min2.X, max2.X) && IsOverlap1D(min1.Y, max1.Y, min2.Y, max2.Y)
                && IsOverlap1D(min1.Z, max1.Z, min2.Z, max2.Z);
        }

        public static bool IsOverlapXY(this BoundingBoxXYZ bb1, BoundingBoxXYZ bb2, bool isTangentValue = true)
        {
            var min1 = bb1.Min;
            var max1 = bb1.Max;
            var min2 = bb2.Min;
            var max2 = bb2.Max;

            return IsOverlap1D(min1.X, max1.X, min2.X, max2.X) && IsOverlap1D(min1.Y, max1.Y, min2.Y, max2.Y);
        }

        public static Outline GetOutline(this BoundingBoxXYZ bb)
        {
            var minPnt = bb.Min; var maxPnt = bb.Max;
            var outline = new Outline(minPnt, maxPnt);

            return outline;
        }

        private static bool IsOverlap1D(double min1, double max1, double min2, double max2)
        {
            return max1.IsEqualOrBigger(min2) && max2.IsEqualOrBigger(min1);
        }

        public static XYZ GetCenterPoint(this BoundingBoxXYZ bb)
        {
            return (bb.Min + bb.Max) / 2.0;
        }
    }
}
