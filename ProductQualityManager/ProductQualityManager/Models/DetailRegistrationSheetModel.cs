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
        public int MaChiTieu { get; set; }
        public string GiaTriDangKy { get; set; }
        public string TenChiTieu { get; set; }
        public string TenDonViTinh { get; set; }
        public string KetQuaSoSanh { get; set; }
        public string GiaTriTieuChuan { get; set; }
        public string Color_KetQuaSoSanh { get; set; }
        public DetailRegistrationSheetModel() { }
        public DetailRegistrationSheetModel(CHITIETPHIEUDANGKY p, DONVITINH u, CHITIEUSANPHAM c)
        {
            this.MaCTPhieuDangKy = p.MaChiTietDangKy;
            this.MaChiTieu = c.MaChiTieu;
            this.TenChiTieu = c.TenChiTieu;
            this.TenDonViTinh = u.TenDonViTinh;
            this.GiaTriDangKy = p.GiaTriDangKy.ToString() +" "+ u.TenDonViTinh;
            this.GiaTriTieuChuan = c.GiaTriTieuChuan.ToString() +" "+ u.TenDonViTinh;
        }
    }
}
