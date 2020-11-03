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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for UserPage2.xaml
    /// </summary>
    public partial class UserPage2 : Page
    {

        public Business currentBus;
        public User currentUser;

        private string buildConnectionString()
        {
            return "Server = localhost; Username = postgres; Database = milestone2.2; password=1234";
        }
        public UserPage2(ref Business B, ref User U)
        {
            currentBus = B;
            currentUser = U;


            InitializeComponent();
            addColumn2Grid();
                setCurrentUserBoxes();

            userName.IsEnabled = false;
            
            
        }

        private void addColumn2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Binding = new Binding("name");

            col1.Width = 50;
            friendsList.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Total Likes";
            col2.Binding = new Binding("totalLikes");

            col2.Width = 50;
            friendsList.Columns.Add(col2);


            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Avg Stars";
            col3.Binding = new Binding("stars");

            col3.Width = 50;
            friendsList.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Yelping since";
            col4.Binding = new Binding("yelping_since");

            col4.Width = 50;
            friendsList.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "User Name";
            col5.Binding = new Binding("username");

            col5.Width = 50;
            friendsTips.Columns.Add(col5);


            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Business";
            col6.Binding = new Binding("businessname");

            col6.Width = 50;
            friendsTips.Columns.Add(col6);


            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Business";
            col7.Binding = new Binding("businessname");

            col7.Width = 50;
            friendsTips.Columns.Add(col7);


            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "City";
            col8.Binding = new Binding("city");

            col8.Width = 50;
            friendsTips.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Header = "Text";
            col9.Binding = new Binding("tiptext");

            col9.Width = 50;
            friendsTips.Columns.Add(col9);


            DataGridTextColumn col10 = new DataGridTextColumn();
            col10.Header = "Date";
            col10.Binding = new Binding("date");

            col10.Width = 50;
            friendsTips.Columns.Add(col10);



        }


        private void updateLocation_Click(object sender, RoutedEventArgs e)
        {
            _long.IsEnabled = false;
            lat.IsEnabled = false;

            if (currentUser.uid != "")
            {
              

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "update users set longitude=" + this._long.Text + " , lattitude=" + this.lat.Text + " where user_id= '" + currentUser.uid + "'";


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


                }

            }


            //do other stuff to
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void newUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserID.Items.Clear();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct user_id FROM public.users WHERE name='" + newUser.Text + "'";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            UserID.Items.Add(reader.GetString(0));
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        //System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        private void _long_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void setCurrentUserBoxes()  
        {



            userName.Text = currentUser.name;
            stars.Text = currentUser.stars.ToString();
            fans.Text = currentUser.fans.ToString();
            yelping.Text = currentUser.yelping_since;
            funny.Text = currentUser.funny.ToString();
            cool.Text = currentUser.cool.ToString();
            usefl.Text = currentUser.usefull.ToString();
            tip_count.Text = currentUser.tipcount.ToString();
            tip_likes.Text = currentUser.totalLikes.ToString();
            lat.Text = currentUser.latitude.ToString();
            _long.Text = currentUser.longitude.ToString();
            populate_Friends();
            populate_FriendsTips();

        }
        private void UserID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (UserID.SelectedItem != null) {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct name, average_stars, fans, yelping_since,funny ,cool, usefull, tipcount,total_likes,longitude,lattitude FROM public.users WHERE user_id='" + UserID.SelectedItem.ToString() + "'";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            currentUser.uid = UserID.SelectedItem.ToString();
                            currentUser.name = reader.GetString(0);
                            currentUser.stars = reader.GetDouble(1);
                            currentUser.fans = reader.GetInt32(2);
                            currentUser.yelping_since = reader.GetString(3);
                            currentUser.funny = reader.GetInt32(4);
                            currentUser.cool = reader.GetInt32(5);
                            currentUser.usefull = reader.GetInt32(6);
                            currentUser.tipcount = reader.GetInt32(7);
                            currentUser.totalLikes = reader.GetInt32(8);
                                currentUser.longitude = reader.GetDouble(9);
                                currentUser.latitude = reader.GetDouble(10);
                        }
                            //currentUser = new User() { name = reader.GetString(0), stars = reader.GetDouble(1), fans = reader.GetInt32(2), yelping_since = reader.GetString(3), funny = reader.GetInt32(4),//
                       // cool = reader.GetInt32(5), usefull = reader.GetInt32(6), tipcount = reader.GetInt32(7), totalLikes = reader.GetInt32(8) };

                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        //System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
                        setCurrentUserBoxes();

            }
        }

        private void populate_Friends()
        {
            if (currentUser.uid != "")
            {

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "Select  Name, total_likes,average_stars,yelping_since from users where user_id in (select distinct friend_id from friends where user_id = '" + currentUser.uid + "');";
                        try
                        {
                            friendsList.Items.Clear();

                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                friendsList.Items.Add(new User() { name = reader.GetString(0), totalLikes=reader.GetInt32(1), stars=reader.GetDouble(2),yelping_since=reader.GetString(3) });
                            }
                        

                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                            //System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

            }
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
                        cmd.CommandText = "Select users.name,business.name,business.city,tip.tiptext,tip.month,tip.year,tip.day,tip.hour,tip.minute,tip.second from tip,business,users where tip.userid in(select distinct friend_id from friends where user_id = '" + currentUser.uid + "') AND business.business_id = tip.businessid AND tip.userid = users.user_id ORDER by(year, month, day, hour, minute, second)  DESC";
                        try
                        {
                            friendsTips.Items.Clear();

                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                friendsTips.Items.Add(new Tip() { username = reader.GetString(0), businessname= reader.GetString(1), city= reader.GetString(2), tiptext=reader.GetString(3), date= reader.GetInt32(4).ToString()+"/"+reader.GetInt32(6).ToString()+"/"+reader.GetInt32(5).ToString()+" "+reader.GetInt32(7).ToString()+":"+reader.GetInt32(8).ToString()+":"+reader.GetInt32(9).ToString() });
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

        private void editLocation_Click(object sender, RoutedEventArgs e)
        {
            _long.IsEnabled = true;
            lat.IsEnabled = true;
        }
    }

  
}
