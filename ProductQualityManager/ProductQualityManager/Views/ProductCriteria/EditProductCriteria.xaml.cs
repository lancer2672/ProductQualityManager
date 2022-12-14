using ProductQualityManager.ViewModels.ProductCriteriaVM;
using ProductQualityManager.Models;
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

namespace ProductQualityManager.Views.ProductCriteria
{
    /// <summary>
    /// Interaction logic for EditProductCriteria.xaml
    /// </summary>
    public partial class EditProductCriteria : Window
    {
        public EditProductCriteria(ProductQualityManager.Models.ProductCriteria selectedItem, ManageProductCriteriaViewModel ProductCriteriaVM)
        {
            InitializeComponent();
            EditProductCriteriaViewModel editCriteriaVM = new EditProductCriteriaViewModel(selectedItem, ProductCriteriaVM);
            this.DataContext = editCriteriaVM;
        }
    }
}
