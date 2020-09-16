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
        public Register()
        {
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

        private void Register_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            try
            {
                string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG223 Project\CMPG223_Project\CMPG223_Project\Textbooks.mdf;Integrated Security=True";
                conn = new SqlConnection(constr);
                conn.Open();
                conn.Close();
            }
            catch(SqlException error)
            {
                MessageBox.Show(error.Message);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name, surname, email, cellnr, password, confirmPassword;
            bool digits, emailAvailable;


            name = txtName.Text;
            surname = txtSurname.Text;
            email = txtEmail.Text;
            cellnr = txtCellphone.Text;
            password = txtPassword.Text;
            confirmPassword = txtConfirm.Text;

            digits = isDigits(cellnr);
            emailAvailable = isemailAvailable(email);

            if(name == "" || surname == "" || email == "" || cellnr == ""|| password != confirmPassword || cellnr.Length != 10 || digits == false || emailAvailable == false)
            {
                if (emailAvailable == false)
                    MessageBox.Show("Email has already been taken!", "Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (password != confirmPassword)
                    MessageBox.Show("Passwords DO NOT match!","Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Enter all of the details correctly!","Client Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(error.Message);
                }
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(10);

            if(progressBar1.Value == 100)
            {
                this.timer1.Stop();
                MessageBox.Show("NEW USER ADDED","Users",MessageBoxButtons.OK,MessageBoxIcon.Information);
                progressBar1.Value = 0;
                progressBar1.Visible = false;
            }
        }
    }
}
