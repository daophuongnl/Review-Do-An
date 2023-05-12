using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using Utility;

namespace Model.Entity
{
    public class EntTab
    {
        private IOData ioData => IOData.Instance;
        private RibbonData ribbonData => RibbonData.Instance;

        public UIControlledApplication Application => ribbonData.Application!;

        public string? Name { get; set; }

        private List<EntPanel>? entPanels;
        public List<EntPanel> EntPanels => entPanels ??= new List<EntPanel>();
    }
}
