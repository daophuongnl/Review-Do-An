using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntParameterUtil
    {
        public static EntParameter GetEntParameter(this Parameter p)
        {
            return new EntParameter
            {
                RevitParameter = p
            };
        }

        public static List<EntParameter> GetEntParameters(this EntParameterDict dict)
        {
            try
            {
                return new List<EntParameter>();
            }
            catch
            {
                throw;
            }
        }

        public static EntParameter? GetEntParameter(this EntParameterDict dict, string name)
        {
            var list = dict.Parameters;
            var param = list.FirstOrDefault(x => x.Name == name);
            if (param == null)
            {
                var elem = dict.Element!.RevitElement!;
                var p = elem.LookupParameter(name);
                if (p != null)
                {
                    param = p.GetEntParameter();
                    list.Add(param);
                }
            }

            return param;
        }

        public static EntParameter? GetEntParameter(this EntParameterDict dict, List<string> names)
        {
            if (names == null) return null;

            EntParameter? param = null;
            foreach (var name in names)
            {
                param = dict[name];
                if (param != null)
                {
                    goto L1;
                }
            }

        L1:
            return param;
        }

        public static EntParameter GetEntParameter(this EntParameterDict dict, EntFilter filter)
        {
            if (!dict.IsFullParameters) throw new Exception("Vui lòng chọn loại Full Parameter cho bộ lọc này");
            return dict.Parameters.FirstOrDefault(x => filter.IsValid(x.Name));
        }

        // Property
        public static dynamic? GetValue(this EntParameter entParam)
        {
            var storageType = entParam.StorageType;
            var param = entParam.RevitParameter;
            if (param is null) return null;

            dynamic? value = null;
            switch (storageType)
            {
                case StorageType.Integer:
                    value = param.AsInteger();
                    break;
                case StorageType.Double:
                    value = param.AsDouble();
                    break;
                case StorageType.String:
                    value = param.AsString();
                    break;
                case StorageType.ElementId:
                    value = param.AsElementId();
                    break;
            }

            return value;
        }

#if (REVIT2020_OR_LESS)
        public static UnitType GetUnitType(this EntParameter entParam)
        {
            return entParam.RevitParameter!.Definition.UnitType;
        }
#elif (REVIT2022_OR_LESS)
#pragma warning disable CS0618 // Type or member is obsolete
        public static ParameterType GetParameterType(this EntParameter entParam)
        {
            return entParam.RevitParameter!.Definition.ParameterType;
        }
#pragma warning restore CS0618 // Type or member is obsolete
#endif

        public static string GetValueString(this EntParameter entParam)
        {
            if (entParam.StorageType == StorageType.String)
            {
                return entParam.Value!;
            }
            return entParam.RevitParameter!.AsValueString();
        }

        public static string GetUnit(this EntParameter entParam)
        {
            string? unit = null;

#if (REVIT2020_OR_LESS)
            switch (entParam.UnitType)
            {
                case UnitType.UT_Length:
                    unit = ValueUnit.m;
                    break;
                case UnitType.UT_Area:
                    unit = ValueUnit.m2;
                    break;
                case UnitType.UT_Volume:
                    unit = ValueUnit.m3;
                    break;
                case UnitType.UT_Mass:
                    unit = ValueUnit.kg;
                    break;
            }
#elif (REVIT2022_OR_LESS)
#pragma warning disable CS0618 // Type or member is obsolete
            switch (entParam.ParameterType)
            {
                case ParameterType.Length:
                    unit = ValueUnit.m;
                    break;
                case ParameterType.Area:
                    unit = ValueUnit.m2;
                    break;
                case ParameterType.Volume:
                    unit = ValueUnit.m3;
                    break;
                case ParameterType.Mass:
                    unit = ValueUnit.kg;
                    break;
            }
#pragma warning restore CS0618 // Type or member is obsolete
#else
            var valueType = entParam.RevitParameter!.Definition.GetDataType().TypeId;
            if (valueType.Contains("length"))
            {
                unit = ValueUnit.m;
            }
            else if (valueType.Contains("area"))
            {
                unit = ValueUnit.m2;
            }
            else if (valueType.Contains("volume"))
            {
                unit = ValueUnit.m3;
            }
            else if (valueType.Contains("mass"))
            {
                unit = ValueUnit.m3;
            }
#endif

            return unit!;
        }

        public static double GetSIValue(this EntParameter entParam)
        {
            double siValue = 0;
            var value = entParam.Value;

            switch (entParam.StorageType)
            {
                case StorageType.Integer:
                    siValue = value;
                    break;
                case StorageType.Double:
                    {
                        var val = (double)value;

                        switch (entParam.Unit)
                        {
                            case ValueUnit.m:
                                siValue = val.feet2Meter();
                                break;
                            case ValueUnit.m2:
                                siValue = val.feet2MeterSquare();
                                break;
                            case ValueUnit.m3:
                                siValue = val.feet2MeterCubic();
                                break;
                        }
                    }
                    break;
            }

            return siValue;
        }

        // Method
        public static void OnSetValueFunc(this EntParameter entParam, dynamic value)
        {
            var param = entParam.RevitParameter!;
            if (value is int num)
            {
                if (num != 0)
                {
                    param.Set(num);
                }
            }
            else if (value is double num2)
            {
                if (num2 != 0)
                {
                    param.Set(num2);
                }
            }
            if (value is string s)
            {
                param.Set(s);
            }
            if (value is ElementId elemId)
            {
                param.Set(elemId);
            }
        }
    }
}
