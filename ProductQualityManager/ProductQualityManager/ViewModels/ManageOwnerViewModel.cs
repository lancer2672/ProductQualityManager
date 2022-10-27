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

namespace ProductQualityManager.ViewModels
{
    public class ManageOwnerViewModel : BaseViewModel
    {
        private string _name;
        private string _phonenumber;
        private int _numberfacilities;
        private ObservableCollection<COSOSANXUAT> _listFacilities;
        private COSOSANXUAT _selectFacilities;
        private SnackbarMessageQueue myMessageQueue;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(_name)); } }
        public string PhoneNumber { get { return _phonenumber; } set { _phonenumber = value; OnPropertyChanged(nameof(_phonenumber)); } }
        public int NumberFacilities { get { return _numberfacilities; } set { _numberfacilities = value; OnPropertyChanged(nameof(_numberfacilities)); } }
        public ObservableCollection<COSOSANXUAT> ListFacilities { get { return _listFacilities; } set { _listFacilities = value; OnPropertyChanged(nameof(_listFacilities)); } }
        public COSOSANXUAT SelectFacilities { get { return _selectFacilities; } set { _selectFacilities = value; OnPropertyChanged(nameof(_selectFacilities)); } }
        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public int IdOwner;

        public ICommand EditInfor { get; set; }
        public ICommand AddProduct { get; set; }


        //Khởi tạo
        public ManageOwnerViewModel()
        {
            //Chọn chủ cơ sở đầu tiên
            IdOwner = 1;
            ListFacilities = new ObservableCollection<COSOSANXUAT>();
            LoadDataOwner();
            LoadListFacilities();
            NumberFacilities = CountFacilities();

            EditInfor = new RelayCommand<ManageOwnerViewModel>((p) => { return true; }, (p) => { OpenEditInforOwnerWindow(p); });
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
            foreach(COSOSANXUAT item in listfacilities)
            {
                if(item.MaChuCoSo == IdOwner)
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
        }

        //Hiện window sửa thông tin cá nhân của chủ cơ sở
        public void OpenEditInforOwnerWindow(ManageOwnerViewModel manageownerVM)
        {
            EditInforOwnerWindow EditInforWindow = new EditInforOwnerWindow(manageownerVM);
            EditInforWindow.Show();
        }
    }
}
