using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntParameterDict
    {
        public EntElement? Element { get; set; }

        private List<EntParameter>? parameters;
        public List<EntParameter> Parameters => parameters ??= this.GetEntParameters();

        public bool IsFullParameters { get; set; } = false;

        public EntParameter? this[string name]  => this.GetEntParameter(name);

        public EntParameter? this[List<string> names] => this.GetEntParameter(names);

        public EntParameter? this[EntFilter filter] => this.GetEntParameter(filter);
    }
}
