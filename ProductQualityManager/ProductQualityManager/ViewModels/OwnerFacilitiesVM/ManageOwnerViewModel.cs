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
using ProductQualityManager.Views.OwnerFacilities;
using ProductQualityManager.Views.LoginAndSignUp;
using System.Windows.Controls;
using System.Windows;
using ProductQualityManager.Views.TestingSheet;

namespace ProductQualityManager.ViewModels.OwnerFacilitiesVM
{
    public class ManageOwnerViewModel : BaseViewModel
    {
        private string _name;
        private string _phonenumber;
        private int _numberfacilities;
        private string _namefacility;
        private string _addressfacility;
        private ObservableCollection<COSOSANXUAT> _listFacilities;
        private COSOSANXUAT _selectFacilities;
        private SnackbarMessageQueue myMessageQueue;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string PhoneNumber { get { return _phonenumber; } set { _phonenumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public int NumberFacilities { get { return _numberfacilities; } set { _numberfacilities = value; OnPropertyChanged(nameof(NumberFacilities)); } }
        public string NameFacility { get { return _namefacility; } set { _namefacility = value; OnPropertyChanged(nameof(NameFacility)); } }
        public string AddressFacility { get { return _addressfacility; } set { _addressfacility = value; OnPropertyChanged(nameof(AddressFacility)); } }
        public ObservableCollection<COSOSANXUAT> ListFacilities { get { return _listFacilities; } set { _listFacilities = value; OnPropertyChanged(nameof(ListFacilities)); } }
        public COSOSANXUAT SelectFacilities { get { return _selectFacilities; } set { _selectFacilities = value; OnPropertyChanged(nameof(SelectFacilities)); } }
        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public int IdOwner;

        public ICommand EditInfor { get; set; }
        public ICommand AddFacility { get; set; }
        public ICommand DetailFacility { get; set; }
        public ICommand Register { get; set; }


        //Khởi tạo
        public ManageOwnerViewModel(int idowner)
        {
            //Chọn chủ cơ sở đầu tiên
            IdOwner = idowner;
            ListFacilities = new ObservableCollection<COSOSANXUAT>();
            LoadDataOwner();
            LoadListFacilities();
            NumberFacilities = CountFacilities();

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))
            {
                DiscardDuplicates = true
            };

            EditInfor = new RelayCommand<ManageOwnerViewModel>((p) => { return true; }, (p) => { OpenEditInforOwnerWindow(); });
            AddFacility = new RelayCommand<object>((p) => { return true; }, (p) => { AddInforFacility(); });
            DetailFacility = new RelayCommand<object>((p) => { return true; }, (p) => { OpenDetailFacilityWindow(p); });
            Register = new RelayCommand<object>((p) => { return true; }, (p) => { OpenCreateRegistrationSheetWindow(p); });
        }

        //Load thông tin chủ cơ sở sản xuất
        public void LoadDataOwner()
        {
            CHUCOSO Owner = DataProvider.Ins.DB.CHUCOSOes.Where(x => x.MaChuCoSo == IdOwner).FirstOrDefault();
            Name = Owner.HoTen;
            PhoneNumber = Owner.DienTHoai;
        }

        //Đếm số cơ sở sản xuất của chủ cơ sở
        public int CountFacilities()
        {
            int number = 0;
            List<COSOSANXUAT> listfacilities = DataProvider.Ins.DB.COSOSANXUATs.ToList();
            foreach (COSOSANXUAT item in listfacilities)
            {
                if (item.MaChuCoSo == IdOwner)
                    number++;
            }
            return number;
        }

        //Hiển thị danh sách cơ sở sản xuất
        public void LoadListFacilities()
        {
            ListFacilities.Clear();
            List<COSOSANXUAT> listfacilities = DataProvider.Ins.DB.COSOSANXUATs.ToList();
            foreach (COSOSANXUAT item in listfacilities)
            {
                if (item.MaChuCoSo == IdOwner)
                {
                    ListFacilities.Add(item);
                }
            }
            NumberFacilities = CountFacilities();
        }

        //Hiện window sửa thông tin cá nhân của chủ cơ sở
        public void OpenEditInforOwnerWindow()
        {
            EditInforOwnerWindow EditInforWindow = new EditInforOwnerWindow(IdOwner);
            EditInforWindow.ShowDialog();
            LoadDataOwner();
            MyMessageQueue.Enqueue("Lưu thông tin thành công.");
        }

        //Kiểm tra địa chỉ (true: đã có, false: chưa có)
        public bool CheckAddress()
        {
            List<COSOSANXUAT> listfacilities = DataProvider.Ins.DB.COSOSANXUATs.ToList();
            foreach (var item in listfacilities)
            {
                if (AddressFacility == item.DiaChi)
                    return true;
            }
            return false;
        }

        //Thêm cơ sở sản xuất
        public void AddInforFacility()
        {
            if (NameFacility == null || AddressFacility == null)
                MyMessageQueue.Enqueue("Vui lòng điền đầy đủ thông tin!");
            else
            {
                if (CheckAddress())
                {
                    MyMessageQueue.Enqueue("Địa chỉ cơ sở đã có.");
                }
                else
                {
                    COSOSANXUAT NewFacility = new COSOSANXUAT()
                    {
                        TenCoSo = NameFacility,
                        MaChuCoSo = IdOwner,
                        DiaChi = AddressFacility,
                        TinhTrang = "Còn hoạt động",
                    };
                    DataProvider.Ins.DB.COSOSANXUATs.Add(NewFacility);
                    DataProvider.Ins.DB.SaveChanges();
                    MyMessageQueue.Enqueue("Thêm cơ sở sản xuất thành công.");
                    NameFacility = AddressFacility = " ";
                    LoadListFacilities();
                }
            }
        }

        //
        public void ClearData()
        {
            Name = PhoneNumber = "";
            NumberFacilities = 0;
            ListFacilities.Clear();
        }

        //Hiện window thông tin chi tiết của cơ sở
        public void OpenDetailFacilityWindow(object p)
        {
            SelectFacilities = p as COSOSANXUAT;
            DetailFacilityWindow DetailFacilityWindow = new DetailFacilityWindow(SelectFacilities);
            DetailFacilityWindow.ShowDialog();
            LoadListFacilities();
            MyMessageQueue.Enqueue("Lưu thông tin thành công.");
        }

        //Hiện window tạo phiếu đăng kí
        public void OpenCreateRegistrationSheetWindow(object p)
        {
            SelectFacilities = p as COSOSANXUAT;
            CreateSheet window = new CreateSheet(SelectFacilities);
            window.ShowDialog();
        }
    }
}
