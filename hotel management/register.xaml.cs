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
using System.Data;
using System.Data.SqlClient;

namespace hotel_management
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Window
    {
        public register()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(connectionstring);

            SqlCommand cmd = new SqlCommand("insert into users(name,uid,email,phone,password) values(@a,@b,@c,@d,@e)", sqlcon);

            cmd.Parameters.Add("@a", SqlDbType.VarChar).Value = reg_name.Text;
            cmd.Parameters.Add("@b", SqlDbType.VarChar).Value = reg_id.Text;
            cmd.Parameters.Add("@c", SqlDbType.VarChar).Value = reg_email.Text;
            cmd.Parameters.Add("@d", SqlDbType.VarChar).Value = reg_phone.Text;
            cmd.Parameters.Add("@e", SqlDbType.VarChar).Value = reg_pass.Text;

            sqlcon.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                MessageBox.Show("Registration Successfull!");
            sqlcon.Close();
        }
    }
}
