using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class BeamEntSolidUtil
    {
        private static RevitData revitData => RevitData.Instance;

        #region Property

        //public static Solid ModifyFamilySolid(this BeamEntSolid beamEntSolid, Solid familySolid)
        //{
        //    var entBeam = beamEntSolid.EntElement as EntBeam;

        //    var entLocCurve = entBeam.EntLocation as EntLocationCurve;
        //    var dir = entLocCurve.Direction;

        //    PlanarFace pf = null;
        //    foreach (Face f in familySolid.Faces)
        //    {
        //        if (f is PlanarFace)
        //        {
        //            pf = f as PlanarFace;
        //            if (pf.FaceNormal.IsOppositeDirection(dir))
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    if (pf == null)
        //    {
        //        throw new Exception("This case haven't been checked yet!");
        //    }

        //    var minPnt = entLocCurve.StartPoint;
        //    var maxPnt = entLocCurve.EndPoint;

        //    var minDis = pf.GetDistance(minPnt);
        //    var maxDis = pf.GetDistance(maxPnt);

        //    var CSs = pf.GetEntFace().CreateTransformed(-dir * minDis);

        //    var extrusDist = maxDis - minDis;
        //    dir *= -1;

        //    if (extrusDist < 0)
        //    {
        //        extrusDist *= -1;
        //        dir *= -1;
        //    }

        //    Solid solid = null;
        //    try
        //    {
        //        solid = GeometryCreationUtilities.CreateExtrusionGeometry(CSs, dir, extrusDist);
        //    }
        //    catch
        //    {
        //        revitData.Selection.SetElement(Line.CreateBound(minPnt, maxPnt).CreateModel());
        //        throw;
        //    }
        //    return solid;
        //}

        #endregion
    }
}
