﻿using Autodesk.Revit.DB;
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

        private LocationCurve? ductLocation;
        public LocationCurve DuctLocation => this.ductLocation ??= this.GetDuctLocation();

        private Line? ductLine;
        public Line? DuctLine =>this.ductLine??= this.GetDuctLine();

        private XYZ? ductDirection;
        public XYZ? DuctDirection => this.ductDirection??= this.GetDuctDirection();


        private XYZ? middlePoint { get; set; }
        public XYZ MiddlePoint => this .middlePoint??= this.GetMiddlePoint();

        private ElementId? levelId;
        public ElementId LevelId => this.levelId ??= this.GetLevelId();
        private ElementId? systemTypeId;
        public ElementId SystemTypeId => this.systemTypeId ??= this.GetSystemTypeId();
        private ElementId? ductTypeId;
        public ElementId DuctTypeId => this.ductTypeId ??= this.Duct!.GetTypeId();

        public double HorizontalOffset { get; set; } = 600.0.milimeter2Feet();

        public double ZOffset { get; set; } = 500.0.milimeter2Feet();

        private double? width;
        public double Width =>this.width??= this.Duct!.LookupParameter("Width").AsDouble();

        private double? height;
        public double Height => this.height ??= this.Duct!.LookupParameter("Height").AsDouble();

        private Duct? mainDuct1;
        public Duct MainDuct1 => this.mainDuct1 ??= this.GetMainDuct1();
       
        private Duct? mainDuct2;
        public Duct MainDuct2 => this.mainDuct2??= this.GetMainDuct2();

        private Duct? middleDuct;
        public Duct MiddleDuct => this.middleDuct ??= this.GetMiddleDuct();
        public Duct? ConnectDuct1 { get; set; }
        public Duct? ConnectDuct2 { get; set; }
    }
}