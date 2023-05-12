using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class LineIntersectLineResultUtil
    {
        public static LineIntersectLineResult GetLineIntersectLineResult(this Line line1, Line line2)
        {
            var result = new LineIntersectLineResult
            {
                Line1 = line1,
                Line2 = line2
            };
            return result;
        }

        #region Property

        public static Tuple<LineIntersectLineResultType, XYZ> GetResultObjects(this LineIntersectLineResult result)
        {
            var line1 = result.Line1!;
            var line2 = result.Line2!;

            var pnt1 = line1.GetEndPoint(0);
            var pnt2 = line2.GetEndPoint(0);

            var resultType = LineIntersectLineResultType.Coincident;
            XYZ? intersectPnt = null;

            var vec1 = -line1.Direction;
            var vec2 = line2.Direction;
            var vecAB = pnt2 - pnt1;

            if (vec1.IsParallel(vec2))
            {
                if (vecAB.IsParallel(vec1) || vecAB.IsEqual(XYZ.Zero))
                {
                    resultType = LineIntersectLineResultType.Coincident;
                }
                else
                {
                    resultType = LineIntersectLineResultType.Parallel;
                }
            }
            else
            {
                var vec1Xvec2 = vec1.CrossProduct(vec2);
                if (!vec1Xvec2.DotProduct(vecAB).IsEqual(0))
                {
                    resultType = LineIntersectLineResultType.Noncoplanar;
                }
                else
                {
                    resultType = LineIntersectLineResultType.Intersect;

                    var len_vec1Xvec2 = vec1Xvec2.GetLength();
                    var a = (vecAB.CrossProduct(vec2)).DotProduct(vec1Xvec2) / (len_vec1Xvec2 * len_vec1Xvec2);

                    intersectPnt = pnt1 + vec1 * a;
                }
            }

            return Tuple.Create(resultType, intersectPnt!);
        }

        public static LineIntersectLineResultType GetResultType(this LineIntersectLineResult result)
        {
            return result.ResultObjects.Item1;
        }

        public static XYZ GetIntersectPoint(this LineIntersectLineResult result)
        {
            return result.ResultObjects.Item2;
        }

        public static Tuple<LineIntersectLineResultCoincidentType, XYZ?, Line?> GetCoincidentType_Point_Line(this LineIntersectLineResult result)
        {
            var coincidentType = LineIntersectLineResultCoincidentType.NonIntersect;
            XYZ? coincidentPoint = null;
            Line? coincidentLine = null;

            var line1 = result.Line1!;
            var line2 = result.Line2!;

            var dir1 = line1.Direction;
            var dir2 = line2.Direction;

            XYZ pntA0 = line1.GetEndPoint(0);
            XYZ pntA1 = line1.GetEndPoint(1);

            XYZ pntB0 = line2.GetEndPoint(0);
            XYZ pntB1 = line2.GetEndPoint(1);

            double x1 = 0;
            var x2 = line1.Length;

            var vecA0B0 = pntB0 - pntA0;
            var multiFactor1 = vecA0B0.DotProduct(dir1) > 0 ? 1 : -1;
            var y1 = vecA0B0.GetLength() * multiFactor1;

            var vecA0B1 = pntB1 - pntA0;
            var multiFactor2 = vecA0B1.DotProduct(dir1) > 0 ? 1 : -1;
            var y2 = vecA0B1.GetLength() * multiFactor2;
            if (y1 > y2)
            {
                var temp = y1;
                y1 = y2;
                y2 = temp;
            }

            var isCoincidentLine = false;
            double firstValue = 0;
            double secondValue = 0;

            List<List<double>> remainderValues1 = new List<List<double>>();
            List<List<double>> remainderValues2 = new List<List<double>>();

            var isCoincidentPoint = false;
            double pointValue = 0;

            if (x1.IsEqual(y1) && x2.IsEqual(y2))
            {
                coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                isCoincidentLine = true;
                coincidentLine = line1;
            }
            else if (x2.IsEqual(y1) && x1.IsEqual(y2))
            {
                coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                isCoincidentLine = true;
                coincidentLine = line1;
            }
            else
            {
                if (y1.IsBigger(x1) && y1.IsSmaller(x2))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                    isCoincidentLine = true;
                    firstValue = y1;

                    if (y2.IsSmaller(x2))
                    {
                        secondValue = y2;
                        remainderValues1.Add(new List<double> { x1, y1 });
                        remainderValues1.Add(new List<double> { y2, x2 });
                    }
                    else if (y2.IsEqual(x2))
                    {
                        secondValue = x2;
                        remainderValues1.Add(new List<double> { x1, y1 });
                    }
                    else
                    {
                        secondValue = x2;
                        remainderValues1.Add(new List<double> { x1, y1 });
                        remainderValues2.Add(new List<double> { x2, y2 });
                    }
                }
                else if (y2.IsBigger(x1) && y2.IsSmaller(x2))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                    isCoincidentLine = true;
                    secondValue = y2;

                    if (y1.IsSmaller(x1))
                    {
                        firstValue = x1;
                        remainderValues1.Add(new List<double> { y2, x2 });
                        remainderValues2.Add(new List<double> { y1, x1 });
                    }
                    // y1 == x1
                    else
                    {
                        firstValue = y1;
                        remainderValues1.Add(new List<double> { y2, x2 });
                    }
                }
                else if (x1.IsBigger(y1) && x1.IsSmaller(y2))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                    isCoincidentLine = true;
                    firstValue = x1;

                    if (x2.IsSmaller(y2))
                    {
                        secondValue = x2;
                        remainderValues2.Add(new List<double> { y1, x1 });
                        remainderValues2.Add(new List<double> { x2, y2 });
                    }
                    // x2 = y2
                    else
                    {
                        secondValue = y2;
                        remainderValues2.Add(new List<double> { y1, x1 });
                    }
                }
                else if (x2.IsBigger(y1) && x2.IsSmaller(y2))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.LineOverlap;
                    isCoincidentLine = true;
                    secondValue = x2;

                    if (x1.IsEqual(y1))
                    {
                        firstValue = y1;
                        remainderValues2.Add(new List<double> { x2, y2 });
                    }
                }
                else if (x2.IsEqual(y1))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.PointIntersect;
                    isCoincidentPoint = true;
                    pointValue = x2;
                }
                else if (x1.IsEqual(y2))
                {
                    coincidentType = LineIntersectLineResultCoincidentType.PointIntersect;
                    isCoincidentPoint = true;
                    pointValue = x1;
                }
            }

            if (isCoincidentLine)
            {
                if (coincidentLine != null)
                {
                    result.RemainderLines1 = new List<Line>();
                    result.RemainderLines2 = new List<Line>();
                }
                else
                {
                    var firstPnt = pntA0 + firstValue * dir1;
                    var secondPnt = pntA0 + secondValue * dir1;

                    try
                    {
                        coincidentLine = Line.CreateBound(firstPnt, secondPnt);

                        var remainderLines1 = result.RemainderLines1 = new List<Line>();
                        foreach (var rvs in remainderValues1)
                        {
                            var fp = pntA0 + rvs[0] * dir1;
                            var sp = pntA0 + rvs[1] * dir1;

                            try
                            {
                                remainderLines1.Add(Line.CreateBound(fp, sp));
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        var remainderLines2 = result.RemainderLines2 = new List<Line>();
                        foreach (var rvs in remainderValues2)
                        {
                            var fp = pntA0 + rvs[0] * dir1;
                            var sp = pntA0 + rvs[1] * dir1;

                            try
                            {
                                remainderLines2.Add(Line.CreateBound(fp, sp));
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    catch
                    {
                        coincidentType = LineIntersectLineResultCoincidentType.PointIntersect;
                        coincidentPoint = firstPnt;
                    }
                }
            }

            if (isCoincidentPoint)
            {
                coincidentPoint = pntA0 + pointValue * dir1;
            }

            return Tuple.Create(coincidentType, coincidentPoint, coincidentLine);
        }

        public static LineIntersectLineResultCoincidentType GetCoincidentType(this LineIntersectLineResult result)
        {
            return result.CoincidentType_Point_Line.Item1;
        }

        public static XYZ? GetCoincidentPoint(this LineIntersectLineResult result)
        {
            return result.CoincidentType_Point_Line.Item2;
        }

        public static Line? GetCoincidentLine(this LineIntersectLineResult result)
        {
            return result.CoincidentType_Point_Line.Item3;
        }

        #endregion
    }
}
