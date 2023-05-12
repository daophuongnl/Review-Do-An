using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ApiCall
    {
        public string? Domain { get; set; }

        private ApiField_Dict? field_Dict;
        public ApiField_Dict Field_Dict => field_Dict ??= this.GetApiField_Dict();

        public ApiField this[string field] => Field_Dict[field];
    }
}
