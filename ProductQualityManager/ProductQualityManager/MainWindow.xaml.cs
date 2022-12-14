using ProductQualityManager.Views;
using ProductQualityManager.Views.TestingSheet;
using ProductQualityManager.Views.LoginAndSignUp;
using ProductQualityManager.Views.TestSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using ProductQualityManager.Views.ProductCriteria;
using ProductQualityManager.Views.Report;
using ProductQualityManager.ViewModels;

namespace ProductQualityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EnrollSheet enrollSheet;
        TestingSheet testingSheet;
        ManageProductCriteria manageProductCriteria;
        Report report;
        SignUpWindow signUpWindow;
  
        public MainWindow()
        {
            InitializeComponent();
            this.enrollSheet = new EnrollSheet();
            this.testingSheet = new TestingSheet();
            this.manageProductCriteria = new ManageProductCriteria();
            this.report = new Report();
            this.signUpWindow = new SignUpWindow();

            //default
            this.content_Control.Content = enrollSheet;
            //Style = (Style)FindResource("WindowStyle");


        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                //Do your stuff
                switch (item.Name)
                {
                    case "EnrollSheet":
                        {
                            this.content_Control.Content = enrollSheet;
                            break;
                        }
                    case "TestingSheet":
                        {
                            this.content_Control.Content = testingSheet;
                            break;
                        }
                    case "Criteria":
                        {
                            this.content_Control.Content = manageProductCriteria;
                            break;
                        }
                    case "Report":
                        {
                            this.content_Control.Content = report;
                            break;
                        }
                    case "SignUp":
                        {
                            this.content_Control.Content = signUpWindow;
                            break;
                        }
                    case "SignOut" :
                        {
                            LoginWindow loginWindow = new LoginWindow();
                            loginWindow.Show();
                            this.Close();
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (enrollSheet.DataContext as RegistrationSheetViewModel).LoadDataSheetList();
        }
    }
}
