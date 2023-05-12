using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ApiField
    {
        public ApiField_Dict? Dict { get; set; }

        private ApiCall? apiCall;
        public ApiCall ApiCall => apiCall ??= Dict!.ApiCall!;

        private ApiCommand_Dict? command_Dict;
        public ApiCommand_Dict Command_Dict => command_Dict ??= this.GetApiCommand_Dict();

        public string? Name { get; set; }

        public string? Field { get; set; }

        public ApiCommand this[string command] => Command_Dict[command];
    }
}
