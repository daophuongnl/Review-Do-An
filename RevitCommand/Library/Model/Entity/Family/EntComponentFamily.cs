using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
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
    public class EntComponentFamily : EntFamily
    {
        public override Type TargetElementClass => targetElementClass ??= typeof(FamilyInstance);

        protected Document? familyDocument;
        public virtual Document? FamilyDocument
        {
            get=> familyDocument;
            set => familyDocument = value;
        }

        protected Family? revitFamily;
        public virtual Family RevitFamily
        {
            get => revitFamily ??= this.GetRevitFamily();
            set => revitFamily = value;
        }

        public override string Name => name ??= RevitFamily.Name;

        public override BuiltInCategory TargetBuiltInCategory => targetBuiltInCategory ??= RevitFamily.FamilyCategory.GetBuiltInCategory();

        private IEnumerable<FamilySymbol>? familySymbols;
        public IEnumerable<FamilySymbol> FamilySymbols => familySymbols ??= this.GetFamilySymbols();

        private FamilySymbol? defaultFamilySymbol;
        public FamilySymbol DefaultFamilySymbol => defaultFamilySymbol ??= this.GetDefaultFamilySymbol();
    }
}
