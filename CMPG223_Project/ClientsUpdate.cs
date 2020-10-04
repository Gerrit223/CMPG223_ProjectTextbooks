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
using System.ComponentModel.Design.Serialization;


namespace CMPG223_Project
{
    public partial class ClientsUpdate : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int id;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tinus\Desktop\CMPG223(v2)\CMPG223_Project\Textbooks.mdf;Integrated Security = True";

        
        public string name;
        public string last;
        public string email;
        public string cellnr;
        public string pass;



        public ClientsUpdate()
        {
            InitializeComponent();
        }

        public string getStringValue(string sql)
        {
            string sqlStatement = sql;
            string value;
            using (conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tinus\Desktop\CMPG223(v2)\CMPG223_Project\Textbooks.mdf;Integrated Security = True"))
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


        private void DisplayAll(string sql)
        {
            // Populates the datagrid
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand(sql, conn);
                comm.Connection = conn;
                ds = new DataSet();
                SqlDataAdapter ads = new SqlDataAdapter(sql, conn);
                ads.SelectCommand = comm;
                ads.Fill(ds, "Table");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Table";
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void ClientsUpdate_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DisplayAll("Select * From Clients");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult update = MessageBox.Show("Are you sure you want to update this Client's Details?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (update == DialogResult.Yes)
            {
                // Validates if the user selected a  valid RowIndex
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        dataGridView1.CurrentRow.Selected = true;
                        id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ClientId"].FormattedValue.ToString());

                        name = getStringValue("SELECT Name from Clients WHERE ClientId = '" +id + "' ");
                        last = getStringValue("SELECT Surname from Clients WHERE ClientId = '" + id + "' ");
                        email = getStringValue("SELECT email from Clients WHERE ClientId = '" + id + "' ");
                        cellnr = getStringValue("SELECT cellnr from Clients WHERE ClientId = '" + id + "' ");
                        pass = getStringValue("SELECT password from Clients WHERE ClientId = '" + id + "' ");


                        txtFirst.Text = name;
                        txtLast.Text = last;
                        txtEmail.Text = email;
                        txtCell.Text = cellnr;
                        txtPass.Text = pass;


                    }
                }
                catch (System.ArgumentOutOfRangeException a)
                {
                    MessageBox.Show(a.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                


                conn = new SqlConnection(constr);
                conn.Open();
                string sql = "UPDATE Clients SET Name = @name, Surname = @Surname, email = @email, cellnr = @cell, password = @pass WHERE ClientId = '" + id + "'";
                comm = new SqlCommand(sql, conn);

                comm.Parameters.AddWithValue("@name", txtFirst.Text);
                comm.Parameters.AddWithValue("@Surname", txtLast.Text);
                comm.Parameters.AddWithValue("@email", txtEmail.Text);
                comm.Parameters.AddWithValue("@cell", txtCell.Text);
                comm.Parameters.AddWithValue("@pass", txtPass.Text);

                
                comm.ExecuteNonQuery();
                conn.Close();
                
                MessageBox.Show("Client Details Updated", "Change Details");
                DisplayAll("Select * From Clients");

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
