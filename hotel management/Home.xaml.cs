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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        static public int wifi = 0;
        static public int bb = 0;
        static public int bf = 0;
        static public int taxi = 0;
        static public int visa = 0;
        static public int master = 0;
        static public int room = 2000;
        static public int total;
        static public string ab = "booked";
        public Home()
        {
            InitializeComponent();
            table_view();
        }
        void table_view()
        {
            string con = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();

            string sqlquery = "Select room as [Room Name],status as [Avaibility] from main";
            SqlCommand sqlcmd = new SqlCommand(sqlquery, sqlcon);

            SqlDataAdapter data_adapter = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable("main");
            data_adapter.Fill(dt);
            grid_room.ItemsSource = dt.DefaultView;
            data_adapter.Update(dt);
            sqlcon.Close();
        }

        private void grid_room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_room.Text = row_selected["Room Name"].ToString();
                txt_stat.Text = row_selected["Avaibility"].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            status_update();
            date_insert();
            resident_update();
            MessageBox.Show("done");
        }
        void status_update()
        {
            string con = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();

            string commandstring = "UPDATE main set status='"+ab+"' WHERE room='" + txt_room.Text + "'";
            SqlCommand sqlcmd = new SqlCommand(commandstring, sqlcon);
            int rows = sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
        }
        void date_insert()
        {
            string connectionstring = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(connectionstring);

            SqlCommand cmd = new SqlCommand("insert into booked(room,datefr,dateto) values(@c,@a,@b)", sqlcon);

            cmd.Parameters.Add("@c", SqlDbType.VarChar).Value = txt_room.Text;
            cmd.Parameters.Add("@a", SqlDbType.Date).Value = date_from.SelectedDate.Value.ToShortDateString();
            cmd.Parameters.Add("@b", SqlDbType.Date).Value = date_to.SelectedDate.Value.ToShortDateString();

            sqlcon.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlcon.Close();
        }
        void resident_update()
        {
            string connectionstring = @"Data Source=SHUVRO\SQLEXPRESS;Initial Catalog=tanzee;Integrated Security=True";
            SqlConnection sqlcon = new SqlConnection(connectionstring);

            SqlCommand cmd = new SqlCommand("insert into resident(room,resident,bill) values(@a,@b,@c)", sqlcon);

            MainWindow mw = new MainWindow();


            cmd.Parameters.Add("@a", SqlDbType.VarChar).Value = txt_room.Text;
            cmd.Parameters.Add("@b", SqlDbType.VarChar).Value = mw.Name;
            cmd.Parameters.Add("@c", SqlDbType.VarChar).Value = total_bill.Content.ToString();
            sqlcon.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlcon.Close();
        }

        private void gg(object sender, RoutedEventArgs e)
        {
            if (chck_wifi.IsChecked.Value == true)
            {
                wifi = 100;
            }
            if (chck_break.IsChecked.Value == true)
            {
                bb = 350;
            }
            if (chck_buff.IsChecked.Value == true)
            {
                bf = 750;
            }
            if (chck_taxi.IsChecked.Value == true)
            {
                taxi = 500;
            }
            if(chck_visa.IsChecked.Value == true)
            {
                visa = 50;
                total = wifi + bb + bf + taxi + visa + room;
            }
            if (chck_master.IsChecked.Value == true)
            {
                master = 70;
                total = wifi + bb + bf + taxi + master + room;
            }
            total_bill.Content = total;
        }
    }
    
}
