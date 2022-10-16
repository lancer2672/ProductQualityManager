using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProductQualityManager.ViewModels
{
    public class TestingSheetViewModel  : BaseViewModel
    {

        private ObservableCollection<PHIEUDANGKY> _testingSheetListObs;
        private PHIEUDANGKY _selectedSheet;


        public  PHIEUDANGKY SelectedSheet { get { return _selectedSheet; } set { _selectedSheet = value; OnPropertyChanged(nameof(_selectedSheet)); } }
        public ObservableCollection<PHIEUDANGKY> TestingSheetListObs { get { return _testingSheetListObs; } set { _testingSheetListObs = value; OnPropertyChanged(nameof(_testingSheetListObs)); } }
        
        
        public TestingSheetViewModel()
        { 
            TestingSheetListObs = new ObservableCollection<PHIEUDANGKY>();
        }
        //Load danh sách phiếu đăng ký 
        public void LoadDataSheetList() 
        {
            List<PHIEUDANGKY> SheetList = DataProvider.Ins.DB.PHIEUDANGKies.ToList();
            SelectedSheet = new PHIEUDANGKY();
            TestingSheetListObs = GetDataSheetFromList(SheetList);
        }

        //chuyển dữ liệu từ list về observable collection
        public ObservableCollection<PHIEUDANGKY> GetDataSheetFromList( List<PHIEUDANGKY> SheetList)
        {
            ObservableCollection<PHIEUDANGKY> SheetListObs = new ObservableCollection<PHIEUDANGKY>();
            int NumberOfRecord = SheetList.Count();

            for(int i = 0; i < NumberOfRecord; i++)
            {
                PHIEUDANGKY TestingSheet = SheetList[i];
                SheetListObs.Add(TestingSheet);
            }
            return SheetListObs;
        }

    }
}
