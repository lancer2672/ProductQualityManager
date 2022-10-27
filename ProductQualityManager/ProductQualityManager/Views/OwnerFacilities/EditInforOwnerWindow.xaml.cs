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
using ProductQualityManager.ViewModels.OwnerFacilitiesVM;
using ProductQualityManager.ViewModels;

namespace ProductQualityManager.Views.OwnerFacilities
{
    /// <summary>
    /// Interaction logic for EditInforOwnerWindow.xaml
    /// </summary>
    public partial class EditInforOwnerWindow : Window
    {
        public EditInforOwnerWindow(ManageOwnerViewModel ManageOwnerVM)
        {
            InitializeComponent();
            EditInforOwnerViewModel editinforVM = new EditInforOwnerViewModel(ManageOwnerVM);
            this.DataContext = editinforVM;
        }
    }
}
