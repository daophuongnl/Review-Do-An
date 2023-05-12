using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static partial class CurveUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static ViewData viewData => ViewData.Instance;

        public static bool IsLinesOverlapOrExtend(this Autodesk.Revit.DB.Line line1, Autodesk.Revit.DB.Line line2)
        {
            var dir1 = line1.Direction;
            var dir2 = line2.Direction;
            if (!dir1.IsParallel(dir2)) return false;

            var vec1 = line2.GetEndPoint(1) - line1.GetEndPoint(0);
            if (!vec1.IsParallel(dir1)) return false;

            return true;
        }

        public static Autodesk.Revit.DB.Line? LineOverlap(this Autodesk.Revit.DB.Line line1, Autodesk.Revit.DB.Line line2)
        {
            if (!line1.IsLinesOverlapOrExtend(line2)) return null;

            var dir1 = line1.Direction;
            var dir2 = line2.Direction;

            var pnt00 = line1.GetEndPoint(0);
            var pnt01 = line1.GetEndPoint(1);
            var pnt10 = line2.GetEndPoint(0);
            var pnt11 = line2.GetEndPoint(1);

            if (dir1.IsSameDirection(dir2))
            {
                var vec00 = pnt10 - pnt00;
                if (vec00.IsSameDirection(dir1))
                {
                    var vec10 = pnt10 - pnt01;
                    if (vec10.IsOppositeDirection(dir1)) return Autodesk.Revit.DB.Line.CreateBound(pnt00, pnt11);
                    return null;
                }
                else
                {
                    var vec01 = pnt11 - pnt00;
                    if (vec01.IsSameDirection(dir1)) return Autodesk.Revit.DB.Line.CreateBound(pnt01, pnt10);
                    return null;
                }
            }
            else
            {
                var vec01 = pnt11 - pnt00;
                if (vec01.IsSameDirection(dir1))
                {
                    var vec11 = pnt11 - vec01;
                    if (vec11.IsOppositeDirection(dir1)) return Autodesk.Revit.DB.Line.CreateBound(pnt00, pnt10);
                    return null;
                }
                else
                {
                    var vec00 = pnt10 - pnt00;
                    if (vec00.IsSameDirection(dir1)) return Autodesk.Revit.DB.Line.CreateBound(pnt01, pnt11);
                    return null;
                }
            }
        }

        public static Autodesk.Revit.DB.Line ChangeDirection(this Autodesk.Revit.DB.Line line)
        {
            return Autodesk.Revit.DB.Line.CreateBound(line.GetEndPoint(1), line.GetEndPoint(0));
        }

        public static IEnumerable<Autodesk.Revit.DB.Curve> ChangeDirections(this IEnumerable<Autodesk.Revit.DB.Curve> curves)
        {
            return curves.Reverse().Select(x => (x as Autodesk.Revit.DB.Line)!.ChangeDirection());
        }

        public static void EvaluateCurveOn2D(this IEnumerable<Autodesk.Revit.DB.Curve> curves)
        {
            var doc = viewData.Document;
            var view = viewData.ActiveView;
            var sel = revitData.Selection;

            var workPlane = viewData.WorkPlane;

            var pnt = sel.PickPoint();
            Autodesk.Revit.DB.XYZ? vec = null;
            var vecX = view.RightDirection;
            var vecY = view.UpDirection;
            Autodesk.Revit.DB.XYZ? orgPnt = null;
            Autodesk.Revit.DB.Transform? transform = null;

            foreach (var item in curves)
            {
                if (vec == null)
                {
                    orgPnt = item.GetEndPoint(0);
                    vec = pnt - orgPnt;
                    transform = Autodesk.Revit.DB.Transform.CreateTranslation(vec);
                }

                doc.Create.NewDetailCurve(view, item.CreateTransformed(transform));
            }

        }

        public static Autodesk.Revit.DB.Line Stretch(this Autodesk.Revit.DB.Line line, double newLength)
        {
            var startPnt = line.GetEndPoint(0);
            var dir = line.Direction;

            return Autodesk.Revit.DB.Line.CreateBound(startPnt, startPnt + dir * newLength);
        }

        public static Autodesk.Revit.DB.Arc Scale(this Autodesk.Revit.DB.Arc arc, double newRadius)
        {
            var radius = arc.Radius;
            var scaleTf = Autodesk.Revit.DB.Transform.Identity.ScaleBasis(newRadius / radius);

            return (arc.CreateTransformed(scaleTf) as Autodesk.Revit.DB.Arc)!;
        }

        public static Autodesk.Revit.DB.Arc ScaleAndMove2FirstPoint(this Autodesk.Revit.DB.Arc arc, double newRadius)
        {
            var firstPnt = arc.GetEndPoint(0);
            var scaleArc = arc.Scale(newRadius);
            var translateTf = Autodesk.Revit.DB.Transform.CreateTranslation(firstPnt - scaleArc.GetEndPoint(0));
            return (scaleArc.CreateTransformed(translateTf) as Autodesk.Revit.DB.Arc)!;
        }

        public static List<Autodesk.Revit.DB.XYZ> GetPoints(this IEnumerable<Autodesk.Revit.DB.Curve> curves)
        {
            var pnts = curves.Select(x => x.GetEndPoint(0)).ToList();
            pnts.Add(curves.Last().GetEndPoint(1));

            return pnts;
        }

        public static Autodesk.Revit.DB.CurveArray CreateCurveArray(this IEnumerable<Autodesk.Revit.DB.XYZ> pnts)
        {
            var curArr = new Autodesk.Revit.DB.CurveArray();

            Autodesk.Revit.DB.Line? line = null;
            Autodesk.Revit.DB.XYZ? pnt1 = null;
            Autodesk.Revit.DB.XYZ? pnt2 = null;
            foreach (var pnt in pnts)
            {
                if (pnt1 == null)
                {
                    pnt1 = pnt;
                    continue;
                }

                pnt2 = pnt;
                line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
                curArr.Append(line);
                pnt1 = pnt;
            }
            pnt2 = pnts.First();
            line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
            curArr.Append(line);

            return curArr;
        }

        public static Autodesk.Revit.DB.CurveArray CreateCurveArray(params Autodesk.Revit.DB.XYZ[] pnts)
        {
            var curArr = new Autodesk.Revit.DB.CurveArray();

            Autodesk.Revit.DB.Line? line = null;
            Autodesk.Revit.DB.XYZ? pnt1 = null;
            Autodesk.Revit.DB.XYZ? pnt2 = null;
            foreach (var pnt in pnts)
            {
                if (pnt1 == null)
                {
                    pnt1 = pnt;
                    continue;
                }

                pnt2 = pnt;
                line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
                curArr.Append(line);
                pnt1 = pnt;
            }
            pnt2 = pnts.First();
            line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
            curArr.Append(line);

            return curArr;
        }

        public static List<Autodesk.Revit.DB.Curve> CreateCurves(this IEnumerable<Autodesk.Revit.DB.XYZ> pnts, bool isClosed = true)
        {
            var curves = new List<Autodesk.Revit.DB.Curve>();

            Autodesk.Revit.DB.Line? line = null;
            Autodesk.Revit.DB.XYZ? pnt1 = null;
            Autodesk.Revit.DB.XYZ? pnt2 = null;
            foreach (var pnt in pnts)
            {
                if (pnt1 == null)
                {
                    pnt1 = pnt;
                    continue;
                }

                pnt2 = pnt;
                line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
                curves.Add(line);
                pnt1 = pnt;
            }

            if (isClosed)
            {
                pnt2 = pnts.First();
                line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
                curves.Add(line);
            }

            return curves;
        }

        public static List<Autodesk.Revit.DB.Curve> CreateCurves(params Autodesk.Revit.DB.XYZ[] pnts)
        {
            var curves = new List<Autodesk.Revit.DB.Curve>();

            Autodesk.Revit.DB.Line? line = null;
            Autodesk.Revit.DB.XYZ? pnt1 = null;
            Autodesk.Revit.DB.XYZ? pnt2 = null;
            foreach (var pnt in pnts)
            {
                if (pnt1 == null)
                {
                    pnt1 = pnt;
                    continue;
                }

                pnt2 = pnt;
                line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
                curves.Add(line);
                pnt1 = pnt;
            }
            pnt2 = pnts.First();
            line = Autodesk.Revit.DB.Line.CreateBound(pnt1, pnt2);
            curves.Add(line);

            return curves;
        }

        public static CurveArray GetCurveArray(this CurveLoop curveloop)
        {
            var ca = new CurveArray();
            foreach (var c in curveloop)
            {
                ca.Append(c);
            }
            return ca;
        }

        public static CurveLoop GetCurveLoop(this List<Curve> curves)
        {
            return CurveLoop.Create(curves);
        }

        public static List<Curve> GetCurves(this CurveLoop curveloop)
        {
            var cs = new List<Curve>();
            foreach (var c in curveloop)
            {
                cs.Add(c);
            }
            return cs;
        }

        public static XYZ GetCenterPoint(this Curve curve)
        {
            var p1 = curve.GetEndPoint(0);
            var p2 = curve.GetEndPoint(1);
            return (p1 + p2) * 0.5;
        }
    }
}
