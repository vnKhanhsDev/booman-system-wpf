using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using booman.Services;

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for BookingManagementView.xaml
    /// </summary>
    public partial class BookingManagementView : UserControl
    {
        public BookingManagementView()
        {
            InitializeComponent();
            LoadRoom();
        }

        private void LoadRoom()
        {
            MySQLDatabaseService dbService = new MySQLDatabaseService();
            DataTable bookings = dbService.GetTableData("booking");
            BookingDataGrid.ItemsSource = bookings.DefaultView;
        }
    }
}
