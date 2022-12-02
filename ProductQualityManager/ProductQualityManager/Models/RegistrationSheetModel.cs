using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class RegistrationSheetModel
    {

        public RegistrationSheetModel() { }
        public RegistrationSheetModel(PHIEUDANGKY PhieuDangKy, string DonViTinhSanPham, string TenCoSo, string TenSanPham)
        {
            this.MaPhieuDangKy = PhieuDangKy.MaPhieuDangKy;
            this.MaCoSo = (int)PhieuDangKy.MaCoSo;
         
            this.MaSanPham = (int)PhieuDangKy.MaSanPham;
            this.SoLuong = (int)PhieuDangKy.SoLuong;
            this.TenCoSo = TenCoSo;
            this.TenSanPham = TenSanPham;
            this.DonViTinh = DonViTinhSanPham;
            this.NgayDangKy = (DateTime)PhieuDangKy.NgayDangKy;
            this.ThoiHanDangKy = (DateTime)PhieuDangKy.HanDangKy;
            this.TrangThai = (int)PhieuDangKy.TrangThai;
            MauChu = "";
            STrangThai = "";
           
        }
  
        public int MaPhieuDangKy { get; set; }
     
        public int MaSanPham { get; set; }
        public int MaCoSo { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime ThoiHanDangKy { get; set; }
        public int TrangThai { get; set; }
        public int SoLuong { get; set; }
        public string MauChu { get; set; }
        public string TenCoSo { get; set; }
        public string TenSanPham { get; set; }
        public string DonViTinh { get; set; }
        public string STrangThai { get; set; }    }
}
