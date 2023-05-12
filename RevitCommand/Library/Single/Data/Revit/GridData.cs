using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace SingleData
{
    public class GridData
    {
        private static GridData? instance;
        public static GridData Instance
        {
            get => instance ??= new GridData();
            set => instance = value;
        }

        private RevitData revitData => RevitData.Instance;

        private IEnumerable<Grid>? allGrids;
        public IEnumerable<Grid> AllGrids
        {
            get => allGrids ??= revitData.InstanceElements.OfType<Grid>();
            set => allGrids = value;
        }

        private XYZ? basePoint;
        public XYZ? BasePoint
        {
            get => basePoint ??= GridDataUtil.GetBasePoint();
        }
    }
}
