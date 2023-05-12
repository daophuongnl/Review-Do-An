using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class DisciplineType_Dict
    {
        private List<DisciplineType>? disciplineTypes;
        public List<DisciplineType> DisciplineTypes => disciplineTypes ??= DisciplineTypeUtil.GetList();

        private DisciplineType? selectedDisciplineType;
        public DisciplineType SelectedDisciplineType
        {
            get => selectedDisciplineType ??= DisciplineTypes.FirstOrDefault();
            set
            {
                selectedDisciplineType = value;
                OnDisciplineTypeChanged?.Invoke(value);
            }
        }

        public Action<DisciplineType>? OnDisciplineTypeChanged { get; set; }
    }
}
