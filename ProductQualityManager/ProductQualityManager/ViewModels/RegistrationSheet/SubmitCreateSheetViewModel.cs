using MaterialDesignThemes.Wpf;
using Microsoft.Expression.Interactivity.Core;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.RegistrationSheet
{
    public class SubmitCreateSheetViewModel : BaseViewModel
    {
        private COSOSANXUAT _facility;
        private CreateRegistrationSheetViewModel _vm;

        public string OwnerName { get; set; } 
        public string OwnerPhoneNumber { get; set; }
        public string OwnerEmail { get; set; }
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductQuantity { get; set; }
        public DateTime Now { get; set; }
        public DateTime DueDate { get; set; }
        public ICommand CSubmitForm { get; set; }

        public ObservableCollection<CreateEnrollSheetModel> ListCriteria { get; set; }

        public SubmitCreateSheetViewModel(CreateRegistrationSheetViewModel vm, COSOSANXUAT facility, CHUCOSO owner)
        {
            OwnerName = "Họ và tên: " + owner.HoTen;
            OwnerPhoneNumber ="Số điện thoại: " + owner.DienTHoai;
            FacilityId = "Mã cơ sở: " + facility.MaCoSo.ToString();
            FacilityName = "Tên cở sở: " + facility.TenCoSo;
            FacilityAddress = "Địa chỉ: " + facility.DiaChi;
            ProductId = "Mã sản phẩm: " + vm.SelectedProduct.MaCoSo.ToString();
            ProductName = "Tên sản phẩm: " + vm.SelectedProduct.TenSanPham;
            ProductQuantity = "Số lượng đăng ký: " + vm.Quantity.ToString();
            Now = DateTime.Now;
            DueDate =vm.StartDay;
            ListCriteria = vm.DetailRegistrationSheetList;

            _facility = facility;
            _vm = vm;


            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });

        }
        public void SubmitForm(Window p)
        {
            if(checkIfAllowed() == false)
            {
                p.Close();  
                return;
            }
            try
            {
                PHIEUDANGKY newSheet = new PHIEUDANGKY();
                newSheet.TrangThai = 0;
                newSheet.MaSanPham = _vm.SelectedProduct.MaSanPham;
                newSheet.SoLuong = _vm.Quantity;
                newSheet.MaCoSo = _facility.MaCoSo;
                newSheet.NgayDangKy = DateTime.Now;
                newSheet.HanDangKy = _vm.StartDay;
                DataProvider.Ins.DB.PHIEUDANGKies.Add(newSheet);
                for (int i=0;i< ListCriteria.Count; i++)
                {
                    CHITIETPHIEUDANGKY item = new CHITIETPHIEUDANGKY();
                    item.MaPhieuDangKy = newSheet.MaPhieuDangKy;
                    item.GiaTriDangKy = ListCriteria[i].dec_GiaTri;
                    item.MaChiTieu = ListCriteria[i].MaChiTieu;
                    DataProvider.Ins.DB.CHITIETPHIEUDANGKies.Add(item);
                }
                SANPHAM product = DataProvider.Ins.DB.SANPHAMs.Where(t => t.MaSanPham == _vm.SelectedProduct.MaSanPham).FirstOrDefault();
                product.TinhTrang = "Đang sản xuất";
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
                _vm.MyMessageQueue.Enqueue("Tạo phiếu đăng ký thành công");

            }
            catch(Exception err)
            {
                throw err;
            }
        }
        private bool checkIfAllowed()
        {
            int id = _vm.SelectedProduct.MaSanPham;
            if(_vm.SelectedProduct.TinhTrang == "Cấm")
            {
                _vm.MyMessageQueue.Enqueue("Sản phẩm bị cấm không thể tạo phiếu đăng ký");
                return false;
            }
            if (_vm.SelectedProduct.TinhTrang == "Ngưng sản xuất")
            {
                _vm.MyMessageQueue.Enqueue("Sản phẩm hiện tại đang bị tạm ngưng sản xuất ");
                return false;
            }
            //kiểm tra xem có phiếu nào của sản phẩm này đang chờ duyệt hay không
            PHIEUDANGKY checkDueDay = DataProvider.Ins.DB.PHIEUDANGKies.Where(t => t.MaSanPham == id && t.TrangThai != -1).ToList().LastOrDefault();          
            if(checkDueDay == null)
            {
                return true;
            }
            if(checkDueDay.HanDangKy > DateTime.Now)
            {
                _vm.MyMessageQueue.Enqueue("Lỗi! Phiếu đăng ký của sản phẩm này vẫn còn hạn hoặc chưa được duyệt");
                return false;
            }
            return true;
        }
    }
}
