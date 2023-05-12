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
    public class EntBoundingBoxXYZ
    {
        public BoundingBoxXYZ? RevitBoundingBoxXYZ { get; set; }

        private XYZ? minPoint;
        public XYZ MinPoint => minPoint ??= this.GetMinPoint();

        private XYZ? maxPoint;
        public XYZ MaxPoint => maxPoint ??= this.GetMaxPoint();

        private double? width;
        public double Width => width ??= this.GetWidth();

        private double? height;
        public double Height => height ??= this.GetHeight();

        private double? length;
        public double Length => length ??= this.GetLength();
    }
}
