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

        public MainWindow()
        {
            InitializeComponent();
            addFriendsDataColums2Grid();
            addFriendsTipDataColums2Grid();
            addColums2Grid();
            addStates();
            addSortResultsList();
            friendRecommendationsButton.IsEnabled = false;
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

            //DataGridTextColumn col8 = new DataGridTextColumn();
            //col8.Binding = new Binding("totalCheckins");
            //col8.Header = "Total Checkins";
            //col8.Width = 60;
            //businessGrid.Columns.Add(col8);

            //DataGridTextColumn col9 = new DataGridTextColumn();
            //col9.Binding = new Binding("bid");
            //col9.Header = "";
            //col9.Width = 0;
            //businessGrid.Columns.Add(col9);
        }

        private void addSortResultsList()
        {
            sortResultsList.Items.Add("Name");
            sortResultsList.Items.Add("Highest rated");
            sortResultsList.Items.Add("Most number of tips");
            //sortResultsList.Items.Add("Most checkins");
            sortResultsList.Items.Add("Nearest");
        }

        private void inputUserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listBox.SelectedIndex == -1 || inputUserTextBox.Text == "")
                friendRecommendationsButton.IsEnabled = false;
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
            if(listBox.SelectedIndex != -1)
                friendRecommendationsButton.IsEnabled = true;
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
                                friendsTipsDataGrid.Items.Add(new Tip
                                {
                                    user_name = reader.GetString(1),
                                    business_name = reader.GetString(2),
                                    city = reader.GetString(3),
                                    tipText = reader.GetString(4),
                                    tipDate = date.ToString()
                                });
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

            if (latTxt.Text != "" && longTxt.Text != "")
            {
                curUser.latitude = Convert.ToDouble(latTxt.Text);
                curUser.longitude = Convert.ToDouble(longTxt.Text);
            }
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

        private List<Business> calDistance(Business bis)
        {
            List<Business> listBusinesses = new List<Business>();
            if (curUser.latitude != 0.0 && curUser.longitude != 0.0) // if user latitude exists
            {
                using (var comm = new NpgsqlConnection(buildConnectionString()))
                {
                    comm.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = comm;

                        string buisLoc = "(SELECT latitude, longitude FROM business " +
                            "WHERE state ='" + StateList.SelectedItem.ToString() + "' and city ='" + CityList.SelectedItem.ToString() + "' " +
                            "and zipcode = '" + ZipList.SelectedItem.ToString() + "' and business.business_id = '" + bis.bid + "' ) as busi";

                        string dis = "(SELECT 2 * 3961 * asin(sqrt((sin(radians((" + curUser.latitude + " - LOC1.latitude) / 2))) ^ 2 " +
                            "+ cos(radians(LOC1.latitude)) * cos(radians(" + curUser.latitude + ")) * " +
                            "(sin(radians((" + curUser.longitude + " - LOC1.longitude) / 2))) ^ 2)) as DISTANCE FROM (SELECT latitude, longitude FROM " +
                            buisLoc.ToString() + ") as LOC1) as dis";

                        cmd.CommandText = "SELECT dis.* FROM " + dis;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int i = businessGrid.Items.IndexOf(bis);
                                if (i != -1) 
                                    businessGrid.Items.Remove(bis);
                                bis.distance = Math.Round(reader.GetDouble(0), 2);
                                listBusinesses.Add(bis);
                            }
                        }
                        comm.Close();
                    }
                }
            }
            return listBusinesses;
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
            updateBusinessGridWithAttributes();
        }

        private void insertCategoriesAttribute(Business B)  // inserts categories into selected business category box
        {

            selectedBusinessDetailsListBox.Items.Clear();
            using (var comm = new NpgsqlConnection(buildConnectionString()))
            {
                List<string> cat = new List<string>();
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
                            cat.Add(reader.GetString(1));
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
            updateBusinessGridWithAttributes();
        }

        private void showReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1 && curUser.userID != "")
            {
                BusinessReviews reviews = new BusinessReviews((Business)businessGrid.SelectedItem, curUser);
                reviews.Show();
            }
            else if(businessGrid.SelectedIndex > -1 && curUser.userID == "")
            {
                MessageBox.Show("Must Select User");
            }
        }

        private string sortingBy()
        {
            string sortBy = " ORDER BY name ASC";
            if (sortResultsList.SelectedIndex > -1) // if sorting selection changed in drop-down menu
            {
                switch (sortResultsList.SelectedItem.ToString())
                {
                    case "Name":
                        sortBy = " ORDER BY name ASC";
                        break;
                    case "Highest rated":
                        sortBy = " ORDER BY stars DESC";
                        break;
                    case "Most number of tips":
                        sortBy = " ORDER BY numtips DESC";
                        break;
                    //case "Most checkins":
                    //    sortBy = " ORDER BY numcheckins DESC";
                    //    break;
                    case "Nearest":
                        sortBy = " ";
                        break;
                    default:
                        break;
                }
            }
            return sortBy;
        }
        private string getAttributes_Price()
        {
            string cmdstr = "";
            if (oneMoneyBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsPriceRange2' AND attribute='1')";

            else if (twoMoneyBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsPriceRange2' AND attribute='2')";

            else if (threeMoneyBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsPriceRange2' AND attribute='3')";

            else if (fourMoneyBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsPriceRange2' AND attribute='4')";

            return cmdstr;
        }

        private string getAttributes_Main()
        {
            string cmdstr = "";
            if (acceptsCardBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key = 'BusinessAcceptsCreditCards' AND attribute = 'True')";

            if (takesReservBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsReservations' AND attribute='True')";

            if (wheelchairBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='WheelchairAccessible' AND attribute='True')";

            if (outdoorSeatingBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='OutdoorSeating' AND attribute='True')";

            if (kidsBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='GoodForKids' AND attribute='True')";

            if (groupsBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsGoodForGroups' AND attribute='True')";

            if (deliveryBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='RestaurantsDelivery' AND attribute='True')";

            if (takeOutBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='WiFi' AND attribute='free')";

            if (bikeParkingBox.IsChecked == true)
                cmdstr += " AND business_id IN (SELECT business_id FROM attributes WHERE attribute_key='BikeParking' AND attribute='True')";

            return cmdstr;
        }

        //private string getAttributes_Meal()
        //{
        //    string cmdstr = "";

        //    if (breakfastBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='breakfast' AND attribute='True')";

        //    if (brunchBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='brunch' AND attribute='True')";

        //    if (lunchBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='lunch' AND attribute='True')";

        //    if (dinnerBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='dinner' AND attribute='True')";

        //    if (dessertBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='dessert' AND attribute='True')";

        //    if (lateNightBox.IsChecked == true)
        //        cmdstr += " AND business_id IN ( select business_id from attributes where attribute_key='latenight' AND attribute='True')";

        //    return cmdstr;
        //}

        private string getAttributes()
        {
            string cmdstr = "";
            cmdstr += getAttributes_Price();
            cmdstr += getAttributes_Main();
            //cmdstr += getAttributes_Meal();

            return cmdstr;
        }

        private string getCommandStr()
        {
            String cmdstr = "";
            if (categorySelectedListBox.Items.Count > 0) // if 1+ categories selected
            {
                cmdstr = "SELECT DISTINCT * FROM business where business_id IN (SELECT business_ID FROM categories WHERE category IN (";

                for (int i = 0; i < categorySelectedListBox.Items.Count; i++)
                {
                    cmdstr += "'" + categorySelectedListBox.Items[i] + "'";
                    if (categorySelectedListBox.Items.Count - 1 > i) // if multiple categories selected
                        cmdstr += ", ";
                }
                cmdstr += ")) AND";
            }

            else                                                // if no categories selected
                cmdstr = "SELECT DISTINCT * FROM business WHERE ";

            cmdstr += " state = '" + StateList.SelectedItem.ToString() + "'";
            cmdstr += " AND city = '" + CityList.SelectedItem.ToString() + "'";
            cmdstr += " AND zipcode = '" + ZipList.SelectedItem.ToString() + "'";

            cmdstr += getAttributes(); // sees if any attributes are selected
            cmdstr += sortingBy();  // sort by name, most tips, most checkins, distance, Highest rated

            return cmdstr;
        }


        private void updateBusinessGridWithAttributes()
        {
            selectedBusinessDetailsListBox.Items.Clear();

            if (CityList.SelectedIndex > -1 && StateList.SelectedIndex > -1 && ZipList.SelectedIndex > -1)
            {
                businessGrid.Items.Clear();

                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = getCommandStr();

                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int numTips = reader.GetInt32(9);
                                Business b = new Business()
                                {
                                    bid = reader.GetString(0),
                                    name = reader.GetString(1),
                                    address = reader.GetString(2),
                                    city = reader.GetString(3),
                                    state = reader.GetString(4),
                                    zip = reader.GetString(5),
                                    lat = reader.GetDouble(6),
                                    lon = reader.GetDouble(7),
                                    star = reader.GetDouble(8),
                                    //totalCheckins = reader.GetInt32(11),
                                    numTips = numTips
                                };
                                List<Business> listBusinesses = calDistance(b);

                                if (b.numTips < 1)
                                    b.numTips = getNumTips(b.bid);

                                businessGrid.Items.Add(b);
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

                numOfBusinesses.Content = "# of businesses: " + businessGrid.Items.Count.ToString();

                if (sortResultsList.SelectedIndex != -1)
                    if (sortResultsList.SelectedItem.ToString() == "Nearest")
                        sortByDistance();
            }
        }

        private void insertNumTips(string bid, int count) // inserts updated tip count for business in db
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

        private int getNumTips(string bid)  // counts number of tips for business and updates db value
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

            if (total > 0)
                insertNumTips(bid, total);

            return total;
        }

        private void oneMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void twoMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void threeMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void fourMoneyBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void acceptsCardBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void takesReservBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void wheelchairBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void outdoorSeatingBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void kidsBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void groupsBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void deliveryBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void takeOutBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void wifiBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void bikeParkingBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void breakfastBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void lunchBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void brunchBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void dinnerBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void dessertBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void lateNightBox_Click(object sender, RoutedEventArgs e)
        {
            updateBusinessGridWithAttributes();
        }

        private void friendRecommendationsButton_Click(object sender, RoutedEventArgs e)
        {
            FriendRecommendations freindRec = new FriendRecommendations(curUser);
            freindRec.Show();
        }
    }
}
