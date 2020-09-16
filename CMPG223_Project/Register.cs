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
        public Register()
        {
            InitializeComponent();
        }

        public bool isDigits(String cellnr) //Method om te kyk of cell nr. uit digits bestaan
        {
            foreach(char c in cellnr)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void Register_Load(object sender, EventArgs e)
        {
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
            String name, surname, email, cellnr, password, confirmPassword;
            bool digits;


            name = txtName.Text;
            surname = txtSurname.Text;
            email = txtEmail.Text;
            cellnr = txtCellphone.Text;
            password = txtPassword.Text;
            confirmPassword = txtConfirm.Text;

            digits = isDigits(cellnr);

            if(name == "" || surname == "" || email == "" || cellnr == ""|| password != confirmPassword || cellnr.Length != 10 || digits == false)
            {
                if (password != confirmPassword)
                    MessageBox.Show("Password", "Passwords DO NOT match!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Details", "Enter all of the details correctly!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string insert = "INSERT INTO Clients VALUES(@Name,@Surname,@email,@cellnr,@password)";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insert, conn);
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
            this.Close();
        }
    }
}
