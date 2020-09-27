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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        public SqlConnection conn;
        public SqlCommand comm;
        public DataSet ds;
        public SqlDataAdapter adap;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tinus\Desktop\CMPG223 Final\CMPG223_Project\Textbooks.mdf;Integrated Security = True";
        int clientID;
        string name, last, Pass;

        private void button1_Click(object sender, EventArgs e)
        {
            clientID = Login.ClientIdValue;
            Pass = getStringValue("SELECT password from Clients WHERE ClientId = '" + clientID + "' ");

            if(txtOld.Text == Pass)
            {
                if(txtNew.Text == txtConfirm.Text)
                {
                    try
                    {
                        clientID = Login.ClientIdValue;


                        conn = new SqlConnection(constr);
                        conn.Open();
                        string sql = "UPDATE Clients SET password = @pass WHERE ClientId = @Id";
                        comm = new SqlCommand(sql, conn);

                        comm.Parameters.AddWithValue("@pass", txtConfirm.Text);
                        
                        comm.Parameters.AddWithValue("@Id", clientID);

                        comm.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Client Password Updated");

                    }
                    catch (SqlException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not Match", "Change Password");
                }
            }
            else
            {
                MessageBox.Show("Password incorrect","Change Password");
            }

            


        }

        public string getStringValue(string sql)
        {
            string sqlStatement = sql;
            string value;
            using (conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\tinus\Desktop\CMPG223 Final\CMPG223_Project\Textbooks.mdf; Integrated Security = True"))
            {
                conn.Open();
                comm = new SqlCommand(sqlStatement, conn);
                conn.Close();
                try
                {
                    conn.Open();
                    value = comm.ExecuteScalar().ToString();
                    conn.Close();
                    return value;
                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);

                    return "";
                }

            }
        }


        private void ChangePassword_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            clientID = Login.ClientIdValue;
            name = getStringValue("SELECT Name from Clients WHERE ClientId = '" + clientID + "' ");
            last = getStringValue("SELECT Surname from Clients WHERE ClientId = '" + clientID + "' ");
            Pass = getStringValue("SELECT password from Clients WHERE ClientId = '" + clientID + "' ");

            label1.Text = "Change Password for " + name + " " + last;




        }
    }
}
