using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class CreateEnrollSheetModel
    {
        public int MaChiTietDangKy { get; set; } // Primary key
        public int MaPhieuDangKy { get; set; }
        public int MaDonViTinh { get; set; }
        public int MaChiTieu { get; set; }
        public string GiaTri { get; set; }
        public string TenChiTieu { get; set; }
        public string TenDonViTinh { get; set; }

        public CreateEnrollSheetModel() { }
        public CreateEnrollSheetModel(PHIEUDANGKY pdk,DONVITINH dvt, CHITIETPHIEUDANGKY ct, CHITIEUSANPHAM ctsp )
        {
            MaChiTietDangKy = ct.MaChiTietDangKy;
            MaPhieuDangKy = pdk.MaPhieuDangKy;
            MaDonViTinh = dvt.MaDonViTinh;
            MaChiTieu = ctsp.MaChiTieu;
            TenDonViTinh = dvt.TenDonViTinh;
            TenChiTieu = ctsp.TenChiTieu;
            GiaTri = ct.GiaTriDangKy.ToString() + " " + dvt.TenDonViTinh;
        }
        public CreateEnrollSheetModel(CreateEnrollSheetModel pre)
        {
            MaChiTietDangKy = pre.MaChiTietDangKy;
            MaPhieuDangKy = pre.MaPhieuDangKy;
            MaDonViTinh = pre.MaDonViTinh;
            MaChiTieu = pre.MaChiTieu;
            TenDonViTinh = pre.TenDonViTinh;
            TenChiTieu = pre.TenChiTieu;
            GiaTri = pre.GiaTri;
        }
    }
}
