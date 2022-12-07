using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class CreateTestingSheetModel
    {
        public int MaDonViTinh { get; set; }
        public int MaChiTieu { get; set; }
        //Giá trị tiêu chuẩn + đơn vị
        public string GiaTri { get; set; }
        public decimal Dec_GiaTri { get; set; }
        //Giá trị thực tế + đơn vị
        public string GiaTriThucTe { get; set; }
        public decimal Dec_GiaTriThucTe { get; set; }
        //Giá trị đăng ký trên phiếu đăng ký + đơn vị
        public string GiaTriDangKy { get; set; }
        public decimal Dec_GiaTriDangKy { get; set; }

        public string TenChiTieu { get; set; }
        public string TenDonViTinh { get; set; }
        public string DanhGia { get; set; }
        public string Color_DanhGia { get; set; }

        public CreateTestingSheetModel() { }

        public CreateTestingSheetModel(DONVITINH dvt, CHITIETPHIEUDANGKY ctdk,decimal  GiaTriThucTe , CHITIEUSANPHAM ctsp)
        {
            MaDonViTinh = dvt.MaDonViTinh;
            MaChiTieu = ctsp.MaChiTieu;
            TenDonViTinh = dvt.TenDonViTinh;
            TenChiTieu = ctsp.TenChiTieu;
            Dec_GiaTri = (decimal)ctsp.GiaTriTieuChuan;
            Dec_GiaTriDangKy = (decimal)ctdk.GiaTriDangKy;
            Dec_GiaTriThucTe = GiaTriThucTe;
            GiaTriDangKy = ctdk.GiaTriDangKy.ToString() + " " + dvt.TenDonViTinh;
            //Nếu giá trị = -1 tức là chưa nhập vào giá trị thực tế 
            this.GiaTriThucTe =  GiaTriThucTe == -1 ?   "" :  GiaTriThucTe.ToString() +  " " + dvt.TenDonViTinh;
            GiaTri = Dec_GiaTri.ToString() + " " + dvt.TenDonViTinh;
        }

        public CreateTestingSheetModel(CreateTestingSheetModel preSheet)
        {
            Dec_GiaTri = preSheet.Dec_GiaTri;
            Dec_GiaTriThucTe = preSheet.Dec_GiaTriThucTe;
            GiaTriThucTe = preSheet.GiaTriThucTe;
            MaDonViTinh = preSheet.MaDonViTinh;
            MaChiTieu = preSheet.MaChiTieu;
            TenDonViTinh = preSheet.TenDonViTinh;
            TenChiTieu = preSheet.TenChiTieu;
            GiaTri = preSheet.GiaTri;
        }
    }
}
