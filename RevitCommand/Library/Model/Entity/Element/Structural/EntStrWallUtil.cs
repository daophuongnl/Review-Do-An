using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntStrWallUtil
    {
        public static Level? GetLevel(this EntStrWall wall)
        {
            return LevelUtil.GetForWall(wall);
        }

        public static Level? GetBaseLevel(this EntStrWall wall)
        {
            return LevelUtil.GetBaseForWall(wall);
        }

        public static Level? GetTopLevel(this EntStrWall wall)
        {
            return LevelUtil.GetTopForWall(wall);
        }
    }
}
