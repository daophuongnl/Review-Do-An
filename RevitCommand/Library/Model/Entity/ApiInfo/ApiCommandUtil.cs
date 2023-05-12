using Model.Constant;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class ApiCommandUtil
    {
        #region Property

        public static string GetUrl(this ApiCommand command)
        {
            return $"{command.Domain}/{command.Field}/{command.Command}";
        }

        #endregion
    }
}
