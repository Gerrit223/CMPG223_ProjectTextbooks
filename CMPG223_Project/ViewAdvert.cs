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
    public partial class ViewAdvert : Form
    {
        private int bookId, clientId;
        String tabs = "{0,0}{1,60}";
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Renier Botha\Documents\IT2020\CMPG223\FinalProg\CMPG223_Project\TextbookDB.mdf;Integrated Security=True";
        public ViewAdvert(int id)
        {
            InitializeComponent();
        }

        public int getPrimaryKeyValue(string sql)
        {
            string sqlStatement = sql;
            int primarykey;
            using (conn = new SqlConnection(constr))
            {
                conn.Open();
                comm = new SqlCommand(sqlStatement, conn);
                conn.Close();
                try
                {
                    conn.Open();
                    primarykey = (int)comm.ExecuteScalar();
                    conn.Close();
                    return primarykey;
                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);

                    return -1;
                }

            }
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
        private void ViewAdvert_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand("SELECT * FROM Books");
                comm.Connection = conn;
                ds = new DataSet();
                SqlDataAdapter ads = new SqlDataAdapter("SELECT * FROM Books", conn);
                ads.SelectCommand = comm;
                ads.Fill(ds, "Books");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Books";
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                listBox1.Items.Clear();
            }
            //listBox1.Items.Add(textBox1.Text);
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand("SELECT * FROM Books WHERE Title LIKE '%"+textBox1.Text+ "%' OR ISBN LIKE '%" + textBox1.Text + "%' ORDER BY Price ");
                comm.Connection = conn;
                ds = new DataSet();
                SqlDataAdapter ads = new SqlDataAdapter("SELECT * FROM Books WHERE Title LIKE '%" + textBox1.Text + "%'OR ISBN LIKE '%" + textBox1.Text + "%' ORDER BY Price ", conn);
                ads.SelectCommand = comm;
                ads.Fill(ds, "Books");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Books";
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Clear();
            string name, surname, cell, title, price,edition;

            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                bookId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BookId"].FormattedValue.ToString());
                clientId = getPrimaryKeyValue("SELECT ClientId from BookAdverts WHERE BookId = '"+ bookId +"' ");
                name = getStringValue("SELECT Name from Clients WHERE ClientId = '" + clientId + "' ");
                surname = getStringValue("SELECT Surname from Clients WHERE ClientId = '" + clientId + "' ");
                cell = getStringValue("SELECT cellnr from Clients WHERE ClientId = '" + clientId + "' ");
                title = getStringValue("SELECT Title from Books WHERE BookId = '" + bookId + "' ");
                price = getStringValue("SELECT Price from Books WHERE BookId = '" + bookId + "' ");
                edition = getStringValue("SELECT Edition from Books WHERE BookId = '" + bookId + "' ");


               
                listBox1.Items.Add(title);
                listBox1.Items.Add("========================");
                listBox1.Items.Add(String.Format(tabs, "Name", name + " " + surname));
                listBox1.Items.Add("");
                listBox1.Items.Add(String.Format(tabs, "Edition", edition));
                listBox1.Items.Add("");
                listBox1.Items.Add(String.Format(tabs, "Cellphone", cell));
                listBox1.Items.Add("");
                listBox1.Items.Add(String.Format(tabs, "Price","R"+price));
   
            }
        }
    }
}
