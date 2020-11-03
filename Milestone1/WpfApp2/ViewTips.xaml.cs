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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ViewTips.xaml
    /// </summary>
    public partial class ViewTips : Window
    {
        private string buildConnectionString()
        {
            return "Server = localhost; Username = postgres; Database = milestone2.2; password=1234";
        }

        public Business currentBus;

        public User currentUser;
        public ViewTips(ref User U, ref Business B )
        {
            currentBus = B;
            currentUser = U;

            InitializeComponent();

            addColumn2Grid();
            LoadAllTips();
            populate_FriendsTips();



        }

        private void addColumn2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Date";
            col1.Binding = new Binding("date");

            col1.Width = 50;
            Alltips.Columns.Add(col1);


            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "user Name";
            col2.Binding = new Binding("username");

            col2.Width = 100;
            Alltips.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Likes";
            col3.Binding = new Binding("likes");

            col3.Width = 50;
            Alltips.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Text";
            col4.Binding = new Binding("tiptext");

            col4.Width = 50;
            Alltips.Columns.Add(col4);


            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "User Name";
            col5.Binding = new Binding("username");

            col5.Width = 100;
            FriendsTips.Columns.Add(col5);


            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Date";
            col6.Binding = new Binding("date");

            col6.Width = 50;
            FriendsTips.Columns.Add(col6);


            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Text";
            col7.Binding = new Binding("tiptext");

            col7.Width = 50;
            FriendsTips.Columns.Add(col7);


        }

        private void LoadAllTips()
        {
            if(currentBus.bid !="")
            {

                Alltips.Items.Clear();

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {


                        cmd.Connection = connection;
                        //                            0         1           2           3            4         5         6      7         8            9       10
                        cmd.CommandText = "SELECT tip.tiptext,tip.likes, tip.userid,tip.businessid,tip.day,tip.month,tip.year,tip.hour,tip.minute,tip.second,users.name  FROM tip, users WHERE tip.businessid= '" + currentBus.bid.ToString() + "' And users.user_id=tip.userid";
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                Alltips.Items.Add(new Tip() { tiptext = reader.GetString(0), likes = reader.GetInt32(1), userid = reader.GetString(2), businessid = currentBus.bid, businessname = currentBus.name, day = reader.GetInt32(4), month = reader.GetInt32(5), year = reader.GetInt32(6), hour = reader.GetInt32(7), minute = reader.GetInt32(8), second = reader.GetInt32(9), username = reader.GetString(10), date = reader.GetInt32(5).ToString() + "/" + reader.GetInt32(4).ToString() + "/" + reader.GetInt32(5).ToString() + " " + reader.GetInt32(7).ToString() + ":" + reader.GetInt32(8).ToString() + ":" + reader.GetInt32(9).ToString() });
                        }
                        catch (NpgsqlException ex)             
                        {
                            Console.WriteLine(ex.Message.ToString());
                            System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        private void AddTip_Click(object sender, RoutedEventArgs e)
        {

            if(currentUser.uid!="" && currentBus.bid != "")
            {
                DateTime now = DateTime.Now;


                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO tip(businessid,year,month,day,hour,minute,second,likes,tiptext,userid) Values('" + currentBus.bid.ToString() +
                            "'," + now.Year.ToString() + "," + now.Month.ToString() + "," + now.Day.ToString() + "," + now.Hour.ToString() + "," + now.Minute.ToString() + "," + now.Second.ToString() + ",0,'" + newTip.Text + "','" + currentUser.uid + "')";




                        try
                        {
                            cmd.ExecuteNonQuery();




                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                            System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        }



                    }

                    LoadAllTips();

                }

            }

        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Alltips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void populate_FriendsTips()
        {

            if (currentUser.uid != "")
            {

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "Select users.name,tip.tiptext,tip.month,tip.year,tip.day,tip.hour,tip.minute,tip.second from tip,users where tip.userid in(select distinct friend_id from friends where user_id = '" + currentUser.uid + "') AND tip.businessid ='" + currentBus.bid + "' AND tip.userid = users.user_id ORDER by(year, month, day, hour, minute, second)  DESC";
                        try
                        {
                            FriendsTips.Items.Clear();

                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                FriendsTips.Items.Add(new Tip() { username = reader.GetString(0),  tiptext = reader.GetString(1), date = reader.GetInt32(2).ToString() + "/" + reader.GetInt32(4).ToString() + "/" + reader.GetInt32(3).ToString() + " " + reader.GetInt32(5).ToString() + ":" + reader.GetInt32(6).ToString() + ":" + reader.GetInt32(7).ToString() });
                            }


                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                            System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
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
}
