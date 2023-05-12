using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class Object_DictUtil
    {
        #region Method

        public static EntObject GetEntObject(this Object_Dict dict, string name)
        {
            var list = dict.Objects;
            var item = list.FirstOrDefault(x => x.Name == name);
            if (item == null)
            {
                item = new EntObject
                {
                    Name = name
                };
                list.Add(item);
            }
            return item;
        }

        #endregion
    }
}
