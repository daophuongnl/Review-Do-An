using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace Model.Entity
{
    public class EntTransform
    {
        public XYZ Origin { get; set; } = XYZ.Zero;

        public XYZ BasisX { get; set; } = XYZ.BasisX;

        public XYZ BasisY { get; set; } = XYZ.BasisY;

        public XYZ BasisZ { get; set; } = XYZ.BasisZ;

        private Transform? transform;
        public Transform Transform => this.transform ??= this.GetTransform();

        private Transform? inverse;
        public Transform Inverse => inverse ??= this.GetInverse();

        protected double lengthX = -1;
        public virtual double LengthX
        {
            get => lengthX;
            set => lengthX = value;
        }


        protected double lengthY = -1;
        public virtual double LengthY
        {
            get => lengthY;
            set => lengthY = value;
        }

        protected double lengthZ = -1;
        public virtual double LengthZ
        {
            get => lengthZ;
            set => lengthZ = value;
        }
    }
}
