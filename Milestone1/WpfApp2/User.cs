using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class User
    {
        public string uid { get; set; }
        public string name { get; set; }

        public double stars { get; set; }

        public string yelping_since { get; set; }

        public int fans { get; set; }

        public int funny { get; set; }

        public int cool { get; set; }

        public int usefull { get; set; }

        public int tipcount { get; set; }

        public int totalLikes { get; set; }

        public double latitude {get; set;}

        public double longitude { get; set; }

        public User()
        {
            name = "";
            uid = "";
            stars = 0;
            yelping_since = "";
            fans = 0;
            funny = 0;
            cool = 0;
            usefull = 0;
            tipcount = 0;
            totalLikes = 0;
            latitude = 0;
            longitude = 0;
        }
    }
}
