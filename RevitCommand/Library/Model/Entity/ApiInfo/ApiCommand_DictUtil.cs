using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class ApiCommand_DictUtil
    {
        public static ApiCommand_Dict GetApiCommand_Dict(this ApiField field)
        {
            return new ApiCommand_Dict
            {
                Field = field
            };
        }

        #region Method

        public static ApiCommand GetApiCommand(this ApiCommand_Dict dict, string command)
        {
            var list = dict.Commands;
            var item = list.FirstOrDefault(x => x.Command == command);
            if (item == null)
            {
                item = new ApiCommand
                {
                    Command = command,
                    Dict = dict
                };
                list.Add(item);
            }
            return item;
        }

        #endregion
    }
}
