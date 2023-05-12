using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntColumnUtil
    {
        #region Property

        public static Level? GetLevel(this EntColumn column)
        {
            return LevelUtil.GetForColumn(column);
        }

        public static Level? GetBaseLevel(this EntColumn column)
        {
            return LevelUtil.GetBaseForColumn(column);
        }

        public static Level? GetTopLevel(this EntColumn column)
        {
            return LevelUtil.GetTopForColumn(column);
        }

        #endregion
    }
}
