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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User currentUser = new User();
        public Business currentBusiness =new Business();

    

        public MainWindow()
        {
            InitializeComponent();
            
        }

   
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UserPage2(ref currentBusiness, ref currentUser);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new businessPage2(ref currentBusiness, ref currentUser);
        }
    }

    
}
