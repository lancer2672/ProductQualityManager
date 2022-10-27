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
namespace ProductQualityManager.ViewModels
{
    public class RegistrationSheetViewModel  : BaseViewModel
    {

        private ObservableCollection<RegistrationSheetModel> _testingSheetListObs;
        private RegistrationSheetModel _selectedSheet;
        //enum State
        //{
        //    Denied,
        //    Undecided,
        //    Aproved
        //}

        public RegistrationSheetModel SelectedSheet { get { return _selectedSheet; } set { _selectedSheet = value; OnPropertyChanged(nameof(_selectedSheet)); } }
        public ObservableCollection<RegistrationSheetModel> TestingSheetListObs { get { return _testingSheetListObs; } set { _testingSheetListObs = value; OnPropertyChanged(nameof(TestingSheetListObs)); } }
        
        
        public ICommand OpenViewDetailWindow { get; set; }
        public ICommand Approve { get; set; }
        public ICommand Reject { get; set; }
        
        public RegistrationSheetViewModel()
        {
            TestingSheetListObs = new ObservableCollection<RegistrationSheetModel>();
            OpenViewDetailWindow = new RelayCommand<RegistrationSheetModel>((p) => { return true; }, (p) => { OpenDetailWindow(p); });
            Approve = new RelayCommand<ListView>((p) => { return true; }, (p) => { ApproveSheet(p); });
            Reject = new RelayCommand<ListView>((p) => { return true; }, (p) => { RejectSheet(p); });

            LoadDataSheetList();
        }
        //Load danh sách phiếu đăng ký 
        public void LoadDataSheetList() 
        {
            List<PHIEUDANGKY> SheetList = DataProvider.Ins.DB.PHIEUDANGKies.ToList();
            SelectedSheet = new RegistrationSheetModel();
            TestingSheetListObs = GetDataSheetFromList(SheetList);
        }
        public void ApproveSheet(ListView p)
        {
            //if (p.SelectedIndex == -1)
            //    return;
            PHIEUDANGKY Sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaPhieuDangKy == SelectedSheet.MaPhieuDangKy).FirstOrDefault();
            if (Sheet.TrangThai == 0)
            {
                Sheet.TrangThai = 1;
            }
            else return;
            try
            {
                DataProvider.Ins.DB.SaveChanges();
                RefreshList();

            }catch(Exception e)
            {

            }          
        }
        public void RejectSheet(ListView p)
        {
            //if (p.SelectedIndex == -1)
            //    return;

            PHIEUDANGKY Sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaPhieuDangKy == SelectedSheet.MaPhieuDangKy).FirstOrDefault();
            if (Sheet.TrangThai == 0)
            {
                Sheet.TrangThai = -1;
            }
            else return;
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
        public void OpenDetailWindow(RegistrationSheetModel SelectedItem)
        {
            if (SelectedItem != null)
            {
                ViewDetailWindow DetailWindow = new ViewDetailWindow(SelectedItem);
                DetailWindow.Show();
            }
        }
    }
}
