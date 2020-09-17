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
using System.Data;

namespace CMPG223_Project
{
    public partial class Login : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adapt;
        public DataTable dt;
        public SqlCommand com;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassowrd.Text;

            if (username == "ADMIN" && password == "ADMIN")
            {
                ////////
            }
            else
            {
                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Renier Botha\Documents\IT2020\CMPG223\Final\CMPG223_Project\Textbooks.mdf; Integrated Security = True");
                conn.Open();
                adapt = new SqlDataAdapter("SELECT COUNT(*) FROM Clients WHERE Email = '" + username + "'and Password = '" + password + "'", conn);
                dt = new DataTable();
                adapt.Fill(dt);

                if(dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Correct you may proceed");
                }
                else
                {
                    MessageBox.Show("Username/Password is wrong");
                }
                conn.Close();
            }
        }
    }
}
