using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntLocationPoint : EntLocation
    {
        private XYZ? centerPoint;
        public XYZ CenterPoint => centerPoint ??= this.GetCenterPoint();

        public override XYZ PurgeOffset => purgeOffset ??= this.GetPurgeOffset();
    }
}
