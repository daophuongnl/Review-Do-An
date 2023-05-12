using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using System.IO;

namespace Utility
{
    public static class EntLocationUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static EntLocation GetEntLocation(this Location location)
        {
            EntLocation? entLoc;

            if (location is LocationPoint)
            {
                entLoc = new EntLocationPoint();
            }
            else if (location is LocationCurve)
            {
                entLoc = new EntLocationCurve();
            }
            else 
            {
                entLoc = new EntLocation();
            }

            entLoc.RevitLocation = location;
            return entLoc;
        }

        public static EntLocation GetEntLocation(this EntElement entElement)
        {
            return entElement.RevitElement!.Location.GetEntLocation();
        }

        #region Property
        public static Transform GetPurgeTransform(this EntLocation entLocation)
        {
            Transform? tf;

            var purgeOffset = entLocation.PurgeOffset;
            if (purgeOffset.IsEqual(XYZ.Zero))
            {
                tf = Transform.Identity;
            }
            else
            {
                tf = Transform.CreateTranslation(purgeOffset);
            }

            return tf;
        }
        #endregion

        #region Method

        #endregion
    }
}
