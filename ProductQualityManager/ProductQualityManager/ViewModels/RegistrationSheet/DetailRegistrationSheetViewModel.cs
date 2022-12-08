using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.TestSheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.TestingSheet
{
    public class DetailRegistrationSheetViewModel : BaseViewModel
    {
        enum Status{
             DUYET,
             TUCHOI,
        }
        private RegistrationSheetViewModel _vm;
        private ObservableCollection<DetailRegistrationSheetModel> _detailRegistrationSheet;

        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerEmail { get; set; }
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Result { get; set; }
        public string ResultColor { get; set; }
        public ObservableCollection<DetailRegistrationSheetModel> DetailRegistrationSheet { get { return _detailRegistrationSheet; } set { _detailRegistrationSheet = value; OnPropertyChanged(nameof(DetailRegistrationSheet)); } }


        public ICommand CSubmitForm { get; set; }

        public DetailRegistrationSheetViewModel(RegistrationSheetViewModel vm)
        {
            _vm = vm;
            Result = "CHẤP THUẬN";
            ResultColor = "Green";
            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });
            DetailRegistrationSheet = new ObservableCollection<DetailRegistrationSheetModel>();
            LoadDataForm();
        }
        public void LoadDataForm()
        {
            COSOSANXUAT facility = DataProvider.Ins.DB.COSOSANXUATs.
                Where(t => t.MaCoSo == _vm.SelectedSheet.MaCoSo).FirstOrDefault();

            CHUCOSO owner = DataProvider.Ins.DB.CHUCOSOes.Where(t => t.MaChuCoSo == facility.MaChuCoSo).FirstOrDefault();
            SANPHAM product = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaSanPham == _vm.SelectedSheet.MaSanPham).FirstOrDefault();
            OwnerName = owner.HoTen;
            OwnerPhoneNumber = owner.DienTHoai;
            FacilityId = facility.MaCoSo;
            FacilityAddress = facility.DiaChi;
            FacilityName = facility.TenCoSo;
            ProductId = product.MaSanPham;
            ProductName = product.TenSanPham;
            ProductQuantity = _vm.SelectedSheet.SoLuong;
            InitDate = _vm.SelectedSheet.NgayDangKy;
            DueDate = _vm.SelectedSheet.ThoiHanDangKy;

            List<CHITIETPHIEUDANGKY> detailSheets = DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Where(t => t.MaPhieuDangKy == _vm.SelectedSheet.MaPhieuDangKy).ToList();
            for(int i=0;i< detailSheets.Count; i++)
            {
                int criteriaId = (int)detailSheets[i].MaChiTieu;
                CHITIEUSANPHAM criteria = DataProvider.Ins.DB.CHITIEUSANPHAMs.Where(t => t.MaChiTieu == criteriaId).FirstOrDefault();
                DONVITINH unit = DataProvider.Ins.DB.DONVITINHs.Where(t => t.MaDonViTinh == criteria.MaDonViTinh).FirstOrDefault();
                DetailRegistrationSheetModel item = new DetailRegistrationSheetModel(detailSheets[i], unit, criteria);
                if(detailSheets[i].GiaTriDangKy <= criteria.GiaTriTieuChuan)
                {
                    item.KetQuaSoSanh = "Đạt tiêu chuẩn";
                    item.Color_KetQuaSoSanh = "Green";
                }
                else
                {
                    Result = "TỪ CHỐI";
                    ResultColor = "Red";
                    item.KetQuaSoSanh = "Không đạt tiêu chuẩn";
                    item.Color_KetQuaSoSanh = "Red";
                }
                DetailRegistrationSheet.Add(item);
            }

        }

        public void SubmitForm(Window p)
        {
            PHIEUDANGKY sheet = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaPhieuDangKy == _vm.SelectedSheet.MaPhieuDangKy).FirstOrDefault();
            switch (Result)
            {
                case "CHẤP THUẬN":
                    {
                        sheet.TrangThai = 1;
                        break;
                    }
                default:
                    {
                        sheet.TrangThai = -1;
                        break;
                    }
            }
            try
            {
                DataProvider.Ins.DB.SaveChanges();
                _vm.LoadDataSheetList();
                p.Close();
            }
            catch (Exception err) {
                throw err;
            }
        }
    }
}
