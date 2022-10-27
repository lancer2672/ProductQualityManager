using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private COSOSANXUAT _selectedFacility;
        private SANPHAM _selectedProduct;
        private ObservableCollection<COSOSANXUAT> _facilityList;
        private ObservableCollection<SANPHAM> _productList;


        public ObservableCollection<COSOSANXUAT> FacilityList { get { return _facilityList; } set { _facilityList = value; OnPropertyChanged(nameof(FacilityList)); } }
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged(nameof(Quantity)); } }
        public DateTime StartDay { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDay)); } }
        public string ProductName { get { return _productName; } set { _productName = value; OnPropertyChanged(nameof(ProductName)); } }
        public COSOSANXUAT SelectedFacility { get { return _selectedFacility; } set { _selectedFacility = value; LoadDataProductList(); LoadDataProductList(); OnPropertyChanged(nameof(SelectedFacility)); } }
        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value;  OnPropertyChanged(nameof(SelectedProduct)); } }

        public ICommand CSubmitForm { get; set; }

        public CreateRegistrationSheetViewModel()
        {
            SelectedFacility = new COSOSANXUAT();
            SelectedProduct = new SANPHAM();
            CSubmitForm = new RelayCommand<object>((p) => { return true; }, (p) => { SubmitForm(p); });
            LoadDataFacilityList();
        }
        void LoadDataFacilityList()
        {
            List<COSOSANXUAT> List = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaChuCoSo == OwnerId).ToList();
            FacilityList = new ObservableCollection<COSOSANXUAT>(List);
            LoadDataProductList();
            
        }
        void LoadDataProductList()
        {
            if (SelectedFacility.MaCoSo != 0)
            {
                List<SANPHAM> Products = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == SelectedFacility.MaCoSo).ToList();
                ProductList = new ObservableCollection<SANPHAM>(Products);
            }
        }
        void SubmitForm(object p)
        {
            PHIEUDANGKY NewSheet = new PHIEUDANGKY();
            NewSheet.MaCoSo = SelectedFacility.MaCoSo;
            NewSheet.TrangThai = 0;
            NewSheet.NgayDangKy = DateTime.Now;
            NewSheet.HanDangKy = StartDay;

            try
            {
                DataProvider.Ins.DB.PHIEUDANGKies.Add(NewSheet);
                DataProvider.Ins.DB.SaveChanges();
            }catch(Exception e)
            {

            }
        }
        void ClearForm()
        {

        }
    }
}
