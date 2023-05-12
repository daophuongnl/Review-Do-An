using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public static class EntFilterUtil
    {
        #region Method

        public static bool IsValid(this EntFilter filter, dynamic? value)
        {
            if (value == null) return false;

            var isValid = false;
            var filterValues = filter.values!;
            switch (filter.valueType)
            {
                case FilterValueType.bystring:
                    if (value is string val)
                    {
                        var filterType = filter.stringFilterType;
                        switch (filterType)
                        {
                            case StringFilterType.byequal:
                                foreach (var filterValue in filterValues)
                                {
                                    if (filterValue == val)
                                    {
                                        isValid = true; goto L1;
                                    }
                                }
                                break;
                            case StringFilterType.bycontain:
                                var lowerVal = val.ToLower();
                                foreach (var filterValue in filterValues)
                                {
                                    if (lowerVal.Contains(filterType.ToLower()))
                                    {
                                        isValid = true; goto L1;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case FilterValueType.bynumber:
                    if (value.IsNumber())
                    {

                    }
                    break;
            }

            L1:
            return isValid;
        }

        public static bool IsCategoryValid(this EntElement entElem, EntFilter filter)
        {
            var categoryName = entElem.Category?.Name;
            return filter.IsValid(categoryName);
        }

        #endregion
    }
}
