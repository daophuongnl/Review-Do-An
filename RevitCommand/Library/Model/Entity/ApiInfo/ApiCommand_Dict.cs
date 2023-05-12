using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ApiCommand_Dict
    {
        public ApiField? Field { get; set; }

        private List<ApiCommand>? commands;
        public List<ApiCommand> Commands => commands ??= new List<ApiCommand>();

        public ApiCommand this[string command] => this.GetApiCommand(command);
    }
}
