using ProductQualityManager.Views.OwnerFacilities;
using ProductQualityManager.Views.Product;
using ProductQualityManager.Views.TestSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductQualityManager.Views
{
    internal class Pages
    {
        public static List<Page> pages = new List<Page>();
        public static Page EnrollPage { get => pages[0]; }
        public static Page ProductPage { get => pages[1]; }
        static Pages()
        {
            pages.Add(new EnrollSheet());
            pages.Add(new ManageProducts());

        }
    }
}
