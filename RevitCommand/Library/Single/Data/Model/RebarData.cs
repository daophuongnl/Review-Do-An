using Autodesk.Revit.DB.Structure;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class RebarData : DataBase
    {
        private static RebarData? instance;
        public static RebarData Instance
        {
            get => instance ??= new RebarData();
            set => instance = value;
        }

        private RebarSetting? rebarSetting;
        public RebarSetting RebarSetting => rebarSetting ??= new RebarSetting();

        private IEnumerable<Rebar>? rebars;
        public IEnumerable<Rebar> Rebars
        {
            get => rebars ??= InstanceElements.OfType<Rebar>();
            set => rebars = value;
        }

        private IEnumerable<RebarBarType>? rebarBarTypes;
        public IEnumerable<RebarBarType> RebarBarTypes
        {
            get => rebarBarTypes ??= TypeElements.OfType<RebarBarType>();
            set => rebarBarTypes = value;
        }

        private IEnumerable<RebarShape>? rebarShapes;
        public IEnumerable<RebarShape> RebarShapes
        {
            get => rebarShapes ??= TypeElements.OfType<RebarShape>();
            set => rebarShapes = value;
        }

        private IEnumerable<RebarCoverType>? rebarCoverTypes;
        public IEnumerable<RebarCoverType> RebarCoverTypes
        {
            get => rebarCoverTypes ??= TypeElements.OfType<RebarCoverType>();
            set => rebarCoverTypes = value;
        }

        private ReinforcementSettings? reinforcementSettings;
        public ReinforcementSettings ReinforcementSettings
        {
            get => reinforcementSettings ??= InstanceElements.OfType<ReinforcementSettings>().SingleOrDefault();
            set=>reinforcementSettings = value;
        }

        private RebarRoundingManager? rebarRoundingManager;
        public RebarRoundingManager RebarRoundingManager
        {
            get => rebarRoundingManager ??= ReinforcementSettings.GetRebarRoundingManager();
            set => rebarRoundingManager = value;
        }

        private IEnumerable<Autodesk.Revit.DB.ViewSchedule>? rebarRevitViewSchedules;
        public IEnumerable<Autodesk.Revit.DB.ViewSchedule> RebarRevitViewSchedules
        {
            get => rebarRevitViewSchedules ??= RebarDataUtil.GetAllRebarRevitViewSchedules();
            set => rebarRevitViewSchedules = value;
        }

        private RebarDiamterWeightSetting? diamterWeightSetting;
        public RebarDiamterWeightSetting DiamterWeightSetting
        {
            get => diamterWeightSetting ??= RebarDiamterWeightSettingUtil.Get();
        }
    }
}
