using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class Object_Dict
    {
        private List<EntObject>? objects;
        public List<EntObject> Objects => objects ??= new List<EntObject>();

        public EntObject this[string name] => this.GetEntObject(name);
    }
}
