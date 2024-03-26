using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using booman.Models;
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
        public BookingManagementViewModel bookingManagementVM { get; set; }

        public BookingManagementView()
        {
            InitializeComponent();
            bookingManagementVM = new BookingManagementViewModel();
            this.DataContext = bookingManagementVM;
        }

        private void BookingDetail_Clicked(object sender, RoutedEventArgs e)
        {
            // Get selected booking from datagrid
            Booking selectedBooking = (sender as Button).DataContext as Booking;

            // Find customer
            Customer selectedCustomer = bookingManagementVM.Customers.FirstOrDefault(x => x.ID == selectedBooking.CustomerID);

            // Binding data
            if (selectedCustomer != null)
            {
                txtCustomerID.Text = selectedCustomer.ID;
                txtCustomerName.Text = selectedCustomer.Name;
                txtCustomerPhone.Text = selectedCustomer.Phone;
                txtCustomerEmail.Text = selectedCustomer.Email;
                txtCustomerAddress.Text = selectedCustomer.Address;
            }
            if (selectedBooking != null)
            {
                txtBookingID.Text = selectedBooking.ID;
                txtBookingStatus.Text = selectedBooking.Status;
                txtBookingDate.Text = selectedBooking.BookingDate.ToString();
                txtCheckInDate.Text = selectedBooking.CheckInDate.ToString();
                txtStayDuration.Text = selectedBooking.StayDuration.ToString();
                txtCheckOutDate.Text = selectedBooking.CheckOutDate.ToString();
                txtActCheckInTime.Text = selectedBooking.ActCheckInTime != null ? selectedBooking.ActCheckInTime.ToString() : "trống";
                txtActCheckOutTime.Text = selectedBooking.ActCheckOutTime != null ? selectedBooking.ActCheckOutTime.ToString() : "trống";
                txtSpecialRequest.Text = selectedBooking.SpecialRequest;
            }

            // Setup elements of form
            titleForm.Text = "Đơn đặt phòng";
            saveBtn.Visibility = Visibility.Collapsed;
            confirmBtn.Visibility = Visibility.Collapsed;
            if (selectedBooking.Status == "depositing")
            {
                cancelBtn.Visibility = Visibility.Visible;
                checkInBtn.Visibility = Visibility.Collapsed;
                checkOutBtn.Visibility = Visibility.Collapsed;
            }
            if (selectedBooking.Status == "checkin")
            {
                cancelBtn.Visibility = Visibility.Visible;
                checkInBtn.Visibility = Visibility.Visible;
                checkOutBtn.Visibility = Visibility.Collapsed;
            }
            if (selectedBooking.Status == "checkout")
            {
                cancelBtn.Visibility = Visibility.Collapsed;
                checkInBtn.Visibility = Visibility.Collapsed;
                checkOutBtn.Visibility = Visibility.Visible;
            }
            if (selectedBooking.Status == "canceled")
            {
                cancelBtn.Visibility = Visibility.Collapsed;
                checkInBtn.Visibility = Visibility.Collapsed;
                checkOutBtn.Visibility = Visibility.Collapsed;
            }

            bookingList.Visibility = Visibility.Collapsed;
            bookingForm.Visibility = Visibility.Visible;
        }

        private void HeaderBackBtn_Clicked(object sender, RoutedEventArgs e)
        {
            bookingList.Visibility = Visibility.Visible;
            bookingForm.Visibility = Visibility.Collapsed;
            ClearDataTextBoxes();
        }

        private void CreateBookingBtn_Clicked(object sender, RoutedEventArgs e)
        {
            // Setup elements form
            titleForm.Text = "Tạo đơn đặt phòng";

            saveBtn.Visibility = Visibility.Collapsed;
            confirmBtn.Visibility = Visibility.Visible;
            cancelBtn.Visibility = Visibility.Visible;
            cancelBookingBtn.Visibility = Visibility.Collapsed;
            checkInBtn.Visibility = Visibility.Collapsed;
            checkOutBtn.Visibility = Visibility.Collapsed;

            bookingList.Visibility = Visibility.Collapsed;
            bookingForm.Visibility = Visibility.Visible;

            ClearDataTextBoxes();

            txtBookingDate.Text = DateTime.Now.ToString();
        }

        private void txtStayDuration_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateCheckOutDate();
        }

        private void CalculateCheckOutDate()
        {
            DateTime checkInDate;
            if (DateTime.TryParse(txtCheckInDate.Text, out checkInDate))
            {
                int stayDuration;
                if (int.TryParse(txtStayDuration.Text, out stayDuration))
                {
                    DateTime checkOutDate = checkInDate.AddDays(stayDuration);
                    txtCheckOutDate.Text = checkOutDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    MessageBox.Show("Số ngày lưu trú không hợp lệ!");
                }
            }
            else
            {
                MessageBox.Show("Ngày nhận phòng không hợp lệ!");
            }
        }

        private void ConfirmBtn_Clicked(Object sender, RoutedEventArgs e)
        {
            Customer _customer = new Customer();
            _customer.ID = txtCustomerID.Text;
            _customer.Name = txtCustomerName.Text;
            _customer.Phone = txtCustomerPhone.Text;
            _customer.Email = txtCustomerEmail.Text;
            _customer.Address = txtCustomerAddress.Text;

            Booking _booking = new Booking();
            _booking.ID = txtBookingID.Text;
            _booking.CustomerID = txtCustomerID.Text;
            _booking.BookingDate = DateTime.Now;
            DateTime? _txtCheckInDate = txtCheckInDate.SelectedDate;
            if (_txtCheckInDate.HasValue) _booking.CheckInDate = _txtCheckInDate.Value;
            int _txtStayDuration;
            if (int.TryParse(txtStayDuration.Text, out _txtStayDuration)) _booking.StayDuration = _txtStayDuration;
            //DateTime _txtCheckOutDate;
            //if (DateTime.TryParse(txtCheckOutDate.Text, out _txtCheckOutDate)) _booking.CheckOutDate = _txtCheckOutDate;
            //MessageBox.Show(_booking.CheckOutDate.ToString());
            _booking.CheckOutDate = _booking.CheckInDate.AddDays(_booking.StayDuration);
            _booking.ActCheckInTime = null;
            _booking.ActCheckOutTime = null;
            _booking.SpecialRequest = txtSpecialRequest.Text;
            _booking.Status = "depositing";

            bookingManagementVM.CreateBooking(_customer, _booking);

            this.DataContext = bookingManagementVM;

            bookingList.Visibility = Visibility.Visible;
            bookingForm.Visibility = Visibility.Collapsed;
        }

        private void CancelBtn_Clicked(Object sender, RoutedEventArgs e)
        {
            bookingList.Visibility = Visibility.Visible;
            bookingForm.Visibility = Visibility.Collapsed;
            ClearDataTextBoxes();
        }

        // Clear textbox booking detail form
        private void ClearDataTextBoxes()
        {
            var tbsCustomerSection = customerDataSection.Children.OfType<TextBox>();
            var tbsBookingSection = bookingDataSection.Children.OfType<TextBox>();
            var textboxs = tbsCustomerSection.Union(tbsBookingSection);

            var dps = bookingDataSection.Children.OfType<DatePicker>();

            foreach (var tb in textboxs)
            {
                tb.Clear();
                tb.IsReadOnly = false;
            }

            foreach (var dp in dps)
            {
                dp.SelectedDate = null;
            }
        }
    }
}
