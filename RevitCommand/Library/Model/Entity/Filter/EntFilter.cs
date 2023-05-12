using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class EntFilter
    {
        public string? valueType { get; set; }

        public string? numberFilterType { get; set; }

        public string? stringFilterType { get; set; }

        public List<dynamic>? values { get; set; }
    }
}
