using System;
using System.Data;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using booman.Helpers;
using booman.Services;

namespace booman.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Data fields
        private string username;
        private string password;
        private DataTable accountDT;

        public ICommand LoginCommand { get; private set; }

        // Constructors
        public LoginViewModel()
        {
            GetAccountData();
            LoginCommand = new RelayCommand(LoginCheck);
        }

        // Properties
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        // Methods
        private void GetAccountData()
        {
            MySQLDatabaseService dbService = new MySQLDatabaseService();
            this.accountDT = dbService.GetTableData("account");
        }

        private void LoginCheck()
        {
            if (this.accountDT != null)
            {
                bool isAuthenticated = false;

                foreach (DataRow row in this.accountDT.Rows)
                {
                    string _phone = row["phone"].ToString();
                    string _email = row["email"].ToString();
                    string _password = row["password"].ToString();

                    if ((Username == _phone) || (Username == _email) && Password == _password)
                    {
                        isAuthenticated = true;
                        break;
                    }
                }

                if (isAuthenticated)
                {
                    var mainWindow = Application.Current.MainWindow;

                    if (Password == "booman") mainWindow.DataContext = new SetPasswordViewModel();
                    else mainWindow.DataContext = new DashboardViewModel();
                }
                else
                {
                    MessageBox.Show("Email/Số điện thoại hoặc mật khẩu không đúng!");
                }
            }
            else
            {
                MessageBox.Show("Lấy dữ liệu tài khoản thất bại!");
            }
        }
    }

}
