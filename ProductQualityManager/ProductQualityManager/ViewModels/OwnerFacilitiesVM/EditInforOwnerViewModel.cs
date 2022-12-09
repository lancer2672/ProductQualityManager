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
using System.Security.Cryptography;

namespace ProductQualityManager.ViewModels.OwnerFacilitiesVM
{
    public class EditInforOwnerViewModel : BaseViewModel
    {
        private CHUCOSO _owner;
        private int _idowner;
        private string _name;
        private string _phonenumber;
        private TAIKHOAN _account;
        private string _oldpass;
        private string _newpass1;
        private string _newpass2;
        private SnackbarMessageQueue myMessageQueue;

        public CHUCOSO Owner { get { return _owner; } set { _owner = value; OnPropertyChanged(nameof(Owner)); } }
        public int IdOwner { get { return _idowner; } set { _idowner = value; OnPropertyChanged(nameof(IdOwner)); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string PhoneNumber { get { return _phonenumber; } set { _phonenumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public TAIKHOAN Account { get { return _account; } set { _account = value; OnPropertyChanged(nameof(Account)); } }
        public string OldPass { get { return _oldpass; } set { _oldpass = value; OnPropertyChanged(nameof(OldPass)); } }
        public string NewPass1 { get { return _newpass1; } set { _newpass1 = value; OnPropertyChanged(nameof(NewPass1)); } }
        public string NewPass2 { get { return _newpass2; } set { _newpass2 = value; OnPropertyChanged(nameof(NewPass2)); } }
        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }

        public ICommand OldPassCommand { get; set; }
        public ICommand NewPass1Command { get; set; }
        public ICommand NewPass2Command { get; set; }
        public ICommand ChangePass { get; set; }
        public ICommand SaveInfor { get; set; }

        //Khởi tạo 
        public EditInforOwnerViewModel(int idowner)
        {
            IdOwner = idowner;
            Owner = DataProvider.Ins.DB.CHUCOSOes.Where(x => x.MaChuCoSo == IdOwner).FirstOrDefault();
            Account = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaChuCoSo == IdOwner).FirstOrDefault();
            LoadDataOwner();

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))
            {
                DiscardDuplicates = true
            };

            OldPassCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { OldPass = p.Password; });
            NewPass1Command = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPass1 = p.Password; });
            NewPass2Command = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPass2 = p.Password; });
            ChangePass = new RelayCommand<object>((p) => { return true; }, (p) => { ChangePassWord(); });
            SaveInfor = new RelayCommand<Window>((p) => { return true; }, (p) => { SaveInforOwner(); });
        }

        //Hiện thông tin chủ cơ sở
        public void LoadDataOwner()
        {
            Name = Owner.HoTen;
            PhoneNumber = Owner.DienTHoai;
        }

        //Hash sang Base64
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        //Hash sang MD5
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        //ĐỔi mật khẩu
        public void ChangePassWord()
        {
            if (OldPass == null)
                MyMessageQueue.Enqueue("Nhập mật khẩu cũ.");
            else if (NewPass1 == null || NewPass2 == null)
                MyMessageQueue.Enqueue("Nhập mật khẩu mới.");
            else if (Account.MatKhau != MD5Hash(Base64Encode(OldPass)))
            {
                MyMessageQueue.Enqueue("Mật khẩu cũ không đúng.");
            }
            else
            {
                if (NewPass1 != NewPass2)
                    MyMessageQueue.Enqueue("Mật khẩu không trùng nhau.");
                else
                {
                    Account.MatKhau = MD5Hash(Base64Encode(NewPass1));
                    DataProvider.Ins.DB.SaveChanges();
                    MyMessageQueue.Enqueue("Thay đổi mật khẩu thành công.");
                }
            }
        }
        //Lưu thông tin chủ cơ sở
        public void SaveInforOwner()
        {
            Owner.HoTen = Name;
            Owner.DienTHoai = PhoneNumber;
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
