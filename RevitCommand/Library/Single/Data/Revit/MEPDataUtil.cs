using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class MEPDataUtil
    {
        public static void Dispose()
        {
            MEPData.Instance = null;
        }
    }
}
