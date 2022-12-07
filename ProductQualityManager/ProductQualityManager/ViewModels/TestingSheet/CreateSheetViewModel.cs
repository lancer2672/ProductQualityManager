using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.SharedControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class CreateSheetViewModel: BaseViewModel
    {

        private COSOSANXUAT _facility;
        private int _searchId;
        private string _facilityName;
        private string _source;
        private string _facilityAddress;
        private ObservableCollection<CHITIEUSANPHAM> _criteriaList;
        private ObservableCollection<SANPHAM> _productList;
        private PHIEUDANGKY _registrationSheet;
        private CHITIEUSANPHAM _selectedCriteria;
        private SANPHAM _selectedProduct;
        private SnackbarMessageQueue _myMessageQueue;
        private string _criteriaValue;
        private string _unitCriteriaName;
        public ObservableCollection<CreateTestingSheetModel> _testingCriteraList;

        public string UnitCriteriaName { get { return _unitCriteriaName; } set { _unitCriteriaName = value; OnPropertyChanged(nameof(UnitCriteriaName)); } }
        public string CriteriaValue { get { return _criteriaValue; } set { _criteriaValue = value; OnPropertyChanged(nameof(CriteriaValue)); } }
        public int SearchId { get { return _searchId; } set { _searchId = value; OnPropertyChanged(nameof(SearchId)); } }
        public CHITIEUSANPHAM SelectedCriteria { get { return _selectedCriteria; } set { _selectedCriteria = value; SelectCriteriaChange(); OnPropertyChanged(nameof(SelectedCriteria)); } }
        public SANPHAM SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; LoadListCriteria(); OnPropertyChanged(nameof(SelectedProduct)); } }
        public string FacilityName { get { return _facilityName; } set { _facilityName = value; OnPropertyChanged(nameof(FacilityName)); } }
        public string FacilityAddress { get { return _facilityAddress; } set { _facilityAddress = value; OnPropertyChanged(nameof(FacilityAddress)); } }
        public ObservableCollection<CHITIEUSANPHAM> CriteriaList { get { return _criteriaList; } set { _criteriaList = value; OnPropertyChanged(nameof(CriteriaList)); } }
        public ObservableCollection<CreateTestingSheetModel> TestingCriteraList { get { return _testingCriteraList; } set { _testingCriteraList = value; OnPropertyChanged(nameof(TestingCriteraList)); } }
        public ObservableCollection<SANPHAM> ProductList { get { return _productList; } set { _productList = value; OnPropertyChanged(nameof(ProductList)); } }
        public SnackbarMessageQueue MyMessageQueue { get { return _myMessageQueue; } set { _myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand CSearch { get; set; }
        public ICommand CAddImage { get; set; }
        public ICommand CSubmitForm { get; set; }
        
        public CreateSheetViewModel()
        {
            CSearch = new RelayCommand<object>((p) => { return true; }, (p) => { Search(p); });
            CAddImage = new RelayCommand<object>((p) => { return true; }, (p) => { HandleSaveImage(); });
            CSubmitForm= new RelayCommand<object>((p) => { return true; }, (p) => { HandleCreateTestingSheet(); });
            CriteriaList = new ObservableCollection<CHITIEUSANPHAM>();
            TestingCriteraList = new ObservableCollection<CreateTestingSheetModel>();

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1500));
            MyMessageQueue.DiscardDuplicates = true;

        }
        private void HandleCreateTestingSheet()
        {
            // Không có phiếu đăng ký nào để kiểm nghiệm
            if(_registrationSheet == null)
            {
                return;
            }

            PHIEUKIEMNGHIEM newSheet = new PHIEUKIEMNGHIEM();
            
            
        }
        private void HandleSaveImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = ofd.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                string _source = bitmap.ToString();
            }
        }
        private void SelectCriteriaChange()
        {
            DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == SelectedCriteria.MaDonViTinh).FirstOrDefault();
            UnitCriteriaName = unit.TenDonViTinh;
        }
        private void LoadListProduct()
        {
            if(_facility != null)
            {   
                List<SANPHAM> list = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaCoSo == _facility.MaCoSo).ToList();
                ProductList = new ObservableCollection<SANPHAM>(list);
                
            }
        }
        private void LoadListCriteria()
        {
            if(_facility != null && SelectedProduct != null)
            {
                CriteriaList.Clear();
                TestingCriteraList.Clear();
                //Lấy ra phiếu đăng ký ĐƯỢC DUYỆT cuối cùng của sản phẩm này
                PHIEUDANGKY registrationSheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaCoSo == _facility.MaCoSo && t.MaSanPham == SelectedProduct.MaSanPham && t.TrangThai == 1).ToList().LastOrDefault();
                
                if (registrationSheet == null)
                {
                    return;
                }
                else
                {
                    _registrationSheet = registrationSheet;
                    //Lấy ra danh sách chỉ tiêu của sản phẩm đã đăng ký
                    List<CHITIETPHIEUDANGKY> list = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(t => t.MaPhieuDangKy == registrationSheet.MaPhieuDangKy).ToList();
                    foreach(CHITIETPHIEUDANGKY item in list)
                    {
                        int id = (int)item.MaChiTieu;
                        CHITIEUSANPHAM ct = DataProvider.Ins.DB.CHITIEUSANPHAMs.Where( t=>t.MaChiTieu == id).FirstOrDefault();
                        //Dữ liệu cho combobox chỉ tiêu
                        CriteriaList.Add(ct);

                        //Lấy dữ liệu cho listview chỉ tiêu
                        DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == ct.MaDonViTinh).FirstOrDefault();
                        CreateTestingSheetModel newItem = new CreateTestingSheetModel(unit, item, -1, ct);
                        TestingCriteraList.Add(newItem);
                    }
                }
            }
        }
        private void Search(object p)
        {
            COSOSANXUAT g = DataProvider.Ins.DB.COSOSANXUATs.Where(t => t.MaCoSo == SearchId).FirstOrDefault();
            if(g != null)
            {
                _facility = g;
                FacilityName = g.TenCoSo;
                FacilityAddress = g.DiaChi;
                LoadListProduct();

            }
            else
            {
                MyMessageQueue.Enqueue("Không tìm thấy cơ sở!");
            }
        }
    }
}
