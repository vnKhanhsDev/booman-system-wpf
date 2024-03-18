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


namespace booman.Views
{
    public class Room
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
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
          
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void LoadRoom()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService("localhost", "booman_db", "root", "khanh1907");
            DataTable listRoom = connection.GetTableData("room");
            DataRow row = listRoom.Rows[0];
            RoomListView.ItemsSource = listRoom.DefaultView;
            Room room = new Room();
            room.RoomNumber = row[0].ToString();
            room.RoomType = row[1].ToString();
            room.Price = row[2].ToString();
            room.Status = row[3].ToString();
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
            AddRoomGrid.Visibility = Visibility.Collapsed;
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
                    room.Price = dataRowView[2].ToString();
                    room.Status = dataRowView[3].ToString();
                }
            }

        }

    }
}