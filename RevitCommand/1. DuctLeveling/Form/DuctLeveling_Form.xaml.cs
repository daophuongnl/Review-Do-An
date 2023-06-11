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
using Model.Data;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for PM_ProjectUC.xaml
    /// </summary>
    public partial class DuctLeveling_Form : System.Windows.Window
    {
        private DuctLevelingProcessor_Data data => DuctLevelingProcessor_Data.Instance;
        public DuctLeveling_Form()
        {
            InitializeComponent();
        }

        private void run_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
            data.Processor.Do();
        }
    }
}
