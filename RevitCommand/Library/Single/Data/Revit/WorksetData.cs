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
    public class WorksetData : DataBase
    {
        private static WorksetData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static WorksetData Instance
        {
            get => instance ??= new WorksetData();
            set =>instance = value;
        }

        // property
        private IEnumerable<Workset>? worksets;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Worksets người dùng trong dự án hiện hành
        /// </summary>
        public IEnumerable<Workset> Worksets
        {
            get => worksets ??= new FilteredWorksetCollector(Document).OfKind(WorksetKind.UserWorkset);
            set => worksets = null;
        }

        private WorksetDefaultVisibilitySettings? worksetDefaultVisibilitySettings;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng WorksetDefaultVisibilitySettings dùng để hiệu chỉnh chế độ ẩn/ hiển đối tượng Workset trong dự án hiện hành
        /// </summary>
        public WorksetDefaultVisibilitySettings WorksetDefaultVisibilitySettings
        {
            get => worksetDefaultVisibilitySettings ??= InstanceElements.OfType<WorksetDefaultVisibilitySettings>().FirstOrDefault();
            set => worksetDefaultVisibilitySettings = value;
        }
    }
}
