using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class DetailRegistrationSheetModel
    {
        public int MaCTPhieuDangKy { get; set; }
        public Nullable<int> MaPhieuDangKy { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string TenSanPham { get; set; }

        public DetailRegistrationSheetModel() { }
        public DetailRegistrationSheetModel(CHITIETPHIEUDANGKY p)
        {
            MaPhieuDangKy = p.MaPhieuDangKy;
            MaCTPhieuDangKy = p.MaCTPhieuDangKy;
            MaSanPham = p.MaSanPham;
            SoLuong = p.SoLuong;
            TenSanPham = "";
        }
    }
}
