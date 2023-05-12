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
    /// <summary>
    /// Các thông tin dữ liệu về Workset truy xuất từ Revit 
    /// </summary>
    public class AnnotationData : DataBase
    {
        private static AnnotationData? instance;
        /// <summary>
        /// Thuộc tính tĩnh là dùng để truy xuất các dữ liệu bên trong
        /// </summary>
        public static AnnotationData Instance
        {
            get => instance ??= new AnnotationData();
            set => instance = value;
        }

        private IEnumerable<MultiReferenceAnnotationType>? multiReferenceAnnotationTypes;
        public IEnumerable<MultiReferenceAnnotationType> MultiReferenceAnnotationTypes
        {
            get => multiReferenceAnnotationTypes ??= TypeElements.OfType<MultiReferenceAnnotationType>();
            set => multiReferenceAnnotationTypes = value;
        }

        private IEnumerable<IndependentTag>? independentTags;
        public IEnumerable<IndependentTag> IndependentTags
        {
            get => independentTags ??= InstanceElements.OfType<IndependentTag>();
            set => independentTags = value;
        }

        private IEnumerable<IndependentTag>? rebarTags;
        public IEnumerable<IndependentTag> RebarTags
        {
            get => rebarTags ??= IndependentTags.Where(x => x.Category.IsEqual(BuiltInCategory.OST_RebarTags));
            set => rebarTags = value;
        }

        private IEnumerable<FamilySymbol>? rebarTagTypes;
        public IEnumerable<FamilySymbol> RebarTagTypes
        {
            get => rebarTagTypes ??= FamilySymbols.Where(x => x.Category.IsEqual(BuiltInCategory.OST_RebarTags));
            set => rebarTagTypes = value;
        }

        private IEnumerable<Family>? rebarTagFamilies;
        public IEnumerable<Family> RebarTagFamilies
        {
            get => rebarTagFamilies ??= Families.Where(x => x.FamilyCategory.IsEqual(BuiltInCategory.OST_RebarTags));
            set => rebarTagFamilies = value;
        }

        private List<TextNoteType>? textNoteTypes;
        /// <summary>
        /// Thuộc tính chứa tập hợp các TextNoteType trong dự án hiện hành
        /// </summary>
        public List<TextNoteType> TextNoteTypes
        {
            get => textNoteTypes ??= TypeElements.OfType<TextNoteType>().ToList();
            set => textNoteTypes = value;
        }

        private IEnumerable<TextNote>? textNotes;
        public IEnumerable<TextNote> TextNotes
        {
            get => textNotes = InstanceElements.OfType<TextNote>();
            set => textNotes = value;
        }
    }
}
