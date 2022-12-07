using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.ViewModels.ReportVM
{
    public class ReportViewModel : BaseViewModel
    {
        private ObservableCollection<ProductReport> _reportList;
        public ObservableCollection<ProductReport> ReportList { get { return _reportList; } set { _reportList = value; OnPropertyChanged(nameof(ReportList)); } }
        public ReportViewModel()
        {
            ReportList = new ObservableCollection<ProductReport>();
            LoadReportList();
        }
        public ObservableCollection<ProductReport> GetReportFromList(List<SANPHAM> reportList)
        {
            ObservableCollection<ProductReport> list = new ObservableCollection<ProductReport>();

            for (int i = 0; i < reportList.Count; i++)
            {
                if (reportList[i].TinhTrang == "Cấm" || reportList[i].TinhTrang == "Hết hạn đăng ký")
                {
                    ProductReport newReport = new ProductReport();
                    newReport.MaSanPham = reportList[i].MaSanPham;
                    newReport.TenSanPham = reportList[i].TenSanPham;
                    newReport.MaCoSo = reportList[i].MaCoSo;
                    newReport.TenCoSo = FindFacilityName((int)reportList[i].MaCoSo);
                    newReport.TinhTrang = reportList[i].TinhTrang;

                    list.Add(newReport);
                }    
            }

            return list;
        }
        public void LoadReportList()
        {
            List<SANPHAM> listReport = DataProvider.Ins.DB.SANPHAMs.ToList();
            ReportList = GetReportFromList(listReport);
        }
        public string FindFacilityName(int maCoSo)
        {
            List<SANPHAM> listProduct = DataProvider.Ins.DB.SANPHAMs.ToList();
            List<COSOSANXUAT> listFacility = DataProvider.Ins.DB.COSOSANXUATs.ToList();
            string tenCoSo = "";
            for (int i = 0; i < listFacility.Count(); i++)
            {
                if (maCoSo == listFacility[i].MaCoSo)
                {
                    tenCoSo = (string)listFacility[i].TenCoSo;
                    break;
                }
            }

            return tenCoSo;
        }
    }
}
