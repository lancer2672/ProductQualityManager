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
        CreateRegistrationSheetViewModel _vm;
        public ObservableCollection<CreateEnrollSheetModel> List { get; set; }

        public ICommand CSubmitForm { get; set; }


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
            List = vm.DetailRegistrationSheetList;

            _vm = vm;


            CSubmitForm = new RelayCommand<Window>((p) => { return true; }, (p) => { SubmitForm(p); });

        }
        public void SubmitForm(Window p)
        {

        }
    }
}
