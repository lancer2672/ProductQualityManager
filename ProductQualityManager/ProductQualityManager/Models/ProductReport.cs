using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class ProductReport
    {
        public Nullable<int> MaSanPham { get; set; }
        public Nullable<int> MaCoSo { get; set; }
        public string TenSanPham { get; set; }
        public string TenCoSo { get; set; }
        public string TinhTrang { get; set; }
        public ProductReport(SANPHAM SanPham)
        {
            this.MaSanPham = SanPham.MaSanPham;
            this.MaCoSo = SanPham.MaCoSo;
            this.TenSanPham = SanPham.TenSanPham;
            this.TinhTrang = SanPham.TinhTrang;
        }
        public ProductReport() { }
    }
}
