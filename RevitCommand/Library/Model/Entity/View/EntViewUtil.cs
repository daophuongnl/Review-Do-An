using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public static class EntViewUtil
    {
        public static EntView GetEntView(this View q, EntDocument document)
        {
            return new EntView
            {
                ModelItem = q,
                Document = document
            };
        }

        // property
        public static Document GetRevitDocument(this EntView q)
        {
            return (q.Document != null ? q.Document.RevitDocument : q.ModelItem!.Document)!;
        }

        public static IEnumerable<Workset> GetHiddenWorksets(this EntView q)
        {
            var view = q.ModelItem!;
            var worksets = q.Document!.Worksets;

            return worksets.Where(x => !view.IsWorksetVisible(x.Id));
        }

        public static ElementFilter GetModelCategory_FilterSet_Filter(this EntView q)
        {
            return ElementFilterUtil.GetElementFilter(q.ModelItem!);
        }

        public static ElementFilter GetElementFilter(this EntView q, EntDocument? otherDocument = null)
        {
            var modelCategory_FilterSet_Filter = q.ModelCategory_FilterSet_Filter;

            if (otherDocument == null)
            {
                return modelCategory_FilterSet_Filter;
            }
            else
            {
                var hiddenWorksetNames = q.HiddenWorksets.Select(x => x.Name).ToList();
                var hiddenWorksets = otherDocument.Worksets.Where(ws => hiddenWorksetNames.Contains(ws.Name));

                var hiddenWorksetFilter = hiddenWorksets.Select(ws => new ElementWorksetFilter(ws.Id, true)).And()!;

                return modelCategory_FilterSet_Filter.And(hiddenWorksetFilter);
            }
        }
    }
}
