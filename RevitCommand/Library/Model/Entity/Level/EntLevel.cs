using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntLevel
    {
        public Level? RevitLevel { get; set; }

        public string Name => RevitLevel!.Name;

        public double Elevation => RevitLevel!.Elevation;

        public double Elevation_SI => Elevation.feet2Meter();
    }
}
