using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntParameterDictUtil
    {
        public static EntParameterDict GetEntParameterDict(this EntElement entElement)
        {
            return new EntParameterDict
            {
                Element = entElement
            };
        }

        public static EntParameterDict GetFullParameterDict(this EntElement entElement)
        {
            var revitElem = entElement.RevitElement!;
            var dict = new EntParameterDict
            {
                Element = entElement,
                IsFullParameters = true
            };

            foreach (Parameter p in revitElem.Parameters)
            {
                var item = dict[p.Definition.Name];
            }

            return dict;
        }
    }
}
