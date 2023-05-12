using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class LevelDataUtil
    {
        public static void Dispose()
        {
            LevelData.Instance = null;
        }
    }
}
