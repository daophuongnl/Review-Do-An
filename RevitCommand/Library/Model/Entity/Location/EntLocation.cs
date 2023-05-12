using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntLocation
    {
        public Location? RevitLocation { get; set; }

        protected XYZ? purgeOffset;
        public virtual XYZ PurgeOffset => purgeOffset ??= XYZ.Zero;

        private Transform? purgeTransform;
        public Transform PurgeTransform => purgeTransform ??= this.GetPurgeTransform();
    }
}
