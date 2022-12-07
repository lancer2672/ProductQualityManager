using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.ProductCriteria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.ProductCriteriaVM
{
    public class ManageProductCriteriaViewModel : BaseViewModel
    {
        private ObservableCollection<DONVITINH> _unitList;
        private string _criteriaName;
        private decimal _standardValue;
        private decimal _unsafeValue;
        private DONVITINH _selectedUnit;
        private ObservableCollection<ProductCriteria> _criteriaList;
        private ProductCriteria _selectedCriteria;

        public ObservableCollection<DONVITINH> UnitList { get { return _unitList; } set { _unitList = value; OnPropertyChanged(nameof(_unitList)); } }
        public string CriteriaName { get { return _criteriaName; } set { _criteriaName = value; OnPropertyChanged(nameof(CriteriaName)); } }
        public decimal StandardValue { get { return _standardValue; } set { _standardValue = value; OnPropertyChanged(nameof(StandardValue)); } }
        public decimal UnsafeValue { get { return _unsafeValue; } set { _unsafeValue = value; OnPropertyChanged(nameof(UnsafeValue)); } }
        public DONVITINH SelectedUnit { get { return _selectedUnit; } set { _selectedUnit = value; OnPropertyChanged(nameof(SelectedUnit)); } }
        public ObservableCollection<ProductCriteria> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }
        public ProductCriteria SelectedCriteria { get { return _selectedCriteria; } set { _selectedCriteria = value; OnPropertyChanged(nameof(SelectedCriteria)); } }
        public ICommand OpenAddCriteriaCommand { get; set; }
        public ICommand OpenEditCriteriaCommand { get; set; }
        public ICommand AddCriteriaCriteriaCommand { get; set; }

        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        private SnackbarMessageQueue myMessageQueue;
        public ManageProductCriteriaViewModel()
        {
            CriteriaList = new ObservableCollection<ProductCriteria>();
            OpenAddCriteriaCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddCriteria(p); });
            OpenEditCriteriaCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenEditCriteria(p); });
            LoadCriteriaList();

            CriteriaName = "";
            StandardValue = 0;
            UnsafeValue = 0;

            UnitList = new ObservableCollection<DONVITINH>();
            AddCriteriaCriteriaCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => { AddCriteriaCriteria(p); });
            LoadUnitList();

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            MyMessageQueue.DiscardDuplicates = true;
        }
        public ObservableCollection<ProductCriteria> GetCriteriaFromList(List<CHITIEUSANPHAM> criteriaList)
        {
            ObservableCollection<ProductCriteria> list = new ObservableCollection<ProductCriteria>();

            for (int i = 0; i < criteriaList.Count; i++)
            {
                ProductCriteria newCriteria = new ProductCriteria();
                newCriteria.MaChiTieu = criteriaList[i].MaChiTieu;
                newCriteria.TenChiTieu = criteriaList[i].TenChiTieu;
                newCriteria.GiaTriTieuChuan = (decimal)criteriaList[i].GiaTriTieuChuan;
                newCriteria.GiaTriNguyHiem = (decimal)criteriaList[i].GiaTriNguyHiem;
                newCriteria.TenDonViTinh = FindUnit((int)criteriaList[i].MaDonViTinh);
                list.Add(newCriteria);

            }
            return list;
        }
        public void LoadCriteriaList()
        {
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            CriteriaList = GetCriteriaFromList(listCriteria);
        }
        public string FindUnit(int maDonVi)
        {
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            List<DONVITINH> listUnit = DataProvider.Ins.DB.DONVITINHs.ToList();
            string tenDonVi = "";
            for (int i = 0; i < listUnit.Count(); i++)
            {
                if (maDonVi == listUnit[i].MaDonViTinh)
                {
                    tenDonVi = (string)listUnit[i].TenDonViTinh;
                    break;
                }
            }

            return tenDonVi;
        }
        public void OpenAddCriteria(object p)
        {
            AddCriteriaCriteria window = new AddCriteriaCriteria(this);
            window.ShowDialog();
        }
        public void OpenEditCriteria(object p)
        {
            ProductCriteria selectedItem = p as ProductCriteria;
            EditProductCriteria editProductCriteria = new EditProductCriteria(selectedItem, this);
            editProductCriteria.Show();
        }
        public void RefreshData(object p)
        {
            List<CHITIEUSANPHAM> listCriteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            CriteriaList = GetCriteriaFromList(listCriteria);
        }
        public void RefreshAddForm()
        {
            CriteriaName = "";
            StandardValue = 0;
            UnsafeValue = 0;
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
        public void AddCriteriaCriteria(Grid addCriteriaForm)
        {
            if (CriteriaName == "" || StandardValue.ToString() == "" || UnsafeValue.ToString() == "")
            {
                MyMessageQueue.Enqueue("Vui lòng điền đầy đủ thông tin");
            }
            else if (UnsafeValue <= StandardValue)
            {
                MyMessageQueue.Enqueue("Giá trị tiêu chuẩn phải nhỏ hơn giá trị nguy hiểm");
            }
            else if (Resources.Utils.Validator.IsValid(addCriteriaForm))
            {
                CHITIEUSANPHAM newCriteria = new CHITIEUSANPHAM();
                newCriteria.TenChiTieu = CriteriaName;
                newCriteria.GiaTriTieuChuan = StandardValue;
                newCriteria.GiaTriNguyHiem = UnsafeValue;

                newCriteria.MaDonViTinh = SelectedUnit.MaDonViTinh;
                DataProvider.Ins.DB.CHITIEUSANPHAMs.Add(newCriteria);
                DataProvider.Ins.DB.SaveChanges();
                //MessageBox.Show("Thêm chỉ tiêu thành công");
                MyMessageQueue.Enqueue("Thêm chỉ tiêu thành công");
                CriteriaName = "";
                StandardValue = 0;
                UnsafeValue = 0;
                ManageProductCriteria window = new ManageProductCriteria();
                RefreshData(window);

            }
        }
    }
}
