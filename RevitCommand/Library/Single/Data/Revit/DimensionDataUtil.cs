using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class DimensionDataUtil
    {
        private static DimensionData dimensionData
        {
            get
            {
                return DimensionData.Instance;
            }
        }

        public static void RefreshDocument()
        {
            dimensionData.Dimensions = null;
            dimensionData.DimensionTypes = null;
            dimensionData.LinearDimensions = null;
            dimensionData.LinearDimensionTypes = null;

            (dimensionData as DataBase).RefreshDocument();
        }

        public static void RefreshUIDocument()
        {
            RefreshDocument();

            (dimensionData as DataBase).RefreshDocument();
        }

        public static void Dispose()
        {
            DimensionData.Instance = null;
        }
    }
}
