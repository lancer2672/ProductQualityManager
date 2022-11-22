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
        private ObservableCollection<SANPHAM> _listproduct;
        private SANPHAM _selectedproduct;

        public CHUCOSO Owner { get { return _owner; } set { _owner = value; OnPropertyChanged(nameof(Owner)); } }
        public int IdOwner { get { return _idowner; } set { _idowner = value; OnPropertyChanged(nameof(IdOwner)); } }
        public string NameFacility { get { return _namefacility; } set { _namefacility = value; OnPropertyChanged(nameof(NameFacility)); } }
        public string AddressFacility { get { return _addressfacility; } set { _addressfacility = value; OnPropertyChanged(nameof(AddressFacility)); } }
        public string OldPass { get { return _oldpass; } set { _oldpass = value; OnPropertyChanged(nameof(OldPass)); } }
        public string NewPass1 { get { return _newpass1; } set { _newpass1 = value; OnPropertyChanged(nameof(NewPass1)); } }
        public string NewPass2 { get { return _newpass2; } set { _newpass2 = value; OnPropertyChanged(nameof(NewPass2)); } }
        public COSOSANXUAT Facility { get { return _facility; } set { _facility = value; OnPropertyChanged(nameof(Facility)); } }
        public ObservableCollection<SANPHAM> ListProduct { get { return _listproduct; } set { _listproduct = value; OnPropertyChanged(nameof(ListProduct)); } }
        public SANPHAM SelectedProduct { get { return _selectedproduct; } set { _selectedproduct = value; OnPropertyChanged(nameof(SelectedProduct)); } }

        //Khởi tạo 
        public DetailFacilityViewModel(COSOSANXUAT SelectedFaclility)
        {
            Facility = SelectedFaclility;
            NameFacility = SelectedFaclility.TenCoSo;
            AddressFacility = SelectedFaclility.DiaChi;

        }

        //lấy dữ liệu danh sách sản phẩm
        public void GetListProduct()
        {
            List<SANPHAM> listProduct = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaCoSo == Facility.MaCoSo).ToList();
            foreach(var item in listProduct)
            {
                ListProduct.Add(item);
            }
        }

    }
}
