using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class PickElementSetting : BaseModel
    {
        private DisciplineType disciplineType;
        public DisciplineType DisciplineType
        {
            get
            {
                return disciplineType;
            }
            set
            {
                disciplineType = value;
                OnDisciplineTypeChanged?.Invoke(value);
            }
        }

        public Action<DisciplineType>? OnDisciplineTypeChanged { get; set; }

        public  PickElementType PickElementType { get; set; }

        private IEnumerable<Element>? pickedElements;
        public IEnumerable<Element>? PickedElements
        {
            get
            {
                return pickedElements;
            }
            set
            {
                pickedElements = value;
                this.UpdateViewModel(() => this.Count = this.GetCount());
            }
        }

        public IEnumerable<Element>? ResultElements => this.GetResultElements();

        private int count = -1;
        public int Count
        {
            get
            {
                if (count == -1)
                {
                    count = this.GetCount();
                }
                return count;
            }
            set
            {
                count = value;
                OnCountChanged?.Invoke(value);
            }
        }

        public Action<int>? OnCountChanged { get; set; }
    }
}
