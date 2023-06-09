﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Model.Dataa;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Utility;

namespace Model.Entity
{
    //class công cụ, chứa khối lệnh để xử lí đối tượng AirTerminalProcessor
    public static class AirTerminalProcessorUtil
    {
        //chỗ lưu trữ các đối tượng Revit
        private static RevitData revitData => RevitData.Instance;
        private static AirTerminalProcessor_Data data=> AirTerminalProcessor_Data.Instance;

        public static Connector GetAirTerminalConnector(this AirTerminalProcessor q)
        {
            var airTerminal = q.AirTerminal!;

            return airTerminal.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>()
                .First(x => x.CoordinateSystem.BasisZ.IsParallel(XYZ.BasisZ));
        }
        public static Connector GetAirTerminalConnectorr (this AirTerminalProcessor q)
        {
            var airTerminal = q.AirTerminal!;
            return airTerminal.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>()
                .First(x => x.CoordinateSystem.BasisZ.IsPerpendicular(XYZ.BasisZ));
        }


        // Thuộc tính của đối tượng 
        public static ElementId GetLevelId( this AirTerminalProcessor q) 
        {
            return q.MainDuct!.LookupParameter("Reference Level").AsElementId();
        }
        public static ElementId GetSystemTypeId(this AirTerminalProcessor q)
        {
            return q.MainDuct!.MEPSystem.GetTypeId();
        }
        

        public static XYZ GetTempDuctStartPoint(this AirTerminalProcessor q) 
        {
            var mainDuct = q.MainDuct!;

            var airTerminalConnector = q.AirTerminalConnectorr;
                                    
            var airTerminalConnectorDirection = airTerminalConnector.CoordinateSystem.BasisZ;
            var airTerminalConnectorOrigin = airTerminalConnector.Origin;

            var mainDuctLocationLine = ((mainDuct.Location as LocationCurve)!.Curve as Line)!;
            var projectionPoint = mainDuctLocationLine.GetProjectPoint(airTerminalConnectorOrigin);

            var endpoint = projectionPoint - airTerminalConnectorDirection * mainDuct.Width / 2;
            
            return endpoint - airTerminalConnectorDirection * 100.0.milimeter2Feet();
        }

        public static Duct GetTempDuct(this AirTerminalProcessor q)
        {
            var mainDuct = q.MainDuct!;
            var airTerminal = q.AirTerminal!;

            var doc = revitData.Document;

            var airTerminalConnector = q.AirTerminalConnectorr;
            var airTerminalConnectorDirection = airTerminalConnector.CoordinateSystem.BasisZ;

            //var airTerminalConnectorOrigin = airTerminalConnector.Origin;
            //var mainDuctLocationLine = ((mainDuct.Location as LocationCurve)!.Curve as Line)!;
            //var projectionPoint = mainDuctLocationLine.GetProjectPoint(airTerminalConnectorOrigin);

            //var endpoint = projectionPoint - airTerminalConnectorDirection * mainDuct.Width / 2;


                var systemTypeId = q.SystemTypeId;
                var levelId = q.LevelId;

            // create tempDuct
            //var tempDuctTypeId = 142428.GetElementId();
                var tempDuctTypeId = q.DuctType!.Id;
                var tempDuctStartPoint = q.TempDuctStartPoint;
                
                var tempDuctEndPoint = tempDuctStartPoint - airTerminalConnectorDirection * 400.0.milimeter2Feet();
                var tempDuct = Duct.Create(doc, systemTypeId, tempDuctTypeId, levelId, tempDuctStartPoint, tempDuctEndPoint);
                var tempDuctDiameter = airTerminalConnector.Radius * 2;
                tempDuct.LookupParameter("Diameter").Set(tempDuctDiameter);
                return tempDuct;
        }

       public static FamilyInstance GetTap( this AirTerminalProcessor q)
       {
            
            var doc = revitData.Document;
            var tempDuct = q.TempDuct;
            var mainDuct = q.MainDuct!;

            var tempDuctStartPoint = q.TempDuctStartPoint;

            var tempDuctConnector = tempDuct.ConnectorManager.UnusedConnectors.Cast<Connector>()
                         .First(x => x.Origin.IsEqual(tempDuctStartPoint));
            return doc.Create.NewTakeoffFitting(tempDuctConnector, mainDuct);
       }

        public static FlexDuctType GetFlexDuctType(this AirTerminalProcessor q)
        {
            return data.FlexDuctTypes.FirstOrDefault(x=>x.Shape==ConnectorProfileType.Round);
        }

        public static DuctType GetDuctType(this AirTerminalProcessor q)
        {
            return data.DuctTypes.FirstOrDefault(x=> x.Shape==ConnectorProfileType.Round);
        }

        public static FlexDuct GetFlexDuct ( this AirTerminalProcessor q) 
        {
            var doc = revitData.Document;

            var tap = q.Tap;
            var tapConnector = tap.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>().First();

            var airTerminalConnectorr = q.AirTerminalConnectorr;

            var airTerminalConnector = q.AirTerminalConnector;
            var airTerminalConnectorDirection = airTerminalConnector.CoordinateSystem.BasisZ;
            var airTerminalConnectorDirectionn = airTerminalConnectorr.CoordinateSystem.BasisZ;
            var airTerminalConnectorOrigin = airTerminalConnector.Origin;



            var systemTypeId = q.SystemTypeId;
            var levelId = q.LevelId;

            //create flexduct
            //var flexDuctId = 142444.GetElementId();
            var flexDuctId = q.FlexDuctType!.Id;
            var flexDuctTangent = airTerminalConnectorDirectionn;
            var flexDuctTangentt = airTerminalConnectorDirection;
            var flexDuctEndpoint = tapConnector.Origin;
            var flexDuctPoints = new List<XYZ> { airTerminalConnectorOrigin, flexDuctEndpoint };
            var flexDuct = FlexDuct.Create(doc, systemTypeId, flexDuctId, levelId, flexDuctTangentt, flexDuctTangent, flexDuctPoints);

            var flexDuctConnectors = flexDuct.ConnectorManager.UnusedConnectors.Cast<Connector>();
            foreach (var flexDuctConnector in flexDuctConnectors)
            {
                var origin = flexDuctConnector.Origin;
                if (origin.IsEqual(airTerminalConnectorOrigin))
                {
                    flexDuctConnector.ConnectTo(airTerminalConnector);
                }
                else
                {
                    flexDuctConnector.ConnectTo(tapConnector);
                }
            }
            return flexDuct;
        }

        public static void Do(this AirTerminalProcessor q )
        {
            var doc = revitData.Document;
            using (var transaction = new Transaction(doc, "AirTerminal connnect"))
            {
                var failureHandlingOptions = transaction.GetFailureHandlingOptions();
                failureHandlingOptions.SetFailuresPreprocessor(new TapAttachedToDuctFailurePreprocessor());
                transaction.SetFailureHandlingOptions(failureHandlingOptions);

                transaction.Start();

                //var tap = q.Tap;
                var tempDuct = q.TempDuct;
                var tap = q.Tap;
                doc.Delete(tempDuct.Id);
                var flexDuct = q.FlexDuct;
              
                transaction.Commit();

            }
        }
    }
}
