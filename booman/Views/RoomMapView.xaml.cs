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
    }
}
