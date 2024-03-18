using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booman.Models
{
    public class Room
    {
        // Data fields
        private string roomNum;
        private string roomType;
        private decimal price;
        private string status;

        // Constructors
        public Room() { }
        public Room(string roomNum, string roomType, decimal price, string status)
        {
            this.roomNum = roomNum;
            this.roomType = roomType;
            this.price = price;
            this.status = status;
        }
    }
}
