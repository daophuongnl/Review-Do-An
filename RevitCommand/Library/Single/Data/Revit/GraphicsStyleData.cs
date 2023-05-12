using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Utility;

namespace SingleData
{
    public class GraphicsStyleData : DataBase
    {
        private static GraphicsStyleData? instance;
        public static GraphicsStyleData Instance
        {
            get => instance ??= new GraphicsStyleData();
            set => instance = value;
        }

        // property
        private List<GraphicsStyle>? graphicsStyles;
        public List<GraphicsStyle> GraphicsStyles
        {
            get => graphicsStyles ??= InstanceElements.OfType<GraphicsStyle>().ToList();
            set => graphicsStyles = value;
        }

        private List<GraphicsStyle>? lineStyles;
        public List<GraphicsStyle> LineStyles
        {
            get => lineStyles ??= GraphicsStyles.Where(x =>
                          x.GraphicsStyleType == GraphicsStyleType.Projection &&
                          x.GraphicsStyleCategory != null && x.GraphicsStyleCategory.Parent.IsEqual(BuiltInCategory.OST_Lines))
                        .ToList();
            set => lineStyles = value;
        }
    }
}
