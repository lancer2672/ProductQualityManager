using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
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
        private int _criteriaValue;
        private PHIEUDANGKY _registrationSheet;
        private SANPHAM _selectedProduct;
        private CreateEnrollSheetModel _selectedRecord;
        private COSOSANXUAT _selectedFacility;
        private CHITIEUSANPHAM _selectedCriteria;
        //private ObservableCollection<COSOSANXUAT> _facilityList;
        private ObservableCollection<SANPHAM> _productList;
        private ObservableCollection<CHITIEUSANPHAM> _criteriaList;
        private ObservableCollection<CreateEnrollSheetModel> _detailRegistrationSheet;
        private SnackbarMessageQueue _myMessageQueue;

        public DateTime DateNow { get; set; }
        //public COSOSANXUAT SelectedFacility { get { return _selectedFacility; } set { _selectedFacility = value; LoadDataProductList(); OnPropertyChanged(nameof(SelectedFacility)); } }
        //public ObservableCollection<COSOSANXUAT> FacilityList { get { return _facilityList; } set { _facilityList = value; OnPropertyChanged(nameof(FacilityList)); } }
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public ObservableCollection<CHITIEUSANPHAM> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }
        public ObservableCollection<CreateEnrollSheetModel> DetailRegistrationSheetList { get { return _detailRegistrationSheet; } set { _detailRegistrationSheet = value; OnPropertyChanged(nameof(DetailRegistrationSheetList)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public CreateEnrollSheetModel SelectedRecord { get { return _selectedRecord; } set { _selectedRecord = value; OnPropertyChanged(nameof(SelectedRecord)); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged(nameof(Quantity)); } }
        public int CriteriaValue { get { return _criteriaValue; } set { _criteriaValue = value; OnPropertyChanged(nameof(CriteriaValue)); } }
        public DateTime StartDay { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDay)); } }
        public string ProductName { get { return _productName; } set { _productName = value; OnPropertyChanged(nameof(ProductName)); } }
        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value;  OnPropertyChanged(nameof(SelectedProduct)); } }
        public CHITIEUSANPHAM SelectedCriteria { get { return _selectedCriteria; } set { _selectedCriteria = value;  OnPropertyChanged(nameof(SelectedCriteria)); } }
        public SnackbarMessageQueue MyMessageQueue { get { return _myMessageQueue; } set { _myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand CSubmitForm { get; set; }
        public ICommand CAddCriteria { get; set; }
        public ICommand CDeleteProduct { get; set; }
        public ICommand CDeleteCriteria { get; set; }
        public CreateRegistrationSheetViewModel(COSOSANXUAT selectedFacility)
        {
      
            DateNow = DateTime.Today;
            StartDay = DateTime.Today.AddDays(7);
            _selectedFacility = selectedFacility;
            _registrationSheet = new PHIEUDANGKY();
            DetailRegistrationSheetList = new ObservableCollection<CreateEnrollSheetModel>();
            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });
            CAddCriteria = new RelayCommand<object>((p) => { return true; }, (p) => { AddCriteria(p); });
            CDeleteProduct = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteProduct(p); });
            CDeleteCriteria = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteCriteria(p); });

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1500));
            MyMessageQueue.DiscardDuplicates = true;

            LoadDataProductList();
            LoadCriteriaList();
            //LoadDataFacilityList();
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
            newDetailSheet.MaPhieuDangKy = _registrationSheet.MaPhieuDangKy;
            newDetailSheet.MaChiTieu = SelectedCriteria.MaChiTieu;
            newDetailSheet.GiaTriDangKy = CriteriaValue;
            CreateEnrollSheetModel newItem = new CreateEnrollSheetModel(_registrationSheet, unit, newDetailSheet, SelectedCriteria);

            DetailRegistrationSheetList.Add(newItem);


            //Check if already existed selected product

        }
        void DeleteProduct(object p)
        {
           
        }
        //void LoadDataFacilityList()
        //{
        //    List<COSOSANXUAT> List = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaChuCoSo == OwnerId).ToList();
        //    FacilityList = new ObservableCollection<COSOSANXUAT>(List);
        //    LoadDataProductList();
            
        //}
        void LoadDataProductList()
        {
                List<SANPHAM> Products = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == _selectedFacility.MaCoSo).ToList();
                ProductList = new ObservableCollection<SANPHAM>(Products);
        }
        void SubmitForm(Window p)
        {
            PHIEUDANGKY NewSheet = new PHIEUDANGKY();
            NewSheet.MaCoSo = _selectedFacility.MaCoSo;
            NewSheet.TrangThai = 0;
            NewSheet.NgayDangKy = DateTime.Now;
            if(StartDay < DateTime.Now)
            {
                MyMessageQueue.Enqueue("Lỗi. Ngày hết hạn không hợp lệ!");
                return;
            }
            NewSheet.HanDangKy = StartDay;
            

            try
            {
               
                DataProvider.Ins.DB.PHIEUDANGKies.Add(NewSheet);
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            }
            catch(Exception e)
            {
               
            }
        }
        void ClearForm()
        {

        }
    }
}
