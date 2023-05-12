using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class ViewDataUtil
    {
        private static ViewData viewData
        {
            get
            {
                return ViewData.Instance;
                
            }
        }

        public static void RefreshDocument()
        {
            viewData.ActiveView = null;
            viewData.ActiveUIView = null;
            viewData.ActivePlane = null;
            viewData.ActiveSketchPlane = null;
            viewData.WorkPlane = null;
            viewData.FillPatternElements = null;
            viewData.Views = null;
            viewData.ViewSchedules = null;
            viewData.ViewSheets = null;
            viewData.Viewports = null;
            viewData.ViewSections = null;
            viewData.ViewDetails = null;
            viewData.ViewFamilyTypes = null;
            viewData.ViewSectionTypes = null;
            viewData.ViewDetailTypes = null;

            (viewData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (viewData as DataBase).RefreshUIDocument();
        }

        public static void Dispose()
        {
            ViewData.Instance = null;
        }
    }
}
