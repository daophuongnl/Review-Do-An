using Autodesk.Revit.DB;
using SingleData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class SolidUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static Solid GetSingleSolid(this Element elem, bool isRealSolid = true, bool isGetTransformSolid = true, bool isVolumnZero = false)
        {
            Solid? solid = null;
            GeometryElement? geoElem = null;

            Func<Solid, bool> checkingVolume = (s) => (isVolumnZero && s.Volume.IsEqual(0)) || !s.Volume.IsEqual(0);

            if (!isRealSolid && elem is FamilyInstance)
            {
                #region Code mới: lấy Solid từ nguồn gốc Family và Transform
                var fi = (elem as FamilyInstance)!;
                var inputTf = fi.GetTransform();
                var purgeTf = inputTf.Purge();
                geoElem = fi.GetOriginalGeometry(revitData.GeometryOptions);
                if (isGetTransformSolid)
                { 
                    geoElem = geoElem.GetTransformed(purgeTf);
                }
                #endregion
            }
            else
            {
                geoElem = elem.get_Geometry(revitData.GeometryOptions);
            }

            foreach (var geoObj in geoElem)
            {
                #region Code cũ: lấy Solid đã bị giao cắt (InstanceSolid)
                if (geoObj is GeometryInstance)
                {
                    var geoIns = geoObj as GeometryInstance;
                    if (geoIns != null)
                    {
                        foreach (var geoObj2 in geoIns.GetSymbolGeometry(geoIns.Transform))
                        {
                            if (geoObj2 is Solid)
                            {
                                var s = (geoObj2 as Solid)!;
                                if (checkingVolume(s))
                                {
                                    if (solid != null)
                                    {
                                        throw new Exception("This Element Geometry have more than one Solid");
                                    }
                                    solid = s;
                                }
                            }
                        }

                    }
                }
                #endregion

                if (geoObj is Solid)
                {
                    var s = (geoObj as Solid)!;
                    if (checkingVolume(s))
                    {
                        if (solid != null)
                        {
                            throw new Exception("This Element Geometry have more than one Solid");
                        }
                        solid = s;
                    }
                }
            }


            if (solid == null)
            {
                throw new Model.EntException.NoSolidException($"This Element {elem.Id.IntegerValue} doesn't have a Solid");
            }

            return solid;
        }

        public static List<Solid> GetSolids(this Element elem)
        {
            List<Solid>? solids = null;
            GeometryElement? geoElem = null;

            if (elem is FamilyInstance)
            {
                #region Code mới: lấy Solid từ nguồn gốc Family và Transform

                var fi = (elem as FamilyInstance)!;
                geoElem = fi.GetOriginalGeometry(revitData.GeometryOptions).GetTransformed(fi.GetTotalTransform());

                #endregion
            }
            else
            {
                geoElem = elem.get_Geometry(revitData.GeometryOptions);
            }

            foreach (var geoObj in geoElem)
            {
                if (geoObj is Solid)
                {
                    var s = (geoObj as Solid)!;
                    if (!s.Volume.IsEqual(0))
                    {
                        if (solids == null)
                        {
                            solids = new List<Solid>();
                        }
                        solids.Add(s);
                    }
                }
            }


            if (solids == null)
            {
                throw new Exception("This Element Geometry doesn't have any Solids");
            }

            return solids;
        }

        public static BoundingBoxXYZ GetBoundingBoxXYZ(this Solid solid)
        {
            var bb = solid.GetBoundingBox();

            var origin = bb.Transform.Origin;

            var minPnt = bb.Min + origin;
            var maxPnt = bb.Max + origin;

            return new BoundingBoxXYZ
            {
                Transform = Transform.Identity,
                Min = minPnt,
                Max = maxPnt
            };
        }

        public static Solid MergeSolid(this IEnumerable<Solid> solids)
        {
            Solid? mergedSolid = null;

            foreach (var solid in solids)
            {
                if (mergedSolid == null)
                {
                    mergedSolid = solid;
                }
                else
                {
                    mergedSolid = BooleanOperationsUtils.ExecuteBooleanOperation(mergedSolid, solid, BooleanOperationsType.Union);
                }
            }

            return mergedSolid!;
        }

        public static Solid? TryMerge(this Solid s1, Solid s2)
        {
            Solid? mergedSoild;
            try
            {
                mergedSoild = BooleanOperationsUtils.ExecuteBooleanOperation(s1, s2, BooleanOperationsType.Union);
            }
            catch
            {
                throw;
            }

            if (mergedSoild != null && mergedSoild.SurfaceArea.IsEqual(s1.SurfaceArea + s2.SurfaceArea))
            {
                mergedSoild = null;
            }

            return mergedSoild;
        }

        public static Solid Scale(this Solid s, double ratio)
        {
            var scaleTf = Transform.Identity;
            scaleTf = scaleTf.ScaleBasis(ratio);

            var centerPnt = s.ComputeCentroid();
            var scaledSolid = SolidUtils.CreateTransformed(s, scaleTf);
            var newCenterPnt = scaledSolid.ComputeCentroid();

            var translate = Transform.CreateTranslation(centerPnt - newCenterPnt);
            scaledSolid = SolidUtils.CreateTransformed(scaledSolid, translate);
            return scaledSolid;
        }

        public static Solid GetTransformedSolid(this Solid solid, Transform tf)
        {
            return SolidUtils.CreateTransformed(solid, tf);
        }
    }
}
