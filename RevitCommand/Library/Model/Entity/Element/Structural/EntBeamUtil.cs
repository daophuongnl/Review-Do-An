using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntBeamUtil
    {
        #region Property

        public static Level? GetLevel(this EntBeam beam)
        {
            return LevelUtil.GetForBeam(beam);
        }

        #endregion
    }
}
