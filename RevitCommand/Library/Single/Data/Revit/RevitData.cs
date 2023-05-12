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
using System.Collections;
using System.Windows.Documents;
using Model.Entity;
using Utility;

namespace SingleData
{
    /// <summary>
    /// Các thông tin dữ liệu chung truy xuất từ Revit
    /// </summary>
    public class RevitData
    {
        private static RevitData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static RevitData Instance
        {
            get => instance ??= new RevitData();
            set => instance = value;
        }

        /// <summary>
        /// Thuộc tính chứa thông tin giao diện ứng dụng UIApplication Revit
        /// </summary>
        public UIApplication? UIApplication { get; set; }

        private Application? application;
        /// <summary>
        /// Thuộc tính chứa thông tin dữ liệu ứng dụng Application Revit
        /// </summary>
        public Application Application
        {
            get => application ??= UIApplication!.Application;
            set => application = value;
        }

        private UIDocument? uiDocument;
        /// <summary>
        /// Thuộc tính chứa thông tin giao diện tài liệu UIDocument
        /// </summary>
        public UIDocument UIDocument
        {
            get => uiDocument ??= UIApplication!.ActiveUIDocument;
            set => uiDocument = value;
        }

        private Document? document;
        /// <summary>
        /// Thuộc tính chứa thông tin dữ liệu tài liệu Document hiện hành
        /// </summary>
        public Document Document
        {
            get => document ??= UIDocument.Document;
            set => document = value;
        }

        private List<Document>? linkDocuments;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Document được link vào dự án hiện hành
        /// </summary>
        public List<Document> LinkDocuments
        {
            get
            {
                if (linkDocuments == null)
                {
                    linkDocuments = new List<Document>();
                    foreach (var instance in RevitLinkInstances)
                    {
                        var linkDoc = instance.GetLinkDocument();
                        if (linkDoc == null) continue;
                        if (linkDocuments.Where(x => x.PathName == linkDoc.PathName).Count() == 0)
                        {
                            linkDocuments.Add(linkDoc);
                        }
                    }
                }
                return linkDocuments;
            }
            set => linkDocuments = value;
        }

        private ProjectInfo? projectInfo;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng ProjectInfo thể hiện thông tin dự án hiện hành
        /// </summary>
        public ProjectInfo ProjectInfo
        {
            get => projectInfo ??= Document.ProjectInformation;
            set => projectInfo = value;
        }

        private Selection? selection;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng Selection để phục vụ việc pick chọn đối tượng
        /// </summary>
        public Selection Selection
        {
            get => selection ??= UIDocument.Selection;
            set => selection = value;
        }

        private Transaction? transaction;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng Transaction mặc định, có thể tạo Transaction với tên gọi khác nếu cần thiết
        /// </summary>
        public virtual Transaction Transaction
        {
            get => transaction ??= new Transaction(Document, "Default Transaction");
            set => transaction = value;
        }

        private List<Category>? categories;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Category trong dự án hiện hành
        /// </summary>
        public List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<Category>();
                    foreach (Category cate in Document.Settings.Categories)
                    {
                        (categories as List<Category>).Add(cate);
                    }
                }
                return categories;
            }
            set => categories = value;
        }

        private List<Element>? instanceElements;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Element là Instance trong dự án hiện hành
        /// </summary>
        public List<Element> InstanceElements
        {
            get => instanceElements ??= new FilteredElementCollector(Document).WhereElementIsNotElementType().ToList();
            set => instanceElements = value;
        }

        private List<ElementType>? typeElements;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Element là Type trong dự án hiện hành
        /// </summary>
        public List<ElementType> TypeElements
        {
            get => typeElements ??= new FilteredElementCollector(Document).WhereElementIsElementType().Cast<ElementType>().ToList();
            set => typeElements = value;

        }

        private List<Element>? allElements;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Element trong dự án
        /// </summary>
        public List<Element> AllElements
        {
            get => allElements ??= InstanceElements.Union(TypeElements).ToList();
            set => allElements = value;
        }

        private List<Family>? families;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Family trong dự án hiện hành
        /// </summary>
        public List<Family> Families
        {
            get => families ??= InstanceElements.OfType<Family>().ToList();
            set => families = value;
        }

        private List<FamilySymbol>? familySymbols;
        /// <summary>
        /// Thuộc tính chứa tập hợp các FamilySymbol trong dự án hiện hành
        /// </summary>
        public List<FamilySymbol> FamilySymbols
        {
            get => familySymbols ??= TypeElements.OfType<FamilySymbol>().ToList();
            set => familySymbols = value;
        }

        private List<FamilyInstance>? familyInstances;
        /// <summary>
        /// Thuộc tính chứa tập hợp các FamilyInstance trong dự án hiện hành
        /// </summary>
        public List<FamilyInstance> FamilyInstances
        {
            get => familyInstances ??= InstanceElements.OfType<FamilyInstance>().ToList();
            set => familyInstances = value;
        }

        private List<Level>? levels;
        /// <summary>
        /// Thuộc tính chứa tập hợp các Level trong dự án hiện hành
        /// </summary>
        public List<Level> Levels
        {
            get => levels ??= InstanceElements.OfType<Level>().OrderBy(x => x.Elevation).ToList();
            set => levels = value;
        }

        private List<EntLevel>? entLevels;
        public List<EntLevel> EntLevels
        {
            get => entLevels ??= this.Levels.Select(x => x.GetEntLevel()).ToList();
            set => entLevels = value;
        }

        private BindingMap? parameterBindings;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng ParameterBinding để quản lý các Parameter trong dự án
        /// </summary>
        public BindingMap ParameterBindings
        {
            get => parameterBindings ??= Document.ParameterBindings;
            set => parameterBindings = value;
        }

        private IEnumerable<RevitLinkInstance>? revitLinkInstances;
        /// <summary>
        /// Thuộc tính chứa tập hợp các RevitLinkInstance định nghĩa một thể hiện file Link trong dự án hiện hành
        /// </summary>
        public IEnumerable<RevitLinkInstance> RevitLinkInstances
        {
            get => revitLinkInstances ??= InstanceElements.OfType<RevitLinkInstance>();
            set => revitLinkInstances = value;
        }

        private FailureDefinitionRegistry? failureDefinitionRegistry;
        /// <summary>
        /// Thuộc tính truy xuất đến đối tượng FailureDefinitionRegistry dùng để quản lý tập hợp các Failure của ứng dụng hiện hành
        /// </summary>
        public FailureDefinitionRegistry FailureDefinitionRegistry
        {
            get => failureDefinitionRegistry ??= Application.GetFailureDefinitionRegistry();
            set => failureDefinitionRegistry = value;
        }

        private List<DirectShapeType>? directShapeTypes;
        public List<DirectShapeType> DirectShapeTypes
        {
            get => directShapeTypes ??= TypeElements.OfType<DirectShapeType>().ToList();
            set => directShapeTypes = value;
        }

        private IEnumerable<DirectShape>? directShapes;
        public IEnumerable<DirectShape> DirectShapes
        {
            get => directShapes ??= InstanceElements.OfType<DirectShape>();
        }

        private List<Material>? materials;
        public List<Material> Materials
        {
            get => materials ??= InstanceElements.OfType<Material>().ToList();
            set => materials = value;
        }

        private DirectShapeLibrary? directShapeLibrary;
        public DirectShapeLibrary DirectShapeLibrary
        {
            get => directShapeLibrary ??= DirectShapeLibrary.GetDirectShapeLibrary(Document);
            set => directShapeLibrary = value;
        }

        private Options? geometryOptions;
        public Options GeometryOptions
        {
            get => geometryOptions ??= new Options();
            set => geometryOptions = value;
        }

        public ExternalEvent? ExternalEvent { get; set; }

        private Model.Event.ExternalEventHandler? externalEventHandler;
        public Model.Event.ExternalEventHandler ExternalEventHandler
        {
            get => externalEventHandler ??= new Model.Event.ExternalEventHandler();
        }

        private IEnumerable<RevitLinkType>? revitLinkTypes;
        public IEnumerable<RevitLinkType> RevitLinkTypes
        {
            get => revitLinkTypes ??= TypeElements.OfType<RevitLinkType>();
        }

        private EntDocument? entDocument;
        public EntDocument EntDocument
        {
            get => entDocument ??= this.Document.GetEntDocument();
            set => entDocument = value;
        }

        public bool IsElementForBoQ { get; set; } = false;
    }
}
