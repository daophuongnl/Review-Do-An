using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public enum MassType
    {
        Concrete = 0, Formwork = 1, Rebar = 2, Formwork_Bottom = 3, LeanConcrete = 4,
        BrickWork = 5, PaintWork = 6, PlasterWork = 7, FloorFinish = 8,
        Electrical = 9, Plumbing = 10, HVAC = 11, FireFighting = 12,
        Undefined = 13,
        Lintel = 14, SubColumn = 15
    }
}
