using ProductQualityManager.Views.OwnerFacilities;
using ProductQualityManager.Views.Product;
using ProductQualityManager.Views.ProductCriteria;
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
        public static Page ProductCriteriaPage { get => pages[2]; }
        static Pages()
        {
            pages.Add(new EnrollSheet());
            pages.Add(new ManageProducts());
            pages.Add(new ManageProductCriteria());
        }
    }
}
