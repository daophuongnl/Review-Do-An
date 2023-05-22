using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Model.Dataa;
using Model.Form;
using System;
using System.Collections.Generic;
using Utility;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class AirTerminalConnectCommand : RevitCommand
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