using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.LoginAndSignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels.LoginVM
{
    public class SignUpViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _rePassword;
        private string _name;
        private string _phone;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string RePassword { get => _rePassword; set { _rePassword = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }
        //public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        //private SnackbarMessageQueue myMessageQueue;
        public ICommand SignUpCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public SignUpViewModel()
        {
            Username = "";
            Password = "";
            RePassword = "";
            Name = "";
            Phone = "";

            SignUpCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { SignUp(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });

        }

        void SignUp(Window p)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RePassword) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            }
            else
            {
                TAIKHOAN newAccount = new TAIKHOAN();
                newAccount.TenDangNhap = Username;
                newAccount.MatKhau = Password;
                CHUCOSO newChuCoSo = new CHUCOSO();
                newChuCoSo.HoTen = Name;
                newChuCoSo.DienTHoai = Phone;
                if (IsExist(newAccount) == true)
                {
                    MessageBox.Show("tài khoản đã tồn tại");
                }
                else if (Password != RePassword)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp");
                }
                else
                {
                    DataProvider.Ins.DB.CHUCOSOes.Add(newChuCoSo);

                    newAccount.MaChuCoSo = (int)newChuCoSo.MaChuCoSo;
                    DataProvider.Ins.DB.TAIKHOANs.Add(newAccount);

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Tạo tài khoản mới thành công");
                    p.Close();
                }
            }
        }
        public bool IsExist(TAIKHOAN NewAccount)
        {
            List<TAIKHOAN> AccountList = DataProvider.Ins.DB.TAIKHOANs.ToList();
            for (int i = 0; i < AccountList.Count; i++)
            {
                if (AccountList[i].TenDangNhap == NewAccount.TenDangNhap)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
