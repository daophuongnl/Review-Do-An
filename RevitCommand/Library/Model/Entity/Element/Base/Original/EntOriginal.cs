using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntOriginal : EntElement
    {
        public List<EntPart> MainPart_StorageList { get; set; } = new List<EntPart>();

        public bool IsGet_MainParts { get; set; } = false;

        private List<EntPart>? mainParts;
        public List<EntPart>? MainParts => this.mainParts ??= this.GetMainParts();

        public bool IsGet_ValueParts { get; set; } = false;

        private List<EntPart>? valueParts;
        public List<EntPart>? ValueParts => this.valueParts ??= this.GetValueParts();
    }
}
