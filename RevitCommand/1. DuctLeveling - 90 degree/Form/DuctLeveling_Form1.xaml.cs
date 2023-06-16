using Model.Dataa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model.Entity1;
using Model.Data1;
using SingleData;
using Utility;
using Autodesk.Revit.DB.Mechanical;
using System.Diagnostics;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for PM_ProjectUC.xaml
    /// </summary>
    public partial class DuctLeveling_Form1 : System.Windows.Window
    {
        private DuctLevelingProcessor_Data1 data1 => DuctLevelingProcessor_Data1.Instance;
        private RevitData revitData => RevitData.Instance;
        public DuctLeveling_Form1()
        {
            InitializeComponent();
        }

        private void run_Clicked1(object sender, RoutedEventArgs e)
        {
            this.Close();
            //var process = data.Processor = new DuctLevelingProcessor();

            data1.Processor1.Duct = revitData.Selection.PickElement<Duct>();
            data1.Processor1.PickPoint = revitData.Selection.PickPoint();

            data1.Processor1.Do1();

            //process.Duct = revitData.Selection.PickElement<Duct>();
            //process.PickPoint = revitData.Selection.PickPoint();

            //process.Do();
            //revitData.ExternalEvent!.Raise();

        }
    }
}
