using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booman.Models
{
    public class InfoRoomMap
    {
        private string name;
        private string phone;
        private string email;
        private string checkin_date;
        private int stay_duration;
        private string act_checkin_time;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Checkin_date
        {
            get { return this.checkin_date; }
            set { this.checkin_date = value; }
        }
        public int Stay_duration
        {
            get { return this.stay_duration;}
            set { this.stay_duration = value;}
        }
        public string Act_checkin_time
        {
            get { return this.act_checkin_time; }
            set { this.act_checkin_time = value; }
        }
    }
}
