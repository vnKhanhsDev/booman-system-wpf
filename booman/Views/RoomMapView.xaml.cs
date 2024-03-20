using booman.Models;
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

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for RoomMapView.xaml
    /// </summary>
    public partial class RoomMapView : UserControl
    {
        public RoomMapView()
        {
            InitializeComponent();
            LoadRoom();
        }
        public void LoadRoom()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService();
            DataTable listRoom = connection.GetTableData("room");
            ItemRoom.ItemsSource = listRoom.DefaultView;
        }

        private void Filter_button(object sender, RoutedEventArgs e)
        {
            string valueStatus;
            if (emptyRadioButton.IsChecked == true)
            {
                valueStatus = "empty";
            }else if(bookingRadioButton.IsChecked == true){
                valueStatus = "booked";
            }else if(usingRadioButton.IsChecked == true)
            {
                valueStatus = "using";
            }else
            {
                valueStatus = null;
            }

            string valueQuality;
            if (standardRadioButton.IsChecked == true)
            {
                valueQuality = "Standard";
            }else if(vipRadioButton.IsChecked == true)
            {
                valueQuality = "VIP";
            }
            else
            {
                valueQuality = null;
            }

            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            DataTable filter = new DataTable();
            if(valueStatus != null && valueQuality != null)
            {
                filter = connectionDB.FilterRoom(valueQuality, valueStatus);
                ItemRoom.ItemsSource = filter.DefaultView;
            }
        }

        private void Search_button(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            DataTable search = new DataTable();
            if(SearchText.Text != null)
            {
                search = connectionDB.SearchRoom(SearchText.Text.ToString());
                ItemRoom.ItemsSource = search.DefaultView;
            }
            emptyRadioButton.IsChecked = false;
            bookingRadioButton.IsChecked = false;
            usingRadioButton.IsChecked = false;
            standardRadioButton.IsChecked = false;
            vipRadioButton.IsChecked = false;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            string infoRoomNumber = button.ToolTip.ToString();
            labelRoomNum.DataContext = infoRoomNumber;
            Label labelStatus = button.FindName("roomStatus") as Label;
            string infoRoomStatus = labelStatus.Content.ToString();
            labelRoomStatus.DataContext = infoRoomStatus;
            InfoRoomMap info = new InfoRoomMap();
            List<RoomService> listService = new List<RoomService>();
            if (infoRoomStatus != "empty")
            {
                MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                info = connectionDB.InfoRoomInRoomMap(infoRoomNumber);
                listService = connectionDB.GetRoomSevice(infoRoomNumber);
            }
            if(info.Act_checkin_time == null)
            {
                labelCheckinTime.Visibility = Visibility.Collapsed;
            }

            listRoomService.ItemsSource = listService;

            labelName.DataContext = info.Name;
            labelPhone.DataContext = info.Phone;
            labelEmail.DataContext = info.Email;
            labelCheckinDate.DataContext = info.Checkin_date;
            labelStayDuration.DataContext = info.Stay_duration;
            labelCheckinTime.DataContext = info.Act_checkin_time;
            ViewInfoRoom.Visibility = Visibility.Visible;
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            ViewInfoRoom.Visibility = Visibility.Collapsed;
        }
    }
}
