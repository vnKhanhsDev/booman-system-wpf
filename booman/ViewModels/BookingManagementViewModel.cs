using System;
using System.Collections.ObjectModel;
using System.Data;
using booman.Services;
using booman.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;

namespace booman.ViewModels
{
    public class BookingManagementViewModel
    {
        // Data fields
        private MySQLDatabaseService dbService;
        private ObservableCollection<Booking> bookings;
        private ObservableCollection<Customer> customers;

        private int numDepositing = 0;
        private int numPendingCheckIn = 0;
        private int numPendingCheckOut = 0;
        private int numCompleted = 0;
        private int numCancelled = 0;


        // Constructors
        public BookingManagementViewModel()
        {
            dbService = new MySQLDatabaseService();
            bookings = new ObservableCollection<Booking>();
            LoadBookings();
            customers = new ObservableCollection<Customer>();
            LoadCustomers();
        }

        // Properties
        public ObservableCollection<Booking> Bookings
        {
            get { return this.bookings; }
        }
        public ObservableCollection<Customer> Customers
        {
            get { return this.customers; }
        }
        public int NumDepositing
        {
            get { return this.numDepositing; }
        }
        public int NumPendingCheckIn
        {
            get { return this.numPendingCheckIn; }
        }
        public int NumPendingCheckOut
        {
            get { return this.numPendingCheckOut; }
        }
        public int NumCompleted
        {
            get { return this.numCompleted; }
        }
        public int NumCancelled
        {
            get { return this.numCancelled; }
        }

        // Methods
        private void LoadBookings()
        {
            DataTable bookingDT = dbService.GetTableData("booking");
            DateTime test = DateTime.Now;
            foreach (DataRow row in bookingDT.Rows)
            {
                string _status = row["status"].ToString();

                bookings.Add(new Booking
                {
                    ID = row["id"].ToString(),
                    CustomerID = row["customer_id"].ToString(),
                    BookingDate = (DateTime)row["booking_date"],
                    CheckInDate = (DateTime)row["checkin_date"],
                    StayDuration = (int)row["stay_duration"],
                    CheckOutDate = (DateTime)row["checkout_date"],
                    ActCheckInTime = row["act_checkin_time"] != DBNull.Value ? (DateTime)row["act_checkin_time"] : (DateTime?)null,
                    ActCheckOutTime = row["act_checkout_time"] != DBNull.Value ? (DateTime)row["act_checkout_time"] : (DateTime?)null,
                    SpecialRequest = row["special_request"].ToString(),
                    Status = _status
                });

                if (_status == "depositing") numDepositing++;
                if (_status == "checkin") numPendingCheckIn++;
                if (_status == "checkout") numPendingCheckOut++;
                if (_status == "completed") numCompleted++;
                if (_status == "canceled") numCancelled++;
            }
        }

        private void LoadCustomers()
        {
            DataTable customerDT = dbService.GetTableData("customer");
            foreach (DataRow row in customerDT.Rows)
            {
                customers.Add(new Customer
                {
                    ID = row["id"].ToString(),
                    Name = row["name"].ToString(),
                    Phone = row["phone"].ToString(),
                    Email = row["email"].ToString(),
                    Address = row["address"].ToString()
                });
            }
        }

        public void CreateBooking(Customer _customer, Booking _booking)
        {
            dbService.Connection.Open();

            MySqlCommand cmd = dbService.Connection.CreateCommand();

            InsertCustomer(_customer, cmd);

            InsertBooking(_booking, cmd);

            dbService.Connection.Close();

            LoadBookings();
        }

        private void InsertCustomer(Customer _customer, MySqlCommand cmd)
        {
            if (_customer != null)
            {
                string query = "INSERT INTO `customer` (`id`, `name`, `phone`, `email`, `address`) VALUES (@ID, @Name, @Phone, @Email, @Address)";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ID", _customer.ID);
                cmd.Parameters.AddWithValue("@Name", _customer.Name);
                cmd.Parameters.AddWithValue("@Phone", _customer.Phone);
                cmd.Parameters.AddWithValue("@Email", _customer.Email);
                cmd.Parameters.AddWithValue("@Address", _customer.Address);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertBooking(Booking _booking, MySqlCommand cmd)
        {
            if (_booking != null)
            {
                string query = "INSERT INTO `booking` (`id`, `customer_id`, `booking_date`, `checkin_date`, `stay_duration`, `checkout_date`, `act_checkin_time`, `act_checkout_time`, `special_request`, `status`) VALUES (@BookingID, @CustomerID, @BookingDate, @CheckInDate, @StayDuration, @CheckOutDate, @ActCheckInTime, @ActCheckOutTime, @SpecialRequest, @Status)";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@BookingID", _booking.ID);
                cmd.Parameters.AddWithValue("@CustomerID", _booking.CustomerID);
                cmd.Parameters.AddWithValue("@BookingDate", _booking.BookingDate);
                cmd.Parameters.AddWithValue("@CheckInDate", _booking.CheckInDate);
                cmd.Parameters.AddWithValue("@StayDuration", _booking.StayDuration);
                cmd.Parameters.AddWithValue("@CheckOutDate", _booking.CheckOutDate);
                cmd.Parameters.AddWithValue("@ActCheckInTime", _booking.ActCheckInTime);
                cmd.Parameters.AddWithValue("@ActCheckOutTime", _booking.ActCheckOutTime);
                cmd.Parameters.AddWithValue("@SpecialRequest", _booking.SpecialRequest);
                cmd.Parameters.AddWithValue("@Status", _booking.Status);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
