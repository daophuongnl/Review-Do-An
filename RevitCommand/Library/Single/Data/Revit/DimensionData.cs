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

namespace SingleData
{
    /// <summary>
    /// Các thông tin dữ liệu về Workset truy xuất từ Revit 
    /// </summary>
    public class DimensionData : DataBase
    {
        private static DimensionData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static DimensionData Instance
        {
            get => instance ??= new DimensionData();
            set => instance = value;
        }

        private IEnumerable<Dimension>? dimensions;
        public IEnumerable<Dimension> Dimensions
        {
            get => dimensions ??= InstanceElements.OfType<Dimension>();
            set => dimensions = value;
        }

        private IEnumerable<Dimension>? linearDimensions;
        public IEnumerable<Dimension> LinearDimensions
        {
            get => linearDimensions ??= Dimensions.Where(x => x.DimensionShape == DimensionShape.Linear);
            set => linearDimensions = value;
        }

        private IEnumerable<DimensionType>? dimensionTypes;
        public IEnumerable<DimensionType> DimensionTypes
        {
            get => dimensionTypes ??= TypeElements.OfType<DimensionType>();
            set => dimensionTypes = value;
        }

        private IEnumerable<DimensionType>? linearDimensionTypes;
        public IEnumerable<DimensionType> LinearDimensionTypes
        {
            get => linearDimensionTypes ??= DimensionTypes.Where(x => x.StyleType == DimensionStyleType.Linear);
            set => linearDimensionTypes = value;
        }
    }
}
