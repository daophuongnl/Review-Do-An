using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Application
{
    public class RibbonApp : IExternalApplication
    {
        private RibbonData ribbonData => RibbonData.Instance;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonData.Application = application;

            var tab = EntTabUtil.Get("BiMHoaBinh");
            var quanPanel = tab.GetPanel("Database");
            quanPanel.GetPushButton("Xuất dữ liệu", "Model.RevitCommand.PMCommand", "bim");

            tab.CreateTab();

            return Result.Succeeded;
        }

        //private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        //{
        //    boQExcelLinkData.SelectElementByActiveCell();
        //}

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
