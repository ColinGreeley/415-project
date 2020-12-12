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
    /// Interaction logic for FriendRecommendations.xaml
    /// </summary>
    public partial class FriendRecommendations : Window
    {
        curUserSelected currentUser;
        List<Friend> recommendations = new List<Friend>();
        public FriendRecommendations(curUserSelected user)
        {
            InitializeComponent();
            currentUser = user;
            addColums2Grid();
            getFriendRecommendations();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 415Project; password = 605027";
        }

        private void addColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("friend_name");
            col1.Header = "Name";
            col1.Width = 300;
            recommendationsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("friend_count");
            col2.Header = "# of Friends";
            col2.Width = 428;
            recommendationsDataGrid.Columns.Add(col2);
        }

        private void getFriendRecommendations()
        {
            recommendations.Clear();
            recommendationsDataGrid.Items.Clear();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    cmd.CommandText = "select users.name, users.user_id, recommended_friends.friend_count from users, " +
                        "(select distinct friends.friend_id as user_id, count(friends.friend_id) as friend_count from friends, " +
                        "(select * from friends where friends.user_id like '" + currentUser.userID + "') as user_friends " +
                        "where friends.user_id = user_friends.friend_id and friends.friend_id not like '" + currentUser.userID +
                        "' group by friends.friend_id) as recommended_friends " +
                        "where users.user_id = recommended_friends.user_id order by recommended_friends.friend_count desc limit 10";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Friend()
                            {
                                friend_name=reader.GetString(0),
                                friend_id=reader.GetString(1),
                                friend_count=reader.GetInt32(2)
                            };
                            recommendations.Add(data);
                        }
                        foreach (var recommendation in recommendations)
                        {
                            recommendationsDataGrid.Items.Add(recommendation);
                        }
                    }
                }
            }
        }

        private void addFriendButton_Click(object sender, RoutedEventArgs e)
        {
            //int index = recommendationsDataGrid.SelectedIndex;
            //Friend selected = recommendations[index];
            //using (var connection = new NpgsqlConnection(buildConnectionString()))
            //{
            //    connection.Open();
            //    using (var cmd = new NpgsqlCommand())
            //    {

            //        var te = new UserTips() { user_name = currentUser.name, likes = 0, tipText = tipTextBox.Text, day = DateTime.Now.Day, month = DateTime.Now.Month, year = DateTime.Now.Year, hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second };
            //        cmd.Connection = connection;

            //        cmd.CommandText = "INSERT INTO tip(user_id, business_id, tip_text, likes, day, month, year, hour, minute, second) VALUES('" +
            //            currentUser.userID + "', '" + selectedBusiness.bid + "',  '" + te.tipText + "', " + te.likes.ToString() + ",  " + te.day.ToString() +
            //            ", " + te.month.ToString() + ", " + te.year.ToString() + ", " + te.hour.ToString() + ", " + te.minute.ToString() + ", " + te.second.ToString() + ");";

            //        try
            //        {
            //            cmd.ExecuteNonQuery();
            //            te.date = new DateTime(te.year, te.month, te.day, te.hour, te.minute, te.second);
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
        }
    }
}
