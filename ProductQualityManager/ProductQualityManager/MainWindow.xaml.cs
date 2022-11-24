using ProductQualityManager.Views;
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

namespace ProductQualityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Style = (Style)FindResource("WindowStyle");
            Main.Content = Pages.ProductPage;
        }
        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "NavEnrollPage":
                    Main.Content = Pages.EnrollPage;
                    break;

                case "NavManageOwner":
                    Main.Content = Pages.ProductPage;
                    break;
                    //case "NavEmployeeList":
                    //    Main.Content = Pages.EmployeePage;
                    //    break;
                    //case "NavEmployeeType":
                    //    Main.Content = Pages.EmployeeTypePage;
                    //    break;
                    //case "NavPartTimeScheduler":
                    //    Main.Content = Pages.PartTimeSchedulerPage;
                    //    break;
                    //case "NavSource":
                    //    Main.Content = Pages.SourcePage;
                    //    break;
                    //case "NavStatisticRevenue":
                    //    Main.Content = Pages.StatisticRevenuePage;
                    //    break;
                    //case "NavStatisticFoodType":
                    //    Main.Content = Pages.StatisticFoodTypePage;
                    //    break;
                    //case "NavRule":
                    //    Main.Content = Pages.RegulationPage;
                    //    break;
                    //case "NavDashboard":
                    //    Main.Content = Pages.DashboardPage;
                    //    break;
            }
        }
    }
}
