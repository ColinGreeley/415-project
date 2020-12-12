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
    /// Interaction logic for BusinessReviews.xaml
    /// </summary>
    public partial class BusinessReviews : Window
    {
        Business selectedBusiness;
        curUserSelected currentUser;
        public BusinessReviews(Business b, curUserSelected user)
        {
            InitializeComponent();
            this.selectedBusiness = b;
            currentUser = user;
            addColums2Grid();
            addFriendsColums2Grid();
            loadBusinessDetails();
            addReviews();
            loadRecommendations();
            addSortResultsList();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 415Project; password = 605027";
        }
        private void addSortResultsList()
        {
            minRatingList.Items.Add("0");
            minRatingList.Items.Add("1");
            minRatingList.Items.Add("2");
            minRatingList.Items.Add("3");
            minRatingList.Items.Add("4");
            minRatingList.Items.Add("5");
        }

        private void addColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("date");
            col1.Header = "Date";
            col1.Width = 170;
            reviewGrid.Columns.Add(col1);

            //DataGridTextColumn col2 = new DataGridTextColumn();
            //col2.Binding = new Binding("stars");
            //col2.Header = "Stars";
            //col2.Width = 100;
            //reviewGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("stars");
            col3.Header = "Stars";
            col3.Width = 50;
            reviewGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("useful");
            col4.Header = "Useful";
            col4.Width = 50;
            reviewGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("funny");
            col5.Header = "Funny";
            col5.Width = 50;
            reviewGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("cool");
            col6.Header = "Cool";
            col6.Width = 50;
            reviewGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("text");
            col7.Header = "Text";
            col7.Width = 680;
            reviewGrid.Columns.Add(col7);
        }

        private void addFriendsColums2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Name";
            col1.Width = 200;
            recommendationsDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("average_rating");
            col2.Header = "Average Rating";
            col2.Width = 150;
            recommendationsDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("reviews");
            col3.Header = "Reviews";
            col3.Width = 664;
            recommendationsDataGrid.Columns.Add(col3);
        }

        private void loadBusinessDetails()
        {
            busName.Text = this.selectedBusiness.name;
        }

        private double getSortingValues()
        {
            double minStars = Convert.ToDouble(minRatingList.SelectedIndex);

            if (minStars == -1)
                minStars = 0.0;

            return minStars;
        }

        private void loadRecommendations()
        {
            List<BusinessRecommendations> recommendations = new List<BusinessRecommendations>();
            recommendationsDataGrid.Items.Clear();

            double minStars = getSortingValues();
            int minReviews = 0;
            string limit = "LIMIT 5";

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    cmd.CommandText = "SELECT * FROM (SELECT DISTINCT business.name, SUM(review.stars)/COUNT(review.stars) AS average_rating, COUNT(review.stars) AS reviews " +
                        "FROM business, review, (SELECT DISTINCT review.user_id, review.business_id, review.stars, review.useful FROM review, " +
                        "(SELECT review.* FROM users, review WHERE review.user_id = users.user_id AND users.user_id LIKE '" + currentUser.userID + "' AND review.stars > + " + minStars + ") " +
                        "AS user_reviewed_business WHERE review.business_id = user_reviewed_business.business_id AND review.stars > " + minStars + ") AS users_who_have_reviewed_the_same_business " +
                        "WHERE review.user_id = users_who_have_reviewed_the_same_business.user_id AND business.business_id = review.business_id " +
                        "AND review.business_id != users_who_have_reviewed_the_same_business.business_id GROUP BY business.name) AS recommended_business " +
                        " WHERE recommended_business.average_rating > " + minStars + " AND recommended_business.reviews > " + minReviews +
                        " ORDER BY recommended_business.average_rating * recommended_business.reviews DESC " + limit;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new BusinessRecommendations() { 
                            
                                name = reader.GetString(0),
                                average_rating = reader.GetDouble(1),
                                reviews = reader.GetInt32(2)
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

        private DateTime convertToDate(int y, int m, int d, int hr, int min, int sec)
        {
            string date = y.ToString() + "-" + m.ToString() + "-" + d.ToString() + " " + hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            return Convert.ToDateTime(date);
        }

        private void addReviews()
        {
            List<Review> reviews = new List<Review>();
            reviewGrid.Items.Clear();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM review where user_id='" + currentUser.userID + "' AND business_id='" + selectedBusiness.bid + "'";

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DateTime dat = convertToDate(reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13));
                            var data = new Review()
                            {

                                rid = reader.GetString(0),
                                stars = reader.GetDouble(3),
                                useful = reader.GetInt32(4),
                                funny = reader.GetInt32(5),
                                cool = reader.GetInt32(6),
                                text = reader.GetString(7),
                                date = dat
                            };
                            //reviewGrid.Add(data);
                            // tipGrid.Items.Add(data);
                            reviews.Add(data);
                        }
                        reviews.Sort((x, y) => y.date.CompareTo(x.date));
                        foreach (var review in reviews)
                        {
                            reviewGrid.Items.Add(review);
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

        private void tipGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void minRatingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadRecommendations();
        }
    }
}
