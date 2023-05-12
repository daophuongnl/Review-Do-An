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
    public class ElementEntSolid : EntSolid
    {
        public EntElement? EntElement { get; set; }

        private int? elementId;
        public int? ElementId => elementId ??= EntElement?.ElementId;

        private Solid? originTransformFamilySolid;
        public Solid OriginTransformFamilySolid => originTransformFamilySolid ??= this.GetOriginTransformFamilySolid();

        private Solid? familySolid;
        public Solid FamilySolid => familySolid ??= this.GetFamilySolid();

        private Solid? realSolid;
        public Solid RealSolid => realSolid ??= this.GetRealSolid();

        protected Func<Solid, Solid>? modifyFamilySolidFunc;
        public virtual Func<Solid, Solid>? ModifyFamilySolidFunc=> modifyFamilySolidFunc;

        public override Solid Solid => solid ??= this.GetSolid();

        public bool IsGetFamilySolid { get; set; }

        public override Transform? PurgeTransform => purgeTransform ??= this.GetPurgeTransform();
    }
}
