using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Form;
using Model.Entity;
using Autodesk.Revit.DB.Mechanical;

namespace Model.Dataa
{
    public class AirTerminalProcessor_Data
    {
        //kho dữ liệu
        private static AirTerminalProcessor_Data? instance;
        public static AirTerminalProcessor_Data Instance => instance ??= new AirTerminalProcessor_Data();

        //form
        private AirTerminalProcessor_Form? form;
        public AirTerminalProcessor_Form Form => this.form ??= new AirTerminalProcessor_Form { DataContext= this };

        //data
        private List<FlexDuctType>? flexDuctTypes;
        public List<FlexDuctType> FlexDuctTypes => flexDuctTypes??= this.GetFlexDuctTypes();

        private List<DuctType>? ductTypes;
        public List<DuctType> DuctTypes => ductTypes ??= this.GetDuctTypes();

        //processor
        private AirTerminalProcessor? processor;
        public AirTerminalProcessor Processor => this.processor ??= new AirTerminalProcessor();
        //public AirTerminalProcessor Processor
        //{
        //    get => processor!;
        //    set => processor = value;
        //}

    }
}
