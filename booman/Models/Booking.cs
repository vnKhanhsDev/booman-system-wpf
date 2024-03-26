using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booman.Models
{
    public class Booking
    {
        // Data fields
        private string id;
        private string customerID;
        private DateTime bookingDate;
        private DateTime checkInDate;
        private int stayDuration;
        private DateTime checkOutDate;
        // private List<Room> bookedRooms;
        private DateTime? actCheckInTime;
        private DateTime? actCheckOutTime;
        private string specialRequest;
        private string status;

        // Constructors
        public Booking() { }
        public Booking(string id, string customerID, DateTime bookingDate, DateTime checkInDate, int stayDuration, DateTime checkOutDate, DateTime? actCheckInTime, DateTime? actCheckOutTime, string specialRequest, string status)
        {
            this.id = id;
            this.customerID = customerID;
            this.bookingDate = bookingDate;
            this.checkInDate = checkInDate;
            this.stayDuration = stayDuration;
            this.checkOutDate = checkOutDate;
            this.actCheckInTime = actCheckInTime;
            this.actCheckOutTime = actCheckOutTime;
            this.specialRequest = specialRequest;
            this.status = status;
        }

        // Properties
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string CustomerID
        {
            get { return this.customerID; }
            set { this.customerID = value; }
        }
        public DateTime BookingDate
        {
            get { return this.bookingDate; }
            set { this.bookingDate = value; }
        }
        public DateTime CheckInDate
        {
            get { return this.checkInDate; }
            set { this.checkInDate = value; }
        }
        public int StayDuration
        {
            get { return this.stayDuration; }
            set { this.stayDuration = value; }
        }
        public DateTime CheckOutDate
        {
            get { return this.checkOutDate; }
            set { this.checkOutDate = value; }
        }
        public DateTime? ActCheckInTime
        {
            get { return this.actCheckInTime; }
            set { this.actCheckInTime = value; }
        }
        public DateTime? ActCheckOutTime
        {
            get { return this.actCheckOutTime; }
            set { this.actCheckOutTime = value; }
        }
        public string SpecialRequest
        {
            get { return this.specialRequest; }
            set { this.specialRequest = value; }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        // Methods
    }
}
