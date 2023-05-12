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
    public class EntRebarBase : EntElement
    {
        protected int? number;
        public virtual int Number
        {
            get => number!.Value;
            set => number = value;
        }

        protected double? spacing;
        public virtual double Spacing
        {
            get => spacing!.Value;
            set => spacing = value;
        }

        protected double? distributionLength;
        public virtual double DistributionLength
        {
            get => distributionLength!.Value;
            set => distributionLength = value;
        }

        // Hướng rải thép: norm
        protected XYZ? distributionDirection;
        public virtual XYZ? DistributionDirection
        {
            get => distributionDirection;
            set => distributionDirection = value;
        }

        protected double? barDiameter;
        public virtual double BarDiameter
        {
            get=> barDiameter!.Value;
            set=>barDiameter = value;
        }

        protected RebarBarType? barType;
        public virtual RebarBarType? BarType
        {
            get=> barType;
            set=>barType = value;
        }

        public string BarTypeName => BarType!.Name;

        protected string? rebarShapeName;
        public virtual string? RebarShapeName
        {
            get=> rebarShapeName;
            set=>rebarShapeName = value;
        }

        protected RebarShape? rebarShape;
        public virtual RebarShape? RebarShape
        {
            get=>rebarShape;
            set=>rebarShape = value;
        }

        protected List<Curve>? curves;
        public virtual List<Curve>? Curves
        {
            get=> curves;
            set=>curves = value;
        }

        protected Element? hostElement;
        public virtual Element? HostElement
        {
            get=> hostElement;
            set=>hostElement = value;
        }

        protected Rebar? revitRebar;
        public virtual Rebar RevitRebar
        {
            get=> revitRebar!;
            set=>revitRebar = value;
        }

        public override Element RevitElement => RevitRebar!;

        public virtual RebarStyle RebarStyle { get; set; }

        protected RebarLayoutRule? rebarLayoutRule;
        public virtual RebarLayoutRule RebarLayoutRule
        {
            get=> rebarLayoutRule!.Value;
            set=>rebarLayoutRule = value;
        }

        protected RebarShapeDrivenAccessor? rebarShapeDrivenAccessor;
        public virtual RebarShapeDrivenAccessor RebarShapeDrivenAccessor
        {
            get => rebarShapeDrivenAccessor ??= this.GetRebarShapeDrivenAccessor();
            set => rebarShapeDrivenAccessor = value;
        }

        protected bool? isIncludeFirstBar;
        public virtual bool IsIncludeFirstBar
        {
            get => isIncludeFirstBar ??= true;
            set=>isIncludeFirstBar = value;
        }

        protected bool? isIncludeLastBar;
        public virtual bool IsIncludeLastBar
        {
            get => isIncludeLastBar ??= true;
            set=>isIncludeLastBar = value;
        }

        private double? totalBarLength;
        public double TotalBarLength
        {
            get => totalBarLength ??= this.GetTotalBarLength();
        }

        private double? totalWeightKg;
        public double TotalWeightKg
        {
            get => totalWeightKg ??= this.GetTotalWeightKg();
        }

        private string? partition;
        public string Partition
        {
            get => partition ??= this.GetPartition();
        }
    }
}