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
        private DashboardViewModel dashboardVM;
        private Button lastClickedBtn = null;

        public DashboardView()
        {
            InitializeComponent();

            dashboardVM = new DashboardViewModel();

            this.DataContext = new AccountManagementViewModel();
            ChangeBackgroundClickedBtn(accountBtn);
            titleMainSection.Text = "Tài khoản nhân viên";
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
            titleMainSection.Text = "Tài khoản nhân viên";
        }

        private void GeneralInfoManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new GeneralInfoManagementViewModel();
            ChangeBackgroundClickedBtn(generalInfoBtn);
            titleMainSection.Text = "Thông tin chung";
        }

        private void RoomManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomManagementViewModel();
            ChangeBackgroundClickedBtn(roomBtn);
            titleMainSection.Text = "Quản lý phòng";
        }

        private void RoomMap_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomMapViewModel();
            ChangeBackgroundClickedBtn(roomMapBtn);
            titleMainSection.Text = "Sơ đồ phòng";
        }

        private void ServiceManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServiceManagementViewModel();
            ChangeBackgroundClickedBtn(serviceBtn);
            titleMainSection.Text = "Quản lý dịch vụ";
        }

        private void ServicePanel_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ServicePanelViewModel();
            ChangeBackgroundClickedBtn(servicePanelBtn);
            titleMainSection.Text = "Bảng dịch vụ";
        }

        private void BookingManagement_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BookingManagementViewModel();
            ChangeBackgroundClickedBtn(bookingBtn);
            titleMainSection.Text = "Quản lý đặt phòng";
        }

        private void Report_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportViewModel();
            ChangeBackgroundClickedBtn(reportBtn);
            titleMainSection.Text = "Báo cáo thống kê";
        }
    }
}
