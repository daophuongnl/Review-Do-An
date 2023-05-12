using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Model.Entity;

namespace Utility
{
    public static partial class RebarExportUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static StructuralData structuralData => StructuralData.Instance;
        private static RebarData rebarData => RebarData.Instance;

        public static RebarExport GetRebarExport(this Autodesk.Revit.DB.Structure.Rebar rebar)
        {
            return new RebarExport
            {
                RevitRebar = rebar,
                SingleRebar = rebar
            };
        }

        public static RebarExport GetRebarExport(this Autodesk.Revit.DB.Structure.RebarInSystem rebarInSystem)
        {
            return new RebarExport
            {
                SystemRebar = rebarInSystem
            };
        }

        #region Rebar Property
        public static Autodesk.Revit.DB.Element GetHostElement(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null) return rebar.SingleRebar.GetHostId().GetElement();
            return rebar.SystemRebar!.GetHostId().GetElement();
        }

        public static Autodesk.Revit.DB.Structure.RebarShapeDrivenAccessor? GetRebarShapeDrivenAccessor
            (this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.SingleRebar.GetShapeDrivenAccessor();
            }
            return null;
        }

        public static Autodesk.Revit.DB.Structure.RebarShape GetRebarShape(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return (rebar.SingleRebar.GetShapeId().GetElement()
                    as Autodesk.Revit.DB.Structure.RebarShape)!;
            }
            else
            {
                return (rebar.SystemRebar!.RebarShapeId.GetElement()
                    as Autodesk.Revit.DB.Structure.RebarShape)!;
            }
        }

        public static Autodesk.Revit.DB.Structure.RebarBarType GetBarType(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return (rebar.SingleRebar.GetTypeId().GetElement()
                    as Autodesk.Revit.DB.Structure.RebarBarType)!;
            }
            else
            {
                return (rebar.SystemRebar!.GetTypeId().GetElement()
                    as Autodesk.Revit.DB.Structure.RebarBarType)!;
            }
        }

        public static Autodesk.Revit.DB.Structure.RebarBendData GetRebarBendData(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.SingleRebar.GetBendData();
            }
            else
            {
                return rebar.SystemRebar!.GetBendData();
            }
        }

        public static Autodesk.Revit.DB.BoundingBoxXYZ GetBoundingBoxXYZ(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.SingleRebar.get_BoundingBox(null);
            }
            return rebar.SystemRebar!.get_BoundingBox(null);
        }

        public static Autodesk.Revit.DB.Plane GetPlane(this RebarExport rebar)
        {
            var firstPnt = rebar.CenterCurves.First().GetEndPoint(0);
            return Autodesk.Revit.DB.Plane.CreateByNormalAndOrigin(firstPnt, rebar.DistributionDirection);
        }

        public static Autodesk.Revit.DB.Line GetDistributionPath(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.RebarShapeDrivenAccessor.GetDistributionPath();
            }
            else
            {
                return rebar.SystemRebar!.GetDistributionPath();
            }
        }

        public static Autodesk.Revit.DB.XYZ GetDistributionDirection(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.RebarShapeDrivenAccessor.Normal;
            }
            else
            {
                return rebar.SystemRebar!.Normal;
            }
        }

        public static Autodesk.Revit.DB.Line GetMainPath(this RebarExport rebar)
        {
            double? maxLength = null;
            Autodesk.Revit.DB.Line? mainLine = null;
            foreach (var curve in rebar.CenterCurves)
            {
                if (maxLength == null || curve.Length.IsBigger(maxLength.Value))
                {
                    maxLength = curve.Length;

                    if (curve is Autodesk.Revit.DB.Line)
                    {
                        mainLine = curve as Autodesk.Revit.DB.Line;
                    }
                    else if (curve is Autodesk.Revit.DB.Arc)
                    {
                        mainLine = Autodesk.Revit.DB.Line.CreateBound(curve.GetEndPoint(0), curve.GetEndPoint(1));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
#pragma warning disable CS8603 // Possible null reference return.
            return mainLine;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static List<Autodesk.Revit.DB.XYZ> GetBasises(this RebarExport rebar)
        {
            XYZ? basisX = null, basisY = null;
            var basisZ = rebar.DistributionDirection;

            if (basisZ.IsPerpendicular(XYZ.BasisZ))
            {
                basisY = XYZ.BasisZ;
                basisX = basisY.CrossProduct(basisZ);
            }
            else
            {
                double? maxLength = null;
                Autodesk.Revit.DB.Line? mainLine = null;
                foreach (var curve in rebar.CenterCurves)
                {
                    if (maxLength == null || curve.Length.IsBigger(maxLength.Value))
                    {
                        maxLength = curve.Length;

                        if (curve is Autodesk.Revit.DB.Line)
                        {
                            mainLine = curve as Autodesk.Revit.DB.Line;
                        }
                        else if (curve is Autodesk.Revit.DB.Arc)
                        {
                            mainLine = Autodesk.Revit.DB.Line.CreateBound(curve.GetEndPoint(0), curve.GetEndPoint(1));
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                basisX = mainLine.Direction;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                basisY = basisZ.CrossProduct(basisX);
            }

            return new List<XYZ> { basisX, basisY, basisZ };
        }

        public static Autodesk.Revit.DB.XYZ GetBasisX(this RebarExport rebar)
        {
            return rebar.Basises[0];
        }

        public static Autodesk.Revit.DB.XYZ GetBasisY(this RebarExport rebar)
        {
            return rebar.Basises[1];
        }

        public static Autodesk.Revit.DB.XYZ GetBasisZ(this RebarExport rebar)
        {
            return rebar.Basises[2];
        }

        public static Autodesk.Revit.DB.XYZ GetCenterPoint(this RebarExport rebar)
        {
            return rebar.Transform.OfPoint(XYZ.Zero);
        }

        public static Autodesk.Revit.DB.XYZ GetMinPoint(this RebarExport rebar)
        {
            return rebar.RotateTransform.OfPoint(rebar.RotateInverseBoundaryPoints[0]);
        }

        public static Autodesk.Revit.DB.XYZ GetMaxPoint(this RebarExport rebar)
        {
            return rebar.RotateTransform.OfPoint(rebar.RotateInverseBoundaryPoints[1]);
        }

        public static List<Autodesk.Revit.DB.Curve> GetCenterCurves(this RebarExport rebar)
        {
            List<Autodesk.Revit.DB.Curve> curves;
            var index = ((double)rebar.Number / 2).RoundUp() - 1;

            if (rebar.SingleRebar != null)
            {
                curves = (rebar.SingleRebar.GetCenterlineCurves(false, false, false,
                    Autodesk.Revit.DB.Structure.MultiplanarOption.IncludeOnlyPlanarCurves, index)
                    as List<Autodesk.Revit.DB.Curve>)!;
            }
            else
            {
                curves = (rebar.SystemRebar!.GetCenterlineCurves(false, false, false)
                    as List<Autodesk.Revit.DB.Curve>)!;
            }

            return curves;
        }

        public static List<Autodesk.Revit.DB.Curve> GetSupressBendCurves(this RebarExport rebar)
        {
            List<Autodesk.Revit.DB.Curve> curves;
            var index = ((double)rebar.Number / 2).RoundUp() - 1;

            if (rebar.SingleRebar != null)
            {
                curves = (rebar.SingleRebar.GetCenterlineCurves(false, false, true,
                    Autodesk.Revit.DB.Structure.MultiplanarOption.IncludeOnlyPlanarCurves, index)
                    as List<Autodesk.Revit.DB.Curve>)!;
            }
            else
            {
                curves = (rebar.SystemRebar!.GetCenterlineCurves(false, false, true)
                    as List<Autodesk.Revit.DB.Curve>)!;
            }

            var removeIndexs = new List<int>();
            for (int i = 0; i < curves.Count; i++)
            {
                if (curves[i] is Autodesk.Revit.DB.Arc)
                {
                    var arc = curves[i] as Autodesk.Revit.DB.Arc;
                    if (arc!.Radius.IsEqualOrSmaller(50.0.milimeter2Feet()))
                    {
                        removeIndexs.Add(i);

                        var beforeLine = curves[i - 1] as Autodesk.Revit.DB.Line;
                        var afterLine = curves[i + 1] as Autodesk.Revit.DB.Line;
                        var intersectPnt = afterLine!.GetProjectPoint(beforeLine!.GetEndPoint(1));
                        curves[i - 1] = Autodesk.Revit.DB.Line.CreateBound(beforeLine.GetEndPoint(0), intersectPnt);
                        curves[i + 1] = Autodesk.Revit.DB.Line.CreateBound(intersectPnt, afterLine!.GetEndPoint(1));
                    }
                }
            }

            int j = 0;
            for (int i = 0; i < removeIndexs.Count; i++)
            {
                curves.RemoveAt(removeIndexs[i] - j);
                j++;
            }
            return curves;
        }

        public static List<string> GetDimensionNames(this RebarExport rebar)
        {
            var dimNames = new List<string>();

            var def = rebar.RebarShape.GetRebarShapeDefinition();
            var paramNames = def.GetParameters().Select(x => x.GetElement().Name);
            var hookParamNames = paramNames.Where(x => rebar.RevitRebar.LookupParameter(x).IsReadOnly);

            var rbd = rebar.RebarBendData;
            if (rbd.HookAngle0 > 0)
            {
                //var bip = BuiltInParameter.REBAR_SHAPE_START_HOOK_LENGTH;
                //dimNames.Insert(0, rebar.RevitRebar.get_Parameter(bip).Definition.Name);
                dimNames.Add(hookParamNames.First());
            }

            if (def is Autodesk.Revit.DB.Structure.RebarShapeDefinitionBySegments)
            {
                var defBySegment = rebar.RebarShape.GetRebarShapeDefinition()
                    as Autodesk.Revit.DB.Structure.RebarShapeDefinitionBySegments;

                for (int i = 0; i < defBySegment!.NumberOfSegments; i++)
                {
                    var rss = defBySegment.GetSegment(i);
                    var constraints = rss.GetConstraints()
                        as List<Autodesk.Revit.DB.Structure.RebarShapeConstraint>;
                    foreach (var rsc in constraints!)
                    {
                        if (!(rsc is Autodesk.Revit.DB.Structure.RebarShapeConstraintSegmentLength))
                            continue;
                        ElementId paramId = rsc.GetParamId();
                        if ((paramId == ElementId.InvalidElementId))
                            continue;
                        foreach (Parameter p in rebar.RebarShape.Parameters)
                        {
                            if (p.Id.IntegerValue == paramId.IntegerValue)
                            {
                                dimNames.Add(p.Definition.Name);
                                break;
                            }
                        }
                    }
                }
            }

            if (def is Autodesk.Revit.DB.Structure.RebarShapeDefinitionByArc)
            {
                var defByArc = rebar.RebarShape.GetRebarShapeDefinition()
                    as Autodesk.Revit.DB.Structure.RebarShapeDefinitionByArc;

                var constraints = defByArc!.GetConstraints();
                foreach (var constraint in constraints)
                {
                    ElementId paramId = constraint.GetParamId();
                    if ((paramId == ElementId.InvalidElementId))
                        continue;
                    foreach (Parameter p in rebar.RebarShape.Parameters)
                    {
                        if (p.Id.IntegerValue == paramId.IntegerValue)
                        {
                            dimNames.Add(p.Definition.Name);
                            break;
                        }
                    }
                }
            }

            if (rbd.HookAngle1 > 0)
            {
                //var bip = BuiltInParameter.REBAR_SHAPE_END_HOOK_LENGTH;
                //dimNames.Add(rebar.RevitRebar.get_Parameter(bip).Definition.Name);
                dimNames.Add(hookParamNames.Last());
            }


            return dimNames;
        }

        public static List<double> GetDimensionValues(this RebarExport rebar)
        {
            return rebar.DimensionNames.Select(x => rebar.RevitRebar.LookupParameter(x).AsDouble()).ToList();
        }

        public static List<double> GetDimension_RoundSI_Values(this RebarExport rebar)
        {
            return rebar.DimensionValues.Select(x => x.GetDimension_RoundSI_Value()).ToList();
        }

        public static int GetNumber(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null)
            {
                return rebar.SingleRebar.Quantity;
            }
            else
            {
                return rebar.SystemRebar!.Quantity;
            }
        }

        public static Autodesk.Revit.DB.Structure.RebarLayoutRule GetRebarLayoutRule(this RebarExport rebar)
        {
            if (rebar.SingleRebar != null) return rebar.SingleRebar.LayoutRule;
            return rebar.SystemRebar!.LayoutRule;
        }

        public static Autodesk.Revit.DB.Transform GetPreRotateTransform(this RebarExport rebar)
        {
            var rotateTransform = Transform.Identity;
            rotateTransform.BasisX = rebar.BasisX;
            rotateTransform.BasisY = rebar.BasisY;
            rotateTransform.BasisZ = rebar.BasisZ;

            return rotateTransform;
        }

        public static Autodesk.Revit.DB.Transform GetRotateTransform(this RebarExport rebar)
        {
            return rebar.PreRotateTransform;
        }

        public static Autodesk.Revit.DB.Transform GetTranslateTransform(this RebarExport rebar)
        {
            var ribPnts = rebar.RotateInverseBoundaryPoints;

            var inverseCenterPnt = (ribPnts[0] + ribPnts[1]) / 2.0;
            return Transform.CreateTranslation(inverseCenterPnt);
        }

        public static Autodesk.Revit.DB.Transform GetTransform(this RebarExport rebar)
        {
            return rebar.RotateTransform * rebar.TranslateTransform;
        }

        public static double GetMaxLineLength(this RebarExport rebar)
        {
            return rebar.CenterCurves.Where(x => x is Autodesk.Revit.DB.Line).Max(x => x.Length);
        }

        public static double GetMaxArcLength(this RebarExport rebar)
        {
            return rebar.CenterCurves.Where(x => x is Autodesk.Revit.DB.Arc).Max(x => x.Length);
        }

        public static List<XYZ> GetRotateInverseBoundaryPoints(this RebarExport rebar)
        {
            var rotateInverse = rebar.RotateTransform.Inverse;

            var pnts = new List<Autodesk.Revit.DB.XYZ>();
            var curves = rebar.CenterCurves.Select(x => x.CreateTransformed(rotateInverse));
            foreach (var curve in curves)
            {
                pnts.Add(curve.GetEndPoint(0));
            }
            pnts.Add(curves.Last().GetEndPoint(1));

            double? minX = null, minY = null, maxX = null, maxY = null, zValue = null;
            foreach (var pnt in pnts)
            {
                if (zValue == null)
                {
                    zValue = pnt.Z;
                }
                if (minX == null || pnt.X < minX)
                {
                    minX = pnt.X;
                }

                if (minY == null || pnt.Y < minY)
                {
                    minY = pnt.Y;
                }

                if (maxX == null || pnt.X > maxX)
                {
                    maxX = pnt.X;
                }

                if (maxY == null || pnt.Y > maxY)
                {
                    maxY = pnt.Y;
                }
            }
            return new List<XYZ> { new XYZ(minX!.Value, minY!.Value, zValue!.Value), new XYZ(maxX!.Value, maxY!.Value, zValue.Value) };
        }

        public static double GetWidth(this RebarExport rebar)
        {
            var ribPnts = rebar.RotateInverseBoundaryPoints;
            return (ribPnts[1] - ribPnts[0]).X;
        }

        public static double GetHeight(this RebarExport rebar)
        {
            var ribPnts = rebar.RotateInverseBoundaryPoints;
            return (ribPnts[1] - ribPnts[0]).Y;
        }

        public static List<Curve> GetOriginRealCurves(this RebarExport rebar)
        {
            var curves = rebar.CenterCurves.Select(x => x.CreateTransformed(rebar.InverseTransform));
            return curves.ToList();
        }

        public static Line GetRealMainPath(this RebarExport rebar)
        {
            return (rebar.MainPath.CreateTransformed(rebar.InverseTransform) as Line)!;
        }

        public static List<bool> GetIsBendSegments(this RebarExport rebar)
        {
            return rebar.CenterCurves.Select(x => RebarCurveUtil.IsBendSegment(x)).ToList();
        }
        #endregion

        #region Method
        public static void TransformCurvesForExcelSchedule(this RebarExport rebar)
        {
            var normal = rebar.DistributionDirection;

            var cs = rebar.CenterCurves;
            var supressBendCurves = rebar.SupressBendCurves;

            Autodesk.Revit.DB.XYZ? vecX = null, vecY = null;
            double maxLen = 0;
            Line? lineX = null;

            if (rebar.RebarShape.Name == "TD_O_01")
            {
                var arc = cs.First() as Autodesk.Revit.DB.Arc;
                vecX = arc!.XDirection;
                lineX = Autodesk.Revit.DB.Line.CreateBound(arc.GetEndPoint(0), arc.Center);
            }
            else
            {
                foreach (Curve curve in cs)
                {
                    if (curve is Line)
                    {
                        Line line = (curve as Line)!;
                        if (maxLen < line.Length)
                        {
                            maxLen = line.Length;
                            vecX = line.Direction;
                            lineX = line.Clone() as Line;
                        }
                    }
                }
            }

            vecY = normal.CrossProduct(vecX);

            Transform rotateZ = Transform.Identity, rotateX = Transform.Identity;
            if (normal.IsParallel(Autodesk.Revit.DB.XYZ.BasisZ))
            {
            }
            else
            {
                var axis = Autodesk.Revit.DB.XYZ.BasisZ.CrossProduct(normal);
                rotateZ = Transform.CreateRotation
                    (axis, -normal.AngleTo(Autodesk.Revit.DB.XYZ.BasisZ));
                lineX = lineX!.CreateTransformed(rotateZ) as Line;
            }

            rotateX = Transform.CreateRotation
                (Autodesk.Revit.DB.XYZ.BasisZ, -lineX!.Direction.AngleTo(Autodesk.Revit.DB.XYZ.BasisX));

            cs = cs.Select(x => x.CreateTransformed(rotateX * rotateZ)).ToList();
            supressBendCurves = supressBendCurves.Select(x => x.CreateTransformed(rotateX * rotateZ)).ToList();

            maxLen = 0;
            foreach (Curve curve in cs)
            {
                if (curve is Line)
                {
                    var line = curve as Line;
                    if (maxLen < line!.Length)
                    {
                        maxLen = line.Length;
                        vecX = line.Direction;
                    }
                }
            }

            var angle = vecX!.GetAngle(Autodesk.Revit.DB.XYZ.BasisX, Autodesk.Revit.DB.XYZ.BasisY);
            rotateX = Transform.CreateRotation(Autodesk.Revit.DB.XYZ.BasisZ, -angle);

            rebar.CenterCurves = cs.Select(x => x.CreateTransformed(rotateX)).ToList();
            rebar.SupressBendCurves = supressBendCurves.Select(x => x.CreateTransformed(rotateX)).ToList();
        }

        public static void GetHookLengthIndex(this RebarExport rebar, List<double> dimVals)
        {
            var shapeName = rebar.RebarShape.Name;
            if (shapeName.Contains("TC_L_01"))
            {
                var index = dimVals.IndexOf(dimVals.Min());
                rebar.HookTextIndexs.Add(index);
            }
            if (shapeName.Contains("TC_L_(Nhấn)_01"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_L_(Nhấn)_02"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "F")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_L_(Nhấn)_03"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_L_(Nhấn)_05"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "F")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_UZ_"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_(Nhấn)"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (shapeName.Contains("01"))
                    {
                        if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "G")
                        {
                            rebar.HookTextIndexs.Add(i);
                        }
                    }
                    if (shapeName.Contains("02"))
                    {
                        if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "E")
                        {
                            rebar.HookTextIndexs.Add(i);
                        }
                    }
                }
            }
            if (shapeName.Contains("TC_U_01"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "B")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_03"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "G")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_01"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_04"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "C")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_04"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "C")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_05"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_06"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "C")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_07"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_08"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_U_360_09"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "E")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }

            if (shapeName.Contains("TC_Z_01"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "B" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_Z_04"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "C")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_Z_360") && !shapeName.Contains("TC_Z_360_06"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "C")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_Z_360_06"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_Z_Nhan") && !shapeName.Contains("TC_Z_Nhan360_02"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "E")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
            if (shapeName.Contains("TC_Z_Nhan360_02") || shapeName.Contains("TC_Z_Nhan90"))
            {
                for (int i = 0; i < rebar.DimensionNames.Count; i++)
                {
                    if (rebar.DimensionNames[i] == "A" || rebar.DimensionNames[i] == "D")
                    {
                        rebar.HookTextIndexs.Add(i);
                    }
                }
            }
        }

        public static List<Autodesk.Revit.DB.Curve>? MergeCurves(this RebarExport rebar1, RebarExport rebar2)
        {
            var curves0 = rebar1.SupressBendCurves;
            var curves1 = rebar2.SupressBendCurves;

            var line00 = curves0.First() as Line;
            var line01 = curves0.Last() as Line;
            var line10 = curves1.First() as Line;
            var line11 = curves1.Last() as Line;

            Autodesk.Revit.DB.Line? mergeLine = null;
            if (line00 != null)
            {
                if (line10 != null)
                {
                    mergeLine = line00.LineOverlap(line10);
                    if (mergeLine != null)
                    {
                        var mergeCurves = new List<Autodesk.Revit.DB.Curve>();
                        mergeCurves.AddRange(curves1.ChangeDirections());
                        mergeCurves.AddRange(curves0);

                        mergeCurves.RemoveAt(curves1.Count - 1);
                        mergeCurves[curves1.Count - 1] = mergeLine;

                        return mergeCurves;
                    }
                }
                if (line11 != null)
                {
                    mergeLine = line00.LineOverlap(line11);
                    if (mergeLine != null)
                    {
                        var mergeCurves = new List<Autodesk.Revit.DB.Curve>();
                        mergeCurves.AddRange(curves1);
                        mergeCurves.AddRange(curves0);

                        mergeCurves.RemoveAt(curves1.Count - 1);
                        mergeCurves[curves1.Count - 1] = mergeLine;

                        return mergeCurves;
                    }
                }
            }

            if (line01 != null)
            {
                if (line10 != null)
                {
                    mergeLine = line01.LineOverlap(line10);
                    if (mergeLine != null)
                    {
                        var mergeCurves = new List<Autodesk.Revit.DB.Curve>();
                        mergeCurves.AddRange(curves0);
                        mergeCurves.AddRange(curves1);

                        mergeCurves.RemoveAt(curves0.Count - 1);
                        mergeCurves[curves0.Count - 1] = mergeLine;

                        return mergeCurves;
                    }
                }
                if (line11 != null)
                {
                    mergeLine = line01.LineOverlap(line11);
                    if (mergeLine != null)
                    {
                        var mergeCurves = new List<Autodesk.Revit.DB.Curve>();
                        mergeCurves.AddRange(curves0);
                        mergeCurves.AddRange(curves1.ChangeDirections());

                        mergeCurves.RemoveAt(curves0.Count - 1);
                        mergeCurves[curves0.Count - 1] = mergeLine;

                        return mergeCurves;
                    }
                }
            }

            return null;
        }

        public static RebarExport Clone(this RebarExport sourceRebar)
        {
            var rebar = new RebarExport();
            rebar.HostElement = sourceRebar.HostElement;
            rebar.RebarStyle = sourceRebar.RebarStyle;
            rebar.BarType = sourceRebar.BarType;
            rebar.DistributionDirection = sourceRebar.DistributionDirection;
            rebar.SupressBendCurves = sourceRebar.SupressBendCurves;
            rebar.RebarLayoutRule = sourceRebar.RebarLayoutRule;
            rebar.Number = sourceRebar.Number;
            rebar.Spacing = sourceRebar.Spacing;
            rebar.DistributionLength = sourceRebar.DistributionLength;
            rebar.BarsOnNormalSide = sourceRebar.BarsOnNormalSide;

            return rebar;
        }

        public static void Delete(this RebarExport rebar)
        {
            revitData.Document.Delete(rebar.RevitRebar.Id);
        }

        public static void Deletes(params RebarExport[] rebars)
        {
            revitData.Document.Delete(rebars.Select(x => x.RevitRebar.Id).ToList());
        }

        public static double GetDimension_RoundSI_Value(this double dimVal)
        {
            dimVal = dimVal.feet2Milimeter();

            var rrm = rebarData.RebarRoundingManager;

            double roundingNum = rrm.ApplicableSegmentLengthRounding;
            if (roundingNum.IsEqual(0)) roundingNum = 1;

            if (rrm.ApplicableSegmentLengthRoundingMethod == RoundingMethod.Nearest)
            {
                dimVal = Math.Round(dimVal / roundingNum) * roundingNum;
            }
            else if (rrm.ApplicableSegmentLengthRoundingMethod == RoundingMethod.Up)
            {
                dimVal = Math.Ceiling(dimVal / roundingNum) * roundingNum;
            }
            else
            {
                dimVal = Math.Floor(dimVal / roundingNum) * roundingNum;
            }

            return dimVal;
        }
        #endregion
    }
}