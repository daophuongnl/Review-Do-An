using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntCategoryTypeUtil
    {
        //private static BoQData boQData => BoQData.Instance;

        public static List<EntCategoryType> GetStorageList()
        {
            return new List<EntCategoryType>
            {
                EntCategoryType.Beam, EntCategoryType.StrFloor, EntCategoryType.Column, EntCategoryType.StrWall,
                        EntCategoryType.Foundation, EntCategoryType.Ramp, EntCategoryType.StrStair, EntCategoryType.SpreadConcrete, EntCategoryType.ColumnCapital,
                EntCategoryType.ArcFloor, EntCategoryType.ArcWall, EntCategoryType.ArcStair, EntCategoryType.ArcCeiling,
                EntCategoryType.Generic
            };
        }

        public static Dictionary<DisciplineType, List<EntCategoryType>> GetDict()
        {
            var dict = new Dictionary<DisciplineType, List<EntCategoryType>>
            {
                { DisciplineType.Structural, GetStructuralList() },
                { DisciplineType.Architect, GetArchitectList() },
                { DisciplineType.MEP, GetMEPList() }
            };
            return dict;
        }

        public static List<EntCategoryType> GetStructuralList()
        {
            return new List<EntCategoryType>
            {
                EntCategoryType.Beam, EntCategoryType.StrFloor, EntCategoryType.Column, EntCategoryType.StrWall,
                EntCategoryType.Foundation, EntCategoryType.Ramp, EntCategoryType.StrStair
            };
        }

        public static List<EntCategoryType> GetArchitectList()
        {
            return new List<EntCategoryType>
            {
                EntCategoryType.ArcFloor, EntCategoryType.ArcWall, EntCategoryType.ArcStair, EntCategoryType.ArcCeiling,
                EntCategoryType.Door, EntCategoryType.Window
            };
        }

        public static List<EntCategoryType> GetMEPList()
        {
            return new List<EntCategoryType>
            {
                EntCategoryType.Pipe, EntCategoryType.Duct,
                EntCategoryType.PipeFitting, EntCategoryType.DuctFitting,
                EntCategoryType.PipeAccessory, EntCategoryType.DuctAccessory,
                EntCategoryType.Sprinkler,
                EntCategoryType.Equipment, EntCategoryType.Terminal, EntCategoryType.Fixture,

                EntCategoryType.CableTray, EntCategoryType.CableTrayFitting,
                EntCategoryType.Conduit, EntCategoryType.ConduitFitting,
                EntCategoryType.Switch
            };
        }

        public static List<EntCategoryType> GetListForJoin()
        {
            return new List<EntCategoryType>
            {
                EntCategoryType.Beam, EntCategoryType.StrFloor, EntCategoryType.Column, EntCategoryType.StrWall,
                EntCategoryType.Foundation, EntCategoryType.Ramp, EntCategoryType.StrStair, EntCategoryType.Generic
            };
        }

        //public static EntCategoryType GetFirstStructural()
        //{
        //    return boQData.DisciplineType_EntCategoryTypes[DisciplineType.Structural].First();
        //}

        //public static EntCategoryType GetEntCategoryType(this EntElement entElement)
        //{
        //    EntCategoryType entCateType = EntCategoryType.Beam;

        //    if (entElement is EntStrFloor)
        //    {
        //        entCateType = EntCategoryType.StrFloor;
        //    }
        //    else if (entElement is EntStrWall)
        //    {
        //        entCateType = EntCategoryType.StrWall;
        //    }
        //    else if (entElement is EntBeam)
        //    {
        //        entCateType = EntCategoryType.Beam;
        //    }
        //    else if (entElement is EntColumn)
        //    {
        //        entCateType = EntCategoryType.Column;
        //    }
        //    else if (entElement is EntRamp)
        //    {
        //        entCateType = EntCategoryType.Ramp;
        //    }
        //    else if (entElement is EntFoundation)
        //    {
        //        entCateType = EntCategoryType.Foundation;
        //    }
        //    else if (entElement is EntStrStair)
        //    {
        //        entCateType = EntCategoryType.StrStair;
        //    }
        //    else if (entElement is SpreadConcreteElement)
        //    {
        //        entCateType = EntCategoryType.SpreadConcrete;
        //    }
        //    else if (entElement is ColumnCapital)
        //    {
        //        entCateType = EntCategoryType.ColumnCapital;
        //    }

        //    #region Architect

        //    else if (entElement is ArcFloor)
        //    {
        //        entCateType = EntCategoryType.ArcFloor;
        //    }
        //    else if (entElement is ArcWall)
        //    {
        //        entCateType = EntCategoryType.ArcWall;
        //    }
        //    else if (entElement is ArcCeiling)
        //    {
        //        entCateType = EntCategoryType.ArcCeiling;
        //    }
        //    else if (entElement is EntDoor)
        //    {
        //        entCateType = EntCategoryType.Door;
        //    }
        //    else if (entElement is EntWindow)
        //    {
        //        entCateType = EntCategoryType.Window;
        //    }

        //    #endregion

        //    #region MEP
        //    else if (entElement is EntPipe)
        //    {
        //        entCateType = EntCategoryType.Pipe;
        //    }
        //    else if (entElement is EntDuct)
        //    {
        //        entCateType = EntCategoryType.Duct;
        //    }

        //    else if (entElement is PipingFitting)
        //    {
        //        if (entElement.BuiltInCategory == BuiltInCategory.OST_PipeFitting)
        //        {
        //            entCateType = EntCategoryType.PipeFitting;
        //        }
        //        else if (entElement.BuiltInCategory == BuiltInCategory.OST_DuctFitting)
        //        {
        //            entCateType = EntCategoryType.DuctFitting;
        //        }
        //    }

        //    else if (entElement is Accessory)
        //    {
        //        if (entElement.BuiltInCategory == BuiltInCategory.OST_PipeAccessory)
        //        {
        //            entCateType = EntCategoryType.PipeAccessory;
        //        }
        //        else if (entElement.BuiltInCategory == BuiltInCategory.OST_DuctAccessory)
        //        {
        //            entCateType = EntCategoryType.DuctAccessory;
        //        }
        //    }

        //    else if (entElement is EntEquipment)
        //    {
        //        entCateType = EntCategoryType.Equipment;
        //    }
        //    else if (entElement is EntFixture)
        //    {
        //        entCateType = EntCategoryType.Fixture;
        //    }
        //    else if (entElement is EntSprinkler)
        //    {
        //        entCateType = EntCategoryType.Sprinkler;
        //    }
        //    else if (entElement is EntAirTerminal)
        //    {
        //        entCateType = EntCategoryType.Terminal;
        //    }

        //    #endregion

        //    else
        //    {
        //        entCateType = EntCategoryType.Generic;
        //    }
        //    return entCateType;
        //}

        #region Method

        public static string GetName(this EntCategoryType entCategoryType)
        {
            string name = "";
            switch (entCategoryType)
            {
                #region Structural

                case EntCategoryType.Beam:
                    name = "Dầm";
                    break;
                case EntCategoryType.Column:
                    name = "Cột";
                    break;
                case EntCategoryType.StrFloor:
                    name = "Sàn";
                    break;
                case EntCategoryType.Foundation:
                    name = "Móng";
                    break;
                case EntCategoryType.Ramp:
                    name = "Ramp dốc";
                    break;
                case EntCategoryType.StrStair:
                    name = "Cầu thang";
                    break;
                case EntCategoryType.StrWall:
                    name = "Vách";
                    break;
                case EntCategoryType.SpreadConcrete:
                    name = "Bê tông lèn";
                    break;
                case EntCategoryType.ColumnCapital:
                    name = "Mũ cột";
                    break;

                #endregion

                #region Architect

                case EntCategoryType.ArcFloor:
                    name = "Sàn";
                    break;
                case EntCategoryType.ArcWall:
                    name = "Tường";
                    break;
                case EntCategoryType.ArcStair:
                    name = "Cầu thang";
                    break;
                case EntCategoryType.ArcCeiling:
                    name = "Trần";
                    break;
                case EntCategoryType.Door:
                    name = "Cửa";
                    break;
                case EntCategoryType.Window:
                    name = "Cửa sổ";
                    break;

                #endregion

                #region MEP

                case EntCategoryType.Pipe:
                    name = "Ống nước";
                    break;
                case EntCategoryType.Duct:
                    name = "Ống gió";
                    break;

                case EntCategoryType.PipeFitting:
                    name = "Phụ kiện ống nước";
                    break;
                case EntCategoryType.DuctFitting:
                    name = "Phụ kiện ống gió";
                    break;

                case EntCategoryType.PipeAccessory:
                    name = "Van nước";
                    break;

                case EntCategoryType.DuctAccessory:
                    name = "Van gió";
                    break;

                case EntCategoryType.Equipment:
                    name = "Thiết bị";
                    break;
                case EntCategoryType.Fixture:
                    name = "Thiết bị";
                    break;
                case EntCategoryType.Terminal:
                    name = "Miệng gió";
                    break;
                case EntCategoryType.Sprinkler:
                    name = "Đầu phun";
                    break;

                case EntCategoryType.CableTray:
                    name = "Máng cáp điện";
                    break;
                case EntCategoryType.CableTrayFitting:
                    name = "Phụ kiện máng cáp điện";
                    break;
                case EntCategoryType.Conduit:
                    name = "Ống điện";
                    break;
                case EntCategoryType.ConduitFitting:
                    name = "Phụ kiện ống điện";
                    break;
                case EntCategoryType.Switch:
                    name = "Công tắc";
                    break;

                #endregion

                case EntCategoryType.Generic:
                    name = "Generic";
                    break;
            }
            return name;
        }

        public static BuiltInCategory GetBuiltInCategory(this EntCategoryType entCategoryType)
        {
            BuiltInCategory? bic = null;

            switch (entCategoryType)
            {
                case EntCategoryType.Beam:
                    bic = BuiltInCategory.OST_StructuralFraming;
                    break;
                case EntCategoryType.StrFloor:
                    bic = BuiltInCategory.OST_Floors;
                    break;
                case EntCategoryType.Column:
                    bic = BuiltInCategory.OST_StructuralColumns;
                    break;
                case EntCategoryType.StrWall:
                    bic = BuiltInCategory.OST_Walls;
                    break;
                case EntCategoryType.Foundation:
                    bic = BuiltInCategory.OST_StructuralFoundation;
                    break;
                case EntCategoryType.Ramp:
                    bic = BuiltInCategory.OST_Ramps;
                    break;
                case EntCategoryType.StrStair:
                    bic = BuiltInCategory.OST_Stairs;
                    break;
                case EntCategoryType.Generic:
                    bic = BuiltInCategory.OST_GenericModel;
                    break;

                case EntCategoryType.Door:
                    bic = BuiltInCategory.OST_Doors;
                    break;
                case EntCategoryType.Window:
                    bic = BuiltInCategory.OST_Windows;
                    break;
                case EntCategoryType.DuctFitting:
                    bic = BuiltInCategory.OST_DuctFitting;
                    break;
            }

            return bic!.Value;
        }

        //public static string GetSpecificName(this EntCategoryType categoryType, MassType? massType2)
        //{
        //    string specificName = null;

        //    if (massType2 == null)
        //    {
        //        return categoryType.GetName();
        //    }
        //    var massType = massType2.Value;

        //    var disciplineType = boQData.SelectedSetting_DisciplineType;
        //    var dict = boQData.DisciplineType_MassType_CategoryType_Names_Dictionary[disciplineType];

        //    if (dict.MassType_CategoryType_Names.Any(x => x.MassType == massType))
        //    {
        //        var categoryType_Items = dict[massType];
        //        if (categoryType_Items.CategoryType_Names.Any(x => x.CategoryType == categoryType))
        //        {
        //            specificName = categoryType_Items[categoryType].Name;
        //            return specificName;
        //        }
        //    }

        //    if (specificName == null)
        //    {
        //        specificName = categoryType.GetName();
        //    }

        //    return specificName;
        //}

        public static bool IsEqual(this List<EntCategoryType> cateTypes1, List<EntCategoryType> cateTypes2)
        {
            if (cateTypes1 == null || cateTypes2 == null)
            {
                return false;
            }
            return cateTypes1.Count == cateTypes2.Count && cateTypes1.All(x => cateTypes2.Contains(x));
        }

        #endregion
    }
}
