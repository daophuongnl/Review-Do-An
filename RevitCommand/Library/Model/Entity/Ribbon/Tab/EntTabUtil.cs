using Autodesk.Revit.UI;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utility
{
    public static class EntTabUtil
    {
        private static RibbonData ribbonData => RibbonData.Instance;

        public static EntTab Get(string name)
        {
            var entTabStorageList = ribbonData.EntTabStorageList;
            var entTab = entTabStorageList.SingleOrDefault(x => x.Name == name);

            if (entTab == null)
            {
                entTab = new EntTab { Name = name };
                entTabStorageList.Add(entTab);
            }
            return entTab;
        }

        #region Property
        public static void CreateTab(this EntTab entTab)
        {
            var app = entTab.Application;
            try
            {
                app.CreateRibbonTab(entTab.Name);
            }
            catch { }

            foreach (var entPanel in entTab.EntPanels)
            {
                entPanel.CreatePanel();
            }
        }
        #endregion
    }
}
