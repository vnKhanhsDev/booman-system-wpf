using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for SetPasswordView.xaml
    /// </summary>
    public partial class SetPasswordView : UserControl
    {
        public SetPasswordView()
        {
            InitializeComponent();
        }
        private void CreatePassword_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword == confirmPassword)
            {

                MessageBox.Show("Mật khẩu đã được tạo thành công!", "Thành Công", MessageBoxButton.OK, MessageBoxImage.Information);

                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp. Vui lòng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
            }
        }
    }
}
