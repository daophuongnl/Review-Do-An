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
using Autodesk.Revit.DB.Architecture;

namespace SingleData
{
    public class StructuralData : DataBase
    {
        private static StructuralData? instance;
        public static StructuralData Instance
        {
            get => instance ??=new StructuralData();
            set=>instance = value;
        }

        #region Filter

        private Func<Element, bool>? structuralTypeFilter;
        public Func<Element, bool> StructuralTypeFilter
        {
            get
            {
                if (structuralTypeFilter == null)
                {
                    structuralTypeFilter = x => x is WallType || x is StairsType ||
                        (x is FloorType && (x as FloorType)?.StructuralMaterialId != null) ||
                        (x is FamilySymbol &&
                        (x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming)
                        || x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns)
                        || x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation)
                        || x.Category.IsEqual(BuiltInCategory.OST_Ramps)
                        || x.Category.IsEqual(BuiltInCategory.OST_Stairs)
                        || x.Category.IsEqual(BuiltInCategory.OST_GenericModel)));
                }
                return structuralTypeFilter;
            }
        }

        private Func<Element, bool>? wallTypeFilter;
        public Func<Element, bool> WallTypeFilter
        {
            get => wallTypeFilter ??= x => x is WallType;
        }

        private Func<Element, bool>? floorTypeFilter;
        public Func<Element, bool> FloorTypeFilter
        {
            get => floorTypeFilter ??= x => x is FloorType && x.Category.IsEqual(BuiltInCategory.OST_Floors);
        }

        private Func<Element, bool>? foundationSlabTypeFilter;
        public Func<Element, bool> FoundationSlabTypeFilter
        {
            get => foundationSlabTypeFilter ??= x => x is FloorType && x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation);
        }

        private Func<Element, bool>? columnTypeFilter;
        public Func<Element, bool> ColumnTypeFilter
        {
            get => columnTypeFilter ??= x => x is FamilySymbol && x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns);
        }

        private Func<Element, bool>? beamTypeFilter;
        public Func<Element, bool> BeamTypeFilter
        {
            get => beamTypeFilter ??= x => x is FamilySymbol && x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming);
        }

        private Func<Element, bool>? familyFoundationTypeFilter;
        public Func<Element, bool> FamilyFoundationTypeFilter
        {
            get => familyFoundationTypeFilter ??= x => x is FamilySymbol && x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation);
        }

        private Func<Element, bool>? foundationTypeFilter;
        public Func<Element, bool> FoundationTypeFilter
        {
            get => foundationTypeFilter ??= x => FoundationSlabTypeFilter(x) || FamilyFoundationTypeFilter(x);
        }

        private Func<Element, bool>? structuralInstanceFilter;
        public Func<Element, bool> StructuralInstanceFilter
        {
            get
            {
                if (structuralInstanceFilter == null)
                {
                    structuralInstanceFilter = x => (x is Wall && (x.LookupParameter("Structural") != null && x.ParameterAsInteger("Structural") == 1))
                            || x is Stairs ||
                            (x is Floor && (x.LookupParameter("Structural") != null && x.ParameterAsInteger("Structural") == 1)) ||
                            (x is FamilyInstance &&
                            (x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns) ||
                            x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming) ||
                            x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation) ||
                            x.Category.IsEqual(BuiltInCategory.OST_Ramps) ||
                            x.Category.IsEqual(BuiltInCategory.OST_Stairs) ||
                            x.Category.IsEqual(BuiltInCategory.OST_GenericModel)));
                }
                return structuralInstanceFilter;
            }
        }

        private Func<Element, bool>? wallFilter;
        public Func<Element, bool> WallFilter
        {
            get => wallFilter ??= x => x is Wall;
        }

        private Func<Element, bool>? floorFilter;
        public Func<Element, bool> FloorFilter
        {
            get => floorFilter ??= x => x is Floor && x.Category.IsEqual(BuiltInCategory.OST_Floors);
        }

        private Func<Element, bool>? foundationSlabFilter;
        public Func<Element, bool> FoundationSlabFilter
        {
            get => foundationSlabFilter ??= x => x is Floor && x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation);
        }

        private Func<Element, bool>? columnFilter;
        public Func<Element, bool> ColumnFilter
        {
            get => columnFilter ??= x => x is FamilyInstance && x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns);
        }

        private Func<Element, bool>? beamFilter;
        public Func<Element, bool> BeamFilter
        {
            get => beamFilter ??= x => x is FamilyInstance && x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming);
        }

        private Func<Element, bool>? familyFoundationFilter;
        public Func<Element, bool> FamilyFoundationFilter
        {
            get => familyFoundationFilter ??= x => x is FamilyInstance && x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation);
        }

        private Func<Element, bool>? foundationFilter;
        public Func<Element, bool> FoundationFilter
        {
            get => foundationFilter ??= x => FoundationSlabFilter(x) || FamilyFoundationFilter(x);
        }

        #endregion


        private IEnumerable<ElementType>? allStrucutralTypes;
        public IEnumerable<ElementType> AllStructuralTypes
        {
            get => allStrucutralTypes ??= TypeElements.Where(StructuralTypeFilter).Cast<ElementType>();
            set => allStrucutralTypes = value;
        }

        private IEnumerable<WallType>? wallTypes;
        public IEnumerable<WallType> WallTypes
        {
            get => wallTypes = AllStructuralTypes.OfType<WallType>();
            set => wallTypes = value;
        }

        private List<FloorType>? floorTypes;
        public List<FloorType> FloorTypes
        {
            get => floorTypes ??= AllStructuralTypes.OfType<FloorType>().Where(x => x.Category.IsEqual(BuiltInCategory.OST_Floors)).ToList();
            set => floorTypes = value;
        }

        private List<FloorType>? foundationSlabTypes;
        public List<FloorType> FoundationSlabTypes
        {
            get => foundationSlabTypes ??= AllStructuralTypes.OfType<FloorType>()
                        .Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation)).ToList();
            set =>  foundationSlabTypes = value;
        }

        private IEnumerable<FamilySymbol>? strFamilySymbols;
        public IEnumerable<FamilySymbol> StrFamilySymbols
        {
            get => strFamilySymbols ??= AllStructuralTypes.OfType<FamilySymbol>();
            set =>strFamilySymbols = value;
        }

        private IEnumerable<FamilySymbol>? columnTypes;
        public IEnumerable<FamilySymbol> ColumnTypes
        {
            get => columnTypes ??= StrFamilySymbols.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns));
            set=>columnTypes = value;
        }

        private IEnumerable<FamilySymbol>? beamTypes;
        public IEnumerable<FamilySymbol> BeamTypes
        {
            get => beamTypes ??= StrFamilySymbols.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming));
            set=> beamTypes = value;
        }

        private IEnumerable<FamilySymbol>? foundationTypes;
        public IEnumerable<FamilySymbol> FoundationTypes
        {
            get => foundationTypes ??= StrFamilySymbols.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation));
            set => foundationTypes = value;
        }

        private IEnumerable<Element>? allStructuralInstances;
        public IEnumerable<Element> AllStructuralInstances
        {
            get => allStructuralInstances ??= InstanceElements.Where(StructuralInstanceFilter);
            set => allStructuralInstances = value;
        }

        private IEnumerable<Wall>? walls;
        public IEnumerable<Wall> Walls
        {
            get => walls ??= AllStructuralInstances.OfType<Wall>();
            set => walls = value;
        }

        private List<Floor>? floors;
        public List<Floor> Floors
        {
            get => floors ??= AllStructuralInstances.OfType<Floor>().Where(x => x.Category.IsEqual(BuiltInCategory.OST_Floors)).ToList();
            set => floors = value;
        }

        private List<Floor>? foundationSlabs;
        public List<Floor> FoundationSlabs
        {
            get => foundationSlabs ??= AllStructuralInstances.OfType<Floor>()
                        .Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation)).ToList();
            set => foundationSlabs = value;
        }

        private IEnumerable<FamilyInstance>? strFamilyInstances;
        public IEnumerable<FamilyInstance> StrFamilyInstances
        {
            get => strFamilyInstances ??= AllStructuralInstances.OfType<FamilyInstance>();
            set => strFamilyInstances = value;
        }

        private IEnumerable<FamilyInstance>? columns;
        public IEnumerable<FamilyInstance> Columns
        {
            get => columns ??= StrFamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralColumns));
            set => columns = value;
        }

        private IEnumerable<FamilyInstance>? beams;
        public IEnumerable<FamilyInstance> Beams
        {
            get => beams ??= StrFamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFraming));
            set => beams = value;
        }

        private IEnumerable<FamilyInstance>? foundations;
        public IEnumerable<FamilyInstance> Foundations
        {
            get => foundations ??= StrFamilyInstances.Where(x => x.Category.IsEqual(BuiltInCategory.OST_StructuralFoundation));
            set => foundations = value;
        }
    }
}
