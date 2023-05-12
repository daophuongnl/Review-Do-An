using Autodesk.Revit.UI;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utility
{
    public static class EntPanelUtil
    {
        public static EntPanel GetPanel(this EntTab entTab, string name)
        {
            var entPanels = entTab.EntPanels;
            var entPanel = entPanels.SingleOrDefault(x => x.Name == name);
            if (entPanel == null)
            {
                entPanel = new EntPanel
                {
                    EntTab = entTab,
                    Name = name,
                };
                entPanels.Add(entPanel);
            }
            return entPanel;
        }

        // Property
        public static RibbonPanel GetRibbonPanel(this EntPanel entPanel)
        {
            var ribbonPanel = entPanel.Application.GetRibbonPanels(entPanel.EntTab!.Name).SingleOrDefault(x => x.Name == entPanel.Name);

            if (ribbonPanel == null)
            {
                ribbonPanel = entPanel.Application.CreateRibbonPanel(entPanel.EntTab.Name, entPanel.Name);
            }

            return ribbonPanel;
        }

        // Method
        public static void CreatePanel(this EntPanel entPanel)
        {
            var ribbonPanel = entPanel.RibbonPanel;
            foreach (var entPushButton in entPanel.EntPushButtons)
            {
                var pushButton = entPushButton.PushButton;
            }
        }
    }
}
