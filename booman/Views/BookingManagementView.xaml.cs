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
            SQLiteDatabaseService dbService = new SQLiteDatabaseService();
            DataTable bookingDataTable = dbService.GetDataTable("Booking");
            foreach (DataRow row in bookingDataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Debug.WriteLine(item);
                }
            }
            BookingDataGrid.ItemsSource = bookingDataTable.DefaultView;
        }
    }
}
