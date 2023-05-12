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
    public class SolidFamily : EntComponentFamilyForCreate
    {
        private IEnumerable<Solid>? inputSolids;
        public IEnumerable<Solid>? InputSolids
        {
            get => inputSolids;
            set => inputSolids = value;
        }

        private bool isMerged;
        public bool IsMerged
        {
            get => isMerged;
            set => isMerged = value;
        }

        private Solid? singleSoid;
        public Solid? SingleSolid
        {
            get => singleSoid;
            set => singleSoid = value;
        }

        public override string Name
        {
            get => name ??= this.GetName();
        }

        public override Action<EntComponentFamilyForCreate> EntComponentFamilyForCreate_Action
        {
            get => entComponentFamilyForCreate_Action ??= x => this.GetEntComponentFamilyForCreate_Action();
        }
    }
}
