using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ApiField_Dict
    {
        public ApiCall? ApiCall { get; set; }

        private List<ApiField>? fields;
        public List<ApiField> Fields => fields ??= new List<ApiField>();

        public ApiField this[string field] => this.GetApiField(field);
    }
}
