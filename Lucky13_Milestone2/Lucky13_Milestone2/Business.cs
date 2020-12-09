using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky13_Milestone2
{
    public class Business
    {
        public string bid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string[] category { get; set; }
        public double distance { get; set; }
        public double star { get; set; }
        public int numTips { get; set; }
        public int totalCheckins { get; set; }

        public double lat { get; set; }
        public double lon { get; set; }

    }

    public class Friend
    {
        public string friend_id { get; set; }
        public string friend_name { get; set; }
        public string yelping_since { get; set; }
        public string friend_stars { get; set; }
        public int friend_total_likes { get; set; }
    }

    public class Tip
    {
        public string tipText { get; set; }
        public string user_name { get; set; }
        public string business_name { get; set; }
        public string city { get; set; }
        public string tipDate { get; set; }
    }

    public class UserTips
    {
        public string user_name { get; set; }
        public int likes { get; set; }
        public string tipText { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public DateTime date { get; set; }

    }

    public class curUserSelected
    {
        public string userID { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Hours
    {

        public string day_week { get; set; }
        public string open_time { get; set; }
        public string close_time { get; set; }
        public Hours(string day, string open, string closed)
        {
            day_week = day;
            open_time = open;
            close_time = closed;
        }
    }

    public class checkin
    {
        public string bid { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        //public string time { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }

}