using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booman.Models
{
    public class RoomService
    {
        private string id;
        private string name;
        private int quantity;

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name { 
            get { return this.name; }
            set { this.name = value; }       
        }
        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }
    }
}
