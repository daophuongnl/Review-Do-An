using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ElementMaterialUtil
    {
        // Instance
        public static List<ElementMaterial>? GetElementMaterials(this EntElement element)
        {
            if (element.RevitElement is null) return null; 

            var matIds = element.RevitElement.GetMaterialIds(false);
            return matIds.Select(x => GetElementMaterial(element, x)).ToList();
        }

        public static ElementMaterial GetElementMaterial(this EntElement element, ElementId matId)
        {
            return new ElementMaterial
            {
                EntElement = element,
                MaterialId = matId
            };
        }

        // Property
        public static double GetArea(this ElementMaterial elementMaterial)
        {
            var element = elementMaterial.EntElement!.RevitElement;
            return element!.GetMaterialArea(elementMaterial.MaterialId, false);
        }

        public static double GetVolume(this ElementMaterial elementMaterial)
        {
            var element = elementMaterial.EntElement!.RevitElement;
            return element!.GetMaterialVolume(elementMaterial.MaterialId);
        }

        public static EntMaterial? GetEntMaterial(this ElementMaterial elementMaterial)
        {
            var doc = elementMaterial.EntElement!.Document!.RevitDocument!;
            return elementMaterial.MaterialId!.GetElement<Material>(doc).GetMaterial();
        }
    }
}
