using ProductQualityManager.Models.Database;
using ProductQualityManager.ViewModels.RegistrationSheet;
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

namespace ProductQualityManager.Views.TestSheet
{
    /// <summary>
    /// Interaction logic for SubmitCreateSheet.xaml
    /// </summary>
    public partial class SubmitCreateSheet : Window
    {
        public SubmitCreateSheet(CreateRegistrationSheetViewModel vm, COSOSANXUAT facility, CHUCOSO owner)
        {
            InitializeComponent();
            SubmitCreateSheetViewModel viewModel = new SubmitCreateSheetViewModel(vm, facility, owner);
            this.DataContext = viewModel;
        }
    }
}
