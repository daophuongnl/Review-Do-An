using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ListUtil
    {
        public static ObservableCollection<T> ToObservableCollection<T>
            (this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}
