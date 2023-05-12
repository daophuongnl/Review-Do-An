using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class LineIntersectLineResult
    {
        public Line? Line1 { get; set; }
        public Line? Line2 { get; set; }
        
        private Tuple<LineIntersectLineResultType, XYZ>? resultObjects;
        public Tuple<LineIntersectLineResultType, XYZ> ResultObjects
        {
            get => resultObjects ??= this.GetResultObjects();
        }

        private XYZ? intersectPoint;
        public XYZ IntersectPoint
        {
            get => intersectPoint ??= this.GetIntersectPoint();
            set => intersectPoint = value;
        }

        private LineIntersectLineResultType? resultType;
        public LineIntersectLineResultType ResultType
        {
            get
            {
                if (resultType == null) resultType = this.GetResultType();
                return resultType.Value;
            }
        }

        private Tuple<LineIntersectLineResultCoincidentType, XYZ?, Line?>? coincidentType_Point_Line;
        public Tuple<LineIntersectLineResultCoincidentType, XYZ?, Line?> CoincidentType_Point_Line => coincidentType_Point_Line ??= this.GetCoincidentType_Point_Line();

        private LineIntersectLineResultCoincidentType? coincidentType;
        public LineIntersectLineResultCoincidentType CoincidentType => coincidentType ??= this.GetCoincidentType();

        private XYZ? coincidentPoint;
        public XYZ? CoincidentPoint => coincidentPoint ??= this.GetCoincidentPoint();

        private Line? coincidentLine;
        public Line? CoincidentLine => coincidentLine ??= this.GetCoincidentLine();

        public List<Line>? RemainderLines1 { get; set; }

        public List<Line>? RemainderLines2 { get; set; }
    }
}
