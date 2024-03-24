using System;
using System.Collections.ObjectModel;
using System.Data;
using booman.Services;
using booman.Models;

namespace booman.ViewModels
{
    public class BookingManagementViewModel
    {
        // Data fields
        private MySQLDatabaseService dbService;
        private ObservableCollection<Booking> bookings;

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
        }

        // Properties
        public ObservableCollection<Booking> Bookings
        {
            get { return this.bookings; }
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
                    ActCheckInTime = test,
                    ActCheckOutTime = test,
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
    }
}
