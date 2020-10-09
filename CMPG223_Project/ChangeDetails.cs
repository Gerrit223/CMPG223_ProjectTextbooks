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
        public SqlDataReader datread;
        public string constr;
        public int clientID;
        public string email, cell;

        public ChangeDetails(int id, string myConstr)
        {
            clientID = id;
            constr = myConstr;
            InitializeComponent();
        }

        public bool isDigits(string cellnr) //Method om te kyk of cell nr. uit digits bestaan
        {
            foreach (char c in cellnr)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public bool isemailAvailable(string gEmail)
        {
            conn = new SqlConnection(constr);
            string sql = "SELECT * FROM Clients";
            conn.Open();
            comm = new SqlCommand(sql, conn);
            datread = comm.ExecuteReader();

            while (datread.Read())
            {
                if (datread.GetValue(3).ToString() == gEmail)
                {
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public bool isCellValid(string gCell)
        {
            conn = new SqlConnection(constr);
            string sql = "SELECT * FROM Clients";
            conn.Open();
            comm = new SqlCommand(sql, conn);
            datread = comm.ExecuteReader();

            while (datread.Read())
            {
                if (datread.GetValue(4).ToString() == gCell)
                {
                    conn.Close();
                    return false;
                }

            }
            conn.Close();
            return true;
        }

        public string getStringValue(string sql)
        {
            conn = new SqlConnection(constr);
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
            bool digits, emailAvailable, cellValid;
            bool nEmail = false;
            bool nCell = false;

            digits = isDigits(txtCell.Text);
            emailAvailable = isemailAvailable(email);
            cellValid = isCellValid(txtCell.Text);

            if (email == txtEmail.Text || cell == txtCell.Text)
            {
                if (email == txtEmail.Text && emailAvailable == false)
                {
                    nEmail = true;
                }
                if (cell == txtCell.Text && cellValid == false)
                {
                    nCell = true;
                }

            }

            if (txtEmail.Text == "" || txtCell.Text.Length != 10 || txtPassword.Text == "" || txtPassword.Text != txtConfirm.Text || digits == false || nEmail == false || nCell == false)
            {
                MessageBox.Show("There are missing / wrong fields");
                if (txtEmail.Text == "" || txtCell.Text.Length != 10 || txtPassword.Text == "")
                {
                    MessageBox.Show("There are missing fields");
                    if (txtEmail.Text == "")
                        txtEmail.Clear();
                    if (txtCell.Text.Length != 10)
                        txtCell.Clear();
                    if (txtPassword.Text == "" || txtConfirm.Text == "")
                    {
                        txtPassword.Clear();
                        txtConfirm.Clear();
                    }
                }
                else if (txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Passwords doesn't match");
                    txtConfirm.Clear();
                    txtPassword.Clear();
                }
                else if (digits == false)
                {
                    MessageBox.Show("Please enter a valid cell number");
                    txtCell.Clear();
                }
                else if (nEmail == false)
                {
                    MessageBox.Show("email in use");
                    txtEmail.Clear();
                }
                else if (nCell == false)
                {
                    MessageBox.Show("Cellphone number in use");
                    txtCell.Clear();
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
            email = getStringValue("SELECT email from Clients WHERE ClientId = '" + clientID + "'");
            cell = getStringValue("SELECT cellnr from Clients WHERE ClientId = '" + clientID + "'");
            lblName.Text = "Changing details for: " + getStringValue("SELECT Name from Clients WHERE ClientId = '" + clientID + "'") + " " + getStringValue("SELECT Surname from Clients WHERE ClientId = '" + clientID + "'");
        }
    }

}
