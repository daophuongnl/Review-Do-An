using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace SingleData
{
    public class PartData
    {
        private static PartData? instance;
        public static PartData Instance
        {
            get => instance ??= new PartData();
            set => instance = value;
        }

        private RevitData revitData => RevitData.Instance;

        private IEnumerable<Part>? parts;
        public IEnumerable<Part> Parts => parts ??= revitData.InstanceElements.OfType<Part>();

        private PartSetting? setting;
        public PartSetting Setting => setting ??= new PartSetting();

        private List<EntPart>? entParts;
        public List<EntPart> EntParts => entParts ??= this.GetEntParts();
    }
}
