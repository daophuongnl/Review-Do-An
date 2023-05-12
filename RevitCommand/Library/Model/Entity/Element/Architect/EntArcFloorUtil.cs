using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntArcFloorUtil
    {
        public static Level? GetLevel(this EntArcFloor floor)
        {
            return LevelUtil.GetForFloor(floor);
        }

        public static Face GetBottomFace(this EntArcFloor floor)
        {
            return HostObjectUtils.GetBottomFaces(floor.RevitElement as Floor)[0].GetGeometryObject<Face>();
        }

        public static Face GetTopFace(this EntArcFloor floor)
        {
            return HostObjectUtils.GetTopFaces(floor.RevitElement as Floor)[0].GetGeometryObject<Face>();
        }

        public static EntTransform GetEntTransform(this EntArcFloor q)
        {
            var topFace = (q.TopFace as PlanarFace)!;

            var tf = new EntTransform();

            //tf.BasisX = topFace.XVector;
            //tf.BasisY = topFace.YVector;
            tf.BasisX = XYZ.BasisX;
            tf.BasisY = XYZ.BasisY;
            tf.BasisZ = topFace.FaceNormal;

            var bb = q.BoundingBoxXYZ;
            var max = bb.Max;
            var min = bb.Min;

            tf.Origin = (min + max) * 0.5;
            tf.LengthX = max.X - min.X;
            tf.LengthY = max.Y - min.Y;
            tf.LengthZ = max.Z - min.Z;

            return tf;
        }
    }
}
