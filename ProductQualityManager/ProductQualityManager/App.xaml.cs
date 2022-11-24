using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProductQualityManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzY1NzE1QDMyMzAyZTMzMmUzMGFKSU0veGNudy84ZXZVRXBmUTFaNzJOMS9wR1ZRRGY5ZDNETkdFVDlTdFk9");
        }
    }
}
