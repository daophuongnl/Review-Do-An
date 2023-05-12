using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class ArchitectDataUtil
    {
        public static void Dispose()
        {
            ArchitectData.Instance = null;
        }
    }
}
