using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;  

namespace Model.Entity
{
    public class EntSolidGroup
    {
        private IEnumerable<EntSolid>? entSolids;
        public IEnumerable<EntSolid>? EntSolids
        {
            get=> entSolids;
            set=>entSolids = value;
        }

        private IEnumerable<Solid>? solids;
        public IEnumerable<Solid>? Solids
        {
            get=>solids;
            set=>solids = value;
        }

        private List<EntSolidInGroup>? entSolidInGroups;
        public List<EntSolidInGroup> EntSolidInGroups => entSolidInGroups ??= this.GetEntSolidInGroups();

        public EntSolidInGroup this[string index]=> EntSolidInGroups.SingleOrDefault(x => x.Index == index);

        private List<EntSolidInGroup>? resultEntSolids;
        public List<EntSolidInGroup> ResultEntSolids => resultEntSolids ??= this.GetResultEntSolids();
    }
}
