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
    public class EditProductCriteriaViewModel : BaseViewModel
    {
        private ManageProductCriteriaViewModel _manageProductCriteria;
        private ObservableCollection<DONVITINH> _unitList;
        private int _criteriaId;
        private string _criteriaName;
        private decimal _standardValue;
        private DONVITINH _selectedUnit;
        public ObservableCollection<DONVITINH> UnitList { get { return _unitList; } set { _unitList = value; OnPropertyChanged(nameof(_unitList)); } }
        public int CriteriaId { get { return _criteriaId; } set { _criteriaId = value; OnPropertyChanged(nameof(_criteriaId)); } }
        public string CriteriaName { get { return _criteriaName; } set { _criteriaName = value; OnPropertyChanged(nameof(_criteriaName)); } }
        public decimal StandardValue { get { return _standardValue; } set { _standardValue = value; OnPropertyChanged(nameof(_standardValue)); } }
        public DONVITINH SelectedUnit { get { return _selectedUnit; } set { _selectedUnit = value; OnPropertyChanged(nameof(SelectedUnit)); } }
        public ICommand EditProductCriteriaCommand { get; set; }
        public EditProductCriteriaViewModel(ManageProductCriteriaViewModel CriteriaVM)
        {
            _manageProductCriteria = CriteriaVM;
            UnitList = new ObservableCollection<DONVITINH>();
            EditProductCriteriaCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditProductCriteria(p); });
            LoadWindow();
            LoadUnitList();
            LoadUnit();
        }
        public void LoadUnitList()
        {
            //int facilityOwnerId = (int)App.Current.Properties["FacilityOwner"];
            List<DONVITINH> unitList = DataProvider.Ins.DB.DONVITINHs.ToList();
            UnitList = new ObservableCollection<DONVITINH>(unitList);
        }
        public void LoadUnit()
        {
            DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.TenDonViTinh == _manageProductCriteria.SelectedCriteria.TenDonViTinh).FirstOrDefault();
            SelectedUnit = unit;
        }
        public void LoadWindow()
        {
            ProductCriteria criteria = _manageProductCriteria.SelectedCriteria;
            CriteriaId = (int)criteria.MaChiTieu;
            CriteriaName = criteria.TenChiTieu;
            StandardValue = (decimal)criteria.GiaTriTieuChuan;
        }
        public void EditProductCriteria(Window p)
        {
            if (CriteriaName == "" || StandardValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đẩy đủ thông tin!");
            }
            else
            {
                CHITIEUSANPHAM change = DataProvider.Ins.DB.CHITIEUSANPHAMs.SingleOrDefault(x => x.MaChiTieu == CriteriaId);
                change.TenChiTieu = CriteriaName;
                change.GiaTriTieuChuan = StandardValue;
                change.MaDonViTinh = SelectedUnit.MaDonViTinh;
                DataProvider.Ins.DB.SaveChanges();
                //MessageBox.Show("Chỉnh sửa thành công !");
                _manageProductCriteria.MyMessageQueue.Enqueue("Chỉnh sửa thành công");
                p.Close();
                ManageProductCriteria window = new ManageProductCriteria();
                _manageProductCriteria.RefreshData(window);
            }

        }
    }
}
