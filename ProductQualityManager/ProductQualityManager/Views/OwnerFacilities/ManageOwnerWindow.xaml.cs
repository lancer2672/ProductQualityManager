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
using ProductQualityManager.Views.LoginAndSignUp;

namespace ProductQualityManager.Views.OwnerFacilities
{
    /// <summary>
    /// Interaction logic for ManageOwnerWindow.xaml
    /// </summary>
    public partial class ManageOwnerWindow : Window
    {
        public ManageOwnerWindow(int IdOwner)
        {
            InitializeComponent();
            ManageOwnerViewModel manageownerVM = new ManageOwnerViewModel(IdOwner);
            this.DataContext = manageownerVM;
        }

        // Đăng xuất
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); 
            this.Close();
        }
    }
}
