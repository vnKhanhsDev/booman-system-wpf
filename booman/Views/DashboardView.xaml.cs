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
using booman.ViewModels;

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = new AccountManagementViewModel();
        }

        private void AccountManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AccountManagementViewModel();
        }

        private void GeneralInfoManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new GeneralInfoManagementViewModel();
        }

        private void RoomManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomManagementViewModel();
        }

        private void RoomMap_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomMapViewModel();
        }

        private void ServiceManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServiceManagementViewModel();
        }

        private void ServicePanel_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServicePanelViewModel();
        }

        private void BookingManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BookingManagementViewModel();
        }
    }
}
