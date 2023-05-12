using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class EntSolidUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static EntSolid GetEntSolid(this Solid solid)
        {
            return new EntSolid
            {
                Solid = solid
            };
        }

        #region Property

        public static EntBoundingBoxXYZ GetEntBoundingBoxXYZ(this EntSolid entSolid)
        {
            return entSolid.BoundingBoxXYZ.GetEntBoundingBoxXYZ();
        }

        public static BoundingBoxXYZ GetBoundingBoxXYZ(this EntSolid entSolid)
        {
            var bb = entSolid.Solid.GetBoundingBoxXYZ();
            return bb;
        }

        public static List<Solid> GetSplitSolids(this EntSolid entSolid)
        {
            var splitSolids = SolidUtils.SplitVolumes(entSolid.Solid) as List<Solid>;
#pragma warning disable CS8603 // Possible null reference return.
            return splitSolids;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static Solid? GetPurgeSolid(this EntSolid entSolid)
        {
            Solid? purgeSolid = null;
            if (entSolid.CanPurgeSolid)
            {
                purgeSolid = SolidUtils.CreateTransformed(entSolid.Solid, entSolid.PurgeTransform);
            }
            return purgeSolid;
        }

        public static bool GetCanPurgeSolid(this EntSolid entSolid)
        {
            var canPurgeSolid = false;
            var purgeTf = entSolid.PurgeTransform;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!purgeTf.IsIdentity)
            {
                canPurgeSolid = true;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return canPurgeSolid;
        }

        #endregion

        #region Method

        public static Solid? TryMerge(this EntSolid es1, EntSolid es2)
        {
            Solid? mergeSolid = null;
            var s1 = es1.Solid;
            var s2 = es2.Solid;

            try
            {
                mergeSolid = s1!.TryMerge(s2!);
            }
            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
            {
                if (es1.CanPurgeSolid)
                {
                    var ps1 = es1.PurgeSolid;
                    try
                    {
                        mergeSolid = ps1!.TryMerge(s2!);
                        es1.Solid = ps1!;
                    }
                    catch (Autodesk.Revit.Exceptions.InvalidOperationException)
                    {
                        if (es2.CanPurgeSolid)
                        {
                            var ps2 = es2.PurgeSolid;
                            try
                            {
                                mergeSolid = s1!.TryMerge(ps2!);
                                es2.Solid = ps2!;
                            }
                            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
                            {
                                try
                                {
                                    mergeSolid = ps1!.TryMerge(ps2!);
                                    es1.Solid = ps1!;
                                    es2.Solid = ps2!;
                                }
                                catch (Autodesk.Revit.Exceptions.InvalidOperationException)
                                {
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else if (es2.CanPurgeSolid)
                {
                    var ps2 = es2.PurgeSolid;
                    try
                    {
                        mergeSolid = s1!.TryMerge(ps2!);
                        es2.Solid = ps2!;
                    }
                    catch (Autodesk.Revit.Exceptions.InvalidOperationException)
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }

            if (mergeSolid != null)
            {
                es1.CanPurgeSolid = false;
                es2.CanPurgeSolid = false;
            }
            return mergeSolid;
        }

        #endregion
    }
}
