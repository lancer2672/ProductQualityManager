using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ProductQualityManager.Views.TestingSheet;
using ProductQualityManager.Views.TestSheet;
using Syncfusion.Windows.Shared;

namespace ProductQualityManager.ViewModels
{
    public class RegistrationSheetViewModel  : BaseViewModel
    {

        private ObservableCollection<RegistrationSheetModel> _testingSheetListObs;
        private DateTime _selectedDate;
        private RegistrationSheetModel _selectedSheet;
        private string _searchKey;
        private List<string> _searchOptions;
        private int _searchTypeSelected;
        public RegistrationSheetModel SelectedSheet { get { return _selectedSheet; } set { _selectedSheet = value; OnPropertyChanged(nameof(SelectedSheet)); } }
        public DateTime SelectedDate { get { return _selectedDate; } set { _selectedDate = value; LoadDataSheetList(); OnPropertyChanged(nameof(SelectedDate)); } }
        public string SearchKey { get { return _searchKey; } set { _searchKey = value; OnPropertyChanged(nameof(SearchKey)); } }
        public ObservableCollection<RegistrationSheetModel> TestingSheetListObs { get { return _testingSheetListObs; } set { _testingSheetListObs = value; OnPropertyChanged(nameof(TestingSheetListObs)); } }
        public List<string> SearchOptions { get { return _searchOptions; } set { _searchOptions = value; OnPropertyChanged(nameof(SearchOptions)); } }
        public int SearchTypeSelected { get { return _searchTypeSelected; } set { _searchTypeSelected = value; OnPropertyChanged(nameof(SearchTypeSelected)); } }

        public ICommand COpenViewDetailWindow { get; set; }
        public ICommand CCheck { get; set; }
        public ICommand CSearch { get; set; }

        public RegistrationSheetViewModel()
        {
            SelectedDate = DateTime.Today;
            TestingSheetListObs = new ObservableCollection<RegistrationSheetModel>();
            COpenViewDetailWindow = new RelayCommand<object>((p) => { return true; }, (p) => { OpenDetailWindow(p); });
            CSearch = new RelayCommand<object>((p) => { return true; }, (p) => { Search(p); });
            CCheck = new RelayCommand<object>((p) => { return true; }, (p) => { CheckSheet(p); });
           
            LoadDataSheetList();
            CheckOverDueRegistrationForms();
            SearchOptions = new List<string>() { "ID", "Tên cơ sở", "Chưa duyệt" };
        }
        public void Search(object p)
        {
            if(SearchKey.IsNullOrWhiteSpace() == true)
            {
                if (SearchTypeSelected != 2)
                {
                    LoadDataSheetList();
                    return;
                }
            }
            switch (SearchTypeSelected)
            {
                //ID
                case 0:
                    {
                        List<COSOSANXUAT> list = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo.ToString().ToLower().Contains(SearchKey.ToLower())).ToList();
                        List<PHIEUDANGKY> sheetList = new List<PHIEUDANGKY>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            int id = (int)list[i].MaCoSo;

                            List<PHIEUDANGKY> registrationList = DataProvider.Ins.DB.PHIEUDANGKies.
                              Where(t => t.NgayDangKy.Value.Day == SelectedDate.Day &&
                                       t.NgayDangKy.Value.Month == SelectedDate.Month &&
                                       t.NgayDangKy.Value.Year == SelectedDate.Year &&
                                       t.MaCoSo == id).ToList();
                            sheetList.AddRange(registrationList);
                        }
                        TestingSheetListObs = GetDataSheetFromList(sheetList);
                        break;
                    }
                //Tên cơ sở
                case 1:
                    {   
                        List<COSOSANXUAT> list = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.TenCoSo.ToLower().Contains(SearchKey.ToLower())).ToList();
                        List<PHIEUDANGKY> sheetList = new List<PHIEUDANGKY>();
                        for (int i=0;i< list.Count; i++)
                        {
                            int id = (int)list[i].MaCoSo;
                            List<PHIEUDANGKY> registrationList = DataProvider.Ins.DB.PHIEUDANGKies.
                              Where(t => t.NgayDangKy.Value.Day == SelectedDate.Day &&
                                       t.NgayDangKy.Value.Month == SelectedDate.Month &&
                                       t.NgayDangKy.Value.Year == SelectedDate.Year &&
                                       t.MaCoSo == id).ToList();
                            sheetList.AddRange(registrationList);
                        }
                        TestingSheetListObs = GetDataSheetFromList(sheetList);
                        break;
                    }
                case 2:
                    {
                        List<PHIEUDANGKY> sheetList = DataProvider.Ins.DB.PHIEUDANGKies.
                               Where(t => t.NgayDangKy.Value.Day == SelectedDate.Day &&
                                        t.NgayDangKy.Value.Month == SelectedDate.Month &&
                                        t.NgayDangKy.Value.Year == SelectedDate.Year &&
                                        t.TrangThai == 0).
                               ToList();
                        TestingSheetListObs = GetDataSheetFromList(sheetList);
                        break;
                    }
                default:
                    return;
            }
        }
        public void RefreshData()
        {
            SearchKey = "";
            LoadDataSheetList();
        }
        public void CheckOverDueRegistrationForms()
        {
            List<PHIEUDANGKY> list = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.TrangThai == 0).ToList();
            for(int i=0;i< list.Count; i++)
            {
                if (list[i].HanDangKy < DateTime.Now)
                {
                    list[i].TrangThai = 2;
                }
            }
        }
        public void LoadDataSheetList() 
        {
            List<PHIEUDANGKY> SheetList = DataProvider.Ins.DB.PHIEUDANGKies.
                Where(t =>t.NgayDangKy.Value.Day == SelectedDate.Day && t.NgayDangKy.Value.Month == SelectedDate.Month && t.NgayDangKy.Value.Year == SelectedDate.Year).
                ToList();
            TestingSheetListObs = GetDataSheetFromList(SheetList);
        }

        public void CheckSheet(object p)
        {
            if (SelectedSheet == null)
                return;

            
        }
        public void RefreshList()
        {
            TestingSheetListObs.Clear();
            LoadDataSheetList();
        }
        //chuyển dữ liệu từ list(PHIEUDANGKY) về observable collection(REGISTRATIONSHEETMODEL)
        public ObservableCollection<RegistrationSheetModel> GetDataSheetFromList( List<PHIEUDANGKY> SheetList)
        {
            ObservableCollection<RegistrationSheetModel> SheetListObs = new ObservableCollection<RegistrationSheetModel>();
            int NumberOfRecord = SheetList.Count();

            for(int i = 0; i < NumberOfRecord; i++)
            {
                int idProduct = (int)SheetList[i].MaSanPham;
                int idProductFacility = (int)SheetList[i].MaCoSo;
                COSOSANXUAT produceFacility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == idProductFacility).FirstOrDefault();
                SANPHAM product = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaSanPham == idProduct).FirstOrDefault();
                DONVITINHSANPHAM unit = DataProvider.Ins.DB.DONVITINHSANPHAMs.Where(t => t.MaDonViTinhSP == product.MaDonViTinhSP).FirstOrDefault();
                RegistrationSheetModel TestingSheet = new RegistrationSheetModel(SheetList[i], unit.TenDonViTinhSP, produceFacility.TenCoSo, product.TenSanPham);
                TestingSheet.MauChu = GetColorState((int)TestingSheet.TrangThai);
                TestingSheet.STrangThai = GetStringState((int)TestingSheet.TrangThai);
                SheetListObs.Add(TestingSheet);
            }
            return SheetListObs;
        }
        public string GetColorState(int State)
        {
            switch (State)
            {
                case -1:
                    return "Red";
                case 0:
                    return "Orange";
                case 1:
                    return "Green";
                case 2:
                    return "Gray";
                default:
                    return "Orange";
            }
        }
        public string GetStringState(int State)
        {
            switch (State)
            {
                case -1:
                    return "Từ chối";
                case 0:
                    return "Đang chờ duyệt";
                case 1:
                    return "Chấp thuận";
                case 2:
                    return "Quá hạn";
                default:
                    return "Đang chờ duyệt";
            }
        }
        public void OpenDetailWindow(object p)
        {
           if(SelectedSheet != null)
            {
                DetailRegistrationSheet window = new DetailRegistrationSheet(this);
                window.ShowDialog();
            }
        }
    }
}
