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
    public static class EntPushButtonUtil
    {
        public static EntPushButton GetPushButton(this EntPanel entPanel, string name, string commandName, string iconName)
        {
            var entPushButtons = entPanel.EntPushButtons;
            var entPushButton = entPushButtons.SingleOrDefault(x => x.Name == name);
            if (entPushButton == null)
            {
                entPushButton = new EntPushButton
                {
                    EntPanel = entPanel,
                    Name = name,
                    CommandName = commandName,
                    IconName = iconName
                };
                entPushButtons.Add(entPushButton);
            }
            return entPushButton;
        }

        #region Property
        public static ImageSource? GetLargeImage(this EntPushButton entPushButton)
        {
            if (entPushButton.IconName == null)
            {
                return null;
            }

            var bitmapImage = new BitmapImage(new Uri(entPushButton.IconPath));
            return bitmapImage;
        }

        public static PushButton GetPushButton(this EntPushButton entPushButton)
        {
            var ribbonPanel = entPushButton.EntPanel!.RibbonPanel;

            var pbd = new PushButtonData(entPushButton.Name, entPushButton.Text, entPushButton.AssemblyName, entPushButton.ClassName);

            var pb = ribbonPanel.AddItem(pbd) as PushButton;
            pb!.LargeImage = entPushButton.LargeImage;

            return pb;
        }
        #endregion
    }
}
