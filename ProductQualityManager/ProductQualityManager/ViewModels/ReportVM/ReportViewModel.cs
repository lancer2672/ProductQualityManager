using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.ReportVM
{
    public class ReportViewModel : BaseViewModel
    {
        private ObservableCollection<SANPHAM> _reportList;
        private ObservableCollection<COSOSANXUAT> _listnamefacility;
        private COSOSANXUAT _selectedfacility;
        private ObservableCollection<ProductStatusChart> _productstatuschart;
        private string _selectedItemName;
        private int _selectedItemPercentage;
        private int _selectedIndex;

        public ObservableCollection<SANPHAM> ReportList { get { return _reportList; } set { _reportList = value; OnPropertyChanged(nameof(ReportList)); } }
        public ObservableCollection<COSOSANXUAT> ListNameFacility { get { return _listnamefacility; } set { _listnamefacility = value; OnPropertyChanged(nameof(ListNameFacility)); } }
        public COSOSANXUAT SelectedFacility { get { return _selectedfacility; } set { _selectedfacility = value; OnPropertyChanged(nameof(SelectedFacility)); } }
        public ObservableCollection<ProductStatusChart> ProductStatusChart { get { return _productstatuschart; } set { _productstatuschart = value; OnPropertyChanged(nameof(ProductStatusChart)); } }
        public string SelectedItemName { get => _selectedItemName; set { _selectedItemName = value; OnPropertyChanged(nameof(SelectedItemName)); } }
        public int SelectedItemPercentage { get => _selectedItemPercentage; set { _selectedItemPercentage = value; OnPropertyChanged(nameof(SelectedItemPercentage)); } }
        public int SelectedIndex
        {
            get => _selectedIndex; set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                if (value != -1)
                {
                    SelectedItemName = ProductStatusChart[SelectedIndex].TinhTrang;
                    SelectedItemPercentage = ProductStatusChart[SelectedIndex].TyLe;
                }
            }
        }
        public ICommand SelectionChangeCommand { get; set; }

        public ReportViewModel()
        {
            GetListNameFacility();
            LoadReportWindow();

            SelectionChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadReportWindow(); });
        }
        //public ObservableCollection<SANPHAM> GetReportFromList(List<SANPHAM> reportList)
        //{
        //    ObservableCollection<ProductReport> list = new ObservableCollection<ProductReport>();

        //    for (int i = 0; i < reportList.Count; i++)
        //    {
        //        if (reportList[i].TinhTrang == "Cấm" || reportList[i].TinhTrang == "Hết hạn đăng ký")
        //        {
        //            ProductReport newReport = new ProductReport();
        //            newReport.MaSanPham = reportList[i].MaSanPham;
        //            newReport.TenSanPham = reportList[i].TenSanPham;
        //            newReport.MaCoSo = reportList[i].MaCoSo;
        //            newReport.TenCoSo = FindFacilityName((int)reportList[i].MaCoSo);
        //            newReport.TinhTrang = reportList[i].TinhTrang;

        //            list.Add(newReport);
        //        }    
        //    }

        //    return list;
        //}

        public void GetListNameFacility()
        {
            ListNameFacility = new ObservableCollection<COSOSANXUAT>();
            List<COSOSANXUAT> listfacility = DataProvider.Ins.DB.COSOSANXUATs.ToList();
            foreach(COSOSANXUAT item in listfacility)
            {
                ListNameFacility.Add(item);
            }
        }

        public void LoadReportWindow()
        {
            if (SelectedFacility != null) 
            {
                int tyle_csx = 0, tyle_dsx = 0, tyle_hhsx = 0, tyle_cam = 0;
                //var sqlstring = "from SANPHAM as product, SelectedFacility as facility " + "where product.MaCoSo = facility.MaCoSo " + "order by product.TinhTrang ";
                //var result = DataProvider.Ins.DB.Database.SqlQuery<SANPHAM>(sqlstring).ToList();
                List<SANPHAM> listproduct = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaCoSo == SelectedFacility.MaCoSo).OrderBy(x=>x.TinhTrang).ToList();
                ReportList = new ObservableCollection<SANPHAM>();
                foreach (SANPHAM item in listproduct)
                {
                    if (item.TinhTrang == "Chưa sản xuất")
                        tyle_csx++;
                    if (item.TinhTrang == "Đang sản xuất")
                        tyle_dsx++;
                    if (item.TinhTrang == "Hết hạn sản xuất")
                        tyle_hhsx++;
                    if (item.TinhTrang == "Cấm")
                        tyle_cam++;
                    ReportList.Add(item);
                }
                ProductStatusChart = new ObservableCollection<ProductStatusChart>() { };
                if (tyle_csx != 0)
                {
                    ProductStatusChart csx = new ProductStatusChart(SelectedFacility.MaCoSo, "Chưa sản xuất", (int)(tyle_csx * 100 / (tyle_csx + tyle_dsx + tyle_hhsx + tyle_cam)));
                    ProductStatusChart.Add(csx);
                }
                if(tyle_dsx != 0)
                {
                    ProductStatusChart dsx = new ProductStatusChart(SelectedFacility.MaCoSo, "Đang sản xuất", (int)(tyle_dsx * 100 / (tyle_csx + tyle_dsx + tyle_hhsx + tyle_cam)));
                    ProductStatusChart.Add(dsx);
                }
                if(tyle_hhsx != 0)
                {
                    ProductStatusChart hhsx = new ProductStatusChart(SelectedFacility.MaCoSo, "Hết hạn sản xuất", (int)(tyle_hhsx * 100 / (tyle_csx + tyle_dsx + tyle_hhsx + tyle_cam)));
                    ProductStatusChart.Add(hhsx);
                }
                if (tyle_cam != 0)
                {
                    ProductStatusChart cam = new ProductStatusChart(SelectedFacility.MaCoSo, "Cấm", (int)(tyle_cam * 100 / (tyle_csx + tyle_dsx + tyle_hhsx + tyle_cam)));
                    ProductStatusChart.Add(cam);
                }               
            }
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
