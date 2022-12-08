using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.SharedControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class CreateSheetViewModel : BaseViewModel
    {
   
        private COSOSANXUAT _facility;
        private int _searchId;
        private int _preMin = 0;
        private int _preIndex = -1;
        private int _min = 0;
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
        private string _result;
        private string _resultColor;
        private int _id_DanhGia = 0;
        private TestingSheetViewModel _vm;
        // 0: chưa đánh giá, 1; Đạt, -1: Không Đạt, -2:Gây Nguy Hiểm
        public ObservableCollection<CreateTestingSheetModel> _testingCriteraList;
        public string UnitCriteriaName { get { return _unitCriteriaName; } set { _unitCriteriaName = value; OnPropertyChanged(nameof(UnitCriteriaName)); } }
        public string Result { get { return _result; } set { _result = value; OnPropertyChanged(nameof(Result)); } }
        public string ResultColor { get { return _resultColor; } set { _resultColor = value; OnPropertyChanged(nameof(ResultColor)); } }
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
        public ICommand CAddCriteria { get; set; }

        public CreateSheetViewModel(TestingSheetViewModel vm)
        {
            _vm = vm;
            CSearch = new RelayCommand<object>((p) => { return true; }, (p) => { Search(p); });
            CAddImage = new RelayCommand<object>((p) => { return true; }, (p) => { HandleSaveImage(); });
            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { HandleCreateTestingSheet(p); });
            CAddCriteria = new RelayCommand<object>((p) => { return true; }, (p) => { AddCriteriaToList(); });
            CriteriaList = new ObservableCollection<CHITIEUSANPHAM>();
            TestingCriteraList = new ObservableCollection<CreateTestingSheetModel>();
            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1500));
            MyMessageQueue.DiscardDuplicates = true;

        }
        private void HandleCreateTestingSheet(Window p )
        {
            // Không có phiếu đăng ký nào để kiểm nghiệm
            if (_registrationSheet == null)
            {
                return;
            }
            //Kiểm tra xem list có giá trị thực tế của chỉ tiêu hay không 
            bool checkIfFullEmpty = true;
            for(int i=0;i< TestingCriteraList.Count; i++)
            {
                if (TestingCriteraList[i].Dec_GiaTriThucTe >= 0)
                {
                    checkIfFullEmpty = false;
                }
            }
            if (checkIfFullEmpty)
            {
                MyMessageQueue.Enqueue("Vui lòng điền ít nhất 1 chỉ tiêu");
                return;
            }


            PHIEUKIEMNGHIEM newSheet = new PHIEUKIEMNGHIEM();
            newSheet.MaCoSo = _facility.MaCoSo;
            newSheet.MaPhieuDangKy = _registrationSheet.MaPhieuDangKy;
            newSheet.KetQua = _min;
            newSheet.NgayDanhGia = DateTime.Now;

            DataProvider.Ins.DB.PHIEUKIEMNGHIEMs.Add(newSheet);
            for (int i = 0; i < TestingCriteraList.Count; i++)
            {
                CHITIETPHIEUKIEMNGHIEM item = new CHITIETPHIEUKIEMNGHIEM();
                item.MaPhieuKiemNghiem = newSheet.MaPhieuKiemNghiem;
                item.GiaTriKiemNghiem = TestingCriteraList[i].Dec_GiaTriThucTe;
                item.MaChiTieu = TestingCriteraList[i].MaChiTieu;
                item.KetQua = TestingCriteraList[i].MaDanhGia;
                DataProvider.Ins.DB.CHITIETPHIEUKIEMNGHIEMs.Add(item);
            }
            try
            {
                DataProvider.Ins.DB.SaveChanges();
                _vm.LoadListView();
                p.Close();
            }
            catch(Exception err)
            {
                throw err;
            }

        }
        private void AddCriteriaToList()
        {
            _min = 2;   
            for (int i = 0; i < TestingCriteraList.Count; i++)
             {
                int value = 0;
                if (TestingCriteraList[i].MaChiTieu == SelectedCriteria.MaChiTieu)
                {
                
                    _preIndex = i;
                    int id = TestingCriteraList[i].MaDonViTinh;
                    DONVITINH dvt = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == id).FirstOrDefault();
                    TestingCriteraList[i].GiaTriThucTe = CriteriaValue + " " + dvt.TenDonViTinh;
                    TestingCriteraList[i].Dec_GiaTriThucTe = Convert.ToDecimal(CriteriaValue);
                    SetState(TestingCriteraList[i]);
                    TestingCriteraList[i] = new CreateTestingSheetModel(TestingCriteraList[i]);
                }
                if (TestingCriteraList[i].DanhGia == "Đạt")
                {
                    value = 1;
                }
                else if (TestingCriteraList[i].DanhGia == "Không Đạt")
                {
                    value = -1;
                }
                else if (TestingCriteraList[i].DanhGia == "Gây Nguy Hiểm")
                {
                    value = -2;
                }
                else value = 2;
                if (_min > value)
                {
                     _min = value;
                }
               

            }
                switch (_min)
                    {
                        case 1:
                            {
                                Result = "Đạt";
                                ResultColor = "Green";
                                break;
                            }
                        case -1:
                            {
                                Result = "Không Đạt";
                                ResultColor = "Orange";
                                break;
                            }
                        case -2:
                            {
                                Result = "Gây Nguy Hiểm";
                                ResultColor = "Red";
                                break;
                            }
                    default :
                    {
                        Result = "";
                        break;
                    }
                }
        }
       
        private void SetState(CreateTestingSheetModel item)
        {
            CHITIEUSANPHAM ct = DataProvider.Ins.DB.CHITIEUSANPHAMs.Where(t => t.MaChiTieu == item.MaChiTieu).FirstOrDefault();
            if (item.Dec_GiaTriThucTe == item.Dec_GiaTriDangKy)
            {
                item.SetState(1);
                item.MaDanhGia = 1;
            }
            else if(item.Dec_GiaTriThucTe >= ct.GiaTriNguyHiem)
            {
                item.SetState(-2);
                item.MaDanhGia = -2;
            }
            else
            {
                item.SetState(-1);
                item.MaDanhGia = -1;
            }
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
