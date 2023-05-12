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
    public static class EntLocationPointUtil
    {
        private static RevitData revitData => RevitData.Instance;

        // Property
        public static XYZ GetCenterPoint(this EntLocationPoint entLocationPoint)
        {
            return (entLocationPoint.RevitLocation as LocationPoint)!.Point;
        }

        public static XYZ GetPurgeOffset(this EntLocationPoint entLocationPoint)
        {
            return entLocationPoint.CenterPoint.GetPurgeOffset();
        }
    }
}
