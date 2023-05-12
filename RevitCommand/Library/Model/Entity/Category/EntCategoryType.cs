using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public enum EntCategoryType
    {
        Beam = 100, StrFloor = 101, Column = 102, StrWall = 103, Foundation = 104, Ramp = 105, StrStair = 106, SpreadConcrete = 107, ColumnCapital = 108,

        ArcFloor = 200, ArcWall = 201, ArcStair = 202, ArcCeiling = 203, Door = 204, Window = 205,

        Pipe = 300, Duct = 301, PipeFitting = 302, DuctFitting = 308, PipeAccessory = 303, DuctAccessory = 309,
        Equipment = 304, Terminal = 305, Fixture = 306, Sprinkler = 307,

        CableTray = 500, CableTrayFitting = 501, Conduit = 502, ConduitFitting = 503, Switch = 504,

        Generic = 400
    }
}
