using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntLocationCurve : EntLocation
    {
        private Curve? curve;
        public Curve Curve => curve ??= this.GetCurve();

        private XYZ? startPoint;
        public XYZ StartPoint => startPoint ??= this.GetStartPoint();

        private XYZ? endPoint;
        public XYZ EndPoint => endPoint ??= this.GetEndPoint();

        private XYZ? direction;
        public XYZ Direction => direction ??= this.GetDirection();

        public override XYZ PurgeOffset => purgeOffset ??= this.GetPurgeOffset();
    }
}
