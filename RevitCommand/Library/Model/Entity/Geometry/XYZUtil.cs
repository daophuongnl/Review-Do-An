using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class XYZUtil
    {
        private static GridData gridData => GridData.Instance;

        public static XYZ GetPurgeOffset(this XYZ pnt)
        {
            return pnt.PurgePoint() - pnt;
        }

        public static XYZ PurgePoint(this XYZ pnt)
        {
            var basePnt = gridData.BasePoint;

            var vec = pnt - basePnt;
            var x = vec.X.RoundMM(5);
            var y = vec.Y.RoundMM(5);
            var z = vec.Z.RoundMM(5);

            vec = new XYZ(x, y, z);
            pnt = vec + basePnt;

            return pnt;
        }

        public static XYZ SetZ(this XYZ pnt, double z)
        {
            return new XYZ(pnt.X, pnt.Y, z);
        }

        public static string AsString(this XYZ pnt)
        {
            var pntString = $"({pnt.X.Round()},{pnt.Y.Round()},{pnt.Z.Round()})";
            return pntString;
        }

        public static XYZ GetProjectPointOnXY(this XYZ pnt)
        {
            return new XYZ(pnt.X, pnt.Y, 0);
        }
    }
}
