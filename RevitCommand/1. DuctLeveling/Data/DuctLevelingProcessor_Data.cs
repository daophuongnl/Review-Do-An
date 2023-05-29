using Autodesk.Revit.DB.Mechanical;
using Model.Dataa;
using Model.Entity;
using Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class DuctLevelingProcessor_Data
    {
        //kho dữ liệu
        private static DuctLevelingProcessor_Data? instance;
        public static DuctLevelingProcessor_Data Instance => instance ??= new DuctLevelingProcessor_Data();



        //form
        private DuctLeveling_Form? form;
        public DuctLeveling_Form Form => this.form ??= new DuctLeveling_Form { DataContext = this };

        //processor
        private DuctLevelingProcessor? processor;
        public DuctLevelingProcessor Processor => this.processor ??= new DuctLevelingProcessor();


    }
}
