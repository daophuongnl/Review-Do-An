using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class ReferenceUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static T GetGeometryObject<T>(this Reference rf) where T : GeometryObject
        {
            return (T)rf.GetElement().GetGeometryObjectFromReference(rf);
        }
    }
}
