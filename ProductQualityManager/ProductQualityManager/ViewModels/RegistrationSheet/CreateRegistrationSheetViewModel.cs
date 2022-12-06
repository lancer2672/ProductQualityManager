using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.TestSheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.RegistrationSheet
{
    public class CreateRegistrationSheetViewModel : BaseViewModel
    {
        private string _facilityName;
        private int _quantity;
        private DateTime _startDate;
        private string _productName;
        private string _unitProductName;
        private string _unitCriteriaName;
        private string _criteriaValue;
        private SANPHAM _selectedProduct;
        private CreateEnrollSheetModel _selectedRecord;
        private COSOSANXUAT _selectedFacility;
        private CHITIEUSANPHAM _selectedCriteria;
        private ObservableCollection<SANPHAM> _productList;
        private ObservableCollection<CHITIEUSANPHAM> _criteriaList;
        private ObservableCollection<CreateEnrollSheetModel> _detailRegistrationSheet;
        private SnackbarMessageQueue _myMessageQueue;

        public DateTime DateNow { get; set; }
      
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public ObservableCollection<CHITIEUSANPHAM> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }
        public ObservableCollection<CreateEnrollSheetModel> DetailRegistrationSheetList { get { return _detailRegistrationSheet; } set { _detailRegistrationSheet = value; OnPropertyChanged(nameof(DetailRegistrationSheetList)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public string ProductName { get { return _productName; } set { _productName = value; OnPropertyChanged(nameof(ProductName)); } }
        public string UnitProductName { get { return _unitProductName; } set { _unitProductName = value; OnPropertyChanged(nameof(UnitProductName)); } }
        public string UnitCriteriaName { get { return _unitCriteriaName; } set { _unitCriteriaName = value; OnPropertyChanged(nameof(UnitCriteriaName)); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged(nameof(Quantity)); } }
        public string CriteriaValue { get { return _criteriaValue; } set { _criteriaValue = value; OnPropertyChanged(nameof(CriteriaValue)); } }
        public CreateEnrollSheetModel SelectedRecord { get { return _selectedRecord; } set { _selectedRecord = value; OnPropertyChanged(nameof(SelectedRecord)); } }
        public DateTime StartDay { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDay)); } }
        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; SelectProductChange(); OnPropertyChanged(nameof(SelectedProduct)); } }
        public CHITIEUSANPHAM SelectedCriteria { get { return _selectedCriteria; } set { _selectedCriteria = value; SelectCriteriaChange();  OnPropertyChanged(nameof(SelectedCriteria)); } }
        public SnackbarMessageQueue MyMessageQueue { get { return _myMessageQueue; } set { _myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand CSubmitForm { get; set; }
        public ICommand CAddCriteria { get; set; }
        public ICommand CDeleteCriteria { get; set; }
        public CreateRegistrationSheetViewModel(COSOSANXUAT selectedFacility)
        {
      
            DateNow = DateTime.Today;
            StartDay = DateTime.Today.AddDays(7);
            _selectedFacility = selectedFacility;
          
            DetailRegistrationSheetList = new ObservableCollection<CreateEnrollSheetModel>();
            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });
            CAddCriteria = new RelayCommand<object>((p) => { return true; }, (p) => { AddCriteria(p); });
            CDeleteCriteria = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteCriteria(p); });

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1500));
            MyMessageQueue.DiscardDuplicates = true;

            LoadDataProductList();
            LoadCriteriaList();
            //LoadDataFacilityList();
        }
        public void SelectProductChange()
        {
            DONVITINHSANPHAM unitProduct = DataProvider.Ins.DB.DONVITINHSANPHAMs.Where(t => t.MaDonViTinhSP == SelectedProduct.MaDonViTinhSP).FirstOrDefault();
            UnitProductName = unitProduct.TenDonViTinhSP;
            DetailRegistrationSheetList.Clear();
        }
        public void SelectCriteriaChange()
        {
            DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == SelectedCriteria.MaDonViTinh).FirstOrDefault();
            UnitCriteriaName = unit.TenDonViTinh;
        }
        public void LoadCriteriaList()
        {
            List<CHITIEUSANPHAM> list = DataProvider.Ins.DB.CHITIEUSANPHAMs.ToList();
            CriteriaList = new ObservableCollection<CHITIEUSANPHAM>(list);
        }     
        public void DeleteCriteria(object p)
        {
            if(SelectedRecord != null)
            {
                CreateEnrollSheetModel selectedItem = p as CreateEnrollSheetModel;
                DetailRegistrationSheetList.Remove(selectedItem);
            }
        }
        public void AddCriteria(object p)
        {
            if (SelectedProduct == null)
            {
                MyMessageQueue.Enqueue("Vui lòng chọn 1 sản phẩm");
                return;
            }
            if (SelectedCriteria == null)
            {
                MyMessageQueue.Enqueue("Vui lòng chọn chỉ tiêu cho sản phẩm");
                return;
            }
            //check if existed 
            for (int i = 0; i < DetailRegistrationSheetList.Count; i++)
            {
                if (DetailRegistrationSheetList[i].MaChiTieu == SelectedCriteria.MaChiTieu)
                {
                    DetailRegistrationSheetList[i].GiaTri = CriteriaValue.ToString() + " "+ DetailRegistrationSheetList[i].TenDonViTinh;
                    DetailRegistrationSheetList[i] = new CreateEnrollSheetModel(DetailRegistrationSheetList[i]);
                    return;
                }
            }
            DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where( t=> t.MaDonViTinh == SelectedCriteria.MaDonViTinh ).FirstOrDefault();
            CHITIETPHIEUDANGKY newDetailSheet= new CHITIETPHIEUDANGKY();
            newDetailSheet.MaChiTieu = SelectedCriteria.MaChiTieu;
            newDetailSheet.GiaTriDangKy = decimal.Parse(CriteriaValue);
            CreateEnrollSheetModel newItem = new CreateEnrollSheetModel(unit, decimal.Parse(CriteriaValue), SelectedCriteria);

            DetailRegistrationSheetList.Add(newItem);


            //Check if already existed selected product

        }
        void LoadDataProductList()
        {

                List<SANPHAM> Products = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == _selectedFacility.MaCoSo).ToList();
                ProductList = new ObservableCollection<SANPHAM>(Products);
        }
        void SubmitForm(Window p)
        {
            if (StartDay < DateTime.Now)
            {
                MyMessageQueue.Enqueue("Lỗi. Ngày hết hạn không hợp lệ!");
                return;
            }
            if (Quantity == 0)
            {
                MyMessageQueue.Enqueue("Lỗi. Số lượng phải lớn hơn 0");
                return;
            }
            if(DetailRegistrationSheetList.Count == 0)
            {
                MyMessageQueue.Enqueue("Lỗi. Vui lòng điền ít nhất 1 chỉ tiêu");
                return;
            }
            CHUCOSO owner = DataProvider.Ins.DB.CHUCOSOes.Where(t => t.MaChuCoSo == _selectedFacility.MaChuCoSo).FirstOrDefault();
            SubmitCreateSheet newWindow = new SubmitCreateSheet(this, _selectedFacility, owner);
            newWindow.ShowDialog();

        }
        void ClearForm()
        {

        }
    }
}
