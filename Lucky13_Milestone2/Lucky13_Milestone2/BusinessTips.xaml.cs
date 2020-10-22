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
            return "Host = localhost; Username = postgres; Database = Milestone3db; password = 605027";
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
            //using (var connection = new NpgsqlConnection(buildConnectionString()))
            //{
            //    connection.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {

            //        cmd.Connection = connection;

            //        cmd.CommandText = "SELECT tip.tipdate, tip.tiptext, yelpuser.name FROM business, tip, yelpuser, (SELECT DISTINCT friend.friend_id FROM friend " +
            //            "WHERE friend.user_id = '" + currentUser.userID + "') as fri WHERE fri.friend_id = yelpuser.user_id " +
            //            "AND business.business_id = tip.business_id AND yelpuser.user_id = tip.user_id AND tip.business_id = '" + this.selectedBusiness.bid +
            //            "' ORDER BY tip.tipdate desc;";

            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                friendDataGrid.Items.Add(new UserTips { user_name = reader.GetString(2), date = reader.GetDateTime(0).ToString(), tipText = reader.GetString(1) });
            //            }
            //        }
            //    }
            //}
        }

        private void addTips()
        {
            tipGrid.Items.Clear();
            //using (var connection = new NpgsqlConnection(buildConnectionString()))
            //{
            //    connection.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {
            //        cmd.Connection = connection;
            //        cmd.CommandText = "SELECT tip.tipdate, tip.tiptext, tip.likes, yelpuser.name FROM tip, yelpuser " +
            //            "WHERE tip.business_id = '" + this.selectedBusiness.bid + "' and tip.user_id = yelpuser.user_id ORDER BY tip.tipdate desc";

            //        try
            //        {
            //            var reader = cmd.ExecuteReader();
            //            while (reader.Read())
            //            {
            //                var data = new UserTips() { date = reader.GetDateTime(0).ToString(), user_name = reader.GetString(3), likes = reader.GetInt16(2), tipText = reader.GetString(1) };
            //                tipGrid.Items.Add(data);
            //            }
            //        }
            //        catch (NpgsqlException ex)
            //        {
            //            Console.WriteLine(ex.Message.ToString());
            //            MessageBox.Show("SQL Error - " + ex.Message.ToString());
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
        }

        private void tipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addTipButton_Click(object sender, RoutedEventArgs e)
        {
            //using (var connection = new NpgsqlConnection(buildConnectionString()))
            //{
            //    connection.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {
            //        var te = new UserTips() { date = DateTime.Now.ToString(), user_name = currentUser.name, likes = 0, tipText = tipTextBox.Text };
            //        cmd.Connection = connection;

            //        cmd.CommandText = "INSERT INTO tip(user_id, business_id, tipdate, tiptext, likes) VALUES('" +
            //            currentUser.userID + "', '" + selectedBusiness.bid + "', '" + te.date + "', '" + te.tipText + "', " + te.likes + ")";

            //        try
            //        {
            //            cmd.ExecuteNonQuery();
            //            tipGrid.Items.Insert(0, te);
            //        }
            //        catch (NpgsqlException ex)
            //        {
            //            Console.WriteLine(ex.Message.ToString());
            //            MessageBox.Show("SQL Error - " + ex.Message.ToString());
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
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
