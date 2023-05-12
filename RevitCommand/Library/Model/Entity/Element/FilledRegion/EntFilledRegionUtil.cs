using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntFilledRegionUtil
    {
        public static EntTransform GetEntTransform(this EntFilledRegion q)
        {
            var edges = q.EntSolid.Solid.Edges;

            var tf = new EntTransform();
            var isUnsetOrgin = true;
            var isUnsetBasisY = true;

            foreach (Edge edge in edges)
            {
                if (isUnsetOrgin || isUnsetBasisY)
                {
                    var line = (edge.AsCurve() as Line)!;

                    if (isUnsetOrgin)
                    {
                        tf.Origin = line.GetEndPoint(0);
                        tf.BasisX = line.Direction;
                        tf.LengthX = line.Length;
                        isUnsetOrgin = false;
                    }
                    else if (isUnsetBasisY)
                    {
                        tf.BasisY = line.Direction;
                        tf.LengthY = line.Length;
                        isUnsetBasisY = false;
                    }
                }
            }

            return tf.Translate(tf.BasisX * tf.LengthX / 2 + tf.BasisY * tf.LengthY / 2);
        }
    }
}
