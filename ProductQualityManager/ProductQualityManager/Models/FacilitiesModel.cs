using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductQualityManager.Models.Database;

namespace ProductQualityManager.Models
{
    public class Products
    {
        public int IdProduct { get; set; }
        public Nullable<int> IdFacility { get; set; }
        public string NameProduct { get; set; }
        public string NameUnit { get; set; }
        public string Status { get; set; }
    }
}
