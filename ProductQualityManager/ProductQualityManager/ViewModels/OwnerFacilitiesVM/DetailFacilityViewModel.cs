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

namespace ProductQualityManager.ViewModels.OwnerFacilitiesVM
{
    public class DetailFacilityViewModel : BaseViewModel
    {
        private CHUCOSO _owner;
        private int _idowner;
        private string _namefacility;
        private string _addressfacility;
        private string _oldpass;
        private string _newpass1;
        private string _newpass2;
        private COSOSANXUAT _facility;
        private int _idregister;
        private ObservableCollection<Products> _listproduct;
        private SANPHAM _selectedproduct;
        private SnackbarMessageQueue myMessageQueue;

        public CHUCOSO Owner { get { return _owner; } set { _owner = value; OnPropertyChanged(nameof(Owner)); } }
        public int IdOwner { get { return _idowner; } set { _idowner = value; OnPropertyChanged(nameof(IdOwner)); } }
        public string NameFacility { get { return _namefacility; } set { _namefacility = value; OnPropertyChanged(nameof(NameFacility)); } }
        public string AddressFacility { get { return _addressfacility; } set { _addressfacility = value; OnPropertyChanged(nameof(AddressFacility)); } }
        public string OldPass { get { return _oldpass; } set { _oldpass = value; OnPropertyChanged(nameof(OldPass)); } }
        public string NewPass1 { get { return _newpass1; } set { _newpass1 = value; OnPropertyChanged(nameof(NewPass1)); } }
        public string NewPass2 { get { return _newpass2; } set { _newpass2 = value; OnPropertyChanged(nameof(NewPass2)); } }
        public COSOSANXUAT Facility { get { return _facility; } set { _facility = value; OnPropertyChanged(nameof(Facility)); } }
        public int IdRegister { get { return _idregister; } set { _idregister = value; OnPropertyChanged(nameof(IdRegister)); } }
        public ObservableCollection<Products> ListProduct { get { return _listproduct; } set { _listproduct = value; OnPropertyChanged(nameof(ListProduct)); } }
        public SANPHAM SelectedProduct { get { return _selectedproduct; } set { _selectedproduct = value; OnPropertyChanged(nameof(SelectedProduct)); } }
        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand EditInforFacility { get; set; }
        public ICommand Save { get; set; }

        //Khởi tạo 
        public DetailFacilityViewModel(COSOSANXUAT SelectedFaclility)
        {
            IdRegister = 0;
            Facility = SelectedFaclility;
            NameFacility = Facility.TenCoSo;
            AddressFacility = Facility.DiaChi;

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))
            {
                DiscardDuplicates = true
            };

            EditInforFacility = new RelayCommand<ManageOwnerViewModel>((p) => { return true; }, (p) => { EditInfor(); });
            Save = new RelayCommand<object>((p) => { return true; }, (p) => { SaveAndExit(); });

        }

        //Kiểm tra tình trạng đăng kí của sản phẩm
        public string CheckRegister(SANPHAM item)
        {
            string statusregister = "Chưa đăng kí";
            List<PHIEUDANGKY> listPhieuDangKy = DataProvider.Ins.DB.PHIEUDANGKies.ToList();
;           CHITIETPHIEUDANGKY detailregister;
            for (int i = 1; i <= listPhieuDangKy.Count; i++)
            {
                detailregister = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(x => x.MaSanPham == item.MaSanPham && x.MaPhieuDangKy == listPhieuDangKy[i].MaPhieuDangKy).LastOrDefault();
                if (detailregister != null)
                {
                    IdRegister = i;
                    break;
                }

            }
            if (IdRegister ==0)
            {
                statusregister = "Chưa đăng kí";
            }
            else if(DateTime.Now >= listPhieuDangKy[IdRegister].HanDangKy)
            {
                statusregister = "Chưa đăng kí";
            }
            else
            {
                statusregister = "Chưa đăng kí";
            }
            return statusregister;
        }

        //Kiểm tra kết quả đăng kí
        //public string CheckResult()
        //{
        //    string result
        //}

        //lấy dữ liệu danh sách sản phẩm
        public void GetListProduct()
        {
            List<SANPHAM> listSanPham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaCoSo == Facility.MaCoSo).ToList();
            foreach(var item in listSanPham)
            {
                Products newproduct = new Products()
                {
                    MaSanPham = item.MaSanPham,
                    MaCoSo = item.MaCoSo,
                    TenSanPham = item.TenSanPham,
                    MaDonViTinhSP = item.MaDonViTinhSP,
                    StatusRegister = CheckRegister(item),

                };
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

        //Hiển thị window đăng kí sản phẩm

        //Lưu thông tin và thoát
        public void SaveAndExit()
        {
            NameFacility = AddressFacility = "";
        }
    }
}
