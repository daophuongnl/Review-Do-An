using Model.Entity;
using SingleData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Utility
{
    public static class MassTypeUtil
    {
        //private static BoQData boQData => BoQData.Instance;

        public static List<MassType> GetStorageList()
        {
            return new List<MassType>
            {
                MassType.Concrete, MassType.Formwork, MassType.Formwork_Bottom, MassType.Rebar, MassType.BrickWork, MassType.FloorFinish, MassType.PaintWork, MassType.PlasterWork
            };
        }

        public static Dictionary<DisciplineType, List<MassType>> GetDict()
        {
            var disciplineType_MassTypes = new Dictionary<DisciplineType, List<MassType>>();

            disciplineType_MassTypes.Add(DisciplineType.Structural, GetStructuralList());
            disciplineType_MassTypes.Add(DisciplineType.Architect, GetArchitectList());
            disciplineType_MassTypes.Add(DisciplineType.MEP, GetMEPList());

            return disciplineType_MassTypes;
        }

        public static List<MassType> GetStructuralList()
        {
            return new List<MassType>
            {
                MassType.Concrete, MassType.LeanConcrete, MassType.Formwork, MassType.Rebar,
                //MassType.LeanConcrete, MassType.Formwork_Bottom
            };
        }

        public static List<MassType> GetArchitectList()
        {
            return new List<MassType>
            {
                MassType.BrickWork, MassType.PlasterWork, MassType.Lintel, MassType.SubColumn, MassType.FloorFinish, MassType.PaintWork
            };
        }

        public static List<MassType> GetMEPList()
        {
            return new List<MassType>
            {
                MassType.Electrical, MassType.Plumbing, MassType.HVAC, MassType.FireFighting
            };
        }

        //public static List<MassType> GetValidListForExport()
        //{
        //    var validMassTypesForExport = new List<MassType>();

        //    var massTypeCheckList = boQData.SelectedDisciplineType_MassTypeCheckList.MassTypeCheckList;
        //    var entPackages = boQData.SelectedEntProject.EntPackages;
        //    foreach (var entPackage in entPackages)
        //    {
        //        foreach (var mt_clb in entPackage.MassType_CategoryLevelBindings)
        //        {
        //            var massType = mt_clb.MassType;
        //            if (massTypeCheckList.Contains(massType))
        //            {
        //                validMassTypesForExport.Add(massType);
        //            }

        //            // Kiểm tra vừa đủ và thấy số lượng MassType cần lấy có trong các EntPackage vừa kiểm tra
        //            if (validMassTypesForExport.Count == massTypeCheckList.Count)
        //            {
        //                goto End;
        //            }
        //        }
        //    }
        //End:
        //    return validMassTypesForExport;
        //}

        #region Property

        public static string GetName(this MassType massType)
        {
            return $"Công tác {massType.GetShortName()}";
        }

        public static string? GetShortName(this MassType massType)
        {
            string? name = null;
            switch (massType)
            {
                // Structural
                case MassType.Concrete:
                    name = "Bê tông";
                    break;
                case MassType.Formwork:
                    name = "Ván khuôn";
                    break;
                case MassType.Formwork_Bottom:
                    name = "Ván khuôn mặt đáy";
                    break;
                case MassType.Rebar:
                    name = "Cốt thép";
                    break;
                case MassType.LeanConcrete:
                    name = "Bê tông lót";
                    break;

                // Architect
                case MassType.BrickWork:
                    name = "Xây";
                    break;
                case MassType.PlasterWork:
                    name = "Tô, trát";
                    break;
                case MassType.Lintel:
                    name = "Lanh tô";
                    break;
                case MassType.SubColumn:
                    name = "Bổ trụ";
                    break;
                case MassType.PaintWork:
                    name = "Sơn";
                    break;
                case MassType.FloorFinish:
                    name = "Sàn hoàn thiện";
                    break;

                // MEP
                case MassType.Electrical:
                    name = "Điện";
                    break;
                case MassType.Plumbing:
                    name = "Cấp thoát nước";
                    break;
                case MassType.HVAC:
                    name = "HVAC";
                    break;
                case MassType.FireFighting:
                    name = "Chữa cháy";
                    break;

                // undefined
                case MassType.Undefined:
                    name = "Chưa xác định";
                    break;
            }

            return name;
        }

        #endregion

    }
}
