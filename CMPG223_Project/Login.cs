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
//LOGIN KOMMENTAAR

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
        public int ClientIdValue;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG 223 Project\CMPG223_Project\Textbooks.mdf;Integrated Security = True";
        public Login()
        {
            
        InitializeComponent();
        }

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
           
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username, password;

            password = txtPassowrd.Text;
            username = txtEmail.Text;
            if (username == "admin" && password == "admin")
            {

            }
            else
            {
                try
                {
                    conn = new SqlConnection(constr);
                    conn.Open();
                    adap = new SqlDataAdapter("SELECT COUNT(*) FROM Clients WHERE Email = '" + username + "'and Password = '" + password + "'", conn);
                    dt = new DataTable();
                    adap.Fill(dt);

                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        string sqlStatement = "SELECT ClientId FROM Clients WHERE email = '" + username + "' AND password = '" + password + "' ";

                        using (conn = new SqlConnection(constr))
                        {
                            comm = new SqlCommand(sqlStatement, conn);
                            try
                            {
                                conn.Open();
                                ClientIdValue = (int)comm.ExecuteScalar();
                                conn.Close();
                                MessageBox.Show(ClientIdValue.ToString());
                                Menu menu = new Menu(ClientIdValue);
                                // MakeAdvert ma = new MakeAdvert(ClientIdValue);
                                menu.ShowDialog();
                                this.Close();

                            }
                            catch (SqlException error)
                            {
                                MessageBox.Show(error.Message);
                            }
                        }
                        MessageBox.Show("Correct you may proceed");


                    }
                    else
                    {
                        MessageBox.Show("Username/Password is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();

                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete d1 = new Delete();
            d1.Show();
        }
    }
}
