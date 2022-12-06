using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.LoginAndSignUp;
using ProductQualityManager.Views.OwnerFacilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private string _wrongUser;
        private string _wrongUserColor;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(Username); } }

        //public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        //private SnackbarMessageQueue myMessageQueue;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(Password); } }
        public string WrongUser { get => _wrongUser; set { _wrongUser = value; OnPropertyChanged(); } }
        public string WrongUserColor { get => _wrongUserColor; set { _wrongUserColor = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand OpenSignUpWindowCommand { get; set; }
        public object DataProvier { get; private set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            Username = "";
            WrongUser = "*Sai tài khoản hoặc mật khẩu";
            WrongUserColor = "White";

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
            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenDangNhap == Username && x.MatKhau == passEncode).Count();
            if (accCount > 0)
            {
             

                //IsLogin = true;
                //p.Close();
                //Username = "";
                //Password = "";
                TAIKHOAN Account = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenDangNhap == Username && x.MatKhau == passEncode).FirstOrDefault();
                if (Username == "admin")
                {
                    IsLogin = true;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    p.Close();
                }
                else
                {
                    IsLogin = true;
                    ManageOwnerWindow manageOwnerWindow = new ManageOwnerWindow((int)Account.MaChuCoSo);
                    Username = "";
                    Password = "";
                    p.Close();
                    manageOwnerWindow.Show();
                    
                }
            }
            else
            {
                //MyMessageQueue.Enqueue("Sai tài khoản hoặc mật khẩu");
                //MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                WrongUserColor = "Red";
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
            //window.ShowDialog();
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
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
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
    }
}
