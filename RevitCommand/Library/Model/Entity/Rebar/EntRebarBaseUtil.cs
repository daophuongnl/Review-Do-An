using Autodesk.Revit.DB.Structure;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntRebarBaseUtil
    {
        private static RebarData rebarData => RebarData.Instance;

        #region Property

        public static RebarShapeDrivenAccessor GetRebarShapeDrivenAccessor(this EntRebarBase entRebarBase)
        {
            return entRebarBase.RevitRebar!.GetShapeDrivenAccessor();
        }

        public static double GetTotalBarLength(this EntRebarBase entRebarBase)
        {
            return entRebarBase.RevitRebar.TotalLength;
        }

        public static double GetTotalWeightKg(this EntRebarBase entRebarBase)
        {
            var diaSI = entRebarBase.BarDiameter.feet2Meter();
            var dwSetting = rebarData.DiamterWeightSetting;
            return entRebarBase.TotalBarLength * dwSetting[diaSI].WeightPerLength;
        }

        public static string GetPartition(this EntRebarBase entRebarBase)
        {
            return entRebarBase.ParameterDict[ElementParameterName.Partition]!.Value!;
        }

        #endregion
    }
}
