using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    /// <summary>
    /// Tập hợp các công cụ để xử lý Element
    /// </summary>
    public static partial class ElementUtil
    {
        private static RevitData revitData => RevitData.Instance;

        #region Method

        /// <summary>
        /// Truy xuất đối tượng Element từ kiểu Reference
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.Element GetElement(this Autodesk.Revit.DB.Reference rf, Document doc = null)
        {
            var r_doc = doc != null ? doc : revitData.Document;
            return r_doc.GetElement(rf);
        }

        /// <summaryomo
        /// Truy xuất đối tượng Element từ kiểu ElementId
        /// </summary>
        /// <param name="elemId"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.Element GetElement(this Autodesk.Revit.DB.ElementId elemId, Document doc = null)
        {
            var r_doc = doc != null ? doc : revitData.Document;
            return r_doc.GetElement(elemId);
        }

        public static Autodesk.Revit.DB.Element GetElement(this string guid, Document doc = null)
        {
            var r_doc = doc != null ? doc : revitData.Document;
            return r_doc.GetElement(guid);
        }

        public static Element AddAndGetElement(this ElementId elemId)
        {
            var elem = revitData.Document.GetElement(elemId);
            revitData.AllElements.Add(elem);

            return elem;
        }

        /// <summary>
        /// Truy xuất đối tượng Element từ kiểu integer
        /// </summary>
        /// <param name="elemId"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.Element GetElement(this int elemId, Document doc = null)
        {
            return GetElement(elemId.GetElementId(), doc);
        }

        /// <summary>
        /// Truy xuất đối tượng ElementId từ kiểu integer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.ElementId GetElementId(this int Id)
        {
            return new Autodesk.Revit.DB.ElementId(Id);
        }

        public static T GetElement<T>(this Autodesk.Revit.DB.Reference rf, Document doc = null) where T : Autodesk.Revit.DB.Element
        {
            var r_doc = doc != null ? doc : revitData.Document;
            return (T)r_doc.GetElement(rf);
        }

        public static T GetElement<T>(this Autodesk.Revit.DB.ElementId elemId, Document doc = null) where T : Autodesk.Revit.DB.Element
        {
            var r_doc = doc != null ? doc : revitData.Document;
            return (T)r_doc.GetElement(elemId);
        }

        public static T AddAndGetElement<T>(this ElementId elemId) where T : Autodesk.Revit.DB.Element
        {
            return (T)elemId.AddAndGetElement();
        }

        public static T GetElement<T>(this int elemId, Document doc = null) where T : Autodesk.Revit.DB.Element
        {
            return (T)GetElement(elemId, doc);
        }

        public static IEnumerable<Autodesk.Revit.DB.Element> GetSimilarTypes(this Autodesk.Revit.DB.ElementType elementType)
        {
            return elementType.GetSimilarTypes().Select(x => x.GetElement(elementType.Document));
        }

        public static Autodesk.Revit.DB.ElementType GetElementType(this Autodesk.Revit.DB.Element element)
        {
            return (element.GetTypeId().GetElement(element.Document) as Autodesk.Revit.DB.ElementType)!;
        }

        #endregion
    }
}
