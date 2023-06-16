using Autodesk.Revit.DB.Mechanical;
using Model.Dataa;
using Model.Entity1;
using Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data1
{
    public class DuctLevelingProcessor_Data1
    {
        //kho dữ liệu
        private static DuctLevelingProcessor_Data1? instance;
        public static DuctLevelingProcessor_Data1 Instance => instance ??= new DuctLevelingProcessor_Data1();



        //form
        private DuctLeveling_Form1? form;
        public DuctLeveling_Form1 Form => this.form ??= new DuctLeveling_Form1 { DataContext = this };

        //processor
        private DuctLevelingProcessor1? processor1;
        public DuctLevelingProcessor1 Processor1 => this.processor1 ??= new DuctLevelingProcessor1();


        //public DuctLevelingProcessor Processor
        //{
        //    get => processor!;
        //    set => processor = value;
        //}

    }
}
