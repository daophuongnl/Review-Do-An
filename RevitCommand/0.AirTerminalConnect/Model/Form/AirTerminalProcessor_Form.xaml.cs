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
using Model.Entity;
using SingleData;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for PM_ProjectUC.xaml
    /// </summary>
    public partial class AirTerminalProcessor_Form : System.Windows.Window
    {
        private AirTerminalProcessor_Data data => AirTerminalProcessor_Data.Instance;
        private RevitData revitData => RevitData.Instance; 
        public AirTerminalProcessor_Form()
        {
            InitializeComponent();
        }

        private void run_Clicked(object sender, RoutedEventArgs e)
        {
            //revitData.ExternalEvent!.Raise();

            data.Processor.Do();
        }
    }
}
