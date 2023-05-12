using Autodesk.Revit.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace Model.Entity
{
    public class EntElement
    {
        protected Autodesk.Revit.DB.Element? revitElement;
        public virtual Autodesk.Revit.DB.Element? RevitElement
        {
            get => revitElement;
            set => revitElement = value;
        }

        protected Autodesk.Revit.DB.ElementType? elementType;
        public virtual Autodesk.Revit.DB.ElementType ElementType
        {
            get => elementType ??= this.GetElementType();
            set => elementType = value;
        }

        protected string? typeName;
        public virtual string TypeName => typeName ??= this.GetTypeName();

        private ElementId? elementId_Obj;
        public ElementId ElementId_Obj => elementId_Obj ??= RevitElement!.Id;

        private int? elementId;
        public int ElementId => elementId ??= RevitElement!.Id.IntegerValue;

        private string? guid;
        public string? GUID => guid ??= this.GetGUID();

        private string? name;
        public string? Name => name ??= this.GetName();

        protected EntSolid? entSolid;
        public virtual EntSolid EntSolid => entSolid = this.GetElementEntSolid(false);

        protected BoundingBoxXYZ? boundingBoxXYZ;
        public virtual BoundingBoxXYZ BoundingBoxXYZ => (boundingBoxXYZ ??= this.GetBoundingBoxXYZ())!;

        private EntLocation? entLocation;
        public EntLocation EntLocation => entLocation ??= this.GetEntLocation();

        protected Transform? purgeTransform;
        public virtual Transform PurgeTransform => purgeTransform ??= EntLocation.PurgeTransform;

        //protected EntCategoryType? entCategoryType;
        //public virtual EntCategoryType EntCategoryType
        //{
        //    get
        //    {
        //        if (entCategoryType == null)
        //        {
        //            entCategoryType = this.GetEntCategoryType();
        //        }
        //        return entCategoryType.Value;
        //    }
        //}

        private EntFamily? entFamily;
        public EntFamily EntFamily => entFamily ??= this.GetEntFamily();

        protected EntTransform? entTransform;
        public virtual EntTransform EntTransform => entTransform ??= this.GetEntElementTransform();

        protected XYZ? centerPoint;
        public virtual XYZ CenterPoint => centerPoint ??= this.GetCenterPoint();

        private Document? revitDocument;
        public Document RevitDocument => revitDocument ??= this.GetRevitDocument();

        private EntDocument? document;
        public EntDocument? Document => document ??= this.GetDocument();

        private Family? family;
        public Family? Family => family ??= this.GetFamily();

        private string? familyName;
        public string? FamilyName => familyName ??= this.GetFamilyName();

        private BuiltInCategory? builtInCategory;
        public BuiltInCategory? BuiltInCategory => builtInCategory ??= this.GetBuiltInCategory();

        public bool IsGetCategory { get; set; } = false;

        private Category? category;
        public Category? Category => category ??= this.GetCategory();

        private string? categoryName;
        public string? CategoryName => categoryName ??= Category?.Name;

        public Func<string?>? Override_GetRefinedCategoryName { get; set; }

        private string? refinedCategoryName;
        public string? RefinedCategoryName
        {
            get
            {
                if (this.Override_GetRefinedCategoryName != null)
                {
                    return this.Override_GetRefinedCategoryName();
                }

                return refinedCategoryName ??= this.GetRefinedCategoryName();
            }
        }

        private EntParameterDict? parameterDict;
        public EntParameterDict ParameterDict => parameterDict ??= this.GetEntParameterDict();

        private EntParameterDict? fullParameterDict;
        public EntParameterDict FullParameterDict => fullParameterDict ??= this.GetFullParameterDict();

        private EntElementType? entElementType;
        public EntElementType? EntElementType => entElementType ??= this.ElementType?.GetEntElement() as EntElementType;

        protected string? elementName;
        public virtual string? ElementName
        {
            get => elementName;
            set => elementName = value;
        }

        protected Level? level;
        public virtual Level? Level => level ??= this.GetLevel();

        public string? LevelName => this.GetLevelName();

        protected Level? baseLevel;
        public virtual Level? BaseLevel => baseLevel;

        public string? BaseLevelName => BaseLevel?.Name;

        protected Level? topLevel;
        public virtual Level? TopLevel => topLevel;

        public string? TopLevelName => TopLevel?.Name;

        private List<ElementMaterial>? elementMaterials;
        public List<ElementMaterial>? ElementMaterials => elementMaterials ??= this.GetElementMaterials();

        // method
        public override string ToString()
        {
            return $"Id: {ElementId}";
        }
    }
}
