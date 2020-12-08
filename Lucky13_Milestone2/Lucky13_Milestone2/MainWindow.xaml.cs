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

namespace Lucky13_Milestone2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        curUserSelected curUser = new curUserSelected();
        Dictionary<string, string> whatCostAttributesSelected = new Dictionary<string, string>();

        List<string> whatOtherAttributesSelected = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            addFriendsDataColums2Grid();
            addFriendsTipDataColums2Grid();
            addColums2Grid();
            addStates();
            addSortResultsList();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 415Project; password = 605027";
        }

        private void addFriendsDataColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("friend_name");
            col1.Header = "Name";
            col1.Width = 80;
            friendsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("friend_total_likes");
            col2.Header = "Total Likes";
            col2.Width = 60;
            friendsDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("friend_stars");
            col3.Header = "Avg Stars";
            col3.Width = 60;
            friendsDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("yelping_since");
            col4.Header = "Yelping Since";
            col4.Width = 288;
            friendsDataGrid.Columns.Add(col4);
        }

        private void addFriendsTipDataColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("user_name");
            col1.Header = "User Name";
            col1.Width = 70;
            friendsTipsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("business_name");
            col2.Header = "Business";
            col2.Width = 90;
            friendsTipsDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 60;
            friendsTipsDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("tipText");
            col4.Header = "Text";
            col4.Width = 288;
            friendsTipsDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("tipDate");
            col5.Header = "Date";
            col5.Width = 288;
            friendsTipsDataGrid.Columns.Add(col5);
        }

        private void addColums2Grid()
        {

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "BuisnessName";
            col1.Width = 138;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("address");
            col2.Header = "Address";
            col2.Width = 140;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 50;
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("state");
            col4.Header = "State";
            col4.Width = 30;
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("distance");
            col5.Header = "Distance\n(miles) ";
            col5.Width = 50;
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("star");
            col6.Header = "Stars";
            col6.Width = 45;
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("numTips");
            col7.Header = "# of Tips";
            col7.Width = 55;
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("totalCheckins");
            col8.Header = "Total Checkins";
            col8.Width = 60;
            businessGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Binding = new Binding("bid");
            col9.Header = "";
            col9.Width = 0;
            businessGrid.Columns.Add(col9);
        }

        private void addSortResultsList()
        {
            sortResultsList.Items.Add("Name");
            sortResultsList.Items.Add("Highest rated");
            sortResultsList.Items.Add("Most number of tips");
            sortResultsList.Items.Add("Most checkins");
            sortResultsList.Items.Add("Nearest");
        }


        private void inputUserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBox.Items.Clear();
            clearUserData();
            friendsDataGrid.Items.Clear();
            friendsTipsDataGrid.Items.Clear();

            if (inputUserTextBox.Text.Length > 0)
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT distinct user_id FROM users WHERE name like '" + inputUserTextBox.Text + "' ORDER BY user_id asc";
                        using (var reader = cmd.ExecuteReader())
                            try
                            {
                                while (reader.Read())
                                {
                                    listBox.Items.Add(reader.GetString(0));
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
        }

        private void clearUserData()
        {
            nameTextBox.Text = "";
            starsTextBox.Text = "";
            fansTextBox.Text = "";
            yelpSinceTxt.Text = "";
            votesTxt.Text = "";
            funnyTxt.Text = "";
            coolTxt.Text = "";
            usefulTxt.Text = "";
            tipCountTxt.Text = "";
            totalTipLikesTxt.Text = " ";
            latTxt.Text = "";
            longTxt.Text = "";
        }

        private int[] updateCounts(string userID)
        {
            int[] totals = new int[2];
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    cmd.CommandText = "SELECT COUNT(tip_text), COALESCE(SUM(likes), 0) AS TotalLikes FROM tip WHERE user_id = '" + userID + "'; ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totals[0] = reader.GetInt32(0);
                            totals[1] = reader.GetInt32(1);
                        }
                    }

                    cmd.CommandText = "UPDATE users SET tipcount = " + totals[0] + ", total_likes = " + totals[1] + " WHERE user_id = '" + userID + "' ;";
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
            return totals;           
        }

        private DateTime convertToDate(int y, int m, int d, int hr, int min, int sec)
        {
            string date = y.ToString() + "-" + m.ToString() + "-" + d.ToString() + " " + hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            return Convert.ToDateTime(date);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearUserData();
            friendsDataGrid.Items.Clear();
            friendsTipsDataGrid.Items.Clear();
            List<string> friendIds = new List<string>();
            if (listBox.SelectedIndex > -1)
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        int[] counts = updateCounts(listBox.SelectedItem.ToString());  // cur user
                        cmd.CommandText = "SELECT * FROM users WHERE user_id = '" + listBox.SelectedItem.ToString() + "' ORDER BY user_id";
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                starsTextBox.Text = reader.GetDouble(0).ToString();
                                coolTxt.Text = reader.GetInt32(1).ToString();
                                fansTextBox.Text = reader.GetInt32(2).ToString();
                                funnyTxt.Text = reader.GetInt32(3).ToString();
                                usefulTxt.Text = reader.GetInt32(4).ToString();
                                curUser.name = reader.GetString(5);
                                nameTextBox.Text = curUser.name;
                                tipCountTxt.Text = reader.GetInt32(6).ToString();

                                curUser.userID = reader.GetString(7);
                                yelpSinceTxt.Text = reader.GetString(8);
                                totalTipLikesTxt.Text = reader.GetInt32(9).ToString();

                                //if (reader.GetDouble(9) != 0.0 && reader.GetDouble(10) != 0.0)
                                //{
                                //    latTxt.Text = reader.GetDouble(9).ToString();
                                //    longTxt.Text = reader.GetDouble(10).ToString();
                                //}
                            }
                        }
                        cmd.CommandText = "SELECT distinct * FROM users, (SELECT DISTINCT friends.friend_id FROM friends " +
                            "WHERE friends.user_id = '" + listBox.SelectedItem.ToString() + "') as fri WHERE fri.friend_id = users.user_id; ";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetString(7); // 
                                string stars = reader.GetDouble(0).ToString(); //
                                string nme = reader.GetString(5); //
                                string since = reader.GetString(8); //
                                int totLik = reader.GetInt16(9);
                                friendIds.Add(id);
                                friendsDataGrid.Items.Add(new Friend { friend_id = id, friend_name = nme, friend_stars = stars, yelping_since = since, friend_total_likes = totLik });
                            }
                        }
                        cmd.CommandText = "SELECT * FROM (SELECT tip.user_id, users.name, business.name, business.city, tip.tip_text, tip.year, " +
                            "tip.month, tip.day, tip.hour, tip.minute, tip.second FROM users, business, tip, (SELECT distinct user_id FROM users, " +
                            "(SELECT DISTINCT friend_id FROM friends WHERE user_id = '" + listBox.SelectedItem.ToString() + "') as a " +
                            "WHERE a.friend_id = users.user_id) as b WHERE b.user_id = users.user_id and business.business_id = tip.business_id " +
                            "and tip.user_id = b.user_id) as ti ORDER BY year DESC, month DESC, day DESC, hour DESC, minute DESC, second DESC LIMIT 25";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = convertToDate(reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10));
                                friendsTipsDataGrid.Items.Add(new Tip { user_name = reader.GetString(1), business_name = reader.GetString(2), 
                                    city = reader.GetString(3), tipText = reader.GetString(4), tipDate = date.ToString()});
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void editLocationButton_Click(object sender, RoutedEventArgs e)
        {
            latTxt.IsReadOnly = false;
            longTxt.IsReadOnly = false;
        }

        private void updateLocationButton_Click(object sender, RoutedEventArgs e)
        {
            latTxt.IsReadOnly = true;
            longTxt.IsReadOnly = true;
            //using (var connection = new NpgsqlConnection(buildConnectionString()))
            //{
            //    connection.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {
            //        cmd.Connection = connection;
            //        cmd.CommandText = "UPDATE yelpuser SET userlat = '" + Convert.ToDouble(latTxt.Text) + "', userlong = '" + Convert.ToDouble(longTxt.Text) + "' WHERE user_id = '" + listBox.SelectedItem.ToString() + "' ;";
            //        cmd.ExecuteNonQuery();
            //    }
            //    connection.Close();
            //}
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addStates()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            StateList.Items.Add(reader.GetString(0));

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

        private void StateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CityList.Items.Clear();
            if (StateList.SelectedIndex > -1)
            {

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        //cmd.CommandText = "SELECT distinct city FROM business WHERE state = '" + StateList.SelectedItem.ToString() + "' ORDER BY city";
                        cmd.CommandText = "SELECT distinct (SELECT INITCAP (city)) AS tempCity FROM business WHERE state = '" + StateList.SelectedItem.ToString() + "' ORDER BY tempCity";
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                CityList.Items.Add(reader.GetString(0));

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

        private void CityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZipList.Items.Clear();

            if (CityList.SelectedIndex > -1)
            {

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT distinct zipcode FROM business WHERE city = '" + CityList.SelectedItem.ToString() + "' ORDER BY zipcode";
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                ZipList.Items.Add(reader.GetString(0));

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

        private void ZipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categorylistBox.Items.Clear();
            categorySelectedListBox.Items.Clear();
            businessGrid.Items.Clear();
            selectedBusinessDetailsListBox.Items.Clear();
            numOfBusinesses.Content = "# of businesses: 0";
            BusNameTextBlock.Text = "Business Name";
            addresseBusTextBlock.Text = "Address";
            hoursBusTextBlock.Text = "Today: Opens / Closes ";


            if (ZipList.SelectedIndex > -1)
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;

                        cmd.CommandText = "SELECT DISTINCT LTRIM(category) AS LeftTrimmedString FROM business, categories " +
                            "WHERE business.state = '" + StateList.SelectedItem.ToString() + "' and business.city = '" + CityList.SelectedItem.ToString() + "' " +
                            "and business.zipcode = '" + ZipList.SelectedItem.ToString() + "' and business.business_id = categories.business_id  ORDER BY LeftTrimmedString ASC;";

                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                categorylistBox.Items.Add(reader.GetString(0));

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

        private void categorylistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (categorylistBox.SelectedIndex > -1)
            {
                if (!categorySelectedListBox.Items.Contains(categorylistBox.SelectedItem))
                    categorySelectedListBox.Items.Add(categorylistBox.SelectedItem);
            }
        }

        private void removeCatButton_Click(object sender, RoutedEventArgs e)
        {
            categorySelectedListBox.Items.Remove(categorySelectedListBox.SelectedItem);
        }

        private void calDistance(Business bis)
        {
            List<Business> listBusinesses = new List<Business>();
            //using (var comm = new NpgsqlConnection(buildConnectionString()))
            //{
            //    comm.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {
            //        cmd.Connection = comm;

            //        StringBuilder buisLoc = new StringBuilder();
            //        buisLoc.Append("(SELECT lat, long FROM business " +
            //            "WHERE state ='" + StateList.SelectedItem.ToString() + "' and city ='" + CityList.SelectedItem.ToString() + "' " +
            //            "and zipcode = '" + ZipList.SelectedItem.ToString() + "' and business.business_id = '" + bis.bid + "' ) as busi");
            //        StringBuilder userLoc = new StringBuilder();
            //        userLoc.Append("(SELECT userlat, userlong FROM yelpuser WHERE user_id = '" + curUser.userID + "' ) as userCoor");

            //        StringBuilder dist = new StringBuilder();

            //        dist.Append("(SELECT 2 * 3961 * asin(sqrt((sin(radians((LOC2.userlat - LOC1.lat) / 2))) ^ 2 + cos(radians(LOC1.lat)) * cos(radians(LOC2.userlat)) * (sin(radians((LOC2.userlong - LOC1.long) / 2))) ^ 2)) as DISTANCE " +
            //            "FROM (SELECT lat, long FROM " + buisLoc.ToString() + ") as LOC1, (SELECT userlat, userlong FROM " + userLoc.ToString() + ") as LOC2) as dis ");


            //        cmd.CommandText = "SELECT dis.* FROM " + dist.ToString();

            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                int i = businessGrid.Items.IndexOf(bis);
            //                businessGrid.Items.Remove(bis);
            //                bis.distance = Math.Round(reader.GetDouble(0), 2);
            //                listBusinesses.Add(bis);
            //                businessGrid.Items.Insert(i, bis);
            //            }
            //        }
            //        comm.Close();
            //    }
            //}
        }

        private void sortByDistance()
        {
            List<Business> listBusinesses = new List<Business>();

            for (int i = 0; i < businessGrid.Items.Count; i++)
            {
                listBusinesses.Add(businessGrid.Items.GetItemAt(i) as Business);
            }

            listBusinesses = listBusinesses.OrderBy(item => item.distance).ToList();
            businessGrid.Items.Clear();
            foreach (var obj in listBusinesses)
            {
                businessGrid.Items.Add(obj);
            }
        }


        private void searchBusinessesButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder cmd = new StringBuilder(" ");
            updateAttributes();
        }

        private void insertCategoriesAttribute(Business B)  // inserts categories into selected business category box
        {

            selectedBusinessDetailsListBox.Items.Clear();
            using (var comm = new NpgsqlConnection(buildConnectionString()))
            {

                //comm.Open();
                //using (var cmd = new NpgsqlCommand())
                //{

                //cmd.Connection = comm;
                /*if (StateList.SelectedItem != null)
                {
                    cmdstr += " WHERE state = '" + StateList.SelectedItem.ToString() + "'";
                }
                if (CityList.SelectedItem != null)
                {
                    cmdstr += " AND city = '" + CityList.SelectedItem.ToString() + "'";
                }
                if (ZipList.SelectedItem != null)
                {
                    cmdstr += " AND zipcode = '" + ZipList.SelectedItem.ToString() + "'";
                }
                cmd.CommandText = "Select Distinct category from categories where business_id IN( " + cmdstr + ")";
                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                        categorylistBox.Items.Add(reader.GetString(0));                    //Buisness_Grif.Items.Add(new Buisness() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2) });

                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                }
                finally
                {
                    comm.Close();
                }*/
                comm.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "SELECT * FROM categories WHERE business_id = '" + B.bid + "' ORDER BY category";

                    selectedBusinessDetailsListBox.Items.Add("Categories");
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            selectedBusinessDetailsListBox.Items.Add("\t" + reader.GetString(1));
                        }
                    }

                    cmd.CommandText = "SELECT * FROM attributes WHERE business_id = '" + B.bid + "' " +
                        "AND attribute != 'False' AND attribute != 'none' ORDER BY attribute_key; ";
                    selectedBusinessDetailsListBox.Items.Add("Attributes");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            selectedBusinessDetailsListBox.Items.Add("\t" + reader.GetString(1));
                        }
                    }
                    comm.Close();
                }
            }

        }

        private void businessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (businessGrid.Items.Count > 0)
            {
                Business B = businessGrid.Items[businessGrid.SelectedIndex] as Business;
                BusNameTextBlock.Text = B.name;
                addresseBusTextBlock.Text = B.address + ", " + B.city + ", " + B.state;
                DayOfWeek today = DateTime.Today.DayOfWeek;

                using (var comm = new NpgsqlConnection(buildConnectionString()))
                {
                    comm.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = comm;
                        cmd.CommandText = "SELECT day, open_time, close_time FROM hours WHERE business_id = '" + B.bid + "' ";

                        List<Hours> busHours = new List<Hours>();

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                busHours.Add(new Hours(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                            }
                        }
                        comm.Close();

                        bool hoursExist = false;
                        foreach (var buis in busHours)
                        {
                            if (buis.day_week == today.ToString())
                            {
                                hoursBusTextBlock.Text = "Today (" + today.ToString() + "):  Opens: " + buis.open_time + "    Closes: " + buis.close_time;
                                hoursExist = true;
                            }
                        }
                        if (!hoursExist)
                            hoursBusTextBlock.Text = "Today (" + today.ToString() + "):   Closed";

                        
                    }

                }
                insertCategoriesAttribute(B);
            }
        }

        private void showTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex >= 0)
            {
                Business B = businessGrid.Items[businessGrid.SelectedIndex] as Business;

                if ((B.bid != null) && (B.bid.ToString().CompareTo("") != 0))
                {
                    BusinessTips tipWindow = new BusinessTips(B, curUser);
                    tipWindow.Show();
                }
            }
        }

        private void sortResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder cmd = new StringBuilder(" ");
            updateAttributes();
        }

        private void showCheckinsButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                Checkins Checkins = new Checkins(((Business)businessGrid.SelectedItem).bid);
                Checkins.Show();
            }
        }


        private void updateBusinessGridWithAttributes(StringBuilder temp)
        {
            selectedBusinessDetailsListBox.Items.Clear();
            string sortBy = "ORDER BY name ASC";
            if (sortResultsList.SelectedIndex > -1)
            {
                switch (sortResultsList.SelectedItem.ToString())
                {
                    case "Name":
                        sortBy = "ORDER BY name ASC";
                        break;
                    case "Highest rated":
                        sortBy = "ORDER BY stars DESC";
                        break;
                    case "Most number of tips":
                        sortBy = "ORDER BY numtips DESC";
                        break;
                    case "Most checkins":
                        sortBy = "ORDER BY numcheckins DESC";
                        break;
                    case "Nearest":
                        sortBy = " ";
                        break;
                    default:
                        break;
                }
            }

            if (CityList.SelectedIndex > -1 && StateList.SelectedIndex > -1 && ZipList.SelectedIndex > -1)
            {
                businessGrid.Items.Clear();
                using (var comm = new NpgsqlConnection(buildConnectionString()))
                {
                    comm.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        StringBuilder command = new StringBuilder();
                        StringBuilder commandEnd = new StringBuilder();
                        cmd.Connection = comm;
                        if (categorySelectedListBox.Items.Count > 0)
                        {
                            command.Append(",  (SELECT DISTINCT business_id as bID ");

                            command.Append(temp);
                            if (temp.ToString().ElementAt(temp.ToString().Length - 1) == ')')
                            {
                                command.Append(" AND");
                            }
                            else
                            {
                                command.Append(" FROM business WHERE");
                            }
                            command.Append(" business.business_id IN (SELECT business_id FROM categories WHERE category = '" + categorySelectedListBox.Items[0].ToString().Trim() + "') ");
                            for (int i = 1; i < categorySelectedListBox.Items.Count; i++)
                            {
                                command.Append("AND business.business_id IN (SELECT business_id FROM categories WHERE category = '" + categorySelectedListBox.Items[i].ToString().Trim() + "') ");
                            }
                            command.Append(") bus ");
                            commandEnd.Append("and bus.bID = business.business_id ");
                        }
                        else
                        {
                            if (temp.ToString() != " ") // if attributes exist
                            {
                                command.Append(",  (SELECT DISTINCT business_id as bID ");
                                command.Append(temp);
                                command.Append(") bus ");
                                commandEnd.Append("and bus.bID = business.business_id ");
                            }
                            else
                            {
                                command.Append(" ");
                                commandEnd.Append(" ");
                            }
                        }

                        cmd.CommandText = "SELECT * FROM business " + command.ToString() +
                            "WHERE state ='" + StateList.SelectedItem.ToString() + "' and city ='" + CityList.SelectedItem.ToString() + "' " +
                            "and zipcode = '" + ZipList.SelectedItem.ToString() + "' " + commandEnd.ToString() + sortBy;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string bid = reader.GetString(0);
                                int numTips = reader.GetInt32(9);
                                if (numTips < 1)
                                    numTips = getNumTips(bid);
                                businessGrid.Items.Add(new Business() { bid = bid, name = reader.GetString(1), city = reader.GetString(3), state = reader.GetString(4), zip = reader.GetString(5), address = reader.GetString(2), star = reader.GetDouble(8), totalCheckins = reader.GetInt32(11), numTips = numTips });
                            }
                        }
                        comm.Close();
                        command.Clear();
                    }
                }
                for (int i = 0; i < businessGrid.Items.Count; i++)
                {
                    calDistance(businessGrid.Items.GetItemAt(i) as Business);
                }
                numOfBusinesses.Content = "# of businesses: " + businessGrid.Items.Count.ToString();

                if (sortBy == " ")
                    sortByDistance();
            }
        }

        private void insertNumTips(string bid, int count)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE business SET numtips = " + count + " WHERE business_id = '" + bid + "';";
                    try
                    {
                        cmd.ExecuteNonQuery();
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

        private int getNumTips(string bid)
        {
            int total = 0;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(tip_text) FROM tip WHERE business_id = '" + bid + "'";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            total = reader.GetInt32(0);

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

            if(total > 0)
                insertNumTips(bid, total);

            return total;
        }

        private StringBuilder updateCostAttributes()
        {
            StringBuilder command = new StringBuilder();
            if (whatCostAttributesSelected.Count > 0)
            {
                command.Append(" business.business_id IN (SELECT business_id FROM attributes WHERE attr_name = 'RestaurantsPriceRange2' AND attr_value = '" + whatCostAttributesSelected.First().Value + "' ");
                for (int i = 1; i < whatCostAttributesSelected.Count; i++)
                {
                    command.Append("OR attr_value = '" + whatCostAttributesSelected.Values.ToList()[i] + "' ");
                }
                command.Append(")");
            }
            else
                command.Append("");
            return command;
        }

        private StringBuilder updateOthAttributes()
        {
            StringBuilder command = new StringBuilder();
            if (whatCostAttributesSelected.Count > 0)
            {
                command.Append(" business.business_id IN (SELECT business_id FROM attributes WHERE attribute_key = 'RestaurantsPriceRange2' AND attribute = '" + whatCostAttributesSelected.First().Value + "' ");
                for (int i = 1; i < whatCostAttributesSelected.Count; i++)
                {
                    command.Append("OR attribute = '" + whatCostAttributesSelected.Values.ToList()[i] + "' ");
                }
                command.Append(")");
            }
            else
                command.Append("");
            return command;
        }

        private void updateAttributes()
        {
            StringBuilder command = new StringBuilder();
            if (whatCostAttributesSelected.Count > 0) // cost exists in dictionary
            {
                command.Append("FROM business WHERE");
                command.Append(updateCostAttributes());
            }

            if (whatCostAttributesSelected.Count > 0 && whatOtherAttributesSelected.Count > 0) // both exist
            {
                command.Append("AND ");
                command.Append(updateOthAttributes());
            }

            if (whatOtherAttributesSelected.Count > 0 && whatCostAttributesSelected.Count <= 0) // only oth exists
            {
                command.Append("FROM business WHERE");
                command.Append(updateOthAttributes());
            }

            if (whatOtherAttributesSelected.Count <= 0 && whatCostAttributesSelected.Count <= 0) // only neither contain attributes
            {
                command.Append(" ");
            }

            updateBusinessGridWithAttributes(command);
        }

        private void storeWhichOtherAttributes(string nme, bool isChecked)
        {
            if (whatOtherAttributesSelected.Contains(nme) && isChecked == false) // if box has been unchecked, removes from checked attributes list
                whatOtherAttributesSelected.Remove(nme);
            else if (!whatOtherAttributesSelected.Contains(nme) && isChecked == true) // if box has been checked, adds to checked attributes list
                whatOtherAttributesSelected.Add(nme);
            updateAttributes();
        }

        private void storeWhichCostAttributes(string nme, bool isChecked, string valu)
        {
            if (whatCostAttributesSelected.ContainsKey(nme) && isChecked == false) // if box has been unchecked, removes from checked attributes list
                whatCostAttributesSelected.Remove(nme);
            else if (!whatCostAttributesSelected.ContainsKey(nme) && isChecked == true) // if box has been checked, adds to checked attributes list
                whatCostAttributesSelected.Add(nme, valu);
            updateAttributes();
        }

        private void oneMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (oneMoneyBox.IsChecked == true)
                check = true;

            storeWhichCostAttributes("oneMoneyBox", check, "1");
        }

        private void twoMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (twoMoneyBox.IsChecked == true)
                check = true;

            storeWhichCostAttributes("twoMoneyBox", check, "2");
        }

        private void threeMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (threeMoneyBox.IsChecked == true)
                check = true;

            storeWhichCostAttributes("threeMoneyBox", check, "3");
        }

        private void fourMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (fourMoneyBox.IsChecked == true)
                check = true;

            storeWhichCostAttributes("fourMoneyBox", check, "4");
        }



        private void acceptsCardBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (acceptsCardBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("BusinessAcceptsCreditCards", check);
        }

        private void takesReservBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (takesReservBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("RestaurantsReservations", check);
        }

        private void wheelchairBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (wheelchairBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("WheelchairAccessible", check);
        }

        private void outdoorSeatingBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (outdoorSeatingBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("OutdoorSeating", check);
        }

        private void kidsBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (kidsBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("GoodForKids", check);
        }

        private void groupsBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (groupsBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("RestaurantsGoodForGroups", check);
        }

        private void deliveryBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (deliveryBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("RestaurantsDelivery", check);
        }

        private void takeOutBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (takeOutBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("RestaurantsTakeOut", check);
        }

        private void wifiBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (wifiBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("WiFi", check);
        }

        private void bikeParkingBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (bikeParkingBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("BikeParking", check);
        }

        private void breakfastBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (breakfastBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("breakfast", check);
        }

        private void lunchBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (brunchBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("brunch", check);
        }

        private void brunchBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (brunchBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("lunch", check);
        }

        private void dinnerBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (dinnerBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("dinner", check);
        }

        private void dessertBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (dessertBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("desert", check);
        }

        private void lateNightBox_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (lateNightBox.IsChecked == true)
                check = true;

            storeWhichOtherAttributes("latenight", check);
        }
    }
}
