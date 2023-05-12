using Autodesk.Revit.DB;
using Autodesk.Revit.DB.PointClouds;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public partial class RebarExport : EntRebarBase
    {
        public Rebar? SingleRebar { get; set; }
        public RebarInSystem? SystemRebar { get; set; }

        public override Rebar RevitRebar => revitRebar ??= SingleRebar!;

        public override Element HostElement
        {
            get => hostElement ??= this.GetHostElement();
        }

        public override RebarShapeDrivenAccessor RebarShapeDrivenAccessor
        {
            get => rebarShapeDrivenAccessor ??= this.GetRebarShapeDrivenAccessor()!;
        }

        private bool? barsOnNormalSide;
        public bool? BarsOnNormalSide
        {
            get => barsOnNormalSide ??= RebarShapeDrivenAccessor.BarsOnNormalSide;
            set => barsOnNormalSide = value;
        }

        public override RebarShape RebarShape => rebarShape ??= this.GetRebarShape();

        public override RebarBarType BarType => barType ??= this.GetBarType();

        public override double BarDiameter =>
#if REVIT2022_OR_GREATER
            BarType.BarNominalDiameter;
#else
            BarType.BarDiameter;
#endif

        public override RebarStyle RebarStyle => RebarShape.RebarStyle;

        private RebarBendData? rebarBendData;
        public RebarBendData RebarBendData => rebarBendData ??= this.GetRebarBendData();

        public override Autodesk.Revit.DB.BoundingBoxXYZ BoundingBoxXYZ => boundingBoxXYZ ??= this.GetBoundingBoxXYZ();

        private Autodesk.Revit.DB.Plane? plane;
        public Autodesk.Revit.DB.Plane Plane => plane ??= this.GetPlane();

        private Autodesk.Revit.DB.Line? distributionPath;
        public Autodesk.Revit.DB.Line DistributionPath => distributionPath ??= this.GetDistributionPath();

        public override Autodesk.Revit.DB.XYZ DistributionDirection => distributionDirection ??= this.GetDistributionDirection();

        private Autodesk.Revit.DB.Line? mainPath;
        public Autodesk.Revit.DB.Line MainPath => mainPath ??= this.GetMainPath();

        private List<Autodesk.Revit.DB.XYZ>? basises;
        public List<Autodesk.Revit.DB.XYZ> Basises => basises ??= this.GetBasises();

        private Autodesk.Revit.DB.XYZ? basisX;
        public Autodesk.Revit.DB.XYZ BasisX => basisX ??= this.GetBasisX();

        private Autodesk.Revit.DB.XYZ? basisY;
        public Autodesk.Revit.DB.XYZ BasisY => basisY ??= this.GetBasisY();

        private Autodesk.Revit.DB.XYZ? basisZ;
        public Autodesk.Revit.DB.XYZ BasisZ => basisZ ??= this.GetBasisZ();

        private List<Autodesk.Revit.DB.Curve>? centerCurves;
        public List<Autodesk.Revit.DB.Curve> CenterCurves
        {
            get => centerCurves ??= this.GetCenterCurves();
            set => centerCurves = value;
        }

        private List<Autodesk.Revit.DB.Curve>? supressBendCurves;
        public List<Autodesk.Revit.DB.Curve> SupressBendCurves
        {
            get => supressBendCurves ??= this.GetSupressBendCurves();
            set => supressBendCurves = value;
        }

        private List<string>? dimensionNames;
        public List<string> DimensionNames => dimensionNames ??= this.GetDimensionNames();

        private List<double>? dimensionValues;
        public List<double> DimensionValues => dimensionValues ??= this.GetDimensionValues();

        private List<double>? dimension_RoundSI_Values;
        public List<double> Dimension_RoundSI_Values => dimension_RoundSI_Values ??= this.GetDimension_RoundSI_Values();

        public override int Number => number ??= this.GetNumber();

        public override double DistributionLength => distributionLength ??= DistributionPath.Length;

        public override double Spacing => spacing ??= DistributionLength / (Number - 1);

        public override RebarLayoutRule RebarLayoutRule => rebarLayoutRule ??= this.GetRebarLayoutRule();

        public override Autodesk.Revit.DB.XYZ CenterPoint => centerPoint ??= this.GetCenterPoint();

        private Autodesk.Revit.DB.XYZ? minPoint;
        public Autodesk.Revit.DB.XYZ MinPoint => minPoint ??= this.GetMinPoint();

        private Autodesk.Revit.DB.XYZ? maxPoint;
        public Autodesk.Revit.DB.XYZ MaxPoint => maxPoint ??= this.GetMaxPoint();

        protected Autodesk.Revit.DB.Transform? preRotateTransform;
        public virtual Autodesk.Revit.DB.Transform PreRotateTransform => preRotateTransform ??= this.GetPreRotateTransform();

        protected Autodesk.Revit.DB.Transform? rotateTransform;
        public virtual Autodesk.Revit.DB.Transform RotateTransform => rotateTransform ??= this.GetRotateTransform();

        private Autodesk.Revit.DB.Transform? translateTransform;
        public Autodesk.Revit.DB.Transform TranslateTransform => translateTransform ??= this.GetTranslateTransform();

        private Autodesk.Revit.DB.Transform? transform;
        public Autodesk.Revit.DB.Transform Transform => transform ??= this.GetTransform();

        private Autodesk.Revit.DB.Transform? inverseTransform;
        public Autodesk.Revit.DB.Transform InverseTransform => inverseTransform ??= Transform.Inverse;

        private double? maxLineLength;
        public double MaxLineLength => maxLineLength ??= this.GetMaxLineLength();

        private double? maxArcLength;
        public double? MaxArcLength => maxArcLength ??= this.GetMaxArcLength();

        private List<XYZ>? rotateInverseBoundaryPoints;
        public List<XYZ> RotateInverseBoundaryPoints => rotateInverseBoundaryPoints ??= this.GetRotateInverseBoundaryPoints();

        private double? width;
        public double Width
        {
            get => width ??= this.GetWidth();
            set => width = value;
        }

        private double? height;
        public double Height
        {
            get => height ??= this.GetHeight();
            set => height = value;
        }

        private List<Autodesk.Revit.DB.Curve>? originRealCurves;
        public List<Autodesk.Revit.DB.Curve> OriginRealCurves => originRealCurves ??= this.GetOriginRealCurves();

        private Line? realMainPath;
        public Line RealMainPath => realMainPath ??= this.GetRealMainPath();

        private List<bool>? isBendSegments;
        public List<bool> IsBendSegments => isBendSegments ??= this.GetIsBendSegments();

        public List<int> HookTextIndexs { get; set; } = new List<int>();
    }
}