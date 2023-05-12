using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class GraphicsStyleDataUtil
    {
        private static GraphicsStyleData graphicsStyleData => GraphicsStyleData.Instance;

        public static void RefreshDocument()
        {
            graphicsStyleData.GraphicsStyles = null;
            graphicsStyleData.LineStyles = null;

            (graphicsStyleData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (graphicsStyleData as DataBase).RefreshDocument();
        }

        public static void Dispose()
        {
            GraphicsStyleData.Instance = null;
        }
    }
}
