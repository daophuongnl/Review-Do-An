using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntArcWallUtil
    {
        #region Property

        public static Level? GetLevel(this EntArcWall wall)
        {
            return LevelUtil.GetForWall(wall);
        }

        public static Level? GetBaseLevel(this EntArcWall wall)
        {
            return LevelUtil.GetBaseForWall(wall);
        }

        public static Level? GetTopLevel(this EntArcWall wall)
        {
            return LevelUtil.GetTopForWall(wall);
        }

        #endregion
    }
}
