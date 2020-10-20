/*
 Gerrit van Niekerk - 33061294
 Renier Botha - 30353173
 Tinus van Rooyen - 32100965
 Malcolm Visagie - 31591272
 Vuyani Visagie - 31857256
 */
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
        public int ClientIdValue;
        // Connection string for the database that can be changed upon installation
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\IT2020\CMPG223\New\CMPG223_Project\TextbookDB.mdf;Integrated Security=True";
        public Login()
        {
            
        InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Opens the register form
            Register register = new Register(constr);
            register.ShowDialog();
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          string password = txtPassowrd.Text;
          string  username = txtEmail.Text;
            //Opens the admin form if the data is correct
            if (username == "admin" && password == "admin")
            {
                Admin a = new Admin(constr);
                a.ShowDialog();
                this.Close();
            }
            // Opens the Menu form if the user entered the correct data
            else
            {
                // Validates the username & password
                try
                {
                    conn = new SqlConnection(constr);
                    conn.Open();
                    adap = new SqlDataAdapter("SELECT COUNT(*) FROM Clients WHERE Email = '" + username + "'and Password = '" + password + "'", conn);
                    dt = new DataTable();
                    adap.Fill(dt);

                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        string sqlStatement = "SELECT ClientId FROM Clients WHERE email = '"+ username +"' AND password = '"+ password +"' ";

                        using (conn = new SqlConnection(constr))
                        {
                            comm = new SqlCommand(sqlStatement, conn);
                            try
                            {
                                conn.Open();
                                ClientIdValue = (int)comm.ExecuteScalar();
                                conn.Close();
                                Menu menu = new Menu(ClientIdValue, constr);
                                menu.ShowDialog();
                                this.Close();

                            }
                            catch(SqlException error)
                            {
                                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }                      
                    }
                    else
                    {
                        // Show this message if the username and password is wrong
                        MessageBox.Show("Username/Password incorrect","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtEmail.Clear();
                        txtPassowrd.Clear();
                        txtEmail.Focus();
                    }
                    conn.Close();

                }
                catch(SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   
            }         
           
        }
    }
}
