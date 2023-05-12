using Model.Event;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ExternalEventHandlerUtil
    {
        private static RevitData revitData => RevitData.Instance;

        #region Method
        public static void SetActionAndRaise(this ExternalEventHandler externalEventHandler, Action action = null)
        {
            if (action != null)
            {
                externalEventHandler.Action = action;
            }
            revitData.ExternalEvent!.Raise();
        }
        #endregion
    }
}
