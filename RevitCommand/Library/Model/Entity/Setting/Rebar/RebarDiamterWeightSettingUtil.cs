using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class RebarDiamterWeightSettingUtil
    {
        public static RebarDiamterWeightSetting Get()
        {
            var setting = new RebarDiamterWeightSetting();

            setting[RebarDiameter.d6].WeightPerLength = 0.222;
            setting[RebarDiameter.d8].WeightPerLength = 0.395;
            setting[RebarDiameter.d10].WeightPerLength = 0.617;
            setting[RebarDiameter.d12].WeightPerLength = 0.888;
            setting[RebarDiameter.d14].WeightPerLength = 1.210;
            setting[RebarDiameter.d16].WeightPerLength = 1.580;
            setting[RebarDiameter.d18].WeightPerLength = 2.000;
            setting[RebarDiameter.d20].WeightPerLength = 2.470;
            setting[RebarDiameter.d22].WeightPerLength = 2.980;
            setting[RebarDiameter.d25].WeightPerLength = 3.850;
            setting[RebarDiameter.d28].WeightPerLength = 4.840;
            setting[RebarDiameter.d32].WeightPerLength = 6.310;

            return setting;
        }

        #region Property

        #endregion

        #region Method

        public static RebarDiamterWeightItem GetItem(this RebarDiamterWeightSetting setting, double dia)
        {
            var list = setting.Items;
            var item = list.FirstOrDefault(x => x.Diameter.IsEqual(dia));
            if (item == null)
            {
                item = new RebarDiamterWeightItem
                {
                    Diameter = dia
                };
                list.Add(item);
            }
            return item;
        }

        #endregion
    }
}
