using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using booman.ViewModels;

namespace booman.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private Button lastClickedBtn = null;

        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = new AccountManagementViewModel();
            ChangeBackgroundClickedBtn(accountBtn);
        }

        private void ChangeBackgroundClickedBtn(Button clickedBtn)
        {
            if (clickedBtn != lastClickedBtn)
            {
                if (this.lastClickedBtn != null)
                {
                    this.lastClickedBtn.Style = Application.Current.Resources["menuButton"] as Style;
                }
                clickedBtn.Style = Application.Current.Resources["menuButtonActive"] as Style;
                this.lastClickedBtn = clickedBtn;
            }
        }

        private void AccountManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AccountManagementViewModel();
            ChangeBackgroundClickedBtn(accountBtn);
        }

        private void GeneralInfoManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new GeneralInfoManagementViewModel();
            ChangeBackgroundClickedBtn(generalInfoBtn);
        }

        private void RoomManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomManagementViewModel();
            ChangeBackgroundClickedBtn(roomBtn);
        }

        private void RoomMap_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomMapViewModel();
            ChangeBackgroundClickedBtn(roomMapBtn);
        }

        private void ServiceManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServiceManagementViewModel();
            ChangeBackgroundClickedBtn(serviceBtn);
        }

        private void ServicePanel_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServicePanelViewModel();
            ChangeBackgroundClickedBtn(servicePanelBtn);
        }

        private void BookingManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BookingManagementViewModel();
            ChangeBackgroundClickedBtn(bookingBtn);
        }

        private void Report_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportViewModel();
            ChangeBackgroundClickedBtn(reportBtn);
        }

        public void ShowOverlay(UserControl formUC)
        {
            OverlayGrid.Children.Clear();
            OverlayGrid.Children.Add(formUC);
            OverlayGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public void HideOverlay()
        {
            OverlayGrid.Children.Clear();
            OverlayGrid.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
