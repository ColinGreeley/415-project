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
using Npgsql;

namespace Lucky13_Milestone2
{
    /// <summary>
    /// Interaction logic for Checkins.xaml
    /// </summary>
    public partial class Checkins : Window
    {
        string busID;
        public Checkins(string bisID)
        {
            InitializeComponent();
            busID = bisID;
            checkinColChart();

        }
        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 415Project; password = 605027";
        }

        private void checkinColChart()
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            List<KeyValuePair<int, int>> myChartData = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<string, int>> dataWithMonths = new List<KeyValuePair<string, int>>();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT month, count(business_id) FROM checkin WHERE business_id = '" + busID + "' GROUP BY month ORDER BY month; ";
                    var reader = cmd.ExecuteReader();
                    try
                    {
                        
                        while (reader.Read())
                        {
                            myChartData.Add(new KeyValuePair<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
                        }

                        myChartData.Sort((x, y) => (x.Key.CompareTo(y.Key)));

                        foreach (KeyValuePair<int, int> temp in myChartData)
                        {
                            string month = months[temp.Key - 1];
                            dataWithMonths.Add(new KeyValuePair<string, int>(month, temp.Value));
                        }
                        checkinChart.DataContext = dataWithMonths;
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        }

        private void checkInButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    var te = new checkin() { bid = busID, year = DateTime.Now.Year, month = DateTime.Now.Month, day = DateTime.Now.Day, 
                        hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second };

                    cmd.Connection = connection;

                    cmd.CommandText = "INSERT INTO checkin (business_id, year, month, day, hour, minute, second) VALUES('" +
                         te.bid + "', " + te.year + ", " + te.month + ", " + te.day + ", " + te.hour + ", " + te.minute + ", " + te.second + " )";

                    try
                    {
                        cmd.ExecuteNonQuery();
                        checkinColChart();
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
