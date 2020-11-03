using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ViewCheckIns.xaml
    /// </summary>
    public partial class ViewCheckIns : Window
    {
        public Business currentBus;
        public ViewCheckIns(ref Business bus)
        {
            currentBus = bus;
            InitializeComponent();
            columnChart();
        }

        private void columnChart()
        {
            List<KeyValuePair<string, int>> myChartData = new List<KeyValuePair<string, int>>();

            using (var conn = new NpgsqlConnection("Host=localhost; Username=postgres; Password= cpts451; Database=milestone2"))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "select month,COUNT(business_id) from checkin where business_id = '"+ currentBus.bid + "' group by month order by month";
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            myChartData.Add(new KeyValuePair<string, int>(processData(reader.GetInt16(0)), reader.GetInt32(1)));
                        }
                    }

                }
            }


            checkInChart.DataContext = myChartData;


        }


        private string processData(int month)
        {
            switch (month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "Agust";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return "";
            }
        }
    }
}
