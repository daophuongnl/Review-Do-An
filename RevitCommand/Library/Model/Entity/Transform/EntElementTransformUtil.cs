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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utility
{
    public static class EntElementTransformUtil
    {
        private static StructuralData structuralData => StructuralData.Instance;
        private static RevitData revitData => RevitData.Instance;

        public static EntElementTransform GetEntElementTransform(this EntElement entElement)
        {
            var entTf = new EntElementTransform
            {
                Origin = XYZ.Zero,
                BasisX = XYZ.BasisX,
                BasisY = XYZ.BasisY,
                BasisZ = XYZ.BasisZ
            };
            entTf.EntElement = entElement;

            return entTf;
        }

        //public static EntElementTransform GetEntTransform(this EntColumn entColumn)
        //{
        //    var tf = (entColumn.RevitElement as FamilyInstance).GetTransform();
        //    var origin = tf.Origin;
        //    var bottomZ = entColumn.BoundingBoxXYZ.Min.Z;
        //    var length = entColumn.EntGeometry.Length;
        //    origin = new XYZ(origin.X, origin.Y, bottomZ + length/2);

        //    var entTf = new EntElementTransform
        //    {
        //        Origin = origin,
        //        BasisX = tf.BasisX,
        //        BasisY = tf.BasisY,
        //        BasisZ = tf.BasisZ
        //    };
        //    entTf.EntElement = entColumn;

        //    return entTf;
        //}

        //public static EntElementTransform GetEntTransform(this EntBeam entBeam)
        //{
        //    var tf = (entBeam.RevitElement as FamilyInstance).GetTransform();

        //    var entTf = new EntElementTransform
        //    {
        //        Origin = tf.Origin,
        //        BasisX = tf.BasisY,
        //        BasisY = tf.BasisZ,
        //        BasisZ = tf.BasisX
        //    };
        //    entTf.EntElement = entBeam;

        //    return entTf;
        //}

        //public static EntElementTransform GetEntTransform(this EntStrWall entStrWall)
        //{
        //    var entLocCurve = entStrWall.EntLocation as EntLocationCurve;
        //    var centerLine = entLocCurve.Curve as Line;
        //    var entGeo = (entStrWall.EntGeometry as EntWHLGeometry);
        //    var height = entGeo.Height;
        //    var length = entGeo.Length;
        //    var dir = centerLine.Direction;
        //    var basisZ = XYZ.BasisZ;
        //    var basisX = dir.CrossProduct(basisZ).Normalize();

        //    var startPnt = centerLine.GetEndPoint(0);
        //    var offZ = entStrWall.RevitElement.ParameterAsDouble("Top Offset");

        //    var centerPnt = startPnt + dir * height / 2 + basisZ * (length/2 + offZ);

        //    var entTf = new EntElementTransform
        //    {
        //        Origin = centerPnt,
        //        BasisX = basisX,
        //        BasisY = dir,
        //        BasisZ = basisZ
        //    };
        //    entTf.EntElement = entStrWall;

        //    return entTf;
        //}

        #region Property

        //public static double GetLengthX(this EntElementTransform entTf)
        //{
        //    var geometry = entTf.EntElement.EntGeometry;
        //    if (geometry is EntTAGeometry)
        //    {
        //        throw new Exception("This case haven't been checked yet!");
        //    }
        //    var entWHLGeo = geometry as EntWHLGeometry;
        //    return entWHLGeo.Width;
        //}

        //public static double GetLengthY(this EntElementTransform entTf)
        //{
        //    var geometry = entTf.EntElement.EntGeometry;
        //    if (geometry is EntTAGeometry)
        //    {
        //        throw new Exception("This case haven't been checked yet!");
        //    }
        //    var entWHLGeo = geometry as EntWHLGeometry;
        //    return entWHLGeo.Height;
        //}

        //public static double GetLengthZ(this EntElementTransform entTf)
        //{
        //    var geometry = entTf.EntElement.EntGeometry;
        //    if (geometry is EntTAGeometry)
        //    {
        //        throw new Exception("This case haven't been checked yet!");
        //    }
        //    var entWHLGeo = geometry as EntWHLGeometry;
        //    return entWHLGeo.Length;
        //}

        #endregion

        #region Method

        #endregion
    }
}
