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
    /// Interaction logic for BusinessTips.xaml
    /// </summary>
    public partial class BusinessTips : Window
    {
        Business selectedBusiness;
        curUserSelected currentUser;
        public BusinessTips(Business b, curUserSelected user)
        {
            InitializeComponent();
            this.selectedBusiness = b;
            currentUser = user;
            loadBusinessDetails();
            addColums2Grid();
            addFriendsColums2Grid();
            addTips();
            loadFriends();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 415Project; password = 605027";
        }

        private void addColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("date");
            col1.Header = "Date";
            col1.Width = 170;
            tipGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("user_name");
            col2.Header = "User Name";
            col2.Width = 100;
            tipGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("likes");
            col3.Header = "Likes";
            col3.Width = 50;
            tipGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("tipText");
            col4.Header = "Text";
            col4.Width = 680;
            tipGrid.Columns.Add(col4);
        }

        private void addFriendsColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("user_name");
            col1.Header = "User Name";
            col1.Width = 200;
            friendDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("date");
            col2.Header = "Date";
            col2.Width = 150;
            friendDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("tipText");
            col3.Header = "Text";
            col3.Width = 664;
            friendDataGrid.Columns.Add(col3);
        }

        private void loadBusinessDetails()
        {
            busName.Text = this.selectedBusiness.name;
        }

        private void loadFriends()
        {
            friendDataGrid.Items.Clear();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;

                    //cmd.CommandText = "SELECT tip.tipdate, tip.tip_text, users.name FROM business, tip, users, (SELECT DISTINCT friends.friend_id FROM friends " +
                    //    "WHERE friends.user_id = '" + currentUser.userID + "') as fri WHERE fri.friend_id = users.user_id " +
                    //    "AND business.business_id = tip.business_id AND users.user_id = tip.user_id AND tip.business_id = '" + this.selectedBusiness.bid +
                    //    "' ORDER BY tip.tipdate desc;";

                    cmd.CommandText = "SELECT tip.tip_text, users.name FROM business, tip, users, (SELECT DISTINCT friends.friend_id FROM friends " +
                        "WHERE friends.user_id = '" + currentUser.userID + "') as fri WHERE fri.friend_id = users.user_id " +
                        "AND business.business_id = tip.business_id AND users.user_id = tip.user_id AND tip.business_id = '" + this.selectedBusiness.bid +
                        "' ;"; // ORDER BY tip.tipdate desc;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friendDataGrid.Items.Add(new UserTips { user_name = reader.GetString(2), /*date = reader.GetDateTime(0).ToString(),*/ tipText = reader.GetString(1) });
                        }
                    }
                }
            }
        }

        private void addTips()
        {
            tipGrid.Items.Clear();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT tip.day, tip.month, tip.year, tip.hour, tip.minute, tip.second, tip.tip_text, tip.likes, users.name FROM tip, users " +
                        "WHERE tip.business_id = '" + this.selectedBusiness.bid + "' and tip.user_id = users.user_id "; // ORDER BY tip.tipdate desc";

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var data = new UserTips()
                            {
                                day = reader.GetInt32(0),
                                month = reader.GetInt32(1),
                                year = reader.GetInt32(2),
                                minute = reader.GetInt32(4), // 3 = hour
                                hour = reader.GetInt32(3), // 5 = sec
                                second = reader.GetInt32(5), // 4 = min
                                tipText = reader.GetString(6),
                                likes = reader.GetInt32(7),
                                user_name = reader.GetString(8)
                            };

                            tipGrid.Items.Add(data);
                        }
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

        private void tipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addTipButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    var te = new UserTips() { user_name = currentUser.name, likes = 0, tipText = tipTextBox.Text, day = DateTime.Now.Day, month = DateTime.Now.Month, year = DateTime.Now.Year, hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second };
                    cmd.Connection = connection;

                    //cmd.CommandText = "INSERT INTO tip(user_id, business_id, tipdate, tip_text, likes) VALUES('" +
                    //    currentUser.userID + "', '" + selectedBusiness.bid + "', '" + te.date + "', '" + te.tipText + "', " + te.likes + ")";

                    cmd.CommandText = "INSERT INTO tip(user_id, business_id, tip_text, likes, day, month, year, hour, minute, second) VALUES('" +
                        currentUser.userID + "', '" + selectedBusiness.bid + "',  '" + te.tipText + "', " + te.likes.ToString() + ",  " + te.day.ToString() +
                        ", " + te.month.ToString() + ", " + te.year.ToString() + ", " + te.hour.ToString() + ", " + te.minute.ToString() + ", " + te.second.ToString() + ");";


                    try
                    {
                        cmd.ExecuteNonQuery();
                        te.date = new DateTime(te.year, te.month, te.day, te.hour, te.minute, te.second);
                        tipGrid.Items.Insert(0, te);
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
            tipTextBox.Clear();
        }

        private void likeTipButton_Click(object sender, RoutedEventArgs e)
        {
            //    using (var connection = new NpgsqlConnection(buildConnectionString()))
            //    {
            //        connection.Open();
            //        using (var cmd = new NpgsqlCommand())
            //        {
            //            cmd.Connection = connection;
            //            cmd.CommandText = "UPDATE tip SET likes = likes + 1 WHERE user_id = '" + currentUser.userID + "' ";
            //            cmd.ExecuteNonQuery();
            //        }
            //        connection.Close();
            //    }
            //    UserTips tempTip = tipGrid.Items.GetItemAt(0) as UserTips;
            //    tipGrid.Items.Remove(tempTip);
            //    tempTip.likes += 1;
            //    tipGrid.Items.Insert(0, tempTip);
        }
    }
}
