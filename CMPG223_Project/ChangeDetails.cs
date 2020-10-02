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
    public partial class ChangeDetails : Form
    {
        public SqlConnection conn;
        public SqlCommand comm;
        public DataSet ds;
        public SqlDataAdapter adap;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\IT2020\CMPG223\New\CMPG223_Project\TextbookDB.mdf;Integrated Security = True";

        public int clientID;

        public ChangeDetails(int id)
        {
            clientID = id;
            InitializeComponent();
        }

        public string getStringValue(string sql)
        {
            string sqlStatement = sql;
            string value;
            using (conn = new SqlConnection(constr))
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


        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirm.Text )
            {
                MessageBox.Show("Passwords do not match");
                txtPassword.Clear();
                txtConfirm.Clear();
            }
            else if (txtEmail.Text == "" || txtCell.Text == "" || txtConfirm.Text == "" || txtCell.Text.Length != 10)
            {
                if (txtCell.Text.Length != 10)
                {
                    MessageBox.Show("Cell number is not valid");
                    txtCell.Clear();
                }
                else
                {
                    MessageBox.Show("There are missing fields");
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to change your details?", "Change Details", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        conn = new SqlConnection(constr);
                        conn.Open();
                        string sql = "UPDATE Clients SET email = @email, cellnr = @cell, password = @password WHERE ClientId = '" + clientID + "'";
                        comm = new SqlCommand(sql, conn);

                        comm.Parameters.AddWithValue("@email", txtEmail.Text);
                        comm.Parameters.AddWithValue("@cell", txtCell.Text);
                        comm.Parameters.AddWithValue("@password", txtConfirm.Text);

                        comm.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Client Details Updated");
                        txtCell.Clear();
                        txtConfirm.Clear();
                        txtEmail.Clear();
                        txtPassword.Clear();

                    }
                    catch (SqlException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Client details have not been changed");
                }
            }
        }

        private void ChangeDetails_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            lblName.Text = "Changing details for: " + getStringValue("SELECT Name from Clients WHERE ClientId = '" + clientID + "'") + " " + getStringValue("SELECT Surname from Clients WHERE ClientId = '" + clientID + "'");
        }
    }

}
