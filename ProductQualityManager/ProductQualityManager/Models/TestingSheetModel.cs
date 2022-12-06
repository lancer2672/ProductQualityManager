using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.Models
{
    public class TestingSheetModel
    {
        public int MaPhieuKiemNghiem { get; set; }
        public int MaCoSo { get; set; }
        public DateTime NgayDanhGia { get; set; }
        public string TenCoSo { get; set; }
        public string KetQua { get; set; }
        public string Color_KetQua { get; set; }
        public TestingSheetModel() { }
        public TestingSheetModel(PHIEUKIEMNGHIEM p, COSOSANXUAT t) {
            this.MaPhieuKiemNghiem = p.MaPhieuKiemNghiem;
            this.MaCoSo = t.MaCoSo;
            this.NgayDanhGia = (DateTime)p.NgayDanhGia;
            this.TenCoSo = t.TenCoSo;
            this.KetQua = getKetQua((int)p.KetQua);
            this.Color_KetQua = getColorKetQua((int)p.KetQua);

        }
            
        private string getKetQua(int kq)
        {
            switch (kq)
            {
                case -1:
                    {
                        return "Không đạt";
                       
                    }
                case -2:
                    {
                        return "Nguy hiểm";
                   
                    }
                case 1:
                    {
                        return "Đạt";
                   
                    }
                default:
                    {
                        return "";
                     
                    }
            }
        }
        private string getColorKetQua(int kq)
        {
            switch (kq)
            {
                case -1:
                    {
                        return "Orange";
                 
                    }
                case -2:
                    {
                        return "Red";
                  
                    }
                case 1:
                    {
                        return "Green";

                    }
                default:
                    {
                        return "Green";
                     
                    }
            }
        }
    }
}
