using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ExceptionUtil
    {
        #region Method

        public static string AsString(this Exception ex)
        {
            return $"{ex.Message}\n{ex.StackTrace}";
        }

            #endregion
    }
}
