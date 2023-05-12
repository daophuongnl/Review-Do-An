using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class BuiltInCategoryUtil
    {
        public static List<BuiltInCategory> List(DisciplineType disciplineType)
        {
            List<BuiltInCategory>? list = null;

            switch (disciplineType)
            {
                case DisciplineType.All:
                    list = List(DisciplineType.Structural);
                    list.AddRange(List(DisciplineType.Architect));
                    list.AddRange(List(DisciplineType.MEP));
                    break;
                case DisciplineType.Structural:
                    list = new List<BuiltInCategory>
                    {
                        BuiltInCategory.OST_Walls,
                        BuiltInCategory.OST_Floors,
                        BuiltInCategory.OST_StructuralFraming,
                        BuiltInCategory.OST_StructuralColumns,
                        BuiltInCategory.OST_StructuralFoundation,
                        BuiltInCategory.OST_Ramps,
                        BuiltInCategory.OST_GenericModel
                    };
                    break;
                case DisciplineType.Architect:
                    list = new List<BuiltInCategory>
                    {
                        BuiltInCategory.OST_Walls,
                        BuiltInCategory.OST_Floors,
                        BuiltInCategory.OST_Ceilings,
                        BuiltInCategory.OST_Doors,
                        BuiltInCategory.OST_Windows,
                        BuiltInCategory.OST_GenericModel
                    };
                    break;
                case DisciplineType.MEP:
                    list = new List<BuiltInCategory>
                    {
                        // mechanical
                        BuiltInCategory.OST_PipeCurves,
                        BuiltInCategory.OST_FlexPipeCurves,
                        BuiltInCategory.OST_PipeFitting,
                        BuiltInCategory.OST_PipeAccessory,

                        BuiltInCategory.OST_DuctCurves,
                        BuiltInCategory.OST_FlexDuctCurves,
                        BuiltInCategory.OST_DuctFitting,
                        BuiltInCategory.OST_DuctAccessory,

                        BuiltInCategory.OST_PlumbingFixtures,
                        BuiltInCategory.OST_LightingFixtures,
                        BuiltInCategory.OST_ElectricalFixtures,

                        BuiltInCategory.OST_Sprinklers,
                        BuiltInCategory.OST_DuctTerminal,

                        BuiltInCategory.OST_MechanicalEquipment,

                        // electrical
                        BuiltInCategory.OST_CableTray,
                        BuiltInCategory.OST_CableTrayFitting,

                        BuiltInCategory.OST_Conduit,
                        BuiltInCategory.OST_ConduitFitting,

                        BuiltInCategory.OST_ElectricalEquipment,
                        BuiltInCategory.OST_ElectricalFixtures,

                        BuiltInCategory.OST_LightingFixtures,
                        BuiltInCategory.OST_LightingDevices,

                        BuiltInCategory.OST_DataDevices,
                        BuiltInCategory.OST_CommunicationDevices,
                        BuiltInCategory.OST_FireAlarmDevices,
                        BuiltInCategory.OST_SecurityDevices
                    };
                    break;
            }

            return list!;
        }

        public static List<BuiltInCategory> ModelList()
        {
            return new List<BuiltInCategory>
            {
                BuiltInCategory.OST_BridgeAbutments,
                BuiltInCategory.OST_Areas,
                BuiltInCategory.OST_BridgeBearings,
                BuiltInCategory.OST_BridgeCables,
                BuiltInCategory.OST_BridgeDecks,
                BuiltInCategory.OST_BridgeFoundations,
                BuiltInCategory.OST_CableTrayFitting,
                BuiltInCategory.OST_CableTray,
                BuiltInCategory.OST_Casework,
                BuiltInCategory.OST_Ceilings,
                BuiltInCategory.OST_Columns,
                BuiltInCategory.OST_CommunicationDevices,
                BuiltInCategory.OST_ConduitFitting,
                BuiltInCategory.OST_Conduit,
                BuiltInCategory.OST_CurtainWallPanels,
                BuiltInCategory.OST_Curtain_Systems,
                BuiltInCategory.OST_CurtainWallMullions,
                BuiltInCategory.OST_DataDevices,
                BuiltInCategory.OST_DetailComponents,
                BuiltInCategory.OST_Doors,
                BuiltInCategory.OST_DuctAccessory,
                BuiltInCategory.OST_DuctFitting,
                BuiltInCategory.OST_DuctInsulations,
                BuiltInCategory.OST_DuctCurves,
                BuiltInCategory.OST_FlexDuctCurves,
                BuiltInCategory.OST_ElectricalEquipment,
                BuiltInCategory.OST_ElectricalFixtures,
                BuiltInCategory.OST_Entourage,
                BuiltInCategory.OST_FireAlarmDevices,
                BuiltInCategory.OST_Floors,
                BuiltInCategory.OST_Furniture,
                BuiltInCategory.OST_FurnitureSystems,
                BuiltInCategory.OST_Mass,
                BuiltInCategory.OST_MechanicalEquipment,
                BuiltInCategory.OST_FabricationContainment,
                BuiltInCategory.OST_FabricationDuctwork,
                BuiltInCategory.OST_FabricationHangers,
                BuiltInCategory.OST_FabricationPipework,
                BuiltInCategory.OST_NurseCallDevices,
                BuiltInCategory.OST_Parking,
                BuiltInCategory.OST_Parts,
                BuiltInCategory.OST_BridgePiers,
                BuiltInCategory.OST_PipeAccessory,
                BuiltInCategory.OST_PipeFitting,
                BuiltInCategory.OST_PipeInsulations,
                BuiltInCategory.OST_PipeCurves,
                BuiltInCategory.OST_FlexPipeCurves,
                BuiltInCategory.OST_Planting,
                BuiltInCategory.OST_PlumbingFixtures,

                // lỗi khi check visible in view
                //BuiltInCategory.OST_Railings,

                BuiltInCategory.OST_Ramps,
                BuiltInCategory.OST_RasterImages,
                BuiltInCategory.OST_Roads,
                BuiltInCategory.OST_Roofs,
                BuiltInCategory.OST_SecurityDevices,
                BuiltInCategory.OST_ShaftOpening,
                BuiltInCategory.OST_Site,
                BuiltInCategory.OST_SpecialityEquipment,
                BuiltInCategory.OST_Sprinklers,
                BuiltInCategory.OST_Stairs,
                BuiltInCategory.OST_StructuralFramingSystem,
                BuiltInCategory.OST_StructuralColumns,
                BuiltInCategory.OST_StructConnections,
                BuiltInCategory.OST_FabricAreas,
                BuiltInCategory.OST_FabricReinforcement,
                BuiltInCategory.OST_StructuralFoundation,
                BuiltInCategory.OST_StructuralFraming,
                BuiltInCategory.OST_PathRein,
                BuiltInCategory.OST_Rebar,
                BuiltInCategory.OST_Coupler,
                BuiltInCategory.OST_StructuralStiffener,
                BuiltInCategory.OST_StructuralTruss,
                BuiltInCategory.OST_TelephoneDevices,
                BuiltInCategory.OST_Topography,
                BuiltInCategory.OST_Walls,
                BuiltInCategory.OST_Windows,
                BuiltInCategory.OST_Wire
            };
        }
    }
}
