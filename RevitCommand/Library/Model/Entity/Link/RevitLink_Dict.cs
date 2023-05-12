using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class RevitLink_Dict_Dict
    {
        public EntDocument? Document { get; set; }

        private List<RevitLink_Dict>? items;
        public List<RevitLink_Dict> Items => items ??= new List<RevitLink_Dict>();

        public RevitLink_Dict this[ElementId viewId] => this.GetLinkDict(viewId);
    }
}
