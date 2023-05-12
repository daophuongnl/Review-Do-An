using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntDocument
    {
        public Document? RevitDocument { get; set; }

        //public string Name => Regex.Match(this.PathName, @"[ \w-]+?(?=\.)").Value;

        private string? name;
        public string Name => name ??= Path.GetFileNameWithoutExtension(this.PathName);

        private string? pathName;
        public string PathName => pathName ??= this.GetPathName();

        private ElementCollectorDict? collector;
        public ElementCollectorDict Collector => collector ??= this.GetCollector();

        private RevitLink_Dict_Dict? linkDict_Dict;
        public RevitLink_Dict_Dict LinkDict_Dict => linkDict_Dict ??= this.GetLinkDict_Dict();

        private List<Workset>? worksets;
        public List<Workset> Worksets => worksets ??= this.GetWorksets();

        private EntView? activeView;
        public EntView ActiveView => activeView ??= this.GetActiveView();

        private dynamic? data;
        public dynamic Data
        {
            get => data ?? (data = this.GetData());
            set => data = value;
        }
    }
}
