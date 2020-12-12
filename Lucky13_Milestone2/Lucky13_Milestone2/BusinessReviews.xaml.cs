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
            minRatingList.Items.Add("1");
            minRatingList.Items.Add("2");
            minRatingList.Items.Add("3");
            minRatingList.Items.Add("4");
            //minRatingList.Items.Add("5");
            limRecList.Items.Add("5");
            limRecList.Items.Add("10");
            limRecList.Items.Add("15");
            limRecList.Items.Add("20");
            //minReviewsList.Items.Add("10");
            //minReviewsList.Items.Add("20");
            //minReviewsList.Items.Add("30");
            //minReviewsList.Items.Add("40");
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
            col3.Width = 335;
            recommendationsDataGrid.Columns.Add(col3);
        }

        private void loadBusinessDetails()
        {
            busName.Text = this.selectedBusiness.name;
        }

        private int getMinStars()
        {
            List<int> min = new List<int>() { 1, 2, 3, 4 };
            if (minRatingList.SelectedIndex > -1) // if  selection changed in drop-down menu
            {
                return min[minRatingList.SelectedIndex];
            }
            return 3;
        }

        private int getLimit()
        {
            List<int> limits = new List<int>() { 5, 10, 15, 20 };
            if (limRecList.SelectedIndex > -1) // if  selection changed in drop-down menu
            {
                return limits[limRecList.SelectedIndex];
            }
            return 5;
        }

        //private int getMinReviews()
        //{
        //    List<int> min = new List<int>() { 10, 20, 30, 40 };
        //    if (minRatingList.SelectedIndex > -1) // if  selection changed in drop-down menu
        //    {
        //        return min[minRatingList.SelectedIndex];
        //    }
        //    return 10;
        //}

        private void loadRecommendations()
        {
            List<BusinessRecommendations> recommendations = new List<BusinessRecommendations>();
            recommendationsDataGrid.Items.Clear();

            double minStars = getMinStars();
            //int minReviews = getMinReviews();
            string limit = "LIMIT " + getLimit();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM (SELECT DISTINCT business.name, SUM(review.stars)/COUNT(review.stars) AS average_rating, COUNT(review.stars) AS reviews " +
                        "FROM business, review, (SELECT DISTINCT review.user_id, review.business_id, review.stars, review.useful FROM review, " +
                        "(SELECT review.* FROM users, review WHERE review.user_id = users.user_id AND users.user_id LIKE '" + currentUser.userID + "' AND review.stars >" + minStars + ") " +
                        "AS user_reviewed_business WHERE review.business_id = user_reviewed_business.business_id AND review.stars > " + minStars + ") AS users_who_have_reviewed_the_same_business " +
                        "WHERE review.user_id = users_who_have_reviewed_the_same_business.user_id AND business.business_id = review.business_id " +
                        "AND review.business_id != users_who_have_reviewed_the_same_business.business_id GROUP BY business.name) AS recommended_business " +
                        " WHERE recommended_business.average_rating > " + minStars + " AND recommended_business.reviews > " + 1 +
                        " ORDER BY recommended_business.average_rating * recommended_business.reviews DESC " + limit;

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var data = new BusinessRecommendations()
                            {

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
        
        private DateTime convertToDate(int d, int m, int y, int hr, int min, int sec)
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
                    cmd.CommandText = "SELECT * FROM review where  business_id='" + selectedBusiness.bid + "' LIMIT 20";

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

        private void tipGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void minRatingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadRecommendations();
        }

        private void limRecList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadRecommendations();
        }

        //private void minReviewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    loadRecommendations();
        //}
    }
}
