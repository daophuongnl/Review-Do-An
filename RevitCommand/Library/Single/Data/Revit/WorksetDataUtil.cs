using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class WorksetDataUtil
    {
        private static WorksetData worksetData
        {
            get
            {
                return WorksetData.Instance;
            }
        }

        public static void RefreshDocument()
        {
            worksetData.Worksets = null;
            worksetData.WorksetDefaultVisibilitySettings = null;

            (worksetData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (worksetData as DataBase).RefreshDocument();
        }

        public static void Dispose()
        {
            WorksetData.Instance = null;
        }
    }
}
