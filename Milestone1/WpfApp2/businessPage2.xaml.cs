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
    /// Interaction logic for businessPage2.xaml
    /// </summary>
    public partial class businessPage2 : Page
    {
        Business currentBus;
        User currentUser;


    
        public businessPage2(ref Business B, ref User U)
        {

            currentBus = B;

            currentUser= U;

            InitializeComponent();
            addState();
            addColumn2Grid();

        }
        private string buildConnectionString()
        {
            return "Server = localhost; Username = postgres; Password = cpts451; Database = milestone2";
        }

        private void addState()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM public.business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            state.Items.Add(reader.GetString(0));
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

            updateCategories();
        }

        private void updateCategories()
        {
            business_categories.Items.Clear();

            String cmdstr = "SELECT business_id FROM public.business";
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    if (state.SelectedItem != null)
                    {
                        cmdstr += " WHERE state = '" + state.SelectedItem.ToString() + "'";
                    }
                    if (cityList.SelectedItem != null)
                    {
                        cmdstr += " AND city = '" + cityList.SelectedItem.ToString() + "'";
                    }
                    if (city_zipcodes.SelectedItem != null)
                    {
                        cmdstr += " AND zipcode = '" + city_zipcodes.SelectedItem.ToString() + "'";
                    }
                    cmd.CommandText = "Select Distinct category from categories where business_id IN( " + cmdstr + ")";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            business_categories.Items.Add(reader.GetString(0));                    //Buisness_Grif.Items.Add(new Buisness() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2) });

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


        private void addColumn2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Business Name";
            col1.Binding = new Binding("name");

            col1.Width = 50;
            businessesList.Columns.Add(col1);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "State";
            col4.Binding = new Binding("state");

            col4.Width = 50;
            businessesList.Columns.Add(col4);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Address";
            col2.Binding = new Binding("address");
            col2.Width = 50;
            businessesList.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "City";
            col3.Binding = new Binding("city");
            col3.Width = 50;
            businessesList.Columns.Add(col3);



            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Distance";
            col5.Binding = new Binding("distance");
            col5.Width = 50;
            businessesList.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Stars";
            col6.Binding = new Binding("stars");
            col6.Width = 50;
            businessesList.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "# of Tip";
            col7.Binding = new Binding("num_tips");
            col7.Width = 60;
            businessesList.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "Num Checkins";
            col8.Binding = new Binding("num_checkins");
            col8.Width = 100;
            businessesList.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Header = "bid";
            col9.Binding = new Binding("bid");
            col9.Width = 100;
            businessesList.Columns.Add(col9);

        }

        private void state_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateCategories();

            cityList.Items.Clear();
            if (state.SelectedIndex > -1)
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT distinct city FROM public.business WHERE state = '" + state.SelectedItem.ToString() + "' ORDER BY city";
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                cityList.Items.Add(reader.GetString(0));
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

        private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateCategories();

            city_zipcodes.Items.Clear();
            if (cityList.SelectedIndex > -1)
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {

                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT distinct zipcode FROM public.business WHERE state= '" + state.SelectedItem.ToString() + "' AND city= '" + cityList.SelectedItem.ToString() + "'ORDER BY zipcode";
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                                city_zipcodes.Items.Add(reader.GetInt32(0).ToString());
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

        private void city_zipcodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateCategories();

        }

        private void business_categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void selectedCatagoriesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AttributeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        private string getCmdString()
        {
            String cmdstr = "";
            if (selectedCatagoriesBox.Items.Count > 0)
            {
                cmdstr = "SELECT distinct name,state,city,business_id,address,latitude,longitude, stars,numtips,numcheckins,is_open,latitude,longitude FROM public.business where business_id IN (select business_ID from categories where category In(";
                for (int i = 0; i < selectedCatagoriesBox.Items.Count; i++)
                {
                    cmdstr += "'" + selectedCatagoriesBox.Items[i] + "'";
                    if (selectedCatagoriesBox.Items.Count - 1 > i)
                    {
                        cmdstr += ",";
                    }
                }
                cmdstr += "))";

            }

            else
            {
                cmdstr = "SELECT  distinct name,state,city,business_id,address,latitude,longitude, stars,numtips,numcheckins,is_open,latitude,longitude  FROM public.business where ''=''";
            }


                if (price1.IsChecked == true)
                {
                    cmdstr += "AND business_id IN( select business_id from attributes where attributes_key='RestaurantsPriceRange2' AND attribute='1')";
                }
                else if (price2.IsChecked == true)
                {
                    cmdstr += "AND business_id IN( select business_id from attributes where attributes_key='RestaurantsPriceRange2' AND attribute='2')";
                }
                else if (price3.IsChecked == true)
                {
                    cmdstr += "AND business_id IN( select business_id from attributes where attributes_key='RestaurantsPriceRange2' AND attribute='3')";
                }
                else if (price4.IsChecked == true)
                {
                    cmdstr += "AND business_id IN( select business_id from attributes where attributes_key='RestaurantsPriceRange2' AND attribute='4')";
                }

            if (creditcard.IsChecked == true)
            {
                cmdstr += "AND business_id IN (select business_id from attributes where attributes_key = 'BusinessAcceptsCreditCards' AND attribute = 'True')";
            }
             if (Reservation.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='RestaurantsReservations' AND attribute='True')";
            }
            if (Wheelchair.IsChecked == true)
            {
                cmdstr += "AND business_id IN (  select business_id from attributes where attributes_key='WheelchairAccessible' AND attribute='True')";

            }
            if (Outdoors.IsChecked == true)
            {
                cmdstr += "AND business_id IN (   select business_id from attributes where attributes_key='OutdoorSeating' AND attribute='True')";

            }
            if (Kids.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='GoodForKids' AND attribute='True')";

            }
            if (Groups.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='RestaurantsGoodForGroups' AND attribute='True')";
                          }

            if (Delivery.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='RestaurantsDelivery' AND attribute='True')";

            }

            if (TakeOut.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='WiFi' AND attribute='free')";

            }
            if (Bike.IsChecked == true)
            {
                cmdstr += "AND business_id IN (  select business_id from attributes where attributes_key='BikeParking' AND attribute='True')";

            }

            if (Breakfast.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='breakfast' AND attribute='True')";

            }

            if(Brunch.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='brunch' AND attribute='True')";

            }
            if (Lunch.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='lunch' AND attribute='True')";

            }
            if (Dinner.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='dinner' AND attribute='True')";
            }
            if (Dessert.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='dessert' AND attribute='True')";
            }
            if (LateNight.IsChecked == true)
            {
                cmdstr += "AND business_id IN ( select business_id from attributes where attributes_key='latenight' AND attribute='True')";

            }

            if (state.SelectedItem != null)
            {
                cmdstr += " And state = '" + state.SelectedItem.ToString() + "'";
            }
            if (cityList.SelectedItem != null)
            {
                cmdstr += " AND city = '" + cityList.SelectedItem.ToString() + "'";
            }
            if (city_zipcodes.SelectedItem != null)
            {
                cmdstr += " AND zipcode = '" + city_zipcodes.SelectedItem.ToString() + "'";
            }

            if (SortBy.SelectedIndex == 0)
            {
                cmdstr += "order by name ASC";
            }
            else if (SortBy.SelectedIndex == 1)
            {
                cmdstr += "order by stars Desc";
            }
            else if (SortBy.SelectedIndex == 2)
            {
                cmdstr += "order by numtips Desc";
            }
            else if (SortBy.SelectedIndex == 3)
            {
                cmdstr += "order by numcheckins Desc";

            }
            else if (SortBy.SelectedIndex == 4)
            {
                //distance
            }



            return cmdstr;
        }


        private double calcDistance(double lat1,double long1,double lat2, double long2)
        {
            double ret = Math.Abs( Math.Pow((Math.Pow((lat2 - lat1), 2.0)+ Math.Pow((long2-long1),2.0)),.5));



            return ret;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            businessesList.Items.Clear();

        

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    cmd.CommandText = getCmdString();
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            businessesList.Items.Add(new Business() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2), bid = reader.GetString(3), address=reader.GetString(4),distance=calcDistance(reader.GetDouble(10),reader.GetDouble(11),currentUser.latitude,currentUser.longitude),stars=reader.GetDouble(7), num_tips=reader.GetInt32(8), num_checkins=reader.GetInt32(9), isOpen=reader.GetInt32(10),lattitude=reader.GetDouble(11),longitude=reader.GetDouble(12) });                            //Buisness_Grif.Items.Add(new Buisness() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2) });

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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (business_categories.SelectedItem.ToString() != "")
            {
                selectedCatagoriesBox.Items.Add(business_categories.SelectedItem.ToString());

            }

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCatagoriesBox.SelectedItem.ToString() != "")
            {
                selectedCatagoriesBox.Items.Remove(selectedCatagoriesBox.SelectedItem);
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //show tips
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            new ViewTips(ref currentUser,ref currentBus).Show();

  

        }

        private void businessesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentBus = businessesList.Items[businessesList.SelectedIndex] as Business;

            BusinessName.Text = currentBus.name;
            Address.Text = currentBus.address;
            
            if (currentBus.isOpen == 0)
            {
                OpenHours.Text = "Closed";
            }
            else
            {
                OpenHours.Text = "open";

            }

            populateAttributesAndCatagories();

        }

        private void showcheckins_Click(object sender, RoutedEventArgs e)
        {
            new ViewCheckIns(ref currentBus).Show();
        }


       private void populateAttributesAndCatagories()
        {

            AttCat.Items.Clear();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    cmd.CommandText = "select distinct attributes_key from attributes where attribute='True' OR attribute='free' AND Business_id='" + currentBus.bid + "'";
                    AttCat.Items.Add("ATTRIBUTES");
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            AttCat.Items.Add(reader.GetString(0));

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


                connection.Open();
                using (var cmd1 = new NpgsqlCommand())
                {
                    cmd1.Connection = connection;

                    cmd1.CommandText = "select category from categories where business_id='" + currentBus.bid + "'";
                    AttCat.Items.Add("CATAGORIES");
                    try
                    {
                        var reader1 = cmd1.ExecuteReader();
                        while (reader1.Read())
                            AttCat.Items.Add(reader1.GetString(0));

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

        private void AddTipButton_Click(object sender, RoutedEventArgs e)
        {
           }

        private void price1_Checked(object sender, RoutedEventArgs e)
        {
            price2.IsChecked = false;
            price3.IsChecked = false;
            price4.IsChecked = false;
        }

        private void price2_Checked(object sender, RoutedEventArgs e)
        {

            price1.IsChecked = false;
            price3.IsChecked = false;
            price4.IsChecked = false;
        }

        private void price3_Checked(object sender, RoutedEventArgs e)
        {
            
            price1.IsChecked = false;
            price2.IsChecked = false;
            price4.IsChecked = false;
        }

        private void price4_Checked(object sender, RoutedEventArgs e)
        {

            price2.IsChecked = false;
            price3.IsChecked = false;
            price1.IsChecked = false;
        }

        private void SortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
