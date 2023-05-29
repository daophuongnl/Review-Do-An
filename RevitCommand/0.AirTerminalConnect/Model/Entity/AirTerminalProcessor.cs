using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class AirTerminalProcessor
    {
        public Duct? MainDuct { get; set; }

        public FamilyInstance? AirTerminal { get; set; }


        private Connector? airTerminalConnector;               
        public Connector AirTerminalConnector => this.airTerminalConnector ??= this.GetAirTerminalConnector();

        private Connector? airTerminalConnectorr;
        public Connector AirTerminalConnectorr => this.airTerminalConnectorr ??= this.GetAirTerminalConnectorr();


        //Thuộc tính của đối tượng

        private ElementId? levelId;
        public ElementId LevelId => this.levelId ??= this.GetLevelId();

        private ElementId? systemTypeId;
        public ElementId SystemTypeId => this.systemTypeId ??= this.GetSystemTypeId();



        private XYZ? tempDuctStartPoint;
        public XYZ TempDuctStartPoint => this.tempDuctStartPoint ??= this.GetTempDuctStartPoint();

        private Duct? tempDuct;
        public Duct TempDuct => this.tempDuct ??= this.GetTempDuct();

        private FamilyInstance? tap;  //chỗ lưu trữ giá trị
        public FamilyInstance Tap     //phương thức truy xuất
            => this.tap ??=this.GetTap();

        private FlexDuctType? flexDuctType;
        public FlexDuctType? FlexDuctType
        {
            get => this.flexDuctType ??= this.GetFlexDuctType();
            set => this.flexDuctType = value;
        } 

        private DuctType? ductType;
        public DuctType? DuctType
        {
            get => this.ductType ??= this.GetDuctType();
            set => this.ductType = value;
        }

        private FlexDuct? flexDuct;
        public FlexDuct FlexDuct => this.flexDuct ??= this.GetFlexDuct();

        //{
        //    get
        //    {
        //        //nếu chưa có giá trị thì tiếp phương thức xử lí 
        //        //nếu có giá trị thì bỏ qua phương thức và trả ra giá trị

        //        if(this.tap==null)
        //        {
        //            this.tap = this.GetTap();
        //                //AirTerminalProcessorUtil.GetTap(this);
        //        }
        //        return this.tap;
        //    }

        //}
     
                
      

    }
}
