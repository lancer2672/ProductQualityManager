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
    /// Interaction logic for ViewDetailWindow.xaml
    /// </summary>
    public partial class ViewDetailWindow : Window
    {
        public ViewDetailWindow(TestingSheetModel SelectedItem)
        {
            InitializeComponent();
            DetailTestingSheetViewModel VM = new DetailTestingSheetViewModel(SelectedItem);
            this.DataContext = VM;
        }
    }
}
