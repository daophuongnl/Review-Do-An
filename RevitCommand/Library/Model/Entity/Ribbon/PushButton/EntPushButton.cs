using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Utility;

namespace Model.Entity
{
    public class EntPushButton
    {
        private RibbonData ribbonData => RibbonData.Instance;
        private IOData ioData => IOData.Instance;

        public string? Name { get; set; }

        public EntPanel? EntPanel { get; set; }

        public UIControlledApplication Application => EntPanel!.Application;

        private string? text;
        public string? Text
        {
            get => text ??= Name;
            set=>text = value;
        }

        public string? IconName { get; set; }

        private string? iconPath;
        public string IconPath => iconPath ??= Path.Combine(ioData.IconDirectoryPath, $"{IconName}.ico");

        public string AssemblyName => ribbonData.AddinFilePath;

        public string? CommandName { get; set; }

        private string? className;
        public string ClassName => className ??= CommandName!;

        private ImageSource? largeImage;
        public ImageSource? LargeImage => largeImage ??= this.GetLargeImage();

        private PushButton? pushButton;
        public PushButton PushButton => pushButton ??= this.GetPushButton();
    }
}
