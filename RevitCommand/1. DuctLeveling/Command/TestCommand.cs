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
            using (var transaction = new Transaction(doc, "Duct levering"))
            {
                transaction.Start();

                var mainDuct = sel.PickElement<Duct>();

                var mainLocationCurve = (mainDuct.Location as LocationCurve)!;

                var mainLine = (mainLocationCurve.Curve as Line)!;
                var mainDirection = mainLine.Direction;

                var point11 = mainLine.GetEndPoint(0);
                var point32 = mainLine.GetEndPoint(1);

                var midpoint = mainLine.GetCenterPoint();
                var hozzoffset = 1200.0.milimeter2Feet();

                var zOffset = -600.0.milimeter2Feet();

                var point12 = midpoint - hozzoffset * mainDirection;
                var point31 = midpoint + hozzoffset * mainDirection;

                mainLocationCurve.Curve = Line.CreateBound(point11, point12);

                var systemTypeId = mainDuct.MEPSystem.GetTypeId();
                var ductTypeId = mainDuct.GetTypeId();
                var levelId = mainDuct.LookupParameter("Reference Level").AsElementId();

                var hozz2Offset = 500.0.milimeter2Feet();

                var point21 = point12 - XYZ.BasisZ * zOffset + hozz2Offset * mainDirection;
                var point22 = point31 - XYZ.BasisZ * zOffset - hozz2Offset * mainDirection;

                var width = mainDuct.LookupParameter("Width").AsDouble();
                var height = mainDuct.LookupParameter("Height").AsDouble();


                var duct1 = mainDuct;

                var duct2 = Duct.Create(doc, systemTypeId, ductTypeId, levelId, point31, point32);
                duct2.LookupParameter("Width").Set(width);
                duct2.LookupParameter("Height").Set(height);



                var duct3 = Duct.Create(doc, systemTypeId, ductTypeId, levelId, point21, point22);
                duct3.LookupParameter("Width").Set(width);
                duct3.LookupParameter("Height").Set(height);


                var connectDuct1 = Duct.Create(doc, systemTypeId, ductTypeId, levelId, point12, point21);
                connectDuct1.LookupParameter("Width").Set(width);
                connectDuct1.LookupParameter("Height").Set(height);


                var connectDuct2 = Duct.Create(doc, systemTypeId, ductTypeId, levelId, point22, point31);
                connectDuct2.LookupParameter("Width").Set(width);
                connectDuct2.LookupParameter("Height").Set(height);

                Connect2Ducts(duct1, connectDuct1);
                Connect2Ducts(connectDuct1, duct3);
                Connect2Ducts(duct3, connectDuct2);
                Connect2Ducts(connectDuct2, duct2);

                transaction.Commit();
            }

        }
        public void Connect2Ducts(Duct duct1, Duct duct2)
        {
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
            doc.Create.NewElbowFitting(connector1, connector2);
        }
    }
}



