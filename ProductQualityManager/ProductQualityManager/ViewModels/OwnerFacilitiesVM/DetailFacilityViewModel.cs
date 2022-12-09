using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.OwnerFacilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ProductQualityManager.Views.TestingSheet;

namespace ProductQualityManager.ViewModels.OwnerFacilitiesVM
{
    public class DetailFacilityViewModel : BaseViewModel
    {
        private CHUCOSO _owner;
        private int _idowner;
        private string _namefacility;
        private string _addressfacility;
        private string _nameproduct;
        private string _unit;
        private COSOSANXUAT _facility;
        private int _idregister;
        private ObservableCollection<Products> _listproduct;
        private Products _selectedproduct;
        private SnackbarMessageQueue myMessageQueue;

        public CHUCOSO Owner { get { return _owner; } set { _owner = value; OnPropertyChanged(nameof(Owner)); } }
        public int IdOwner { get { return _idowner; } set { _idowner = value; OnPropertyChanged(nameof(IdOwner)); } }
        public string NameFacility { get { return _namefacility; } set { _namefacility = value; OnPropertyChanged(nameof(NameFacility)); } }
        public string AddressFacility { get { return _addressfacility; } set { _addressfacility = value; OnPropertyChanged(nameof(AddressFacility)); } }
        public string NameProduct { get { return _nameproduct; } set { _nameproduct = value; OnPropertyChanged(nameof(NameProduct)); } }
        public string Unit { get { return _unit; } set { _unit = value; OnPropertyChanged(nameof(Unit)); } }
        public COSOSANXUAT Facility { get { return _facility; } set { _facility = value; OnPropertyChanged(nameof(Facility)); } }
        public int IdRegister { get { return _idregister; } set { _idregister = value; OnPropertyChanged(nameof(IdRegister)); } }
        public ObservableCollection<Products> ListProduct { get { return _listproduct; } set { _listproduct = value; OnPropertyChanged(nameof(ListProduct)); } }
        public Products SelectedProduct { get { return _selectedproduct; } set { _selectedproduct = value; OnPropertyChanged(nameof(SelectedProduct)); } }
        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand EditInforFacilityCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        //Khởi tạo 
        public DetailFacilityViewModel(COSOSANXUAT SelectedFaclility)
        {
            IdRegister = 0;
            Facility = SelectedFaclility;
            NameFacility = Facility.TenCoSo;
            AddressFacility = Facility.DiaChi;
            ListProduct = new ObservableCollection<Products>();
            SelectedProduct = null;
            GetListProduct();
            CheckOverDueStatusProducts();
            
            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))
            {
                DiscardDuplicates = true
            };

            AddProductCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddProduct(); });
            EditProductCommand = new RelayCommand<object>((p) => { return true; }, (p) => { EditProduct(); });
            SelectionChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SelectionChange(); });
            EditInforFacilityCommand = new RelayCommand<ManageOwnerViewModel>((p) => { return true; }, (p) => { EditInfor(); });
            RegisterCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenCreateRegistrationSheetWindow(); });
            SaveCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SaveAndExit(); });

        }
        private void CheckOverDueStatusProducts()
        {
            for(int i = 0; i < ListProduct.Count; i++)
            {
                int id = ListProduct[i].IdProduct;
                PHIEUDANGKY sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaSanPham == id && t.TrangThai == 1).ToList().LastOrDefault();
                if(sheet != null)
                {
                    if(sheet.HanDangKy < DateTime.Now)
                    {
                        ListProduct[i].Status = "Hết hạn sản xuất";
                        SANPHAM product = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaSanPham == id).FirstOrDefault();
                        product.TinhTrang = "Hết hạn sản xuất";
                    }
                }
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch(Exception err)
                {
                    throw err;
                }
            }
        }
        //Kiểm tra tình trạng đăng kí của sản phẩm
        //public string CheckRegister(SANPHAM item)
        //{
        //    string statusregister = "Chưa đăng kí";
        //    List<PHIEUDANGKY> listPhieuDangKy = DataProvider.Ins.DB.PHIEUDANGKies.ToList();
        //    ; CHITIETPHIEUDANGKY detailregister;
        //    for (int i = 1; i <= listPhieuDangKy.Count; i++)
        //    {
        //        detailregister = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(x => x.MaSanPham == item.MaSanPham && x.MaPhieuDangKy == listPhieuDangKy[i].MaPhieuDangKy).LastOrDefault();
        //        if (detailregister != null)
        //        {
        //            IdRegister = i;
        //            break;
        //        }

        //    }
        //    if (IdRegister == 0)
        //    {
        //        statusregister = "Chưa đăng kí";
        //    }
        //    else if (DateTime.Now >= listPhieuDangKy[IdRegister].HanDangKy)
        //    {
        //        statusregister = "Chưa đăng kí";
        //    }
        //    else
        //    {
        //        statusregister = "Chưa đăng kí";
        //    }
        //    return statusregister;
        //}

        //Kiểm tra kết quả đăng kí
        //public string CheckResult()
        //{
        //    string result
        //}

        //Lấy tên đơn vị sản phẩm tính từ mã đơn vị tính
        public string GetNameUnit(int idunit)
        {
            string nameunit = "";
            List<DONVITINHSANPHAM> listdonvitinh = DataProvider.Ins.DB.DONVITINHSANPHAMs.ToList();
            foreach (var item in listdonvitinh)
            {
                if (item.MaDonViTinhSP == idunit)
                {
                    nameunit = item.TenDonViTinhSP;
                    break;
                }
            }
            return nameunit;
        }

        //lấy dữ liệu danh sách sản phẩm
        public void GetListProduct()
        {
            ListProduct.Clear();
            List<SANPHAM> listSanPham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaCoSo == Facility.MaCoSo).ToList();
            foreach (SANPHAM item in listSanPham)
            {
                Products product = new Products()
                {
                    IdProduct = item.MaSanPham,
                    IdFacility = item.MaCoSo,
                    NameProduct = item.TenSanPham,
                    NameUnit = GetNameUnit((int)item.MaDonViTinhSP),
                    Status = item.TinhTrang
                };
                ListProduct.Add(product);
            }
        }

        //Kiểm tra đơn vị tính đã có hay chưa
        public DONVITINHSANPHAM CheckUnit(string unit)
        {
            List<DONVITINHSANPHAM> listdonvitinh = DataProvider.Ins.DB.DONVITINHSANPHAMs.ToList();
            foreach (var item in listdonvitinh)
            {
                if (item.TenDonViTinhSP == unit)
                    return item;
            }
            return null;
        }

        //Thêm sản phẩm
        public void AddProduct()
        {
            if (NameFacility == null || Unit == null)
                MyMessageQueue.Enqueue("Vui lòng điền đầy đủ thông tin sản phẩm!");
            else
            {
                //Lấy mã đơn vị tính từ tên đơn vị
                int madonvitinh;
                if (CheckUnit(Unit) == null)
                {
                    DONVITINHSANPHAM newunit = new DONVITINHSANPHAM() { TenDonViTinhSP = Unit };
                    DataProvider.Ins.DB.DONVITINHSANPHAMs.Add(newunit);
                    DataProvider.Ins.DB.SaveChanges();
                    madonvitinh = newunit.MaDonViTinhSP;
                }
                else
                    madonvitinh = CheckUnit(Unit).MaDonViTinhSP;

                SANPHAM newproduct = new SANPHAM()
                {
                    MaCoSo = Facility.MaCoSo,
                    MaDonViTinhSP = madonvitinh,
                    TenSanPham = NameProduct,
                    TinhTrang = "Chưa sản xuất"
                };
                DataProvider.Ins.DB.SANPHAMs.Add(newproduct);
                DataProvider.Ins.DB.SaveChanges();
                GetListProduct();
                MyMessageQueue.Enqueue("Thêm sản phẩm thành công!");
                NameProduct = Unit = "";
                //SelectedProduct = null;
            }
        }

        public void SelectionChange()
        {
            if (SelectedProduct == null)
                return;
            else
            {
                NameProduct = SelectedProduct.NameProduct;
                Unit = SelectedProduct.NameUnit;
            }
        }

        //Sửa thông tin sản phầm
        public void EditProduct()
        {
            if (SelectedProduct == null)
                MyMessageQueue.Enqueue("Vui lòng chọn sản phẩm muốn sửa!");
            else
            {
                SANPHAM sanpham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == SelectedProduct.IdProduct).FirstOrDefault();

                //Lấy mã đơn vị tính từ tên đơn vị
                int madonvitinh;
                if (CheckUnit(Unit) == null)
                {
                    DONVITINHSANPHAM newunit = new DONVITINHSANPHAM() { TenDonViTinhSP = Unit };
                    DataProvider.Ins.DB.DONVITINHSANPHAMs.Add(newunit);
                    DataProvider.Ins.DB.SaveChanges();
                    madonvitinh = newunit.MaDonViTinhSP;
                }
                else
                    madonvitinh = CheckUnit(Unit).MaDonViTinhSP;

                sanpham.TenSanPham = NameProduct;
                sanpham.MaDonViTinhSP = madonvitinh;
                DataProvider.Ins.DB.SaveChanges();
                GetListProduct();
                MyMessageQueue.Enqueue("Sửa sản phẩm thành công!");
                NameProduct = Unit = "";
            }
        }

        //Sửa thông tin cơ sở sản xuất
        public void EditInfor()
        {
            Facility.TenCoSo = NameFacility;
            Facility.DiaChi = AddressFacility;
            DataProvider.Ins.DB.SaveChanges();
            MyMessageQueue.Enqueue("Sửa thông tin cơ sở thành công!");
        }

        //Hiện window tạo phiếu đăng kí
        public void OpenCreateRegistrationSheetWindow()
        {
            CreateSheet window = new CreateSheet(Facility);
            window.ShowDialog();
        }

        //Lưu thông tin và thoát
        public void SaveAndExit()
        {
            NameFacility = AddressFacility = "";
        }
    }
}
