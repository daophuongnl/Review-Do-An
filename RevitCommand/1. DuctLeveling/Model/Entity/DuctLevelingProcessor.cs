using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class DuctLevelingProcessor
    {
        public Duct? Duct { get; set; }

        private XYZ? pickpoint;
        public XYZ PickPoint
        {
            get => pickpoint??= this.GetPickPoint();
            set => pickpoint = value;
        }

        private XYZ? middlePoint;
        public XYZ MiddlePoint => this.middlePoint ??= this.GetMiddlePoint();

        private LocationCurve? ductLocation;
        public LocationCurve DuctLocation => this.ductLocation ??= this.GetDuctLocation();

        private Line? ductLine;
        public Line? DuctLine =>this.ductLine??= this.GetDuctLine();

        private XYZ? ductDirection;
        public XYZ? DuctDirection => this.ductDirection??= this.GetDuctDirection();

        // thay đổi
        //private XYZ? middlePoint { get; set; }
        //public XYZ MiddlePoint => this .middlePoint??= this.GetMiddlePoint();

        private ElementId? levelId;
        public ElementId LevelId => this.levelId ??= this.GetLevelId();
        private ElementId? systemTypeId;
        public ElementId SystemTypeId => this.systemTypeId ??= this.GetSystemTypeId();
        private ElementId? ductTypeId;
        public ElementId DuctTypeId => this.ductTypeId ??= this.Duct!.GetTypeId();

        /// NHẬP GIÁ TRỊ
        //public double HorizontalOffset { get; set; } = 600.0.milimeter2Feet();
        public double ZOffset { get; set; } = -500.0.milimeter2Feet();
        public double Width_MiddleDuct { get; set; } = 600.0.milimeter2Feet();


        public double HorizontalOffset = 600.0.milimeter2Feet();
        public double HorizontalOffsetMM
        {
            get => this.HorizontalOffset.feet2Milimeter();
            set => this.HorizontalOffset = value.milimeter2Feet();
        }



        //private double? zOffset;
        //public double ZOffset
        //{
        //    get => this.zOffset ??= this.ZOffset;
        //    set => this.zOffset = value.milimeter2Feet();
        //}

        private double? width;
        public double Width =>this.width??= this.Duct!.LookupParameter("Width").AsDouble();

        private double? height;
        public double Height => this.height ??= this.Duct!.LookupParameter("Height").AsDouble();

        //TH1 : cả 2 đầu đều đã kết nối connector
        private Connector? startConnector;
        public Connector StartConnector => this.startConnector ??= this.GetStartConnector();

        private Connector? endConnector;
        public Connector EndConnector =>this.endConnector??= this.GetEndConnector();

        private Connector? connectToEndConnector;
        public Connector ConnectToEndConnector => this.connectToEndConnector ??= this.GetConnectToEndConnector();

        // TH2: 1 đầu có connector và 1 đầu ko 
        private bool? isResverse;
        public bool IsResverse => this.isResverse ??= this.GetIsResverse();

        private DuctLevelingMode? mode;
        public DuctLevelingMode Mode =>this.mode??= this.GetMode();
        //

        private Duct? mainDuct1;
        public Duct MainDuct1 => this.mainDuct1 ??= this.GetMainDuct1();
       
        private Duct? mainDuct2;
        public Duct MainDuct2 => this.mainDuct2??= this.GetMainDuct2();

        private Duct? middleDuct;
        public Duct MiddleDuct => this.middleDuct ??= this.GetMiddleDuct();

        private Duct? connectorDuct1;
        public Duct ConnectorDuct1 => this.connectorDuct1 ??= this.ConnectorDuct1();

        private Duct? connectorDuct2;
        public Duct ConnectorDuct2 => this.connectorDuct2 ??= this.ConnectorDuct2();
    }
}
