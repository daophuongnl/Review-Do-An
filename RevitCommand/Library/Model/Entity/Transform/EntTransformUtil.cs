using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntTransformUtil
    {
        private static StructuralData structuralData => StructuralData.Instance;
        private static RevitData revitData => RevitData.Instance;

        public static EntTransform Clone(this EntTransform entTransform)
        {
            var newEntTf = new EntTransform
            {
                Origin = entTransform.Origin,
                BasisX = entTransform.BasisX,
                BasisY = entTransform.BasisY,
                BasisZ = entTransform.BasisZ,
                LengthX = entTransform.LengthX,
                LengthY = entTransform.LengthY,
                LengthZ = entTransform.LengthZ
            };

            return newEntTf;
        }

        // Property
        public static Transform GetTransform(this EntTransform entTransform)
        {
            var tf = Transform.Identity;

            tf.Origin = entTransform.Origin;
            tf.BasisX = entTransform.BasisX;
            tf.BasisY = entTransform.BasisY;
            tf.BasisZ = entTransform.BasisZ;

            return tf;
        }

        public static Transform GetInverse(this EntTransform entTransform)
        {
            return entTransform.Transform.Inverse;
        }

        public static XYZ GetBasisX(this EntTransform entTransform)
        {
            return entTransform.BasisY.CrossProduct(entTransform.BasisZ);
        }

        public static XYZ GetBasisY(this EntTransform entTransform)
        {
            return entTransform.BasisZ.CrossProduct(entTransform.BasisX);
        }

        public static XYZ GetBasisZ(this EntTransform entTransform)
        {
            return entTransform.BasisX.CrossProduct(entTransform.BasisY);
        }

        // method

        public static EntTransform Translate(this EntTransform q, XYZ vec)
        {
            var qI = q.Clone();
            qI.Origin = q.Origin + vec;
            return qI;
        }
    }
}
