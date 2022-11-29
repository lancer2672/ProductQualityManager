using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductQualityManager.ViewModels.RegistrationSheet
{
    internal class ModificationHistoryViewModel :BaseViewModel
    {
        private int _idSheet;
        private ObservableCollection<ModificationHistoryModel> _modificationHistoryList;
        public ObservableCollection<ModificationHistoryModel> ModificationHistoryList { get { return _modificationHistoryList; } set { _modificationHistoryList = value; OnPropertyChanged(nameof(ModificationHistoryList)); } }
        
        public ModificationHistoryViewModel(RegistrationSheetModel SelectedItem)
        {
            ModificationHistoryList = new ObservableCollection<ModificationHistoryModel>();
            _idSheet = SelectedItem.MaPhieuDangKy;
            LoadData();
        }

        public void LoadData()
        {
            List<LICHSUDUYETPHIEUDANGKY> list = DataProvider.Ins.DB.LICHSUDUYETPHIEUDANGKies.Where(t => t.MaPhieuDangKy == _idSheet).ToList();
            for (int i = list.Count-1; i > 0; i--)
            {
                ModificationHistoryModel newItem = new ModificationHistoryModel(list[i]);
                ModificationHistoryList.Add(newItem);
            }
        }
        
    }
}
