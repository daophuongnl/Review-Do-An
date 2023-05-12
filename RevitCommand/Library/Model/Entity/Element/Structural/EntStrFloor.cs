using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntStrFloor : StructuralElement
    {
        public override string ElementName => elementName ??= this.GetElementName();

        public override Level? Level => level ??= this.GetLevel();

        private Face? bottomFace;
        public Face BottomFace => bottomFace ??= this.GetBottomFace();

        private Face? topFace;
        public Face TopFace => topFace ??= this.GetTopFace();

        public override EntTransform EntTransform=> entTransform ??= this.GetEntTransform();
    }
}
