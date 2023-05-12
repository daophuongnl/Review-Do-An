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
    public class EntSolidInGroup : EntSolid
    {
        public EntSolidGroup? EntSolidGroup { get; set; }

        public int FirstElementId { get; set; }

        private List<int>? elementIds;
        public List<int> ElementIds => elementIds ??= new List<int> { FirstElementId };

        private string? index;
        public string? Index
        {
            get => index ??= this.GetIndex();
            set => index = value;
        }

        private List<string>? invalidMergeIndexs;
        public List<string> InvalidMergeIndexs => invalidMergeIndexs ??= new List<string>();
    }
}
