using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleData
{
    public class BaseFormData : NotifyClass
    {
        public bool IsShowDialog { get; set; } = false;

        public System.Windows.Window? TargetForm { get; set; }
    }
}
