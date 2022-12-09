using MaterialDesignThemes.Wpf;
using ProductQualityManager.Models;
using ProductQualityManager.Models.Database;
using ProductQualityManager.Views.LoginAndSignUp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
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
        private string _reUsername;
        private string _password;
        private string _name;
        private string _phone;
        
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string ReUsername { get => _reUsername; set { _reUsername = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        public SnackbarMessageQueue MyMessageQueue { get => myMessageQueue; set { myMessageQueue = value; OnPropertyChanged(nameof(MyMessageQueue)); } }
        private SnackbarMessageQueue myMessageQueue;
        public ICommand SignUpCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public SignUpViewModel()
        {
            Username = "";
            ReUsername = "";
            Password = "";
            Name = "";
            Phone = "";
            

            SignUpCommand = new RelayCommand<StackPanel>((p) => { return true; }, (p) => { SignUp(p); });
            //PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            //RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });

            MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            MyMessageQueue.DiscardDuplicates = true;
        }

        void SignUp(StackPanel infoSignUpForm)
        {
            if (Resources.Utils.Validator.IsValid(infoSignUpForm))
            {
                TAIKHOAN newAccount = new TAIKHOAN();
                newAccount.TenDangNhap = Username;
                CHUCOSO newChuCoSo = new CHUCOSO();
                newChuCoSo.HoTen = Name;
                newChuCoSo.DienTHoai = Phone;
                if (IsExist(newAccount) == true)
                {
                    //MessageBox.Show("Tài khoản đã tồn tại");
                    MyMessageQueue.Enqueue("Tài khoản đã tồn tại");
                }
                else if (Username != ReUsername)
                {
                    //MessageBox.Show("Email không trùng khớp");
                    MyMessageQueue.Enqueue("Email không trùng khớp");
                }
                else
                {
                    var random = new Random();
                    for (int i = 0; i < 8; i++)
                    {
                        Password = String.Concat(Password, random.Next(10).ToString());
                    }
                    string passEncode = MD5Hash(Base64Encode(Password));
                    newAccount.MatKhau = passEncode;
                    DataProvider.Ins.DB.CHUCOSOes.Add(newChuCoSo);

                    newAccount.MaChuCoSo = (int)newChuCoSo.MaChuCoSo;
                    DataProvider.Ins.DB.TAIKHOANs.Add(newAccount);

                    DataProvider.Ins.DB.SaveChanges();
                    //MessageBox.Show("Tạo tài khoản mới thành công");
                    MyMessageQueue.Enqueue("Tạo tài khoản mới thành công");
                    CreateTimeoutTestMessage(Username, Password);
                    Refresh();
                    //p.Close();
                }
            }
        }
        void Refresh()
        {
            Username = "";
            ReUsername = "";
            Name = "";
            Phone = "";
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
        public static void CreateTimeoutTestMessage(string to, string password)
        {
            string from = "quanlychatluongsanpham@gmail.com";
            string subject = "Thông báo tạo tài khoản mới thành công";
            string body = "Bạn đã tạo thành công tài khoản mới ! Mật khẩu đăng nhập vào ứng dụng của bạn là " + password;

            MailMessage message = new MailMessage(from, to, subject, body);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("quanlychatluongsanpham@gmail.com", "nyglcwryncmbiwhu"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch
            {
                throw;
            }
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
