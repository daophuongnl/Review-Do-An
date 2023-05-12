using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;

namespace Utility
{
    public static class RebarDataUtil
    {
        public static RebarData rebarData => RebarData.Instance;
        public static ViewData viewData => ViewData.Instance;

        public static void Dispose()
        {
            RebarData.Instance = null;
        }

        #region Property

        public static IEnumerable<Autodesk.Revit.DB.ViewSchedule> GetAllRebarRevitViewSchedules()
        {
            return viewData.ViewSchedules.Where(x => x.Definition.CategoryId.IntegerValue == (int)BuiltInCategory.OST_Rebar);
        }

        #endregion
    }
}
