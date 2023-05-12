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
    public abstract class DataBase
    {
        protected DataBase()
        {
        }

        protected RevitData RevitData
        {
            get
            {
                return RevitData.Instance;
            }
        }


        // Reference
        protected UIDocument? uiDocument;
        public UIDocument UIDocument
        {
            get => uiDocument ??= RevitData.UIDocument;
            set => uiDocument = null;
        }

        protected Document? document;
        public Document Document
        {
            get => document ??= RevitData.Document;
            set => document = null;
        }

        protected IEnumerable<ElementType>? typeElements;
        public IEnumerable<ElementType> TypeElements
        {
            get => typeElements ??= RevitData.TypeElements;
            set => typeElements = value;
        }

        protected IEnumerable<Element>? instanceElements;
        public IEnumerable<Element> InstanceElements
        {
            get => instanceElements ??= RevitData.InstanceElements;
            set => instanceElements = value;
        }

        protected IEnumerable<FamilySymbol>? familySymbols;
        public IEnumerable<FamilySymbol> FamilySymbols
        {
            get => familySymbols ??= RevitData.FamilySymbols;
            set => familySymbols = value;
        }

        protected IEnumerable<FamilyInstance>? familyInstances;
        public IEnumerable<FamilyInstance> FamilyInstances
        {
            get => familyInstances ??= RevitData.FamilyInstances;
            set => familyInstances = value;
        }

        protected IEnumerable<Family>? families;
        public IEnumerable<Family> Families
        {
            get => families ??= RevitData.Families;
            set => families = value;
        }
    }
}
