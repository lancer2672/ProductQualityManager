using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.ProductCriteria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.ProductCriteriaVM
{
    public class AddCriteriaCriteriaViewModel : BaseViewModel
    {
        private ManageProductCriteriaViewModel _manageProductCriteria;
        private ObservableCollection<DONVITINH> _unitList;
        private string _criteriaName;
        private decimal _standardValue;
        private DONVITINH _selectedUnit;
        public ObservableCollection<DONVITINH> UnitList { get { return _unitList; } set { _unitList = value; OnPropertyChanged(nameof(_unitList)); } }
        public string CriteriaName { get { return _criteriaName; } set { _criteriaName = value; OnPropertyChanged(nameof(_criteriaName)); } }
        public decimal StandardValue { get { return _standardValue; } set { _standardValue = value; OnPropertyChanged(nameof(_standardValue)); } }
        public DONVITINH SelectedUnit { get { return _selectedUnit; } set { _selectedUnit = value;  OnPropertyChanged(nameof(SelectedUnit)); } }
        public ICommand AddCriteriaCriteriaCommand { get; set; }
        public AddCriteriaCriteriaViewModel(ManageProductCriteriaViewModel CriteriaVM)
        {
            _manageProductCriteria = CriteriaVM;
            CriteriaName = "";
            StandardValue = 0;
            UnitList = new ObservableCollection<DONVITINH>();
            AddCriteriaCriteriaCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddCriteriaCriteria(p); });
            LoadUnitList();
        }
        public void LoadUnitList()
        {
            //int facilityOwnerId = (int)App.Current.Properties["FacilityOwner"];
            List<DONVITINH> unitList = DataProvider.Ins.DB.DONVITINHs.ToList();
            UnitList = new ObservableCollection<DONVITINH>(unitList);
        }
        public bool IsExistUnit(DONVITINH NewUnit)
        {
            List<DONVITINH> UnitList = DataProvider.Ins.DB.DONVITINHs.ToList();
            for (int i = 0; i < UnitList.Count; i++)
            {
                if (UnitList[i].TenDonViTinh == NewUnit.TenDonViTinh)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddCriteriaCriteria(Window p)
        {
            if (CriteriaName == "" || StandardValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đẩy đủ thông tin");
            }
            else
            {
                CHITIEUSANPHAM newCriteria = new CHITIEUSANPHAM();
                newCriteria.TenChiTieu = CriteriaName;
                newCriteria.GiaTriTieuChuan = StandardValue;

                newCriteria.MaDonViTinh = SelectedUnit.MaDonViTinh;
                DataProvider.Ins.DB.CHITIEUSANPHAMs.Add(newCriteria);
                DataProvider.Ins.DB.SaveChanges();
                //MessageBox.Show("Thêm chỉ tiêu thành công");
                _manageProductCriteria.MyMessageQueue.Enqueue("Thêm chỉ tiêu thành công");
                CriteriaName = "";
                StandardValue = 0;
                p.Close();
                ManageProductCriteria window = new ManageProductCriteria();
                _manageProductCriteria.RefreshData(window);

            }
        }

    }
}
