using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntLevelUtil
    {
        // Instance
        public static EntLevel GetEntLevel(this Level level)
        {
            return new EntLevel
            {
                RevitLevel = level
            };
        }
    }
}
