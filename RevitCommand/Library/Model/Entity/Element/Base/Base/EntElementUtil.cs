using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;
using System.Windows;

namespace Utility
{
    public static class EntElementUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static EntElement? GetEntElement(this Autodesk.Revit.DB.Element element, bool isAllowUndefinedClass = true)
        {
            EntElement? entElem = null;
            if (element == null) return null;

            // Check by Class
            if (element is ElementType)
            {
                entElem = new EntElementType { RevitElement = element };
            }

            else if (element is Wall)
            {
                var isStructural = element.LookupParameter("Structural") != null && element.ParameterAsInteger("Structural") == 1;
                if (isStructural)
                {
                    entElem = new EntStrWall { RevitElement = element };
                }
                else
                {
                    entElem = new EntArcWall { RevitElement = element };
                }
            }
            else if (element is Floor)
            {
                var isStructural = element.LookupParameter("Structural") != null && element.ParameterAsInteger("Structural") == 1;
                if (isStructural)
                {
                    var revitCate = element.Category;
                    if (revitCate.IsEqual(BuiltInCategory.OST_Floors))
                    {
                        entElem = new EntStrFloor { RevitElement = element };
                    }
                    else if (revitCate.IsEqual(BuiltInCategory.OST_StructuralFoundation))
                    {
                        entElem = new EntFoundationSlab { RevitElement = element };
                    }
                }
                else
                {
                    entElem = new EntArcFloor { RevitElement = element };
                }
            }
            else if (element is Stairs)
            {
                entElem = new EntStair { RevitElement = element };
            }
            else if (element is Part)
            {
                entElem = new EntPart { RevitElement = element };
            }

            // Check by Cateogry
            else if (element is FamilyInstance)
            {
                var revitBic = element.Category.GetBuiltInCategory();

                switch (revitBic)
                {
                    // Strutural
                    case BuiltInCategory.OST_StructuralFraming:
                        entElem = new EntBeam { RevitElement = element };
                        break;
                    case BuiltInCategory.OST_StructuralColumns:
                        entElem = new EntColumn { RevitElement = element };
                        break;
                    case BuiltInCategory.OST_StructuralFoundation:
                        entElem = new EntFoundationInstance { RevitElement = element };
                        break;

                    // Generic Model
                    case BuiltInCategory.OST_GenericModel:
                        entElem = new EntOriginal { RevitElement = element };
                        break;
                }
            }

            else if (element is FilledRegion)
            {
                entElem = new EntFilledRegion { RevitElement = element };
            }

            if (entElem == null)
            {
                if (isAllowUndefinedClass)
                {
                    entElem = new EntOriginal { RevitElement = element };
                }
                else
                {
                    throw new Exception($"Invalid get entElement from Id: {element.Id.IntegerValue}");
                }
            }

            return entElem;
        }

        // Property
        public static BoundingBoxXYZ? GetBoundingBoxXYZ(this EntElement entElement)
        {
            var bb = entElement.RevitElement!.get_BoundingBox(null);
            return bb;
        }

        public static ElementType GetElementType(this EntElement entElement)
        {
            return entElement.RevitElement!.GetTypeId().GetElement<ElementType>(entElement.RevitDocument);
        }

        public static string GetTypeName(this EntElement entElement)
        {
            return entElement.ElementType!.Name;
        }

        public static string? GetName(this EntElement entElement)
        {
            return entElement.RevitElement?.Name;
        }

        public static XYZ GetCenterPoint(this EntElement entElement)
        {
            return entElement.EntTransform.Origin;
        }

        public static Document GetRevitDocument(this EntElement entElement)
        {
            //return entElement.EntDocument.RevitDocument;
            return entElement.RevitElement!.Document;
        }

        public static EntDocument? GetDocument(this EntElement q)
        {
            if (q.RevitDocument is null)
            {
                throw new Exception("Document is null!");
            }

            var name = q.RevitDocument.GetPathName();

            var doc = revitData.EntDocument;
            if (name == doc.PathName)
            {
                return doc;
            }

            try
            {
                return doc.LinkDict_Dict[null].LinkDocuments
                   .First(x => x.PathName == name);
            }
            catch
            {
                throw new Exception("Document Name is not valid!");
            }
        }

        public static string? GetGUID(this EntElement entElement)
        {
            return entElement.RevitElement?.UniqueId;
        }

        public static Family? GetFamily(this EntElement entElement)
        {
            var symbol = entElement.ElementType as FamilySymbol;
            if (symbol != null)
            {
                return symbol.Family;
            }
            return null;
        }

        public static BuiltInCategory? GetBuiltInCategory(this EntElement entElement)
        {
            return entElement.RevitElement?.Category.GetBuiltInCategory();
        }

        public static string? GetFamilyName(this EntElement entElement)
        {
            return entElement.ElementType?.FamilyName;
        }

        public static Category? GetCategory(this EntElement entElement)
        {
            if (entElement.IsGetCategory) return null;
            entElement.IsGetCategory = true;

            return entElement.RevitElement!.Category;
        }

        public static string? GetRefinedCategoryName(this EntElement entElement)
        {
            var cateName = entElement.CategoryName;

            // Xử lý phần Structural Walls và Structural Floors
            if (cateName == "Walls" || cateName == "Floors")
            {
                var isStrucDyn = entElement.ParameterDict[ElementParameterName.Structural]?.Value;
                if (isStrucDyn != null && isStrucDyn == 1)
                {
                    cateName = $"Structural {cateName}";
                }
                else
                {
                    // Curtain Walls
                    if (entElement.FamilyName == "Curtain Wall")
                    {
                        cateName = "Curtain Walls";
                    }
                }
            }
            else if (cateName == "Stairs")
            {
                cateName = $"Structural {cateName}";
            }
            return cateName;
        }

        public static Level? GetLevel(this EntElement entElement)
        {
            return LevelUtil.GetForEntElement(entElement);
        }

        public static string? GetLevelName(this EntElement entElement)
        {
            return entElement.Level is not null ? entElement.Level.Name : entElement.ParameterDict[ElementParameterName.HB_Level]?.ValueString;
        }

        // Method
    }
}
