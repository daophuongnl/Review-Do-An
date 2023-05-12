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
    public class EntPanel
    {
        private IOData ioData => IOData.Instance;

        public EntTab? EntTab { get; set; }

        public UIControlledApplication Application => EntTab!.Application;

        public string? Name { get; set; }

        private List<EntPushButton>? entPushButtons;
        public List<EntPushButton> EntPushButtons => entPushButtons ??= new List<EntPushButton>();

        private RibbonPanel? ribbonPanel;
        public RibbonPanel RibbonPanel => ribbonPanel ??= this.GetRibbonPanel();
    }
}
