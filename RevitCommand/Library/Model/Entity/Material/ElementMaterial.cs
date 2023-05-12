using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class ElementMaterial
    {
        public EntElement? EntElement { get; set; }

        public ElementId? MaterialId { get; set; }

        private double? area;
        public double Area => area ??= this.GetArea();

        public double Area_SI => Area.feet2MeterSquare();

        private double? volume;
        public double Volume => volume ??= this.GetVolume();

        public double Volume_SI => Volume.feet2MeterCubic();

        private EntMaterial? entMaterial;
        public EntMaterial? EntMaterial => entMaterial ??= this.GetEntMaterial();

        public string? Name => EntMaterial?.Name;

        public string? Class => EntMaterial?.Class;

        public string? Category => EntMaterial?.Category;
    }
}