using Autodesk.Revit.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ElementFilterUtil
    {
        public static ElementFilter Get(DisciplineType disciplineType)
        {
            var bics = BuiltInCategoryUtil.List(disciplineType);
            return Get(bics);
        }

        public static ElementFilter Get(Type type)
        {
            return new ElementClassFilter(type);

        }
        public static ElementFilter Get<T>() where T : Autodesk.Revit.DB.Element
        {
            return new ElementClassFilter(typeof(T));
        }

        public static ElementFilter Get(params Type[] classes)
        {
            return new ElementMulticlassFilter(classes);
        }

        public static ElementFilter Get(IEnumerable<Type> classes)
        {
            return new ElementMulticlassFilter(classes is IList<Type> ? (IList<Type>)classes : classes.ToList());
        }

        public static ElementFilter Get(BuiltInCategory bic)
        {
            return new ElementCategoryFilter(bic);
        }

        public static ElementFilter Get(IEnumerable<BuiltInCategory> bics, bool inverter = false)
        {
            return new ElementMulticategoryFilter(bics is ICollection<BuiltInCategory> ? (ICollection<BuiltInCategory>)bics : bics.ToList()
                , inverter);
        }

        public static ElementFilter Get<T>(BuiltInCategory bic) where T : Autodesk.Revit.DB.Element
        {
            return Get<T>().And(Get(bic));
        }

        public static ElementFilter Get<T>(IEnumerable<BuiltInCategory> bics) where T : Autodesk.Revit.DB.Element
        {
            return Get<T>().And(Get(bics));
        }

        public static ElementFilter GetElementFilter(this View view)
        {
            // filter by model category
            var modelbics = BuiltInCategoryUtil.ModelList().Where(x =>
            {
                return view.GetCategoryHidden(new ElementId(x));
            });

            // filter by filterset
            var filter = Get(modelbics, true);

            var view_filters = view.GetFilters();
            var visible_filters = view_filters.Where(x =>
            {
                var res = true;
#if (REVIT2020_OR_LESS)
                res = res && view.IsFilterApplied(x);
#elif (REVIT2021_OR_GREATER)
                res = res && view.GetIsFilterEnabled(x);

#endif
                res = res && !view.GetFilterVisibility(x);
                return res;
            });

            var filter_filters = visible_filters.Select(x =>
             {
                 var pfe = x.GetElement<ParameterFilterElement>();

                 var bics = pfe.GetCategories().Select(c => (BuiltInCategory)c.IntegerValue);

                 // bởi vì not visible trong view nên phải tiến hành invert
                 //var param_fil = ((ElementParameterFilter)((LogicalAndFilter)pfe.GetElementFilter()).GetFilters()[0]).Invert();

                 var pfe_filters = ((ElementLogicalFilter)pfe.GetElementFilter()).GetFilters();
                 var paramFilter = pfe_filters.Select(x =>
                 {
                     return ((ElementParameterFilter)x).Invert();
                 }).And()!;

                 return Get(bics, true).Or(Get(bics).And(paramFilter));

                 //return Get(bics).And(paramFilter);
             }).Or()!;

            // filter by workset



            filter = filter.And(filter_filters);
            return filter;
        }

        //
        // method
        //

        public static ElementFilter And(this ElementFilter filter, ElementFilter otherFilter)
        {
            return otherFilter != null ? new LogicalAndFilter(filter, otherFilter) : filter;
        }

        public static ElementFilter Or(this ElementFilter filter, ElementFilter otherFilter)
        {
            return otherFilter != null ? new LogicalOrFilter(filter, otherFilter) : filter;
        }

        public static ElementFilter? And(this IEnumerable<ElementFilter> filters)
        {
            return filters.Any() ?
                (filters.Count() != 1 ? new LogicalAndFilter(filters is IList<ElementFilter> ? (IList<ElementFilter>)filters : filters.ToList())
                : filters.First())
                : null;
        }

        public static ElementFilter? Or(this IEnumerable<ElementFilter> filters)
        {
            return filters.Any() ?
                (filters.Count() != 1 ? new LogicalOrFilter(filters is IList<ElementFilter> ? (IList<ElementFilter>)filters : filters.ToList())
                : filters.First())
                : null;
        }

        public static ElementParameterFilter Invert(this ElementParameterFilter q)
        {
            var rules = q.GetRules();
            // không xử lý truy vấn được đối với Parameter Workset
            return new ElementParameterFilter(rules, !q.Inverted);
        }
    }
}
