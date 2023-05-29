using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Form;
using System;
using System.Collections.Generic;
using Model.Entity;
using Utility;
using Autodesk.Revit.DB.Mechanical;
using System.Drawing;
using System.Windows;
using Autodesk.Revit.UI.Selection;
using Model.Data;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class DuctLevelingCommand : RevitCommand
    {
        private DuctLevelingProcessor_Data data => DuctLevelingProcessor_Data.Instance;
        public override void Execute()
        {
            //var processor = new DuctLevelingProcessor
            {
                var processor = data.Processor;
                processor.Duct = sel.PickElement<Duct>();
                processor.Point = sel.PickPoint();


                //Duct = sel.PickElement<Duct>(),
                //Point = sel.PickPoint()

            }
            //processor.Do();
            var form = data.Form;
            form.ShowDialog();
        }
    }
}