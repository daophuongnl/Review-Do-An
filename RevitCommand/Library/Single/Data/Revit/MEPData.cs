using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Utility;
using System.Collections;
using System.Windows.Media.Animation;
using System.Windows.Documents;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;

namespace SingleData
{
    /// <summary>
    /// Các thông tin dữ liệu bộ môn Kết cấu truy xuất từ Revit 
    /// </summary>
    public class MEPData : DataBase
    {
        private static MEPData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static MEPData Instance
        {
            get => instance ??= new MEPData();
            set => instance = value;
        }

        // filter
        private Func<Element, bool>? mepInstanceFilter;
        public Func<Element, bool> MEPInstanceFilter => mepInstanceFilter ??= x => MechanicalPipeFilter(x) || ElectricalFilter(x);

        private Func<Element, bool>? mechanicalPipeFilter;
        public Func<Element, bool> MechanicalPipeFilter => mechanicalPipeFilter ??= x => (x is Pipe || x is FlexPipe || x is Duct || x is FlexDuct
                        || (x is FamilyInstance &&
                        (x.Category.IsEqual(BuiltInCategory.OST_PipeFitting) || x.Category.IsEqual(BuiltInCategory.OST_DuctFitting)
                        || x.Category.IsEqual(BuiltInCategory.OST_PipeAccessory) || x.Category.IsEqual(BuiltInCategory.OST_DuctAccessory)

                        || x.Category.IsEqual(BuiltInCategory.OST_PlumbingFixtures) || x.Category.IsEqual(BuiltInCategory.OST_LightingFixtures)
                        || x.Category.IsEqual(BuiltInCategory.OST_ElectricalFixtures)

                        || x.Category.IsEqual(BuiltInCategory.OST_Sprinklers)
                        || x.Category.IsEqual(BuiltInCategory.OST_DuctTerminal)

                        || x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment))));

        private Func<Element, bool>? electricalFilter;
        public Func<Element, bool> ElectricalFilter => electricalFilter ??= x => (x.Category != null &&
                         (x.Category.IsEqual(BuiltInCategory.OST_CableTray) || x.Category.IsEqual(BuiltInCategory.OST_CableTrayFitting)
                         || x.Category.IsEqual(BuiltInCategory.OST_Conduit) || x.Category.IsEqual(BuiltInCategory.OST_ConduitFitting)

                         || x.Category.IsEqual(BuiltInCategory.OST_ElectricalEquipment)
                         || x.Category.IsEqual(BuiltInCategory.OST_ElectricalFixtures)
                         || x.Category.IsEqual(BuiltInCategory.OST_DataDevices)
                         || x.Category.IsEqual(BuiltInCategory.OST_LightingFixtures)
                         || x.Category.IsEqual(BuiltInCategory.OST_LightingDevices)
                         || x.Category.IsEqual(BuiltInCategory.OST_CommunicationDevices)
                         || x.Category.IsEqual(BuiltInCategory.OST_FireAlarmDevices)
                         || x.Category.IsEqual(BuiltInCategory.OST_SecurityDevices)));

        // TypeFilter
        private Func<ElementType, bool>? mepTypeFilter;
        public Func<ElementType, bool> MEPTypeFilter => mepTypeFilter ??= x => MechanicalPipeTypeFilter(x) || ElectricalTypeFilter(x);

        private Func<ElementType, bool>? mechanicalPipeTypeFilter;
        public Func<ElementType, bool> MechanicalPipeTypeFilter => mechanicalPipeTypeFilter ??= x => (x is PipeType || x is FlexPipeType || x is DuctType || x is FlexDuctType
                        || (x is FamilySymbol &&
                        (x.Category.IsEqual(BuiltInCategory.OST_PipeFitting) || x.Category.IsEqual(BuiltInCategory.OST_DuctFitting)
                        || x.Category.IsEqual(BuiltInCategory.OST_PipeAccessory) || x.Category.IsEqual(BuiltInCategory.OST_DuctAccessory)

                        || x.Category.IsEqual(BuiltInCategory.OST_PlumbingFixtures) || x.Category.IsEqual(BuiltInCategory.OST_LightingFixtures)
                        || x.Category.IsEqual(BuiltInCategory.OST_ElectricalFixtures)

                        || x.Category.IsEqual(BuiltInCategory.OST_Sprinklers)
                        || x.Category.IsEqual(BuiltInCategory.OST_DuctTerminal)

                        || x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment))));

        private Func<ElementType, bool>? electricalTypeFilter;
        public Func<ElementType, bool> ElectricalTypeFilter => electricalTypeFilter ??= x => (x.Category != null &&
                        (x.Category.IsEqual(BuiltInCategory.OST_CableTray) || x.Category.IsEqual(BuiltInCategory.OST_CableTrayFitting)
                        || x.Category.IsEqual(BuiltInCategory.OST_Conduit) || x.Category.IsEqual(BuiltInCategory.OST_ConduitFitting)

                        || x.Category.IsEqual(BuiltInCategory.OST_ElectricalEquipment)
                        || x.Category.IsEqual(BuiltInCategory.OST_ElectricalFixtures)
                        || x.Category.IsEqual(BuiltInCategory.OST_DataDevices)
                        || x.Category.IsEqual(BuiltInCategory.OST_LightingFixtures)
                        || x.Category.IsEqual(BuiltInCategory.OST_LightingDevices)
                        || x.Category.IsEqual(BuiltInCategory.OST_CommunicationDevices)
                        || x.Category.IsEqual(BuiltInCategory.OST_FireAlarmDevices)
                        || x.Category.IsEqual(BuiltInCategory.OST_SecurityDevices)));

        //
        private IEnumerable<Element>? allMEPInstances;
        public IEnumerable<Element> AllMEPInstances
        {
            get => allMEPInstances ??= InstanceElements.Where(MEPInstanceFilter);
            set => allMEPInstances = value;
        }

        private IEnumerable<ElementType>? allMEPTypes;
        public IEnumerable<ElementType> AllMEPTypes
        {
            get => allMEPTypes ??= TypeElements.Where(MEPTypeFilter);
            set => allMEPTypes = value;
        }

        #region Old Property

        private IEnumerable<Pipe>? pipes;
        public IEnumerable<Pipe> Pipes => pipes ??= InstanceElements.OfType<Pipe>();

        private IEnumerable<FamilyInstance>? accesories;
        public IEnumerable<FamilyInstance> Accesories => accesories ??= FamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_PipeAccessory) && x.MEPModel != null);

        private IEnumerable<FamilyInstance>? sprinklers;
        public IEnumerable<FamilyInstance> Sprinklers => sprinklers ??= FamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_Sprinklers) && x.MEPModel != null
                        && x.SuperComponent == null);

        private IEnumerable<FamilyInstance>? pipingFittings;
        public IEnumerable<FamilyInstance> PipingFittings => pipingFittings ??= FamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_PipeFitting) && x.MEPModel is MechanicalFitting);

        private IEnumerable<FamilyInstance>? teeFittings;
        public IEnumerable<FamilyInstance> TeeFittings => teeFittings ??= PipingFittings.Where(x => (x.MEPModel as MechanicalFitting)?.PartType == PartType.Tee);

        private IEnumerable<FamilyInstance>? elbowFittings;
        public IEnumerable<FamilyInstance> ElbowFittings => elbowFittings ??= PipingFittings.Where(x => (x.MEPModel as MechanicalFitting)?.PartType == PartType.Elbow);

        private IEnumerable<Duct>? ducts;
        public IEnumerable<Duct> Ducts => ducts ??= InstanceElements.OfType<Duct>();

        private IEnumerable<MEPSystemType>? mepSystemTypes;
        public IEnumerable<MEPSystemType> MEPSystemTypes => mepSystemTypes ??= TypeElements.OfType<MEPSystemType>();

        private IEnumerable<MechanicalSystemType>? mechanicalSystemTypes;
        public IEnumerable<MechanicalSystemType> MechanicalSystemTypes => mechanicalSystemTypes ??= MEPSystemTypes.OfType<MechanicalSystemType>();

        private IEnumerable<PipingSystemType>? pipingSystemTypes;
        public IEnumerable<PipingSystemType> PipingSystemTypes => pipingSystemTypes ??= MEPSystemTypes.OfType<PipingSystemType>();

        private IEnumerable<MEPSystem>? mepSystems;
        public IEnumerable<MEPSystem> MEPSystems => mepSystems ??= InstanceElements.OfType<MEPSystem>();

        #endregion
    }
}
