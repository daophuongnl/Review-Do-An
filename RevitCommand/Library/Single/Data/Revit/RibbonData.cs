using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace SingleData
{
    public class RibbonData
    {
        private static RibbonData? instance;
        public static RibbonData Instance
        {
            get => instance ??= new RibbonData();
            set => instance = value;
        }

        public UIControlledApplication? Application { get; set; }

        private List<EntTab>? entTabStorageList;
        public List<EntTab> EntTabStorageList
        {
            get => entTabStorageList ??= new List<EntTab>();
        }

        private List<EntPanel>? entPanelStorageList;
        public List<EntPanel> EntPanelStorageList
        {
            get => entPanelStorageList ??= new List<EntPanel>();
        }

        private List<EntPushButton>? entPushButtonStorageList;
        public List<EntPushButton> EntPushButtonStorageList
        {
            get => entPushButtonStorageList ??= new List<EntPushButton>();
        }

        private string? addinFilePath;
        public string AddinFilePath
        {
            get => addinFilePath ??= Assembly.GetExecutingAssembly().Location;
        }
    }
}
