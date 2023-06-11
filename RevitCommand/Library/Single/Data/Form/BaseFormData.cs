using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SingleData
{
    public class BaseFormData : NotifyClass
    {
        public bool IsShowDialog { get; set; } = false;

        public System.Windows.Window? TargetForm { get; set; }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private string processor;

        public string Processor { get => processor; set => SetProperty(ref processor, value); }
    }
}
