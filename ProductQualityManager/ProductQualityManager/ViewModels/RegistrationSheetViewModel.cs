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

        private ObservableCollection<TestingSheetModel> _testingSheetListObs;
        private PHIEUDANGKY _selectedSheet;
        //enum State
        //{
        //    Denied,
        //    Undecided,
        //    Aproved
        //}

        public PHIEUDANGKY SelectedSheet { get { return _selectedSheet; } set { _selectedSheet = value; OnPropertyChanged(nameof(_selectedSheet)); } }
        public ObservableCollection<TestingSheetModel> TestingSheetListObs { get { return _testingSheetListObs; } set { _testingSheetListObs = value; OnPropertyChanged(nameof(_testingSheetListObs)); } }
        
        
        public ICommand OpenViewDetailWindow { get; set; }

        public RegistrationSheetViewModel()
        { 
            TestingSheetListObs = new ObservableCollection<TestingSheetModel>();
            OpenViewDetailWindow = new RelayCommand<TestingSheetModel>((p) => { return true; }, (p) => { OpenDetailWindow(p); });

            LoadDataSheetList();
        }
        //Load danh sách phiếu đăng ký 
        public void LoadDataSheetList() 
        {
            List<PHIEUDANGKY> SheetList = DataProvider.Ins.DB.PHIEUDANGKies.ToList();
            SelectedSheet = new PHIEUDANGKY();
            TestingSheetListObs = GetDataSheetFromList(SheetList);
        }

        //chuyển dữ liệu từ list(PHieuDANGKY) về observable collection(TestingSheet)
        public ObservableCollection<TestingSheetModel> GetDataSheetFromList( List<PHIEUDANGKY> SheetList)
        {
            ObservableCollection<TestingSheetModel> SheetListObs = new ObservableCollection<TestingSheetModel>();
            int NumberOfRecord = SheetList.Count();

            for(int i = 0; i < NumberOfRecord; i++)
            {
                TestingSheetModel TestingSheet = new TestingSheetModel(SheetList[i]);
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
                    return "Bị từ chối";
                case 0:
                    return "Đang chờ duyệt";
                case 1:
                    return "Được chấp thuận";
                default:
                    return "Đang chờ duyệt";
            }
        }
        public void OpenDetailWindow(TestingSheetModel SelectedItem)
        {
            if (SelectedItem != null)
            {
                ViewDetailWindow DetailWindow = new ViewDetailWindow(SelectedItem);
                DetailWindow.Show();
            }
        }
    }
}
