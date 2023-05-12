using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using System.IO;
using System.Windows;

namespace Utility
{
    public static class EntFamilyUtil
    {
        private static RevitData revitData => RevitData.Instance;
        //private static BoQData boQData => BoQData.Instance;

        public static EntFamily GetEntFamily(this EntElement entElement)
        {
            return entElement.RevitElement!.GetEntFamily();
        }

        public static EntFamily GetEntFamily(this Element revitElem)
        {
            EntFamily? entFamily;
            if (revitElem is FamilyInstance fi)
            {
                entFamily = Get(fi.Symbol.Family);
            }
            else
            {
                var et = revitElem.GetTypeId().GetElement<ElementType>();
                entFamily = Get(revitElem.GetType(), et.FamilyName);
                entFamily.TargetBuiltInCategory = revitElem.Category.GetBuiltInCategory();
            }

            return entFamily;
        }

        public static EntFamily Get(Type targetElementClass, string name = null)
        {
            EntFamily entFamily = new EntSystemFamily
            {
                TargetElementClass = targetElementClass,
                Name = name
            };

            return entFamily;
        }

        public static EntFamily GetComponentFamily(BuiltInCategory bic, string name = null)
        {
            EntFamily entFamily = new EntComponentFamily
            {
                TargetBuiltInCategory = bic,
                Name = name
            };

            return entFamily;
        }

        public static EntFamily Get(Family family)
        {
            EntFamily entFamily = new EntComponentFamily
            {
                RevitFamily = family
            };

            return entFamily;
        }

        //public static List<EntFamily> GetAll()
        //{
        //    var entCateStorageList = boQData.EntCategoryStorageList;
        //    foreach (var entCate in entCateStorageList)
        //    {
        //        var entFamilies = entCate.EntFamilies;
        //    }

        //    return boQData.EntFamilyStorageList;
        //}

        #region Property

        public static IEnumerable<Element> GetAllElements(this EntFamily entFamily)
        {
            var elems = revitData.InstanceElements.Where(x => entFamily.FilterElementFunc(x));
            return elems;
        }

        #endregion

        #region Method
        public static bool FilterElement(this EntFamily entFamily, Element revitElem)
        {
            if (revitElem is DirectShape) return false;
            if (revitElem is FamilyInstance)
            {
                if (entFamily is EntSystemFamily) return false;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var fs = (revitElem as FamilyInstance).Symbol;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                return fs.Category.IsEqual(entFamily.TargetBuiltInCategory) && fs.FamilyName == entFamily.Name;
            }
            else
            {
                if (entFamily is EntComponentFamily) return false;
                if (revitElem.GetType() != entFamily.TargetElementClass) return false;

                var typeId = revitElem.GetTypeId();
                if (typeId == ElementId.InvalidElementId) return false;

                var et = revitElem.GetTypeId().GetElement<ElementType>();
                return et.FamilyName == entFamily.Name;
            }
        }
        #endregion
    }
}
