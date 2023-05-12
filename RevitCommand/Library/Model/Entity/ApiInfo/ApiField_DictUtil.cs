using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class ApiField_DictUtil
    {
        public static ApiField_Dict GetApiField_Dict(this ApiCall apiCall)
        {
            return new ApiField_Dict
            {
                ApiCall = apiCall
            };
        }

        #region Property

        public static ApiField GetApiField(this ApiField_Dict dict, string field)
        {
            var list = dict.Fields;
            var item = list.FirstOrDefault(x => x.Field == field);
            if (item == null)
            {
                item = new ApiField
                {
                    Dict = dict,
                    Field = field
                };
                list.Add(item);
            }
            return item;
        }

        #endregion
    }
}
