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
        private DateTime stayDuration;
        private DateTime checkOutDate;
        // private List<Room> bookedRooms;
        private DateTime actCheckInTime;
        private DateTime actCheckOutTime;
        private string specialRequest;
        private string status;

        // Constructors
        public Booking() { }
        public Booking(string id, string customerID, DateTime bookingDate, DateTime checkInDate, DateTime stayDuration, DateTime checkOutDate, List<Room> bookedRooms, DateTime actCheckInTime, DateTime actCheckOutTime, string specialRequest, string status)
        {
            this.id = id;
            this.customerID = customerID;
            this.bookingDate = bookingDate;
            this.checkInDate = checkInDate;
            this.stayDuration = stayDuration;
            this.checkOutDate = checkOutDate;
            // this.bookedRooms = bookedRooms;
            this.actCheckInTime = actCheckInTime;
            this.actCheckOutTime = actCheckOutTime;
            this.specialRequest = specialRequest;
            this.status = status;
        }
    }
}
