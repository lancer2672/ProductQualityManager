using ProductQualityManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class DetailTestingSheetViewModel : BaseViewModel
    {
        private TestingSheetModel _selectedRecord;
        private int _idTestingSheet;
        private DateTime _registrationDate;

        public TestingSheetModel SelectedRecord { get { return _selectedRecord; } set { _selectedRecord = value; OnPropertyChanged(nameof(_selectedRecord)); } }
        public int IdTestingSheet { get { return _idTestingSheet; } set { _idTestingSheet = value; OnPropertyChanged(nameof(_idTestingSheet)); } }
        public DateTime RegistrationDate { get { return _registrationDate; } set { _registrationDate = value; OnPropertyChanged(nameof(_registrationDate)); } }
        
        public DetailTestingSheetViewModel(TestingSheetModel SelectedItem)
        {
            IdTestingSheet = SelectedItem.MaPhieuDangKy;
            RegistrationDate = (DateTime)SelectedItem.NgayDangKy;
            SelectedRecord = SelectedItem;
        }
    }
}
