using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;  

namespace Model.Entity
{
    public class EntFamily
    {
        protected Type? targetElementClass;
        public virtual Type? TargetElementClass
        {
            get=>targetElementClass;
            set => targetElementClass = value;
        }

        protected BuiltInCategory? targetBuiltInCategory;
        public virtual BuiltInCategory TargetBuiltInCategory
        {
            get=> targetBuiltInCategory!.Value;
            set => targetBuiltInCategory = value;
        }

        protected string? name;
        public virtual string? Name
        {
            get => name;
            set => name = value;
        }

        //protected EntParameterSet entParameterSet;
        //public virtual EntParameterSet EntParameterSet
        //{
        //    get
        //    {
        //        if (entParameterSet == null)
        //        {
        //            entParameterSet = this.GetEntParameterSet();
        //        }
        //        return entParameterSet;
        //    }
        //    set
        //    {
        //        entParameterSet = value;
        //    }
        //}

        protected IEnumerable<Element>? allElements;
        public virtual IEnumerable<Element>? AllElements
        {
            get => allElements ??= this.GetAllElements();
            set => allElements = value;
        }

        protected Func<Element, bool>? filterElementFunc;
        public virtual Func<Element, bool> FilterElementFunc => filterElementFunc ??= x => this.FilterElement(x);
    }
}
