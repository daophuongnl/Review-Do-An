using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class EntParameter
    {
        public Parameter? RevitParameter { get; set; }

        private StorageType? storageType;
        public StorageType StorageType => storageType ??= RevitParameter!.StorageType;

#if (REVIT2020_OR_LESS)
        private UnitType? unitType;
        public UnitType UnitType
        {
            get
            {
                if (unitType == null)
                {
                    unitType = this.GetUnitType();
                }
                return unitType.Value;
            }
        }
#elif (REVIT2022_OR_LESS)
#pragma warning disable CS0618 // Type or member is obsolete
        private ParameterType? parameterType;
        public ParameterType ParameterType => parameterType ??= this.GetParameterType();
#pragma warning restore CS0618 // Type or member is obsolete
#endif

        private string? name;
        public string Name => name ??= RevitParameter!.Definition.Name;

        private string? unit;
        public string Unit => unit ??= this.GetUnit();

        private dynamic? _value;
        public dynamic? Value
        {
            get => _value ??= this.GetValue();
            set
            {
                _value = value;

                this?.OnSetValue(value);
            }
        }

        private double? siValue;
        public double SIValue => siValue ??= this.GetSIValue();

        private Action<dynamic>? onSetValue;
        public Action<dynamic> OnSetValue => onSetValue ??= (value) => EntParameterUtil.OnSetValueFunc(this, value);

        public Func<string?>? Override_GetValueString { get; set; }

        private string? valueString;
        public string? ValueString
        {
            get
            {
                if (this.Override_GetValueString != null)
                {
                    return this.Override_GetValueString();
                }

                return valueString ??= this.GetValueString();
            }
        }
    }
}
