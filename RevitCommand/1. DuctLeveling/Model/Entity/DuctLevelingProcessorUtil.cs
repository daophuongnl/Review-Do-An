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
            return (duct!.Location as LocationCurve)!;
        }

        public static Line GetDuctLine(this DuctLevelingProcessor q)
        {
            var duct = q.Duct;
            return ((duct!.Location as LocationCurve)!.Curve as Line)!;
        }
        //TH1 : cả 2 đầu đều đã kết nối connector
        public static Connector GetStartConnector(this DuctLevelingProcessor q)
        {
            var duct = q.Duct!;
            var line = q.DuctLine!;
            var startPoint = line.GetEndPoint(0);

            var connector = duct.ConnectorManager.Connectors.Cast<Connector>()
                .Where(connector => connector.IsConnected)
                .FirstOrDefault(connector => connector.Origin.IsEqual(startPoint));
            return connector;
        }
        public static Connector GetEndConnector(this DuctLevelingProcessor q)
        {
            var duct = q.Duct!;
            var line = q.DuctLine!;
            var endPoint = line.GetEndPoint(1);

            var connector = duct.ConnectorManager.Connectors.Cast<Connector>()
                .Where(connector => connector.IsConnected)
                .FirstOrDefault(connector => connector.Origin.IsEqual(endPoint));
            return connector;
        }
        public static Connector GetConnectToEndConnector(this DuctLevelingProcessor q)
        {
            var endConnector = q.EndConnector;
            var duct = q.Duct;
            if(endConnector == null )
            {
                return null!;
            }
            return endConnector.AllRefs.Cast<Connector>()
                .First(connector => connector.Owner.Id != duct!.Id);
        }

        // TH2: 1 đầu có connector và 1 đầu ko
        public static DuctLevelingMode GetMode (this DuctLevelingProcessor q)
        {
            var endConnector = q.EndConnector;
            if (endConnector == null)
            {
                return DuctLevelingMode.Type1;
            }
            else
            {
                var startConnector = q.StartConnector;
                if (startConnector == null)
                {
                    return DuctLevelingMode.Type2;
                }
                else
                {
                    return DuctLevelingMode.Type3;
                }
            }
        }


        public static bool GetIsResverse(this DuctLevelingProcessor q)
        {

            //var duct = q.Duct!;
            //var line = q.DuctLine!;
            //var endPoint = line.GetEndPoint(1);

            //var isResverse = duct.ConnectorManager.Connectors.Cast<Connector>()
            //    .Where(connector =>connector.IsConnected)
            //    .Any(connector => connector.Origin.IsEqual(endPoint));

            return q.EndConnector != null && q.StartConnector == null;
        }

        public static XYZ GetDuctDirection (this DuctLevelingProcessor q) 
        {
            return q.DuctLine!.Direction;
        }
        public static ElementId GetLevelId(this DuctLevelingProcessor q)
        {
            return q.Duct!.LookupParameter("Reference Level").AsElementId();
        }
        public static ElementId GetSystemTypeId(this DuctLevelingProcessor q)
        {
            return q.Duct!.MEPSystem.GetTypeId();
        }

        public static XYZ GetPickPoint(this DuctLevelingProcessor q)
        {
            return q.DuctLine!.GetCenterPoint();
        }
        public static XYZ GetMiddlePoint(this DuctLevelingProcessor q)
        {

            var point = q.PickPoint;
            return q.DuctLine!.GetProjectPoint(point!);

        }
        public static Duct GetMainDuct1(this DuctLevelingProcessor q)
        {
            var widthMiddleDuct = q.Width_MiddleDuct;
            XYZ? startPoint = null;
            XYZ? endPoint = null;

            var isReverse = q.IsResverse;
            if(!isReverse)
            {
                startPoint = q.DuctLine!.GetEndPoint(0);
                endPoint = q.MiddlePoint - q.DuctDirection * (q.HorizontalOffset + widthMiddleDuct / 2);
            }
            else
            {
                startPoint = q.MiddlePoint + q.DuctDirection * (q.HorizontalOffset + widthMiddleDuct / 2);
                endPoint = q.DuctLine!.GetEndPoint(1);
            }

            q.DuctLocation.Curve = Line.CreateBound(startPoint, endPoint);
            var duct = q.Duct!;

            //duct.LookupParameter("Comments").Set("duct1");

            return q.Duct!;
        }
        public static Duct GetMainDuct2(this DuctLevelingProcessor q)
        {
            var doc = revitData.Document;
            var systemTypeId = q.SystemTypeId;
            var ductTypeId = q.DuctTypeId;
            var levelId = q.LevelId;
            var widthMiddleDuct = q.Width_MiddleDuct;
            var width = q.Width;
            var height = q.Height;

            XYZ? startPoint = null;
            XYZ? endPoint = null;

            var isReverse = q.IsResverse;
            if (!isReverse)
            {
                startPoint = q.MiddlePoint + q.DuctDirection * (q.HorizontalOffset + widthMiddleDuct / 2);
                endPoint = q.DuctLine!.GetEndPoint(1);
            }
            else
            {
                startPoint = q.DuctLine!.GetEndPoint(0);
                endPoint = q.MiddlePoint - q.DuctDirection * (q.HorizontalOffset + widthMiddleDuct / 2);
            }
            
            var duct = Duct.Create(doc,systemTypeId, ductTypeId, levelId, startPoint, endPoint);
            duct.LookupParameter("Width").Set(width);
            duct.LookupParameter("Height").Set(height);

            //duct.LookupParameter("Comments").Set("duct2");

            //connect 
            if(q.Mode == DuctLevelingMode.Type3)
            {
                var connector = duct.ConnectorManager.UnusedConnectors.Cast<Connector>()
                    .FirstOrDefault(connector => connector.Origin.IsEqual(endPoint));
                connector.ConnectTo(q.ConnectToEndConnector);
            }    
            return duct;
        }
        public static Duct GetMiddleDuct(this DuctLevelingProcessor q)
        {
            
            var widthMiddleDuct = q.Width_MiddleDuct;
            var midPoint = q.MiddlePoint;
            var dir = q.DuctDirection;

            var zOff = q.ZOffset;
            var startPoint = (midPoint - dir * widthMiddleDuct / 2) + XYZ.BasisZ * zOff;
            var endPoint = (midPoint + dir * widthMiddleDuct / 2) + XYZ.BasisZ * zOff;

            return q.CreateDuct(startPoint, endPoint);
        }
        public static Duct ConnectorDuct1(this DuctLevelingProcessor q)
        {
            var duct1 = q.MainDuct1;
            var duct2 = q.MiddleDuct;

            XYZ? startPoint = null;
            XYZ? endPoint = null;

            var isReverse = q.IsResverse;
            if(!isReverse)
            {
                startPoint = ((duct1.Location as LocationCurve)!.Curve).GetEndPoint(1);
                endPoint = ((duct2.Location as LocationCurve)!.Curve).GetEndPoint(0);
            }
            else
            {
                 
                startPoint = ((duct2.Location as LocationCurve)!.Curve).GetEndPoint(1);
                endPoint = ((duct1.Location as LocationCurve)!.Curve).GetEndPoint(0);
            }

            var connectorDuct = q.CreateDuct(startPoint, endPoint);

            Connect2Ducts(duct1, connectorDuct);
            Connect2Ducts(connectorDuct, duct2);
            return connectorDuct;
        }
        public static Duct ConnectorDuct2(this DuctLevelingProcessor q)
        {
            var duct1 = q.MainDuct2;
            var duct2 = q.MiddleDuct;


            XYZ? startPoint = null;
            XYZ? endPoint = null;

            var isReverse = q.IsResverse;
            if (!isReverse)
            {
                startPoint = ((duct1.Location as LocationCurve)!.Curve).GetEndPoint(0);
                endPoint = ((duct2.Location as LocationCurve)!.Curve).GetEndPoint(1);
            }
            else
            {
                startPoint = ((duct2.Location as LocationCurve)!.Curve).GetEndPoint(0);
                endPoint = ((duct1.Location as LocationCurve)!.Curve).GetEndPoint(1);
            }

            var connectorDuctt = q.CreateDuct(startPoint, endPoint);

            Connect2Ducts(duct1, connectorDuctt);
            Connect2Ducts(connectorDuctt, duct2);

            return connectorDuctt;
        }


        // thực hiện câu lệnh
        public static void Do(this DuctLevelingProcessor q)
        {
            var doc = revitData.Document;
            using (var transition = new Transaction(doc,"Duct leveling"))
            {
                transition.Start();
               if(q.Mode == DuctLevelingMode.Type3)
               {
                    q.EndConnector.DisconnectFrom(q.ConnectToEndConnector);
               }    

                var mainDuct2 = q.MainDuct2;
                var mainDuct1= q.MainDuct1;
                var middleDuct = q.MiddleDuct;
                var connectorDuct1 = q.ConnectorDuct1;
                var connectorDuct2 = q.ConnectorDuct2;

                transition.Commit();
                
            }
        }

        private static Duct CreateDuct(this DuctLevelingProcessor q,XYZ startPoint,XYZ endPoint)
        {
            var doc = revitData.Document;
            var systemTypeId = q.SystemTypeId;
            var ductTypeId = q.DuctTypeId;
            var levelId = q.LevelId;
            
            var width = q.Width;
            var height = q.Height;

            var duct = Duct.Create(doc, systemTypeId, ductTypeId, levelId, startPoint, endPoint);
            duct.LookupParameter("Width").Set(width);
            duct.LookupParameter("Height").Set(height);

            return duct;
        }
        public static void Connect2Ducts(Duct duct1, Duct duct2)
        {
            var doc = revitData.Document;
            var connectors1 = duct1.ConnectorManager.UnusedConnectors.Cast<Connector>();
            var connectors2 = duct2.ConnectorManager.UnusedConnectors.Cast<Connector>();

            Connector? connector1 = null;
            Connector? connector2 = null;

            foreach (var conn1 in connectors1)
            {
                if (connector1 != null && connector2 != null)
                {
                    break;
                }

                var origin1 = conn1.Origin;
                foreach (var conn2 in connectors2)
                {
                    var origin2 = conn2.Origin;
                    if (origin1.IsEqual(origin2))
                    {
                        connector1 = conn1;
                        connector2 = conn2;
                        break; // thoát vòng lặp
                    }
                }
            }
            if (connector1 == null || connector2 == null)
            {
                return;
            }
            doc.Create.NewElbowFitting(connector1, connector2);
        }
    }

}

