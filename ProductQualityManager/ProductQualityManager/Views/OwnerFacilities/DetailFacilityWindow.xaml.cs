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
using ProductQualityManager.Models.Database;
using ProductQualityManager.ViewModels.OwnerFacilitiesVM;

namespace ProductQualityManager.Views.OwnerFacilities
{
    /// <summary>
    /// Interaction logic for DetailFacilityWindow.xaml
    /// </summary>
    public partial class DetailFacilityWindow : Window
    {
        public DetailFacilityWindow(COSOSANXUAT SelectedFacility)
        {
            InitializeComponent();
            DetailFacilityViewModel detailfacilityVM = new DetailFacilityViewModel(SelectedFacility);
            this.DataContext = detailfacilityVM;
        }
    }
}
