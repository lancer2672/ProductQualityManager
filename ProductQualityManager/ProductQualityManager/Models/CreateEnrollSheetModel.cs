using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class CreateEnrollSheetModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }

        public CreateEnrollSheetModel() { }
        public CreateEnrollSheetModel(int MaSanPham ,string TenSanPham, int SoLuong)
        {
            this.MaSanPham = MaSanPham;
            this.TenSanPham = TenSanPham;
            this.SoLuong = SoLuong;
        }
    }
}
