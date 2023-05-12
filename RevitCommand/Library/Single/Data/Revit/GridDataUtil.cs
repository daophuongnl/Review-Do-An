using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;

namespace Utility
{
    public static class GridDataUtil
    {
        private static GridData gridData => GridData.Instance;

        #region Property

        public static XYZ? GetBasePoint()
        {
            var allGrids = gridData.AllGrids;

            var gridX = allGrids.FirstOrDefault(x => x.Name == "1");
            var gridY = allGrids.FirstOrDefault(x => x.Name == "A");

            var l1 = gridX.Curve as Line;
            var l2 = gridY.Curve as Line;

            if (l1 is null || l2 is null) return null;

            var origin = l1.GetLineIntersectLineResult(l2).IntersectPoint;

            return origin;
        }

        #endregion

        #region Method

        public static void Dispose()
        {
            GridData.Instance = null;
        }

        #endregion
    }
}
