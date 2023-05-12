using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ElementCollectorDictUtil
    {
        //
        // Instance
        //

        public static ElementCollectorDict GetCollector(this EntDocument q)
        {
            return new ElementCollectorDict
            {
                Document = q
            };
        }

        //
        // Property
        //



        //
        // Method
        //

    }
}
