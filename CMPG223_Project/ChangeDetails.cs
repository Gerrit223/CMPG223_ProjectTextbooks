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
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return "";
                }
            }
        }


        private void btnChange_Click(object sender, EventArgs e)
        {
            bool digits, emailAvailable, cellValid;

            digits = isDigits(txtCell.Text);

            if (email == txtEmail.Text)
            {
                emailAvailable = true;    
            }
            else
            {
                emailAvailable = isemailAvailable(txtEmail.Text);
            }
            if (cell == txtCell.Text)
            {
                cellValid = true;
            }
            else
            {
                cellValid = isCellValid(txtCell.Text);
            }


            if (txtEmail.Text == "" || txtCell.Text.Length != 10 || txtPassword.Text == "" || txtPassword.Text != txtConfirm.Text || digits == false || emailAvailable == false || cellValid == false|| txtPassword.Text.Length > 10)
            {
                if (txtEmail.Text == "" || txtCell.Text.Length != 10 || txtPassword.Text == "")
                {
                    MessageBox.Show("Some fields are missing!", "Invalid fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtPassword.Text != txtConfirm.Text || txtPassword.Text.Length > 10)
                {
                    if (txtPassword.Text.Length > 10)
                    {
                        MessageBox.Show("The password is to long", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                       MessageBox.Show("Password doesn't match!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    txtConfirm.Clear();
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                if (digits == false)
                {
                    MessageBox.Show("The cellphone number is invalid!", "Invalid Digits", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCell.Clear();
                    txtCell.Focus();
                }
                if (emailAvailable == false)
                {
                    MessageBox.Show("Email in use!", "Invalid email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
                if (cellValid == false)
                {
                    MessageBox.Show("Cellphone number in use!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCell.Clear();
                    txtCell.Focus();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to change your details?", "Change Details", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                        progressBar1.Visible = true;
                        this.timer1.Start();
                    }
                    catch (SqlException error)
                    {
                        MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Details not changed!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(10);

            if (progressBar1.Value == 100)
            {
                this.timer1.Stop();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                MessageBox.Show("Client details updated successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCell.Clear();
                txtConfirm.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                txtEmail.Focus();
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
