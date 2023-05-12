using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntStairUtil
    {
        #region Property

        public static Level? GetLevel(this EntStair stair)
        {
            return LevelUtil.GetForStair(stair);
        }

        public static Level? GetBaseLevel(this EntStair stair)
        {
            return LevelUtil.GetBaseForStair(stair);
        }

        public static Level? GetTopLevel(this EntStair stair)
        {
            return LevelUtil.GetTopForStair(stair);
        }

        #endregion
    }
}
