using ProductQualityManager.ViewModels.LoginVM;
using ProductQualityManager.Views.LoginAndSignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductQualityManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
            IsLoaded = true;
            if (p == null)
                return;
            p.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (loginWindow.DataContext == null)
                return;
            var LoginVM = loginWindow.DataContext as LoginViewModel;
            if (LoginVM.IsLogin)
                {
                    p.Show();
                }    
            else
                {
                    p.Close();
                }
            });
        }
    }
}
