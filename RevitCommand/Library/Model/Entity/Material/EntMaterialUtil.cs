using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class EntMaterialUtil
    {
        public static EntMaterial? GetMaterial(this Material material)
        {
            return material != null ? new EntMaterial
            {
                RevitMaterial = material
            } : null;
        }
    }
}