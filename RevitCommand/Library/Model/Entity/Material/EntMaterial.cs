using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntMaterial
    {
        public Material? RevitMaterial { get; set; }

        public string Name => RevitMaterial!.Name;

        public string Class => RevitMaterial!.MaterialClass;

        public string Category => RevitMaterial!.MaterialCategory;
    }
}