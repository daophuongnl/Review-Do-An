using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class RevitLink_DictUtil
    {
        // 
        // Instance
        //

        public static RevitLink_Dict GetLinkDict(this RevitLink_Dict_Dict q, ElementId viewId)
        {
            var list = q.Items;

            var item = list.FirstOrDefault(x => x.ViewId == viewId);
            if (item == null)
            {
                item = new RevitLink_Dict
                {
                    Dict = q,
                    ViewId = viewId
                };
                list.Add(item);
            }

            return item;
        }

        // 
        // Property
        //

        public static IEnumerable<RevitLinkInstance> GetInstances(this RevitLink_Dict q)
        {
            return q.Document.Collector[q.ViewId!][typeof(RevitLinkInstance)].Cast<RevitLinkInstance>();
        }

        public static IEnumerable<RevitLinkType> GetTypes(this RevitLink_Dict q)
        {
            return q.Document.Collector[q.ViewId!][typeof(RevitLinkType)].Cast<RevitLinkType>();
        }

        public static IEnumerable<EntDocument> GetLinkDocuments(this RevitLink_Dict q)
        {
            return q.Instances.Select(x => x.GetLinkDocument())
                .GroupBy(x => x.PathName)
                .Select(x =>
                {
                    var qI = new EntDocument
                    {
                        Data = new { Count = x.Count() },
                        RevitDocument = x.First()
                    };
                    return qI;
                });
        }
    }
}
