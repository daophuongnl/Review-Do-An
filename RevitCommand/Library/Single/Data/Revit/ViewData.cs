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
    /// Các thông tin dữ liệu về View truy xuất từ Revit
    /// </summary>
    public class ViewData : DataBase
    {
        private static ViewData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static ViewData Instance
        {
            get => instance ??= new ViewData();
            set => instance = value;
        }

        private View? activeView;
        /// <summary>
        /// Thuộc tính truy xuất đển View hiện hành của dự án hiện hành
        /// </summary>
        public View ActiveView
        {
            get => activeView ??= Document.ActiveView;
            set => activeView = value;
        }

        private UIView? activeUIView;
        /// <summary>
        /// Thuộc tính truy xuất đến UIVIew là giao diện View của dự án hiện hành
        /// </summary>
        public UIView ActiveUIView
        {
            get => activeUIView ??= UIDocument.GetOpenUIViews().Where(x => x.ViewId.IntegerValue == ActiveView.Id.IntegerValue).FirstOrDefault();
            set => activeUIView = value;
        }

        private Plane? activePlane;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng Plane thể hiện mặt phẳng làm việc hiện hành
        /// </summary>
        public Plane ActivePlane
        {
            get => activePlane ??= Plane.CreateByOriginAndBasis(ActiveView.Origin, ActiveView.RightDirection, ActiveView.UpDirection);
            set => activePlane = value;
        }

        private SketchPlane? activeSketchPlane;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng SketchPlane quy định mặt phẳng sketch hiện hành
        /// </summary>
        public SketchPlane ActiveSketchPlane
        {
            get => activeSketchPlane ??= SketchPlane.Create(Document, ActivePlane);
            set=>activeSketchPlane = value;
        }

        private SketchPlane? workPlane;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng SketchPlane quy định mặt phẳng làm việc hiện hành
        /// </summary>
        public SketchPlane WorkPlane
        {
            get
            {
                if (workPlane == null)
                {
                    workPlane = ActiveView.SketchPlane;
                    if (workPlane == null)
                    {
                        workPlane = ActiveView.SketchPlane = ActiveSketchPlane;
                    }
                }
                return workPlane;
            }
            set => workPlane = value;
        }

        private IEnumerable<FillPatternElement>? fillPatternElements;
        /// <summary>
        /// Thuộc tính chứa tập hợp các FillPatternElement định nghĩa một loại nét hatch bề mặt trong dự án hiện hành
        /// </summary>
        public IEnumerable<FillPatternElement> FillPatternElements
        {
            get => fillPatternElements ??= InstanceElements.OfType<FillPatternElement>();
            set => fillPatternElements = value;
        }

        private IEnumerable<View>? views;
        /// <summary>
        /// Thuộc tính chứa tập hợp các View trong dự án hiện hành
        /// </summary>
        public IEnumerable<View> Views
        {
            get => views ??= InstanceElements.OfType<View>();
            set => views = value;
        }

        private IEnumerable<ViewSchedule>? viewSchedules;
        /// <summary>
        /// Thuộc tính chứa tập hợp các ViewSchedule trong dự án hiện hành
        /// </summary>
        public IEnumerable<ViewSchedule> ViewSchedules
        {
            get => viewSchedules ??= InstanceElements.OfType<ViewSchedule>().Where(x => !x.IsTemplate);
            set => viewSchedules = value;
        }

        private IEnumerable<ViewSheet>? viewSheets;
        /// <summary>
        /// Thuộc tính chứa tập hợp các ViewSheet trong dự án hiện hành
        /// </summary>
        public IEnumerable<ViewSheet> ViewSheets
        {
            get => viewSheets ??= InstanceElements.OfType<ViewSheet>();
            set => viewSheets = value;
        }

        private IEnumerable<Viewport>? viewports;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Viewport trong dự án hiện hành
        /// </summary>
        public IEnumerable<Viewport> Viewports
        {
            get => viewports ??= InstanceElements.OfType<Viewport>();
            set => viewports = value;
        }

        private IEnumerable<ViewSection>? viewSections;
        public IEnumerable<ViewSection> ViewSections
        {
            get => viewSections ??= Views.OfType<ViewSection>().Where(x => x.ViewType == ViewType.Section);
            set => viewSections = value;
        }

        private IEnumerable<ViewSection>? viewDetails;
        public IEnumerable<ViewSection> ViewDetails
        {
            get => viewDetails = Views.OfType<ViewSection>().Where(x => x.ViewType == ViewType.Detail);
            set => viewDetails = value;
        }

        private IEnumerable<ViewFamilyType>? viewFamilyTypes;
        public IEnumerable<ViewFamilyType> ViewFamilyTypes
        {
            get => viewFamilyTypes ??= TypeElements.OfType<ViewFamilyType>();
            set => viewFamilyTypes = value;
        }

        private IEnumerable<ViewFamilyType>? viewSectionTypes;
        public IEnumerable<ViewFamilyType> ViewSectionTypes
        {
            get => viewSectionTypes ??= ViewFamilyTypes.Where(x => x.ViewFamily == ViewFamily.Section);
            set => viewSectionTypes = value;
        }

        private IEnumerable<ViewFamilyType>? viewDetailTypes;
        public IEnumerable<ViewFamilyType> ViewDetailTypes
        {
            get => viewDetailTypes ??= ViewFamilyTypes.Where(x => x.ViewFamily == ViewFamily.Detail);
            set => viewDetailTypes = value;
        }

        private IEnumerable<View>? legends;
        public IEnumerable<View> Legends
        {
            get => legends ??= Views.Where(x => x.ViewType == ViewType.Legend);
        }
    }
}
