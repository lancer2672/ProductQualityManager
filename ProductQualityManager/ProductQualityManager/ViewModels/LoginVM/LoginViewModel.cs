using ProductQualityManager.Models;
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
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _username;
        private string _password;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        //public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        //private SnackbarMessageQueue myMessageQueue;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand OpenSignUpWindowCommand { get; set; }
        public object DataProvier { get; private set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            Username = "";


            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            OpenSignUpWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenSignUpWindow(p); });

            //MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            //MyMessageQueue.DiscardDuplicates = true;
        }
        void Login(Window p)
        {
            IsLogin = false;
            if (p == null)
                return;
            var accCount = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenDangNhap == Username && x.MatKhau == Password).Count();
            if (accCount > 0)
            {
                IsLogin = true;
                p.Close();             
                
                //Username = "";
                //Password = "";
            }
            else
            {
                //MyMessageQueue.Enqueue("Sai tài khoản hoặc mật khẩu");
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");

            }
        }

        void Close()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Close();
        }
        void OpenSignUpWindow(object p)
        {
            var window = new SignUpWindow();
            window.ShowDialog();
        }
    }
}
