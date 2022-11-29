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
        private int OwnerId = 1;
        private string _facilityName;
        private int _quantity;
        private DateTime _startDate;
        private string _productName;
        //private COSOSANXUAT _facility;
        private SANPHAM _selectedProduct;
        private COSOSANXUAT _selectedFacility;
        private ObservableCollection<COSOSANXUAT> _facilityList;
        private ObservableCollection<SANPHAM> _productList;
        private ObservableCollection<CreateEnrollSheetModel> _selectedProductList;
        private SnackbarMessageQueue _myMessageQueue;

        public DateTime DateNow { get; set; }
        public COSOSANXUAT SelectedFacility { get { return _selectedFacility; } set { _selectedFacility = value; LoadDataProductList(); OnPropertyChanged(nameof(SelectedFacility)); } }
        public ObservableCollection<COSOSANXUAT> FacilityList { get { return _facilityList; } set { _facilityList = value; OnPropertyChanged(nameof(FacilityList)); } }
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public ObservableCollection<CreateEnrollSheetModel> SelectedProductList { get { return _selectedProductList; } set { _selectedProductList = value; OnPropertyChanged(nameof(SelectedProductList)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged(nameof(Quantity)); } }
        public DateTime StartDay { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDay)); } }
        public string ProductName { get { return _productName; } set { _productName = value; OnPropertyChanged(nameof(ProductName)); } }
        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value;  OnPropertyChanged(nameof(SelectedProduct)); } }
        public SnackbarMessageQueue MyMessageQueue { get { return _myMessageQueue; } set { _myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand CSubmitForm { get; set; }
        public ICommand CAddProduct { get; set; }
        public ICommand CDeleteProduct { get; set; }

        public CreateRegistrationSheetViewModel()
        {
      
            DateNow = DateTime.Today;
            StartDay = DateTime.Today.AddDays(7);
            SelectedFacility = new COSOSANXUAT();
            SelectedProduct = new SANPHAM();
            SelectedProductList = new ObservableCollection<CreateEnrollSheetModel>();
            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });
            CAddProduct = new RelayCommand<object>((p) => { return true; }, (p) => { AddProduct(p); });
            CDeleteProduct = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteProduct(p); });

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1500));
            MyMessageQueue.DiscardDuplicates = true;

            LoadDataFacilityList();
        }
        void AddProduct(object p)
        {
            if (SelectedFacility.MaCoSo == 0)
            {
                MyMessageQueue.Enqueue("Vui lòng chọn 1 cơ sở");
                return; 
            }
            if (SelectedProduct.MaSanPham == 0)
            {
                MyMessageQueue.Enqueue("Vui lòng chọn 1 sản phẩm");
                return;
            }
            //Check if already existed selected product

            for (int i = 0; i < SelectedProductList.Count; i++)
            {
                if (SelectedProductList[i].MaSanPham == SelectedProduct.MaSanPham)
                {
                    SelectedProductList.RemoveAt(i);
                    CreateEnrollSheetModel m = new CreateEnrollSheetModel(SelectedProduct.MaSanPham, SelectedProduct.TenSanPham, Quantity);
                    SelectedProductList.Add(m);
                    return;
                }
            }

            CreateEnrollSheetModel model = new CreateEnrollSheetModel(SelectedProduct.MaSanPham,SelectedProduct.TenSanPham,Quantity);
            SelectedProductList.Add(model);
        }
        void DeleteProduct(object p)
        {
           
        }
        void LoadDataFacilityList()
        {
            List<COSOSANXUAT> List = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaChuCoSo == OwnerId).ToList();
            FacilityList = new ObservableCollection<COSOSANXUAT>(List);
            LoadDataProductList();
            
        }
        void LoadDataProductList()
        {
            if (SelectedFacility.MaCoSo != 0 )
            {
                SelectedProductList.Clear();
                List<SANPHAM> Products = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == SelectedFacility.MaCoSo).ToList();
                ProductList = new ObservableCollection<SANPHAM>(Products);
            }
        }
        void SubmitForm(Window p)
        {
            PHIEUDANGKY NewSheet = new PHIEUDANGKY();
            NewSheet.MaCoSo = SelectedFacility.MaCoSo;
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
                for(int i=0;i< SelectedProductList.Count; i++)
                {
                    CHITIETPHIEUDANGKY newDetailSheet = new CHITIETPHIEUDANGKY();
                    newDetailSheet.MaSanPham = SelectedProductList[i].MaSanPham;
                    newDetailSheet.MaPhieuDangKy = NewSheet.MaPhieuDangKy;
                    newDetailSheet.SoLuong = SelectedProductList[i].SoLuong;
                    DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Add(newDetailSheet);
                }
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
