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
        //protected override bool HasExternalEvent => true;
        //protected override bool IsAutoDisposed => false;

        public override void Execute()
        {
            //var processor = new DuctLevelingProcessor
            //{
            //    var processor = data.Processor;
            //    processor.Duct = sel.PickElement<Duct>();
            //    processor.PickPoint = sel.PickPoint();

            //    //Duct = sel.PickElement<Duct>()
            //    //PickPoint = sel.PickPoint()

            //}
            //processor.Do();


            //THIẾT LẬP SỰ KIỆN
            //revitData.ExternalEventHandler.Action = () =>
            //{
            //    var process = data.Processor = new DuctLevelingProcessor();

            //    process.Duct = revitData.Selection.PickElement<Duct>();
            //    process.PickPoint = revitData.Selection.PickPoint();
            //    process.Do();
            //};

            var form = data.Form;
            form.Show();

            while (true)
            { 
                try
                {
                    var duct = sel.PickElement<Duct>();
                    var process = data.Processor = new DuctLevelingProcessor();
                    {
                        Duct = duct
                    };
                    process.Do();
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    break;
                }


            }
            form.Close();
        }
    }
}