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
        public int MaPhieuDangKy { get; set; }
        public int MaDonViTinh { get; set; }
        public int MaChiTieu { get; set; }
        public string GiaTri { get; set; }
        public int int_GiaTri { get; set; }
        public string TenChiTieu { get; set; }
        public string TenDonViTinh { get; set; }

        public CreateEnrollSheetModel() { }
        public CreateEnrollSheetModel(DONVITINH dvt, int giatri, CHITIEUSANPHAM ctsp )
        {
            MaDonViTinh = dvt.MaDonViTinh;
            MaChiTieu = ctsp.MaChiTieu;
            TenDonViTinh = dvt.TenDonViTinh;
            TenChiTieu = ctsp.TenChiTieu;
            int_GiaTri = giatri;
            GiaTri = giatri.ToString() + " " + dvt.TenDonViTinh;
        }
        public CreateEnrollSheetModel(CreateEnrollSheetModel pre)
        {
            int_GiaTri = pre.int_GiaTri;
            MaPhieuDangKy = pre.MaPhieuDangKy;
            MaDonViTinh = pre.MaDonViTinh;
            MaChiTieu = pre.MaChiTieu;
            TenDonViTinh = pre.TenDonViTinh;
            TenChiTieu = pre.TenChiTieu;
            GiaTri = pre.GiaTri;
        }
    }
}
