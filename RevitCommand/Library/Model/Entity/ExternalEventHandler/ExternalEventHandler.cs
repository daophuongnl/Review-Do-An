using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using SingleData;

namespace Model.Event
{
    public class ExternalEventHandler : IExternalEventHandler
    {
        public Action? Action { get; set; }

        public string Name { get; set; } = "Default External Event";

        public void Execute(UIApplication app)
        {
            Action?.Invoke();
        }

        public string GetName()
        {
            return Name;
        }
    }
}
