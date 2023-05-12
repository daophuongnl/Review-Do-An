using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class RebarDiamterWeightSetting
    {
        private List<RebarDiamterWeightItem>? items;
        public List<RebarDiamterWeightItem> Items => items ??= new List<RebarDiamterWeightItem>();

        public RebarDiamterWeightItem this[double dia] => this.GetItem(dia);
    }
}
