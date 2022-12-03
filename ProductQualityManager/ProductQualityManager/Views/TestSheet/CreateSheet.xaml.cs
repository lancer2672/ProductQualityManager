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

namespace ProductQualityManager.Views.TestingSheet
{
    /// <summary>
    /// Interaction logic for CreateSheet.xaml
    /// </summary>
    public partial class CreateSheet : Window
    {
        public CreateSheet(COSOSANXUAT selectedFacility)
        {
            InitializeComponent();
            CreateRegistrationSheetViewModel VM = new CreateRegistrationSheetViewModel(selectedFacility);
            this.DataContext = VM;
        }
    }
}
