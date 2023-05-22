using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Form;
using Model.Entity;
using Autodesk.Revit.DB.Mechanical;
using SingleData;
using Autodesk.Revit.DB;
using System.Runtime.InteropServices;

namespace Model.Dataa
{
    public static class AirTerminalProcessor_DataUtil
    {
        public static RevitData revitData => RevitData.Instance;
        public static List<FlexDuctType> GetFlexDuctTypes(this AirTerminalProcessor_Data q)
        {
            var doc= revitData.Document;
            var flexDuctTypes = new FilteredElementCollector(doc).OfClass(typeof(FlexDuctType)).Cast<FlexDuctType>().ToList();

            return flexDuctTypes;
        }
        
        public static List<DuctType> GetDuctTypes(this AirTerminalProcessor_Data q)
        {
            var doc = revitData.Document;
            var DuctTypes = new FilteredElementCollector(doc).OfClass(typeof(DuctType)).Cast<DuctType>().ToList();

            return DuctTypes;
        }

    }

}
