using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using booman.Services;
using booman.ViewModels;
using booman.Views.Popups;

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
            this.DataContext = new BookingManagementViewModel();
        }

        private void ShowBookingDetail_Clicked(object sender, EventArgs e)
        {
            BookingDetailView bdv = new BookingDetailView();
            // Tìm DashboardView trong cây phân cấp
            DashboardView dashboardView = FindParent<DashboardView>(this);
            if (dashboardView != null)
            {
                // Gọi phương thức trong DashboardView để hiển thị overlay
                dashboardView.ShowOverlay(bdv);
            }
        }

        // Phương thức để tìm kiếm một control cha cụ thể trong cây phân cấp
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }
    }
}
