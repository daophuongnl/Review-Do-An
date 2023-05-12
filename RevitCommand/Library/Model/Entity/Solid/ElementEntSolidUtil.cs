using Autodesk.Revit.DB;
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
    public static class ElementEntSolidUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static ElementEntSolid GetElementEntSolid(this EntElement entElement, bool isGetFamilySolid = false)
        {
            ElementEntSolid? elemEntSolid = null;

            //if (entElement is EntBeam)
            //{
            //    elemEntSolid = new BeamEntSolid();
            //}
            //else
            //{
                elemEntSolid = new ElementEntSolid();
            //}

            elemEntSolid.EntElement = entElement;
            elemEntSolid.IsGetFamilySolid = isGetFamilySolid;

            return elemEntSolid;
        }

        #region Property
        public static Solid GetOriginTransformFamilySolid(this ElementEntSolid elemEntSolid)
        {
            var originTransformFamilySolid = elemEntSolid.EntElement!.RevitElement!.GetSingleSolid(false, false);
            return originTransformFamilySolid;
        }

        public static Solid GetFamilySolid(this ElementEntSolid elemEntSolid)
        {
            var familySolid = elemEntSolid.EntElement!.RevitElement!.GetSingleSolid(false);

            var modifySolidFunc = elemEntSolid.ModifyFamilySolidFunc;
            if (modifySolidFunc != null)
            {
                familySolid = modifySolidFunc(familySolid);
            }

            return familySolid;
        }

        public static Solid GetRealSolid(this ElementEntSolid elemEntSolid)
        {
            var entElement = elemEntSolid.EntElement!;
            var realSolid = entElement.RevitElement!.GetSingleSolid(true, true, entElement is EntFilledRegion);
            return realSolid;
        }

        public static Solid GetSolid(this ElementEntSolid elementEntSolid)
        {
            Solid? solid = null;
            if (elementEntSolid.IsGetFamilySolid)
            {
                solid = elementEntSolid.FamilySolid;
            }
            else
            {
                solid = elementEntSolid.RealSolid;
            }

            return solid!;
        }

        public static Transform? GetPurgeTransform(this ElementEntSolid elementEntSolid)
        {
            if (elementEntSolid.EntElement is null) return null;    
            var puregeTransform = elementEntSolid.EntElement.PurgeTransform;
            return puregeTransform;
        }
        #endregion
    }
}
