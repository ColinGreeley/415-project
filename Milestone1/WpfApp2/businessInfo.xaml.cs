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
using Npgsql;

using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for businessInfo.xaml
    /// </summary>
    /// 

    //public class Tip
    //{
    //    public string user { get; set; }

    //    public string tip { get; set; }
    //}


 


    public partial class businessInfo : Window
    {
        private void addColum2Grid()
        {

            DataGridTextColumn col1 = new DataGridTextColumn();

            col1.Header = "user id";
            col1.Width = 255;
            col1.Binding = new Binding("user");
            TipdataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();

            col2.Header = "tip";
            col2.Width = 500;
            col2.Binding = new Binding("tip");
            TipdataGrid.Columns.Add(col2);





        }
        private string buildConnectionString()
        {
            return "Server = localhost; Username = postgres; Database = milestone2.2; password=1234";
        }
        public businessInfo(string id)
        {

            InitializeComponent();

            addColum2Grid();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT  name,address,numtips,stars,numcheckins FROM public.business WHERE  business_id = '" + id + "'";

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        reader.Read();
                            NametextBox.Text = reader.GetString(0);
                        AddresstextBlock.Text = reader.GetString(1);
                        TipAmttextBlock1.Text = reader.GetInt16(2).ToString();
                        StarstextBox1.Text = reader.GetDouble(3).ToString();
                        BusIDtextBlock.Text = id;




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
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT  userid,tiptext FROM public.tip WHERE  businessid = '" + id + "'";

                    try
                    {
                        //var reader = cmd.ExecuteReader();
                        //while (reader.Read())
                        //    TipdataGrid.Items.Add(new Tip() { user = reader.GetString(0), tip = reader.GetString(1)});




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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submitbutton_Click(object sender, RoutedEventArgs e)
        {

            DateTime now = DateTime.Now;
            

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO tip(businessid,year,month,day,hour,minute,second,likes,tiptext,userid) Values('" + BusIDtextBlock.Text +
                        "'," + now.Year.ToString() + "," + now.Month.ToString() + "," + now.Day.ToString() + "," + now.Hour.ToString() + "," + now.Minute.ToString() + "," + now.Second.ToString() + ",0,'" + InputtextBox.Text + "','" + IDtextBlock.Text + "')";

                  


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
                connection.Open();
                    using (var cmd1 = new NpgsqlCommand())
                    {
                    TipdataGrid.Items.Clear();
                        cmd1.Connection = connection;
                        cmd1.CommandText = "SELECT  userid,tiptext FROM public.tip WHERE  businessid = '" + BusIDtextBlock.Text + "'";

                        try
                    {
                        //    var reader = cmd1.ExecuteReader();
                        //    while (reader.Read())
                        //        TipdataGrid.Items.Add(new Tip() { user = reader.GetString(0), tip = reader.GetString(1) });




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
                using (var cmd2 = new NpgsqlCommand())
                {

                    cmd2.Connection = connection;
                    cmd2.CommandText = "SELECT  name,address,numtips,stars,numcheckins FROM public.business WHERE  business_id = '" + BusIDtextBlock.Text + "'";

                    try
                    {
                        var reader = cmd2.ExecuteReader();
                        reader.Read();
                        NametextBox.Text = reader.GetString(0);
                        AddresstextBlock.Text = reader.GetString(1);
                        TipAmttextBlock1.Text = reader.GetInt16(2).ToString();
                        StarstextBox1.Text = reader.GetDouble(3).ToString();




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
