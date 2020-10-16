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
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG 223 NEWCLONE\CMPG223_Project\Textbooks.mdf;Integrated Security=True";
        public Login()
        {
            
        InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
            if (username == "admin" && password == "admin")
            {
                Admin a = new Admin(constr);
                a.ShowDialog();
                this.Close();
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
