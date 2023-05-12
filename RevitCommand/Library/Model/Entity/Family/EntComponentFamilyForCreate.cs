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
    public class EntComponentFamilyForCreate : EntComponentFamily
    {
        private string? familyTemplatePath;
        public string FamilyTemplatePath => familyTemplatePath ??= @"C:\ProgramData\Autodesk\RVT 2019\Family Templates\English\Metric Generic Model.rft";

        public override Document FamilyDocument => familyDocument ??= this.GetDocument();

        public override Family RevitFamily
        {
            get => revitFamily ??= this.GetRevitFamily();
            set => revitFamily = value;
        }

        protected Action<EntComponentFamilyForCreate>? entComponentFamilyForCreate_Action;
        public virtual Action<EntComponentFamilyForCreate>? EntComponentFamilyForCreate_Action
        {
            get => entComponentFamilyForCreate_Action;
            set => entComponentFamilyForCreate_Action = value;
        }
    }
}
