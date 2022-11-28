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

namespace ProductQualityManager.ViewModels
{
    public class RegistrationSheetViewModel  : BaseViewModel
    {

        private ObservableCollection<RegistrationSheetModel> _testingSheetListObs;
        private DateTime _selectedDate;
        private RegistrationSheetModel _selectedSheet;
        //enum State
        //{
        //    Denied,
        //    Undecided,
        //    Aproved
        //}

        public RegistrationSheetModel SelectedSheet { get { return _selectedSheet; } set { _selectedSheet = value; OnPropertyChanged(nameof(_selectedSheet)); } }
        public DateTime SelectedDate { get { return _selectedDate; } set { _selectedDate = value; LoadDataSheetList(); OnPropertyChanged(nameof(SelectedDate)); } }
        public ObservableCollection<RegistrationSheetModel> TestingSheetListObs { get { return _testingSheetListObs; } set { _testingSheetListObs = value; OnPropertyChanged(nameof(TestingSheetListObs)); } }
        
        
        public ICommand COpenViewDetailWindow { get; set; }
        public ICommand CApprove { get; set; }
        public ICommand CReject { get; set; }
        public ICommand COpenModificationHistoryWindow { get; set; }

        public RegistrationSheetViewModel()
        {
            SelectedDate = DateTime.Today;
            TestingSheetListObs = new ObservableCollection<RegistrationSheetModel>();
            COpenViewDetailWindow = new RelayCommand<object>((p) => { return true; }, (p) => { OpenDetailWindow(p); });
            CApprove = new RelayCommand<object>((p) => { return true; }, (p) => { ApproveSheet(p); });
            CReject = new RelayCommand<object>((p) => { return true; }, (p) => { RejectSheet(p); });
            COpenModificationHistoryWindow = new RelayCommand<object>((p) => { return true; }, (p) => { OpenModificationHistoryWindow(p); });

            LoadDataSheetList();
        }
       
        //Load danh sách phiếu đăng ký 
        public void LoadDataSheetList() 
        {
            List<PHIEUDANGKY> SheetList = DataProvider.Ins.DB.PHIEUDANGKies.
                Where(t =>t.NgayDangKy.Value.Day == SelectedDate.Day && t.NgayDangKy.Value.Month == SelectedDate.Month && t.NgayDangKy.Value.Year == SelectedDate.Year).
                ToList();
            if (SheetList == null) return;
            SelectedSheet = new RegistrationSheetModel();
            TestingSheetListObs = GetDataSheetFromList(SheetList);
        }
        public void OpenModificationHistoryWindow(object p)
        {

            RegistrationSheetModel selectedItem = p as RegistrationSheetModel;
            ModificationHistory window = new ModificationHistory(selectedItem);
            window.Show();
        }
        public void ApproveSheet(object p)
        {
            RegistrationSheetModel selectedItem = p as RegistrationSheetModel;
            
            PHIEUDANGKY sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaPhieuDangKy == selectedItem.MaPhieuDangKy).FirstOrDefault();
            if (sheet.TrangThai == 1)
            {
                return;
            }
            sheet.TrangThai = 1;
            LICHSUDUYETPHIEUDANGKY historySheet = new LICHSUDUYETPHIEUDANGKY();
            historySheet.MaPhieuDangKy = selectedItem.MaPhieuDangKy;
            historySheet.ThoiGianChinhSua = DateTime.Now;
            historySheet.GiaTriChinhSua = 1;
            DataProvider.Ins.DB.LICHSUDUYETPHIEUDANGKies.Add(historySheet);
            try
            {
                DataProvider.Ins.DB.SaveChanges();
                RefreshList();

            }catch(Exception e)
            {

            }          
        }
        public void RejectSheet(object p)
        {
            RegistrationSheetModel selectedItem = p as RegistrationSheetModel;

            PHIEUDANGKY sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaPhieuDangKy == selectedItem.MaPhieuDangKy).FirstOrDefault();
            if (sheet.TrangThai == -1)
            { 
                return; 
            }
            sheet.TrangThai = -1;
            LICHSUDUYETPHIEUDANGKY historySheet = new LICHSUDUYETPHIEUDANGKY();
            historySheet.MaPhieuDangKy = selectedItem.MaPhieuDangKy;
            historySheet.ThoiGianChinhSua = DateTime.Now;
            historySheet.GiaTriChinhSua = -1;
            DataProvider.Ins.DB.LICHSUDUYETPHIEUDANGKies.Add(historySheet);
            try
            {
                DataProvider.Ins.DB.SaveChanges();
                RefreshList();
            }
            catch (Exception e)
            {

            }
        }

        public void RefreshList()
        {
            TestingSheetListObs.Clear();
            LoadDataSheetList();
        }
        //chuyển dữ liệu từ list(PHieuDANGKY) về observable collection(TestingSheet)
        public ObservableCollection<RegistrationSheetModel> GetDataSheetFromList( List<PHIEUDANGKY> SheetList)
        {
            ObservableCollection<RegistrationSheetModel> SheetListObs = new ObservableCollection<RegistrationSheetModel>();
            int NumberOfRecord = SheetList.Count();

            for(int i = 0; i < NumberOfRecord; i++)
            {
                RegistrationSheetModel TestingSheet = new RegistrationSheetModel(SheetList[i]);
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
                default:
                    return "Đang chờ duyệt";
            }
        }
        public void OpenDetailWindow(object p)
        {
            RegistrationSheetModel selectedItem = p as RegistrationSheetModel;
            ViewDetailWindow DetailWindow = new ViewDetailWindow(selectedItem);
            DetailWindow.Show();
        }
    }
}
