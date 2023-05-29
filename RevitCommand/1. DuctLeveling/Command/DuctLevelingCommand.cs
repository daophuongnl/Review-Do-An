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

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class DuctLevelingCommand : RevitCommand
    {
        public override void Execute()
        {
            var processor = new DuctLevelingProcessor
            {
                Duct = sel.PickElement<Duct>(),
                Point = sel.PickPoint()

            };
            processor.Do();
        }
    }
}