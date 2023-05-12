using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static class BaseFormDataUtil
    {
        private static RevitData revitData => RevitData.Instance;

        #region Method

        public static void RaiseExternalEvent(this BaseFormData formData, Action action)
        {
            var isShowDialog = formData.IsShowDialog;

            if (isShowDialog)
            {
                var form = formData.TargetForm!;
                form.Hide();
                action();
                form.ShowDialog();
            }
            else
            {
                try
                {
                    revitData.ExternalEventHandler
                        .SetActionAndRaise(action);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
                    //throw;
                }
            }
        }

        public static void ShowForm(this BaseFormData formData)
        {
            var isShowDialog = formData.IsShowDialog;
            var form = formData.TargetForm!;

            if (isShowDialog)
            {
                form.ShowDialog();
            }
            else
            {
                form.Show();
            }
        }

        #endregion
    }
}
