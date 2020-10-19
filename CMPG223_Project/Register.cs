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
    public partial class Register : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlDataReader datread;
        public SqlCommand cmd;
        string email, confirmPassword, constr;
        int ClientIdValue;
        public Register(string myConstr)
        {
            constr = myConstr;
            InitializeComponent();
        }

        public bool isDigits(string cellnr) //Method om te kyk of cell nr. uit digits bestaan
        {
            foreach(char c in cellnr)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public bool isemailAvailable(string email)
        {
                string sql = "SELECT * FROM Clients";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                datread = cmd.ExecuteReader();

                while (datread.Read())
                {
                    if (datread.GetValue(3).ToString() == email)
                    {
                        conn.Close();
                        return false;
                    }

                }
                conn.Close();
                return true;         
        }

        public bool isCellValid(string cell)
        {
            string sql = "SELECT * FROM Clients";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            datread = cmd.ExecuteReader();

            while (datread.Read())
            {
                if (datread.GetValue(4).ToString() == cell)
                {
                    conn.Close();
                    return false;
                }

            }
            conn.Close();
            return true;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                conn.Close();
            }
            catch(SqlException error)
            {
                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login l1 = new Login();
            l1.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name, surname, cellnr, password;
            bool digits, emailAvailable,cellValid;


            name = txtName.Text;
            surname = txtSurname.Text;
            email = txtEmail.Text;
            cellnr = txtCellphone.Text;
            password = txtPassword.Text;
            confirmPassword = txtConfirm.Text;

            digits = isDigits(cellnr);
            emailAvailable = isemailAvailable(email);
            cellValid = isCellValid(cellnr);

            if (name == "" || surname == "" || email == ""|| password != confirmPassword || cellnr.Length != 10 || digits == false || emailAvailable == false || cellValid == false|| password.Length > 10)
            {
                if (emailAvailable == false)
                {
                    MessageBox.Show("Email in use!", "Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
                if (cellValid == false)
                {
                    MessageBox.Show("Cellphone number in use!", "Cellphone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCellphone.Clear();
                    txtCellphone.Focus();
                }
                if (digits == false || cellnr.Length != 10)
                {
                    MessageBox.Show("Cellphone number contains errors!", "Cellphone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCellphone.Clear();
                    txtCellphone.Focus();
                }
                if (password != confirmPassword || password.Length >= 10)
                {
                    if (password.Length > 10)
                    {
                        MessageBox.Show("Passwords are to long!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Passwords DO NOT match!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    txtConfirm.Clear();
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                if (name == "" || surname == "" || email == "" || password == "")
                {
                    MessageBox.Show("There are missing fields!", "Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    string insert = "INSERT INTO Clients VALUES(@Name,@Surname,@email,@cellnr,@password)";
                    conn.Open();
                    this.timer1.Start();
                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Surname", surname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cellnr", cellnr);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(10);

            if(progressBar1.Value == 100)
            {
                this.timer1.Stop();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                MessageBox.Show("NEW USER ADDED","Users",MessageBoxButtons.OK,MessageBoxIcon.Information);

                string sqlStatement = "SELECT ClientId FROM Clients WHERE email = '" + email + "' AND password = '" + confirmPassword + "' ";

                using (conn = new SqlConnection(constr))
                {
                    cmd = new SqlCommand(sqlStatement, conn);
                    try
                    {
                        conn.Open();
                        ClientIdValue = (int)cmd.ExecuteScalar();
                        conn.Close();
                        Menu menu = new Menu(ClientIdValue, constr);
                        menu.ShowDialog();
                        this.Close();
                    }
                    catch (SqlException error)
                    {
                        MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
