using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleData
{
    public class FormData
    {
        private static FormData? instance;
        public static FormData Instance
        {
            get => instance ??= new FormData();
            set => instance = value;
        }

        public bool IsDropDownOpen { get; set; } = false;
    }
}
