using ProductQualityManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class DetailRegistrationSheetViewModel : BaseViewModel
    {
        private TestingSheetModel _selectedRecord;
        private int _idTestSheet;
        private int _idFactory;
        private DateTime _registrationDate;

        public TestingSheetModel SelectedRecord { get { return _selectedRecord; } set { _selectedRecord = value; OnPropertyChanged(nameof(_selectedRecord)); } }
        public int IdTestSheet { get { return _idTestSheet; } set { _idTestSheet = value; OnPropertyChanged(nameof(_idTestSheet)); } }
        public int IdFactory { get { return _idFactory; } set { _idFactory = value; OnPropertyChanged(nameof(_idFactory)); } }
        public DateTime RegistrationDate { get { return _registrationDate; } set { _registrationDate = value; OnPropertyChanged(nameof(_registrationDate)); } }
        
        public DetailRegistrationSheetViewModel(TestingSheetModel SelectedItem)
        {
            IdTestSheet = SelectedItem.MaPhieuDangKy;
            RegistrationDate = (DateTime)SelectedItem.NgayDangKy;
            SelectedRecord = SelectedItem;
        }
    }
}
