using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class JsonDataUtil
    {
        public static void Dispose()
        {
            JsonData.Instance = null;
        }
    }
}
