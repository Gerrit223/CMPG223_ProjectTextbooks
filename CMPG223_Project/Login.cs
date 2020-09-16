using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMPG223_Project
{
    public partial class Login : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public Login()
        {
            
        InitializeComponent();
        }

        //private bool checkLogin(string email, string password)
        //{
        //    string sql = "SELECT * FROM Clients";
        //    conn.Open();
        //    comm = new SqlCommand(sql, conn);
        //    datread = comm.ExecuteReader();
        //    while (datread.Read())
        //    {
        //        if (datread.GetValue(3).ToString() == email && datread.GetValue(5).ToString() == password)
        //        {
        //            MessageBox.Show("test");
        //            conn.Close();
        //            return true;
        //        }
                   
                    
        //    }
        //    conn.Close();
        //    return false;
       // }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            try
            {
                string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG223 Project\CMPG223_Project\CMPG223_Project\Textbooks.mdf;Integrated Security=True";
               
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand("SELECT * FROM Clients");
                comm.Connection = conn;
                ds = new DataSet();
                SqlDataAdapter ads = new SqlDataAdapter("SELECT * FROM Clients", conn);
                ads.SelectCommand = comm;
                ads.Fill(ds, "Clients");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clients";
                //MessageBox.Show("Open");
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email, password;

            email = txtPassowrd.Text;
            password = txtEmail.Text;
            conn.Open();
            comm = new SqlCommand("SELECT Count(*) FROM Clients WHERE email = '" + email + "' AND password = '" + password + "' ");
            comm.Connection = conn;
            adap = new SqlDataAdapter("SELECT Count(*) FROM Clients WHERE email = '" + email + "' AND password = '" + password + "'",conn);
            adap.SelectCommand = comm;

            dt = new DataTable();
            adap.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Login success");
            }
            else
                MessageBox.Show("Enter correct email and pw");

            conn.Close();

        }
    }
}
