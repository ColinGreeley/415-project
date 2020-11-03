using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Business
    {

     
            public string address { get; set; }
            public double distance { get; set; }
            public double stars { get; set; }
            public int num_tips { get; set; }
            public int num_checkins { get; set; }

            public double longitude { get; set; }

        public double lattitude { get; set; }

            public string name { get; set; }

            public string state { get; set; }

            public string city { get; set; }

            public string bid { get; set; }

        public int isOpen { get; set; }


        public Business()
        {
            address = "";
            distance = 0.0;
            stars = 0;
            num_tips = 0;
            num_checkins = 0;
            name = "";
            state = "";
            city = "";
            bid = "";
        }

        

    }
}
