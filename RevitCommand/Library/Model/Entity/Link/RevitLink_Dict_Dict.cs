using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class RevitLink_Dict
    {
        public RevitLink_Dict_Dict? Dict { get; set; }

        public EntDocument Document => Dict!.Document!;

        public ElementId? ViewId { get; set; }

        private IEnumerable<RevitLinkInstance>? instances;
        public IEnumerable<RevitLinkInstance> Instances => instances ?? (instances = this.GetInstances());

        private IEnumerable<RevitLinkType>? types;
        public IEnumerable<RevitLinkType> Types => types ?? (types = this.GetTypes());

        private IEnumerable<EntDocument>? linkDocuments;
        public IEnumerable<EntDocument> LinkDocuments => linkDocuments ?? (linkDocuments = this.GetLinkDocuments());
    }
}
