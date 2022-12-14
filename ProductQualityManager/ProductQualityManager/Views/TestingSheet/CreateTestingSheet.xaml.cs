using ProductQualityManager.ViewModels.TestingSheet;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProductQualityManager.Views.TestingSheet
{
    /// <summary>
    /// Interaction logic for CreateTestingSheet.xaml
    /// </summary>
    public partial class CreateTestingSheet : Window
    {
        public CreateTestingSheet(TestingSheetViewModel vm)
        {
            InitializeComponent();
            this.DataContext = new CreateSheetViewModel(vm);
        }
    }
}
