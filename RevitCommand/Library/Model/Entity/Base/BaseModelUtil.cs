using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class BaseModelUtil
    {
        #region Method

        public static void UpdateViewModel(this BaseModel model, Action action)
        {
            model.VM2M = false;

            action();

            model.VM2M = true;
        }

        #endregion
    }
}
