using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.Product
{
    public class ProductViewModel: BaseViewModel
    {
        private SANPHAM _selectedProduct;
        private ObservableCollection<SANPHAM> _productList;
        private ObservableCollection<ProductCriteria> _criteriaList;
        private COSOSANXUAT _selectedFacility;
        private ObservableCollection<COSOSANXUAT> _facilityList;

        public ObservableCollection<COSOSANXUAT> FacilityList { get { return _facilityList; } set { _facilityList = value; OnPropertyChanged(nameof(FacilityList)); } }
        public COSOSANXUAT SelectedFacility { get { return _selectedFacility; } set { _selectedFacility = value; LoadProductList(); OnPropertyChanged(nameof(SelectedFacility)); } }
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public ObservableCollection<ProductCriteria> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }

        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; LoadCriteriaList(); OnPropertyChanged(nameof(SelectedProduct)); } }

        public ICommand CDelete { get; set; }

        public ProductViewModel()
        {
            SelectedFacility = new COSOSANXUAT();
            App.Current.Properties["FacilityOwner"] = 1;
            SelectedProduct = new SANPHAM();
            CDelete = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteProduct(p); });
            CriteriaList = new ObservableCollection<ProductCriteria>();
            LoadFacitlityList();
            LoadProductList();
        }
        public void LoadFacitlityList()
        {
            int facilityOwnerId = (int)App.Current.Properties["FacilityOwner"];
            List<COSOSANXUAT> facilityList = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaChuCoSo == facilityOwnerId).ToList();
            FacilityList = new ObservableCollection<COSOSANXUAT>(facilityList);
        }
        public void LoadProductList()
        {
            if (SelectedFacility.MaCoSo == 0) return;
            List<SANPHAM> products = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == SelectedFacility.MaCoSo).ToList();
            ProductList = new ObservableCollection<SANPHAM>(products);

        }
        
        public void DeleteProduct(object p)
        {
            
        }
        public void LoadCriteriaList()
        {
            //chưa chọn gì
            if (SelectedProduct.MaSanPham == 0)
            {
                return;
            }
            CriteriaList.Clear();
            //Lấy ra phiếu đăng ký của cơ sở cho sản phẩm đó
            DANGKYCHITIEU RegisterCritera = DataProvider.Ins.DB.DANGKYCHITIEUx.Where(t => t.MaCoSo == SelectedProduct.MaCoSo && t.MaSanPham == SelectedProduct.MaSanPham).FirstOrDefault();
            // lấy ra chi tiết của phiếu ở phía trên
            List<CHITIETCHITIEUSANPHAM> DetailRegisterCriteria = DataProvider.Ins.DB.CHITIETCHITIEUSANPHAMs.Where(t => t.MaDangKyChiTieu == RegisterCritera.MaDangKyChiTieu).ToList();
            foreach (var item in DetailRegisterCriteria)
            {
                CHITIEUSANPHAM criteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.Where(t => t.MaChiTieu == item.MaChiTieu).FirstOrDefault();
                DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == criteria.MaDonViTinh).FirstOrDefault();

                string criteriaName = criteria.TenChiTieu;
                decimal criteriaStandard = (decimal)criteria.GiaTriTieuChuan;
                string unitName = unit.TenDonViTinh;
                decimal registeredValue = (decimal)item.GiaTriDangKy;
                ProductCriteria productCriteria = new ProductCriteria(registeredValue, criteriaName, criteriaStandard, unitName);
                CriteriaList.Add(productCriteria);
            }
        }
    }
}
