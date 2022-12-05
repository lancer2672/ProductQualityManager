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
        public int MaSanPham { get; set; }
        public Nullable<int> MaCoSo { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<int> MaDonViTinhSP { get; set; }
        public string StatusRegister { get; set; }
        public string Result { get; set; }
    }
}
