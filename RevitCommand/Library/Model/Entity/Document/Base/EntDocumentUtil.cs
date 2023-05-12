using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Model.Entity;

namespace Utility
{
    public static partial class EntDocumentUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static ViewData viewData => ViewData.Instance;

        public static EntDocument GetEntDocument(this Document document)
        {
            return new EntDocument
            {
                RevitDocument = document
            };
        }

        // Property
        public static List<Workset> GetWorksets(this EntDocument q)
        {
            return new FilteredWorksetCollector(q.RevitDocument!).OfKind(WorksetKind.UserWorkset).ToList();
        }

        public static dynamic GetData(this EntDocument q)
        {
            return new
            {
                Count = 1
            };
        }

        public static EntView GetActiveView(this EntDocument q)
        {
            return q.RevitDocument!.ActiveView.GetEntView(q);
        }

        public static string GetPathName(this EntDocument q)
        {
            return q.RevitDocument!.GetPathName();
        }

        // Method


    }
}
