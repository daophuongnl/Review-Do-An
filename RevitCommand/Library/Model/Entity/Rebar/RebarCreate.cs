using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    // Tạo lớp và cung cấp các thông số cần thiết để cấu thành 1 đối tượng thép
    public class RebarCreate : EntRebarBase
    {
        public override int Number
        {
            get => number ??= this.GetNumber();
        }

        public override double Spacing
        {
            get => spacing ??= this.GetSpacing();
        }

        public override double DistributionLength
        {
            get => distributionLength ??= this.GetDistributionLength();
        }

        public override double BarDiameter
        {
            get
            {
                if (barDiameter == -1 && barType != null)
                {
                    barDiameter = this.GetBarDiameter();
                }
                return barDiameter!.Value;
            }
            set
            {
                barDiameter = value;

                if (barDiameter != -1)
                {
                    barType = null;
                }
            }
        }

        public override RebarBarType? BarType
        {
            get
            {
                if (barType == null && barDiameter != -1)
                {
                    barType = this.GetBarType();
                }
                return barType;
            }
            set
            {
                barType = value;

                if (barType != null)
                {
                    barDiameter = -1;
                }
            }
        }

        public override Rebar RevitRebar
        {
            get
            {
                if (revitRebar == null)
                {
                    revitRebar = this.GetRevitRebar();
                }
                return revitRebar;
            }
        }

        public override string? RebarShapeName
        {
            get
            {
                if (rebarShapeName == null && rebarShape != null)
                {
                    rebarShapeName = this.GetRebarShapeName();
                }
                return rebarShapeName;
            }
            set
            {
                rebarShapeName = value;

                if (rebarShapeName != null)
                {
                    rebarShape = null;
                }
            }
        }

        public override RebarShape? RebarShape
        {
            get
            {
                if (rebarShape == null && rebarShapeName != null)
                {
                    rebarShape = this.GetRebarShape();
                }
                return rebarShape;
            }
            set
            {
                rebarShape = value;

                if (rebarShape != null)
                {
                    rebarShapeName = null;
                }
            }
        }
    }
}