using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class LevelUtil
    {
        public static Level? GetForBeam(EntBeam beam)
        {
            return (beam.ParameterDict[ElementParameterName.ReferenceLevel]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetForFloor(EntElement elem)
        {
            return elem.RevitElement?.LevelId.GetElement<Level>();
        }

        public static Level? GetForColumn(EntColumn column)
        {
            return column.BaseLevel;
        }

        public static Level? GetBaseForColumn(EntColumn column)
        {
            return (column.ParameterDict[ElementParameterName.BaseLevel]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetTopForColumn(EntColumn column)
        {
            return (column.ParameterDict[ElementParameterName.TopLevel]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetForWall(EntElement elem)
        {
            return elem.BaseLevel;
        }

        public static Level? GetBaseForWall(EntElement elem)
        {
            return (elem.ParameterDict[ElementParameterName.BaseConstraint]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetTopForWall(EntElement elem)
        {
            var elemId = elem.ParameterDict[ElementParameterName.TopConstraint]?.Value;
            return elemId != null ? (elemId as ElementId)?.GetElement<Level>() : null;
        }

        public static Level? GetForStair(EntStair stair)
        {
            return stair.BaseLevel;
        }

        public static Level? GetBaseForStair(EntStair stair)
        {
            return (stair.ParameterDict[ElementParameterName.BaseLevel]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetTopForStair(EntStair stair)
        {
            return (stair.ParameterDict[ElementParameterName.TopLevel]!.Value as ElementId)?.GetElement<Level>();
        }

        public static Level? GetForEntElement(EntElement elem)
        {
            var paramNames = new List<string> { ElementParameterName.ScheduleLevel, ElementParameterName.ReferenceLevel, ElementParameterName.Level };

            dynamic? elemId = null;
            foreach (var paramName in paramNames)
            {
                elemId = elem.ParameterDict[paramName]?.Value;
                if (elemId != null)
                {
                    break;
                }
            }

            return elemId is not null ? (elemId as ElementId)?.GetElement<Level>() : null;
        }
    }
}
