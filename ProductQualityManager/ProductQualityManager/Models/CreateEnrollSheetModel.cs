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
        //Giá trị + đơn vị
        public string GiaTri { get; set; }
        public decimal dec_GiaTri { get; set; }
        public string TenChiTieu { get; set; }
        public string TenDonViTinh { get; set; }

        public CreateEnrollSheetModel() { }
        public CreateEnrollSheetModel(DONVITINH dvt, decimal giatri, CHITIEUSANPHAM ctsp )
        {
            MaDonViTinh = dvt.MaDonViTinh;
            MaChiTieu = ctsp.MaChiTieu;
            TenDonViTinh = dvt.TenDonViTinh;
            TenChiTieu = ctsp.TenChiTieu;
            dec_GiaTri = giatri;
            GiaTri = giatri.ToString() + " " + dvt.TenDonViTinh;
        }
        public CreateEnrollSheetModel(CreateEnrollSheetModel preSheet)
        {
            dec_GiaTri = preSheet.dec_GiaTri;
            MaPhieuDangKy = preSheet.MaPhieuDangKy;
            MaDonViTinh = preSheet.MaDonViTinh;
            MaChiTieu = preSheet.MaChiTieu;
            TenDonViTinh = preSheet.TenDonViTinh;
            TenChiTieu = preSheet.TenChiTieu;
            GiaTri = preSheet.GiaTri;
        }
    }
}
