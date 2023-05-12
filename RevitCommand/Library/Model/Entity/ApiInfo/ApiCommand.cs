using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ApiCommand
    {
        public ApiCommand_Dict? Dict { get; set; }

        private ApiField? apiField;
        public ApiField ApiField => apiField ??= Dict!.Field!;

        private ApiCall? apiCall;
        public ApiCall ApiCall => apiCall ??= ApiField.ApiCall;

        public string Domain => ApiCall.Domain!;

        public string Field => ApiField.Field!;

        public string? Command { get; set; }

        private string? url;
        public string Url => url ??= this.GetUrl();
    }
}
