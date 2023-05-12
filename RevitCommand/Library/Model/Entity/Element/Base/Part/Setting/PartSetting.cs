using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class PartSetting
    {
        public bool IsCreateSystem { get; set; } = false;

        public bool IsDone { get; set; } = false;

        private Func<Element, EntOriginal>? main_RetrieveOriginal;
        public Func<Element, EntOriginal> Main_RetrieveOriginal
        {
            get => main_RetrieveOriginal ??= this.GetMain_RetrieveOriginal();
            set => main_RetrieveOriginal = value;
        }

        private Func<Element, EntOriginal>? retrieveOriginal;
        public Func<Element, EntOriginal> RetrieveOriginal
        {
            get => retrieveOriginal ??= this.GetRetrieveOriginal();
            set => retrieveOriginal = value;
        }

        private Func<Part, EntPart>? main_RetrievePart;
        public Func<Part, EntPart> Main_RetrievePart
        {
            get => main_RetrievePart ??= this.GetMain_RetrievePart();
            set => main_RetrievePart = value;
        }

        private Func<Part, EntPart>? retrievePart;
        public Func<Part, EntPart> RetrievePart
        {
            get => retrievePart ??= this.GetRetrievePart();
            set => retrievePart = value;
        }

        private Func<EntPart, EntElement?>? retrieveSource;
        public Func<EntPart, EntElement?> RetrieveSource
        {
            get => retrieveSource ??= this.GetRetrieveSource();
            set
            {
                retrieveSource = value;
            }
        }

        public List<EntPart> Handled_Parts { get; set; } = new List<EntPart>();

        public List<EntOriginal> Handled_Originals { get; set; } = new List<EntOriginal>();
    }
}
