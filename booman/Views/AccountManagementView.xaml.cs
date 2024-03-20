using booman.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for AccountManagementView.xaml
    /// </summary>
    public partial class AccountManagementView : UserControl
    {
        public AccountManagementView()
        {
            InitializeComponent();
            LoadAccount();
        }
        public void LoadAccount()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService();
            DataTable listAccount = connection.GetTableData("account");
            dataGridView.ItemsSource = listAccount.DefaultView;
        }

        private void ClearInputFields()
        {
            textBox_phone.Text = string.Empty;
            textBox_email.Text = string.Empty;
            textBox_fullName.Text = string.Empty;
            textBox_role.Text = string.Empty;
            textBox_pass.Text = string.Empty;
        }
        
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {            
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            connectionDB.InsertAccount(textBox_phone.Text.ToString(), textBox_email.Text.ToString(), textBox_pass.Text.ToString(), textBox_fullName.Text.ToString(), textBox_role.Text.ToString());
            MessageBoxResult result = MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                LoadAccount();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "Không thêm vào được", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGridView.SelectedItem;

                // Lấy thông tin từ dòng được chọn
                string phone = row["phone"].ToString();
                string email = row["email"].ToString();
                string password = row["password"].ToString();
                string fullName = row["full_name"].ToString();
                string role = row["role"].ToString();

                // Hiển thị thông tin trong các ô nhập liệu
                textBox_phone.Text = phone;
                textBox_email.Text = email;
                textBox_pass.Text = password;
                textBox_fullName.Text = fullName;
                textBox_role.Text = role;
            }
        }

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Xác nhận", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                connectionDB.UpdateAccount(textBox_phone.Text.ToString(), textBox_email.Text.ToString(), textBox_pass.Text.ToString(), textBox_fullName.Text.ToString(), textBox_role.Text.ToString());
                MessageBoxResult result_2 = MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result_2 == MessageBoxResult.OK)
                {
                    LoadAccount();
                    ClearInputFields();

                }
            }
        }
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá không?", "Xác nhận", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                connectionDB.DeleteAccount(textBox_phone.Text.ToString());
                MessageBoxResult result_2 = MessageBox.Show("Xoá thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result_2 == MessageBoxResult.OK)
                {
                    LoadAccount();
                    ClearInputFields();

                }
                else
                {
                    MessageBox.Show("Lỗi: " + "Không xóa được", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
