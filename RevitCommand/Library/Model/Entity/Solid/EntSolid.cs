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
    public class EntSolid
    {
        protected Solid? solid;
        public virtual Solid Solid
        {
            get =>solid!;
            set =>solid = value;
        }

        private List<Solid>? splitSolids;
        public List<Solid> SplitSolids => splitSolids ??= this.GetSplitSolids();

        private EntBoundingBoxXYZ? entBoundingBoxXYZ;
        public EntBoundingBoxXYZ EntBoundingBoxXYZ => entBoundingBoxXYZ ??= this.GetEntBoundingBoxXYZ();

        private BoundingBoxXYZ? boundingBoxXYZ;
        public BoundingBoxXYZ BoundingBoxXYZ => boundingBoxXYZ ??= this.GetBoundingBoxXYZ();

        protected Transform? purgeTransform;
        public virtual Transform? PurgeTransform
        {
            get => purgeTransform ??= Transform.Identity;
            set => purgeTransform = value;
        }

        private Solid? purgeSolid;
        public Solid? PurgeSolid => purgeSolid ??= this.GetPurgeSolid();

        private bool? canPurgeSolid;
        public bool CanPurgeSolid
        {
            get
            {
                if (canPurgeSolid == null) canPurgeSolid = this.GetCanPurgeSolid();
                return canPurgeSolid.Value;
            }
            set
            {
                canPurgeSolid = value;
            }
        }
    }
}
