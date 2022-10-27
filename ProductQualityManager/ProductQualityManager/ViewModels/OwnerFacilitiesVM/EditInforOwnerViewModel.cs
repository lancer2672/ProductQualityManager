using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.OwnerFacilitiesVM
{
    public class EditInforOwnerViewModel: BaseViewModel
    {
        private int _idowner;
        private string _name;
        private string _phonenumber;

        public int IdOwner { get { return _idowner; } set { _idowner = value; OnPropertyChanged(nameof(IdOwner)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string PhoneNumber { get { return _phonenumber; } set { _phonenumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }

        public ICommand ChangePass { get; set; }
        public ICommand SaveInfor { get; set; }

        //Khởi tạo 
        public EditInforOwnerViewModel(int idowner)
        {
            IdOwner = idowner;
            LoadDataOwner();
            SaveInfor = new RelayCommand<EditInforOwnerViewModel>((p) => { return true; }, (p) => { SaveInforOwner(p); });
        }

        //Hiện thông tin chủ cơ sở
        public void LoadDataOwner()
        {
            CHUCOSO Owner = DataProvider.Ins.DB.CHUCOSOes.Where(x => x.MaChuCoSo == IdOwner).FirstOrDefault();
            Name = Owner.HoTen;
            PhoneNumber = Owner.DienTHoai;
        }

        //Lưu thông tin chủ cơ sở
        public void SaveInforOwner(EditInforOwnerViewModel p)
        {
            CHUCOSO Owner = DataProvider.Ins.DB.CHUCOSOes.Where(x => x.MaChuCoSo == IdOwner).FirstOrDefault();
            Owner.HoTen = Name;
            Owner.DienTHoai = PhoneNumber;
            DataProvider.Ins.DB.SaveChanges();
            
        }
    }
}
