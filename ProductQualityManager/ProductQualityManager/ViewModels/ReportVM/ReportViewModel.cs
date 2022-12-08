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
        private ObservableCollection<COSOSANXUAT> _facilityList;
        private COSOSANXUAT _selectedFacility;
        public ObservableCollection<ProductReport> ReportList { get { return _reportList; } set { _reportList = value; OnPropertyChanged(nameof(ReportList)); } }
        public ObservableCollection<COSOSANXUAT> FacilityList { get { return _facilityList; } set { _facilityList = value; OnPropertyChanged(nameof(_facilityList)); } }
        public COSOSANXUAT SelectedFacility { get { return _selectedFacility; } set { _selectedFacility = value; OnPropertyChanged(nameof(SelectedFacility)); } }
        public ReportViewModel()
        {
            ReportList = new ObservableCollection<ProductReport>();
            FacilityList = new ObservableCollection<COSOSANXUAT>();
            LoadReportList();
            LoadFacilityList();
        }
        public ObservableCollection<ProductReport> GetReportFromList(List<SANPHAM> reportList)
        {
            ObservableCollection<ProductReport> list = new ObservableCollection<ProductReport>();

            for (int i = 0; i < reportList.Count; i++)
            {
                ProductReport newReport = new ProductReport();
                newReport.MaCoSo = reportList[i].MaCoSo;
                newReport.MaSanPham = reportList[i].MaSanPham;
                newReport.TenSanPham = reportList[i].TenSanPham;
                //newReport.TenCoSo = FindFacilityName((int)reportList[i].MaCoSo);
                newReport.TinhTrang = reportList[i].TinhTrang;

                list.Add(newReport);
            }

            return list;
        }
        public void LoadReportList()
        {
            List<SANPHAM> listReport = DataProvider.Ins.DB.SANPHAMs.ToList();
            ReportList = GetReportFromList(listReport);
        }
        public void LoadFacilityList()
        {
            //List<COSOSANXUAT> facilityList = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == SelectedFacility.MaCoSo).ToList();
            //FacilityList = new ObservableCollection<COSOSANXUAT>(facilityList);
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
