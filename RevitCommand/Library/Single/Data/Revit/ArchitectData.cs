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

namespace SingleData
{
    public class ArchitectData : DataBase
    {
        private static ArchitectData? instance;
        public static ArchitectData Instance
        {
            get => instance = new ArchitectData();
            set => instance = value;
        }

        // property
        // filter
        private Func<Element, bool>? architectInstanceFilter;
        public Func<Element, bool> ArchitectInstanceFilter
        {
            get => architectInstanceFilter ??= x => (x is Wall && (x.LookupParameter("Structural") == null || x.ParameterAsInteger("Structural") != 1)) ||
                        (x is Floor && (x.LookupParameter("Structural") == null || x.ParameterAsInteger("Structural") != 1))
                        || (x is FamilyInstance &&
                        (x.Category.IsEqual(BuiltInCategory.OST_Doors) || x.Category.IsEqual(BuiltInCategory.OST_Windows)));
        }

        //
        private IEnumerable<ElementType>? allArchitectTypes;
        public IEnumerable<ElementType> AllArchitectTypes
        {
            get => allArchitectTypes ??= TypeElements
                        .Where(x => x is WallType ||
                        (x is FloorType && (x as FloorType)?.StructuralMaterialId == null && x.Category.IsEqual(BuiltInCategory.OST_Floors))
                        || x is CeilingType
                        || (x is FamilySymbol &&
                        (x.Category.IsEqual(BuiltInCategory.OST_Doors) || x.Category.IsEqual(BuiltInCategory.OST_Windows))));
            set =>allArchitectTypes = value;
        }

        private IEnumerable<WallType>? wallTypes;
        public IEnumerable<WallType> WallTypes
        {
            get => wallTypes ??= AllArchitectTypes.OfType<WallType>();
            set=>wallTypes = value;
        }

        private IEnumerable<FloorType>? floorTypes;
        public IEnumerable<FloorType> FloorTypes
        {
            get => floorTypes ??= AllArchitectTypes.OfType<FloorType>();
            set => floorTypes = value;
        }

        private IEnumerable<CeilingType>? ceilingTypes;
        public IEnumerable<CeilingType> CeilingTypes
        {
            get => ceilingTypes ??= AllArchitectTypes.OfType<CeilingType>();
            set => ceilingTypes = value;
        }

        private IEnumerable<Element>? allArchitectInstances;
        public IEnumerable<Element> AllArchitectInstances
        {
            get => allArchitectInstances ??= InstanceElements.Where(ArchitectInstanceFilter);
            set => allArchitectInstances = value;
        }

        private IEnumerable<Wall>? walls;
        public IEnumerable<Wall> Walls
        {
            get => walls ??= AllArchitectInstances.OfType<Wall>();
            set => walls = value;
        }

        private IEnumerable<Floor>? floors;
        public IEnumerable<Floor> Floors
        {
            get => floors ??= AllArchitectInstances.OfType<Floor>();
            set => floors = value;
        }

        private IEnumerable<Ceiling>? ceilings;
        public IEnumerable<Ceiling> Ceilings
        {
            get => ceilings ??= AllArchitectInstances.OfType<Ceiling>();
            set => ceilings = value;
        }
    }
}
