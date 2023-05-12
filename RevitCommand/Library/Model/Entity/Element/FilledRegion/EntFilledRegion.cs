using Autodesk.Revit.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntFilledRegion : EntElement
    {
        public override EntTransform EntTransform => entTransform ??= this.GetEntTransform();
    }
}
