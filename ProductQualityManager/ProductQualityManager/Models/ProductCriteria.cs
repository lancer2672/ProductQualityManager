using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class ProductCriteria
    {
        //public int MaCTChiTieu { get; set; }
        //public Nullable<int> MaChiTieu { get; set; }
        //public Nullable<int> MaDangKyChiTieu { get; set; }
        public Nullable<decimal> GiaTriDangKy { get; set; }
        public string TenChiTieu { get; set; }
        public decimal GiaTriTieuChuan { get; set; }
        public string TenDonViTinh { get; set; }
        public ProductCriteria( decimal giaTriDangKy, string tenChiTieu, decimal giaTriTieuChuan, string tenDonViTinh)
        {
            //MaCTChiTieu = maCTChiTieu;
            //MaChiTieu = maChiTieu;
            //MaDangKyChiTieu = maDangKyChiTieu;
            GiaTriDangKy = giaTriDangKy;
            TenChiTieu = tenChiTieu;
            GiaTriTieuChuan = giaTriTieuChuan;
            TenDonViTinh = tenDonViTinh;
        }
        public ProductCriteria() { }
    }
}
