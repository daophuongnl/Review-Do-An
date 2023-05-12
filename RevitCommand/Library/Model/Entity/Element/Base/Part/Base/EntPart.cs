using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntPart : EntElement
    {
        public bool IsGet_SupItem { get; set; }

        private EntPart? supItem;
        public EntPart? SupItem
        {
            get => supItem ??= this.GetSupItem();
            set => supItem = value;
        }

        public List<EntPart> SubItems { get; set; } = new List<EntPart>();

        public bool IsGet_Original { get; set; }

        private EntOriginal? original;
        public EntOriginal? Original
        {
            get => original ??= this.GetOriginal();
            set => original = value;
        }

        private List<EntPart>? valueParts;
        public List<EntPart> ValueParts => valueParts ??= this.GetValueParts();
    }
}
