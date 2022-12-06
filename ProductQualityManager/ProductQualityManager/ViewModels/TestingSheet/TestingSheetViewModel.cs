using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class TestingSheetViewModel : BaseViewModel
    {
        private ObservableCollection<TestingSheetModel> _testingSheetList;
        private TestingSheetModel _selectedItem;
        private string _searchKey;
        private List<string> _searchOptions;
        private int _searchTypeSelected;

        public string SearchKey { get { return _searchKey; } set { _searchKey = value; OnPropertyChanged(nameof(SearchKey)); } }
        public List<string> SearchOptions { get { return _searchOptions; } set { _searchOptions = value; OnPropertyChanged(nameof(SearchOptions)); } }
        public ObservableCollection<TestingSheetModel> TestingSheetList { get { return _testingSheetList; } set { _testingSheetList = value; OnPropertyChanged(nameof(TestingSheetList)); } }
        public TestingSheetModel SelectedItem { get { return _selectedItem; } set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }
        public int SearchTypeSelected { get { return _searchTypeSelected; } set { _searchTypeSelected = value; OnPropertyChanged(nameof(SearchTypeSelected)); } }

        public ICommand CSearch { get; set; }
        public ICommand COpenViewDetailWindow { get; set; }


        public TestingSheetViewModel()
        {
            CSearch = new RelayCommand<object>((p) => { return true; }, (p) => { Search(p); });
            SearchOptions = new List<string>() { "Mã Phiếu", "Mã Cơ sở", "Tên Cơ Sở" };
            COpenViewDetailWindow = new RelayCommand<object>((p) => { return true; }, (p) => { OpenDetailWindow(p); });

            LoadListView();
        }
        public void Search(object p)
        {
            if (SearchKey.IsNullOrWhiteSpace() == true)
            {
                LoadListView();
                return;
            }
            TestingSheetList.Clear();
            switch (SearchTypeSelected)
            {
                //ID
                case 0:
                    {
                        List<PHIEUKIEMNGHIEM> list = DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.Where(t=>t.MaPhieuKiemNghiem.ToString() == SearchKey).ToList();
                        foreach (PHIEUKIEMNGHIEM item in list)
                        {
                            int id = item.MaPhieuKiemNghiem;
                            List<COSOSANXUAT> listFacility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == id).ToList();
                            foreach (COSOSANXUAT facility in listFacility)
                            {
                                TestingSheetModel listViewItem = new TestingSheetModel(item, facility);
                                TestingSheetList.Add(listViewItem);
                            }
                        }
                        break;
                    }
                //Mã cơ sở
                case 1:
                    {                 
                        List<COSOSANXUAT> listFacility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo.ToString().ToLower().Contains(SearchKey.ToLower())).ToList();
                        foreach (COSOSANXUAT facility in listFacility)
                        {
                            int id = facility.MaCoSo;
                            List<PHIEUKIEMNGHIEM> list = DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.Where(t=>t.MaCoSo == id).ToList();
                            foreach (PHIEUKIEMNGHIEM item in list)
                            {
                                TestingSheetModel listViewItem = new TestingSheetModel(item, facility);
                                TestingSheetList.Add(listViewItem);
                            }
                        }
                        
                        break;
                    }
                //Tên cơ sở
                case 2:
                    {
                        List<COSOSANXUAT> listFacility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.TenCoSo.Contains(SearchKey.ToLower())).ToList();
                        foreach (COSOSANXUAT facility in listFacility)
                        {
                            int id = facility.MaCoSo;
                            List<PHIEUKIEMNGHIEM> list = DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.Where(t => t.MaCoSo == id).ToList();
                            foreach (PHIEUKIEMNGHIEM item in list)
                            {
                                TestingSheetModel listViewItem = new TestingSheetModel(item, facility);
                                TestingSheetList.Add(listViewItem);
                            }
                        }
                        break;
                    }
                default:
                    return;
            }
        }
        private void OpenDetailWindow(object p)
        {

        }
        public void LoadListView()
        {
            TestingSheetList = new ObservableCollection<TestingSheetModel>();
            List<PHIEUKIEMNGHIEM> list = DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.ToList();
            foreach (PHIEUKIEMNGHIEM item in list)
            {
                List<COSOSANXUAT> listFacility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == item.MaCoSo).ToList();
                foreach (COSOSANXUAT facility in listFacility)
                {
                    TestingSheetModel listViewItem = new TestingSheetModel(item, facility);
                    TestingSheetList.Add(listViewItem);
                }
            }
        }
    }
}
