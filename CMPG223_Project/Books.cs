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
    public partial class Books : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int id;
        public string constr;


        public string ISBN;
        public string Title;
        public string Edition;
        public string Price;

        public Books(string myConstr)
        {
            constr = myConstr;
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

        private void Books_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DisplayAll("Select * From Books");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEdition.Text == ""  || txtISBN.Text.Length != 17  || txtPrice.Text == "" ||  txtTitle.Text == "" || int.Parse(txtEdition.Text) < 1 || int.Parse(txtPrice.Text) < 1)
                { 
                    if (txtEdition.Text == "" || int.Parse(txtEdition.Text) < 1 )
                    {
                        MessageBox.Show("Please enter a valid edition number");
                        txtEdition.Clear();
                    }
                    if (txtISBN.Text.Length != 17)
                    {
                        MessageBox.Show("Please enter a valid ISBN number that consists of 17 digits");
                        txtISBN.Clear();
                    }
                    if (txtPrice.Text == "" || int.Parse(txtPrice.Text) < 1)
                    {
                        MessageBox.Show("Please enter a valid price > 0");
                        txtPrice.Clear();
                    }
                    if (txtTitle.Text == "")
                    {
                        MessageBox.Show("Please enter a valid book title");
                    }
                }
                else
                {
                    conn = new SqlConnection(constr);
                    conn.Open();
                    string sql = "UPDATE Books SET ISBN = @ISBN, Title = @title, Edition = @edition, Price = @price WHERE BookId = '" + id + "'";
                    comm = new SqlCommand(sql, conn);

                    comm.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                    comm.Parameters.AddWithValue("@title", txtTitle.Text);
                    comm.Parameters.AddWithValue("@edition", txtEdition.Text);
                    comm.Parameters.AddWithValue("@price", txtPrice.Text);
                    comm.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Book Details Updated", "Change Details");
                    DisplayAll("Select * From Books");

                    txtISBN.Clear();
                    txtTitle.Clear();
                    txtEdition.Clear();
                    txtPrice.Clear();
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult update = MessageBox.Show("Are you sure you want to update this Book's Details?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (update == DialogResult.Yes)
            {
                // Validates if the user selected a  valid RowIndex
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        dataGridView1.CurrentRow.Selected = true;
                        id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BookId"].FormattedValue.ToString());

                        ISBN = getStringValue("SELECT ISBN from Books WHERE BookId = '" + id + "' ");
                        Title = getStringValue("SELECT Title from Books WHERE BookId = '" + id + "' ");
                        Edition = getStringValue("SELECT Edition from Books WHERE BookId = '" + id + "' ");
                        Price = getStringValue("SELECT Price from Books WHERE BookId = '" + id + "' ");
                        txtISBN.Text = ISBN;
                        txtTitle.Text = Title;
                        txtEdition.Text = Edition;
                        txtPrice.Text = Price;
                    }
                }
                catch (System.ArgumentOutOfRangeException a)
                {
                    MessageBox.Show(a.Message);
                }
            }
        }
    }
}
