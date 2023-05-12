using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace Model.Entity
{
    public class EntElementTransform : EntTransform
    {
        public EntElement? EntElement { get; set; }

        //public override double LengthX
        //{
        //    get
        //    {
        //        if (lengthX == -1)
        //        {
        //            lengthX = this.GetLengthX();
        //        }
        //        return lengthX;
        //    }
        //}

        //public override double LengthY
        //{
        //    get
        //    {
        //        if (lengthY == -1)
        //        {
        //            lengthY = this.GetLengthY();
        //        }
        //        return lengthY;
        //    }
        //}

        //public override double LengthZ
        //{
        //    get
        //    {
        //        if (lengthZ == -1)
        //        {
        //            lengthZ = this.GetLengthZ();
        //        }
        //        return lengthZ;
        //    }
        //}
    }
}
