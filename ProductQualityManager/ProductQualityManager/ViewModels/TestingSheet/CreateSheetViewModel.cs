using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.SharedControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class CreateSheetViewModel: BaseViewModel
    {

        private COSOSANXUAT _facility;
        private int _searchId;
        private string _facilityName;
        private string _facilityAddress;

        public int SearchId { get { return _searchId; } set { _searchId = value; OnPropertyChanged(nameof(SearchId)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public string FacilityAddress { get { return _facilityAddress; } set { _facilityAddress = value; OnPropertyChanged(nameof(FacilityAddress)); } }


        public ICommand CSearch { get; set; }
        public CreateSheetViewModel()
        {
            CSearch = new RelayCommand<object>((p) => { return true; }, (p) => { Search(p); });

        }
        private void Search(object p)
        {
            _facility = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == SearchId).FirstOrDefault();
            FacilityName = _facility.TenCoSo;
            FacilityAddress = _facility.DiaChi;
        }
    }
}
