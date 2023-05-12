using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class RevitLink_Dict_DictUtil
    {
        // 
        // Instance
        //

        public static RevitLink_Dict_Dict GetLinkDict_Dict(this EntDocument q)
        {
            return new RevitLink_Dict_Dict
            {
                Document = q
            };
        }

        // 
        // Property
        //

        
    }
}
