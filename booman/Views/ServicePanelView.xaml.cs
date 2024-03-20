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
    /// Interaction logic for ServicePanelView.xaml
    /// </summary>
    public partial class ServicePanelView : UserControl
    {
        public ServicePanelView()
        {
            InitializeComponent();
            LoadService();
        }
        public void LoadService()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService();
            DataTable listRoom = connection.GetTableData("service");
            ItemService.ItemsSource = listRoom.DefaultView;
        }

        private void Search_button(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            DataTable search = new DataTable();
            if (SearchText.Text != null)
            {
                search = connectionDB.SearchService(SearchText.Text.ToString());
                ItemService.ItemsSource = search.DefaultView;
            }
        }
    }
}
