using ProductQualityManager.Models;
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
    /// Interaction logic for DetailTestingSheet.xaml
    /// </summary>
    public partial class DetailTestingSheet : Window
    {
        public DetailTestingSheet(TestingSheetModel item)
        {
            InitializeComponent();
            DetailTestingSheetViewModel newVM = new DetailTestingSheetViewModel(item);
            this.DataContext = newVM;
        }
    }
}
