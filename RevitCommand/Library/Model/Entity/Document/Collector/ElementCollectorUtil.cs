using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ElementCollectorUtil
    {
        //
        // Instance
        //

        public static ElementCollector GetElementCollector(this ElementCollectorDict dict, ElementId viewId)
        {
            return new ElementCollector
            {
                Dict = dict,
                ViewId = viewId
            };
        }

        //
        // Property
        //

        public static FilteredElementCollector GetFilteredElementCollector(this ElementCollector q)
        {
            var doc = q.Document.RevitDocument;
            var viewId = q.ViewId;

            return viewId == null ?
                new FilteredElementCollector(doc) :
                new FilteredElementCollector(doc, viewId);
        }

        //
        // Method
        //

    }
}
