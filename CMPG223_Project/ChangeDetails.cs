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
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tinus\Desktop\CMPG223 Final\CMPG223_Project\Textbooks.mdf;Integrated Security = True";

        public int clientID;
        public string name;
        public string last;
        public string email;
        public string cellnr;

        public ChangeDetails()
        {
            InitializeComponent();
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

        private void ChangeDetails_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            clientID = Login.ClientIdValue;
            name = getStringValue("SELECT Name from Clients WHERE ClientId = '" + clientID + "' ");
            last = getStringValue("SELECT Surname from Clients WHERE ClientId = '" + clientID + "' ");
            email = getStringValue("SELECT email from Clients WHERE ClientId = '" + clientID + "' ");
            cellnr = getStringValue("SELECT cellnr from Clients WHERE ClientId = '" + clientID + "' ");

            txtName.Text = name;
            txtSurname.Text = last;
            txtEmail.Text = email;
            txtCell.Text = cellnr;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to change your details?", "Change Details", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    clientID = Login.ClientIdValue;


                    conn = new SqlConnection(constr);
                    conn.Open();
                    string sql = "UPDATE Clients SET Name = @name, Surname = @Surname, email = @email, cellnr = @cell WHERE ClientId = @Id";
                    comm = new SqlCommand(sql, conn);

                    comm.Parameters.AddWithValue("@name", txtName.Text);
                    comm.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    comm.Parameters.AddWithValue("@email", txtEmail.Text);
                    comm.Parameters.AddWithValue("@cell", txtCell.Text);
                    comm.Parameters.AddWithValue("@Id", clientID);

                    comm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Client Details Updated");

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
    
}
