using booman.Service;
using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity.Core.Metadata.Edm;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;


namespace booman.Views
{
    public class Room
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// Interaction logic for RoomManagementView.xaml
    /// </summary>
    public partial class RoomManagementView : UserControl
    {
        public RoomManagementView()
        {
            InitializeComponent();
            LoadRoom();
            Room room = new Room();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void LoadRoom()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService();
            DataTable listRoom = connection.GetTableData("room");
            RoomListView.ItemsSource = listRoom.DefaultView;
        }
        private void ShowAddRoom(object sender, RoutedEventArgs e)
        {
            AddRoomGrid.Visibility = Visibility.Visible;

        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            AddRoomGrid.Visibility = Visibility.Collapsed;
            UpdateRoomGrid.Visibility = Visibility.Collapsed;
        }

        private void AddRoom(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            connectionDB.InsertRoom(createRoomNumber.Text.ToString(), createRoomType.Text.ToString(), createBedType.Text.ToString(), Decimal.Parse(createPrice.Text));
            MessageBoxResult result = MessageBox.Show("Thêm phòng thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                LoadRoom();
                AddRoomGrid.Visibility = Visibility.Collapsed;
            }
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Room room = new Room();
            UpdateRoomGrid.Visibility = Visibility.Visible;
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                DataRowView dataRowView = listViewItem.Content as DataRowView;
                if (dataRowView != null)
                {
                    room.RoomNumber = dataRowView[0].ToString();
                    room.RoomType = dataRowView[1].ToString();
                    room.BedType = dataRowView[2].ToString();
                    room.Price = dataRowView[3].ToString();
                    room.Status = dataRowView[4].ToString();
                }
            }
            DataContext = room;

        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if(buttonUpdate.Content.ToString() == "Chỉnh sửa")
            {
                labelUpdateRoom.Content = "Chỉnh sửa thông tin phòng";
                buttonCancel.Visibility = Visibility.Visible;
                buttonDelete.Visibility = Visibility.Collapsed;
                textRoomType.IsReadOnly = false;
                textPrice.IsReadOnly = false;
                textStatus.IsReadOnly = false;
                buttonUpdate.Content = "Xác nhận";
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin phòng không?", "Xác nhận", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {

                    MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                    connectionDB.UpdateRoom(textRoomNumber.Content.ToString(), textRoomType.Text.ToString(), textBedType.Text.ToString(), Decimal.Parse(textPrice.Text), textStatus.Text.ToString());
                    MessageBoxResult result_2 = MessageBox.Show("Cập nhật thông tin phòng thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result_2 == MessageBoxResult.OK)
                    {
                        LoadRoom();
                        UpdateRoomGrid.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá phòng không?", "Xác nhận", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
               
                MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                connectionDB.DeleteRoom(textRoomNumber.Content.ToString());
                MessageBoxResult result_2 = MessageBox.Show("Xoá phòng thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result_2 == MessageBoxResult.OK)
                {
                    LoadRoom();
                    UpdateRoomGrid.Visibility = Visibility.Collapsed;
                }
            }

        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn huỷ không?", "Xác nhận", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                labelUpdateRoom.Content = "Thông tin phòng";
                UpdateRoomGrid.Visibility = Visibility.Collapsed;
                buttonCancel.Visibility = Visibility.Collapsed;
                textRoomType.IsReadOnly = true;
                textPrice.IsReadOnly = true;
                textStatus.IsReadOnly = true;
                buttonUpdate.Content = "Chỉnh sửa";
            }
        }
    }
}