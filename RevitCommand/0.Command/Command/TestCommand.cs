using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Model.Entity;
using Model.Dataa;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : RevitCommand
    {
        public override void Execute()
        {
            var airTerminal = sel.PickElement<FamilyInstance>();
            var duct = sel.PickElement<Duct>();

            var airTerminalConnector = airTerminal.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>()
                .First(x => x.CoordinateSystem.BasisZ.IsPerpendicular(XYZ.BasisZ));

            var airTerminalConnectorDirection = airTerminalConnector.CoordinateSystem.BasisZ;
            var airTerminalConnectorOrigin = airTerminalConnector.Origin;

            var ductLocationLine = ((duct.Location as LocationCurve)!.Curve as Line)!;
            var projectionPoint = ductLocationLine.GetProjectPoint(airTerminalConnectorOrigin);

            var endpoint = projectionPoint - airTerminalConnectorDirection * duct.Width / 2;

            using (var transaction = new Transaction(doc, "AirTerminal connnect"))
            {
                transaction.Start();
                var systemTypeId = duct.MEPSystem.GetTypeId();
                var levelId = duct.LookupParameter("Reference Level").AsElementId();
                // create tempDuct
                var tempDuctTypeId = 142428.GetElementId();
                var tempDuctStartPoint = endpoint - airTerminalConnectorDirection * 100.0.milimeter2Feet();
                var tempDuctEndPoint = tempDuctStartPoint - airTerminalConnectorDirection * 400.0.milimeter2Feet();
                var tempDuct = Duct.Create(doc, systemTypeId, tempDuctTypeId, levelId, tempDuctStartPoint, tempDuctEndPoint);
                var tempDuctDiameter = airTerminalConnector.Radius * 2;
                tempDuct.LookupParameter("Diameter").Set(tempDuctDiameter);
                //create Tap
                var tempDuctConnector = tempDuct.ConnectorManager.UnusedConnectors.Cast<Connector>()
                    .First(x => x.Origin.IsEqual(tempDuctStartPoint));
                var ductFitting = doc.Create.NewTakeoffFitting(tempDuctConnector, duct);
                // delete tempDuct
                doc.Delete(tempDuct.Id);
                var ductFittingConnector = ductFitting.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>().First();

                //create flexduct
                var flexDuctId = 142444.GetElementId();
                var flexDuctTangent = airTerminalConnectorDirection;
                var flexDuctEndpoint = ductFittingConnector.Origin;
                var flexDuctPoints = new List<XYZ> { airTerminalConnectorOrigin, flexDuctEndpoint };
                var flexDuct = FlexDuct.Create(doc, systemTypeId, flexDuctId, levelId, flexDuctTangent, flexDuctTangent, flexDuctPoints);

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
                        flexDuctConnector.ConnectTo(ductFittingConnector);
                    }

                }
                transaction.Commit();

            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class TestCommand2 : RevitCommand
    {
        public override void Execute()
        {
            //var airTerminal = sel.PickElement<FamilyInstance>();
            //var duct = sel.PickElement<Duct>();

            var processor = new AirTerminalProcessor
            {
                MainDuct = sel.PickElement<Duct>(),
                AirTerminal = sel.PickElement<FamilyInstance>()
            };

            processor.Do();

            // kết quả
            //var tap = processor.Tap;
            //TaskDialog.Show("Revit", $"Đối tượng tap: {tap.Name}");

        }

    }

    [Transaction(TransactionMode.Manual)]
    public class TestCommand3 : RevitCommand
    {
        private AirTerminalProcessor_Data data => AirTerminalProcessor_Data.Instance;
        public override void Execute()
        {
            //var form = new AirTerminalProcessor_Form();
            //form.DataContext = new AirTerminalProcessor_Data();

            ////xuất hiện hộp thoại Form
            //form.ShowDialog();
            {
                var processor = data.Processor;
                processor.MainDuct = sel.PickElement<Duct>();
                processor.AirTerminal = sel.PickElement<FamilyInstance>();

            }

            var form = data.Form;
            form.ShowDialog();
        }

    }

}



