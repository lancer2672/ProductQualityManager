using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class ModificationHistoryModel
    {
        public int MaPhieu { get; set; }
        public Nullable<int> MaPhieuDangKy { get; set; }
        public Nullable<System.DateTime> ThoiGianChinhSua { get; set; }
        public Nullable<int> GiaTriChinhSua { get; set; }
        public string SGiaTriChinhSua { get; set; }
        public string MauChu { get; set; }
        public ModificationHistoryModel(LICHSUDUYETPHIEUDANGKY item)
        {
            this.MaPhieu = item.MaPhieu;
            this.MaPhieuDangKy = item.MaPhieuDangKy;
            this.ThoiGianChinhSua = item.ThoiGianChinhSua;
            this.GiaTriChinhSua = item.GiaTriChinhSua;
            this.SGiaTriChinhSua = item.GiaTriChinhSua == -1 ? "Từ chối" : "Chấp thuận";
            this.MauChu = item.GiaTriChinhSua == -1 ? "Red" : "Green";
        }
    }
}
