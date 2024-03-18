using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using booman.Helpers;

namespace booman.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginCheck);
        }

        private void LoginCheck()
        {
            if ((Username == "admin@gmail.com" || Username == "0123456789") && Password == "admin")
            {
                var mainWindow = Application.Current.MainWindow;
                mainWindow.DataContext = new DashboardViewModel();
            }
            else
            {
                MessageBox.Show("Email (Số điện thoại) hoặc mật khẩu không đúng!");
            }
        }
    }

}
