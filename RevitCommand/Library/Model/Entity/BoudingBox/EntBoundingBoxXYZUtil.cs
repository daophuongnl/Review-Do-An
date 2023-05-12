using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class EntBoundingBoxXYZUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static EntBoundingBoxXYZ GetEntBoundingBoxXYZ(this BoundingBoxXYZ boundingBoxXYZ)
        {
            return new EntBoundingBoxXYZ
            {
                RevitBoundingBoxXYZ = boundingBoxXYZ
            };
        }

        // Property
        public static XYZ GetMinPoint(this EntBoundingBoxXYZ entBb)
        {
            return entBb.RevitBoundingBoxXYZ!.Min;
        }

        public static XYZ GetMaxPoint(this EntBoundingBoxXYZ entBb)
        {
            return entBb.RevitBoundingBoxXYZ!.Max;
        }

        public static double GetWidth(this EntBoundingBoxXYZ entBb)
        {
            return Math.Abs(entBb.MaxPoint.X - entBb.MinPoint.X);
        }

        public static double GetHeight(this EntBoundingBoxXYZ entBb)
        {
            return Math.Abs(entBb.MaxPoint.Y - entBb.MinPoint.Y);
        }

        public static double GetLength(this EntBoundingBoxXYZ entBb)
        {
            return Math.Abs(entBb.MaxPoint.Z - entBb.MinPoint.Z);
        }
    }
}
