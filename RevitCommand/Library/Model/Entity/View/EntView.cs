using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntView
    {
        public View? ModelItem { get; set; }

        public EntDocument? Document { get; set; }

        private Document? revitDocument;
        public Document RevitDocument => revitDocument ??= this.GetRevitDocument();

        private IEnumerable<Workset>? hiddenWorksets;
        public IEnumerable<Workset> HiddenWorksets => hiddenWorksets ??= this.GetHiddenWorksets();

        private ElementFilter? modelCategory_FilterSet_Filter;
        public ElementFilter ModelCategory_FilterSet_Filter => modelCategory_FilterSet_Filter ??= this.GetModelCategory_FilterSet_Filter();
    }
}
