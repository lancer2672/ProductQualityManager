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
        public RegistrationSheetModel(PHIEUDANGKY PhieuDangKy)
        {
            this.MaPhieuDangKy = PhieuDangKy.MaPhieuDangKy;
            this.MaCoSo = PhieuDangKy.MaCoSo;
            this.NgayDangKy = PhieuDangKy.NgayDangKy;
            this.ThoiHanDangKy = PhieuDangKy.HanDangKy;
            this.TrangThai = PhieuDangKy.TrangThai;
            MauChu = "";
            STrangThai = "";
           
        }
  
        public int MaPhieuDangKy { get; set; }
        public Nullable<int> MaCoSo { get; set; }
        public Nullable<System.DateTime> NgayDangKy { get; set; }
        public Nullable<DateTime> ThoiHanDangKy { get; set; }
        public Nullable<int> TrangThai { get; set; }
        public string MauChu { get; set; }
        public string STrangThai { get; set; }    }
}
