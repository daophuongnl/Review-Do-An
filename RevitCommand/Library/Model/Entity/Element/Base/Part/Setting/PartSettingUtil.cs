using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static class PartSettingUtil
    {
        private static RevitData revitData => RevitData.Instance;

        // Property
        public static Func<Element, EntOriginal> GetMain_RetrieveOriginal(this PartSetting setting)
        {
            return (x) =>
            {
                var qI = x.GetEntElement();
                return (qI as EntOriginal)!;
            };
        }

        public static Func<Element, EntOriginal> GetRetrieveOriginal(this PartSetting setting)
        {
            Func<Element, EntOriginal>? fx = null;
            if (setting.IsCreateSystem)
            {
                fx = (x) =>
                {
                    var id = x.Id;
                    var list = setting.Handled_Originals;

                    var qI = list.FirstOrDefault(y => y.ElementId_Obj == id);
                    if (qI == null)
                    {
                        qI = setting.Main_RetrieveOriginal(x);
                        list.Add(qI);
                    }

                    return qI;
                };
            }
            else
            {
                fx = (x) =>
                {
                    return setting.Main_RetrieveOriginal(x);
                };
            }

            return fx;
        }

        public static Func<Part, EntPart> GetMain_RetrievePart(this PartSetting setting)
        {
            return (x) =>
            {
                var qI = new EntPart();
                qI.RevitElement = x;
                return qI;
            };
        }

        public static Func<Part, EntPart> GetRetrievePart(this PartSetting setting)
        {
            Func<Part, EntPart>? fx = null;
            if (setting.IsCreateSystem)
            {
                fx = (x) =>
                {
                    var id = x.Id;
                    var list = setting.Handled_Parts;
                    var qI = list.FirstOrDefault(y => y.ElementId_Obj == id);
                    if (qI == null)
                    {
                        qI = setting.Main_RetrievePart(x);
                        list.Add(qI);
                    }
                    return qI;
                };
            }
            else
            {
                fx = (x) =>
                {
                    return setting.Main_RetrievePart(x);
                };
            }
            return fx;
        }

        public static Func<EntPart, EntElement?> GetRetrieveSource(this PartSetting setting)
        {
            var doc = revitData.Document;
            Func<EntPart, EntElement?> fx = (x) =>
            {
                var part = (x.RevitElement as Part)!;
                var hostElem = doc.GetElement(part.GetSourceElementIds().First().HostElementId);
                if (hostElem is Part supPart)
                {
                    var item = setting.RetrievePart(supPart);
                    item.SubItems.Add(x);
                    return item;
                }
                else
                {
                    var item = setting.RetrieveOriginal(hostElem);
                    if (item != null)
                    {
                        item.MainPart_StorageList.Add(x);
                    }
                    return item;
                }
            };
            return fx;
        }
    }
}
