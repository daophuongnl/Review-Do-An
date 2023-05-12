using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class StructuralElement : EntOriginal
    {
        private IEnumerable<Autodesk.Revit.DB.Structure.Rebar>? revitRebars;
        public IEnumerable<Autodesk.Revit.DB.Structure.Rebar> RevitRebars => revitRebars ??= this.GetRevitRebars();

        private IEnumerable<RebarExport>? rebarExports;
        public IEnumerable<RebarExport> RebarExports => rebarExports ??= this.GetRebarExports();

        public override string? ElementName => elementName ??= this.GetElementName();
    }
}
