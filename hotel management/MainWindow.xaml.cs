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
using System.Data;
using System.Data.SqlClient;

namespace hotel_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            register reg = new register();
            reg.Show();
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(connectionstring);
            sqlcon.Open();
            string commandstring = "select name,password from users";
            SqlCommand sqlcmd = new SqlCommand(commandstring, sqlcon);
            SqlDataReader read = sqlcmd.ExecuteReader();
            int i = 0;
            while (read.Read())
            {
                if (read[0].ToString() == mw_uid.Text && read[1].ToString() == mw_pass.Password)
                {
                    i = 1;
                    Home h = new Home();
                    h.Show();
                    this.Close();
                }


            }
            if (i == 0)
            {
                MessageBox.Show("Incorrect data", "Error");
            }

            sqlcon.Close();
        }
    }
}
