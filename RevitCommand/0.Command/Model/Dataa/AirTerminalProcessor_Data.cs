using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Form;
using Model.Entity;

namespace Model.Dataa
{
    public class AirTerminalProcessor_Data
    {
        //kho dữ liệu
        //form
        //processor

        private static AirTerminalProcessor_Data? instance;

        public static AirTerminalProcessor_Data Instance => instance ??= new AirTerminalProcessor_Data();

        //form
        private AirTerminalProcessor_Form? form;
        public AirTerminalProcessor_Form Form => this.form ??= new AirTerminalProcessor_Form { DataContext= this };

        //processor
        private AirTerminalProcessor? processor;
        public AirTerminalProcessor Processor => this.processor ??= new AirTerminalProcessor();

    }
}
