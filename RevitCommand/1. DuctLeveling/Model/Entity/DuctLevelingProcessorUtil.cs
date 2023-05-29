using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public static class DuctLevelingProcessorUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static LocationCurve GetDuctLocation(this DuctLevelingProcessor q)
        {
            var duct = q.Duct;
            return (duct.Location as LocationCurve)!;
        }

        public static Line GetDuctLine(this DuctLevelingProcessor q)
        {
            var duct = q.Duct;
            return ((duct.Location as LocationCurve)!.Curve as Line)!;
        }
        public static XYZ GetDuctDirection (this DuctLevelingProcessor q) 
        {
            return q.DuctLine.Direction;
        }
        public static ElementId GetLevelId(this DuctLevelingProcessor q)
        {
            return q.Duct!.LookupParameter("Reference Level").AsElementId();
        }
        public static ElementId GetSystemTypeId(this DuctLevelingProcessor q)
        {
            return q.Duct!.MEPSystem.GetTypeId();
        }
        public static XYZ GetMiddlePoint(this DuctLevelingProcessor q)
        {
            return q.DuctLine.GetCenterPoint();
        }
        public static Duct GetMainDuct1(this DuctLevelingProcessor q)
        {
            var startPoint = q.DuctLine.GetEndPoint(0);
            var endPoint = q.MiddlePoint - q.DuctDirection * q.HorizontalOffset;
            q.DuctLocation.Curve = Line.CreateBound(startPoint, endPoint);

            return q.Duct!;
        }
        public static Duct GetMainDuct2(this DuctLevelingProcessor q)
        {
            var doc = revitData.Document;
            var systemTypeId = q.SystemTypeId;
            var ductTypeId = q.DuctTypeId;
            var levelId = q.LevelId;

            var startPoint = q.MiddlePoint + q.DuctDirection * q.HorizontalOffset;
            var endPoint = q.DuctLine.GetEndPoint(1);

            var width = q.Width;
            var height = q.Height;

            var duct = Duct.Create(doc,systemTypeId, ductTypeId, levelId, startPoint, endPoint);
            duct.LookupParameter("Width").Set(width);
            duct.LookupParameter("Height").Set(height);

            return duct;
        }
        public static Duct GetMiddleDuct(this DuctLevelingProcessor q)
        {
            var doc = revitData.Document;
            var systemTypeId = q.SystemTypeId;
            var ductTypeId = q.DuctTypeId;
            var levelId = q.LevelId;

            var midPoint = q.MiddlePoint;
            var dir = q.DuctDirection;

           

            var width = q.Width;
            var height = q.Height;
            var hozOffset = q.HorizontalOffset;
            var zOff = q.ZOffset;
            var startPoint = (midPoint - dir * hozOffset) + XYZ.BasisZ * zOff;
            var endPoint = (midPoint + dir * hozOffset) + XYZ.BasisZ * zOff;

            var duct = Duct.Create(doc, systemTypeId, ductTypeId, levelId, startPoint, endPoint);
            duct.LookupParameter("Width").Set(width);
            duct.LookupParameter("Height").Set(height);

            return duct;
        }


        public static void Do(this DuctLevelingProcessor q)
        {
            var doc = revitData.Document;
            using (var transition = new Transaction(doc,"Duct leveling"))
            {
                transition.Start();

                var mainDuct2 = q.MainDuct2;
                var mainDuct1= q.MainDuct1;
                var middleDuct = q.MiddleDuct;
                transition.Commit();
                

            }
        }
    }
}
