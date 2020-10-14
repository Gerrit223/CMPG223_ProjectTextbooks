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

        public bool isDigits(string number) //Method om te kyk of cell nr. uit digits bestaan
        {
            foreach (char c in number)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
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
                    MessageBox.Show("Please contact page advisor!\n" + error.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Books_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DisplayAll("Select * From Books");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEdition.Text == "" || txtISBN.Text.Length != 17 || txtPrice.Text == "" || txtTitle.Text == "" || isDigits(txtEdition.Text) == false || isDigits(txtPrice.Text) == false)
            {
                if (txtEdition.Text == "" || isDigits(txtEdition.Text) == false)
                {
                    MessageBox.Show("Please enter a valid edition number!", "Invalid Edition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEdition.Clear();
                    txtEdition.Focus();
                }
                if (txtISBN.Text.Length != 17)
                {
                    MessageBox.Show("Please enter a valid ISBN number that consists of 17 digits!", "Invalid ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtISBN.Clear();
                    txtISBN.Focus();
                }
                if (txtPrice.Text == "" || isDigits(txtPrice.Text) == false)
                {
                    MessageBox.Show("Please enter a valid price!", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Clear();
                    txtPrice.Focus();
                }
                if (txtTitle.Text == "")
                {
                    MessageBox.Show("Please enter a valid book title!", "Invalid Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTitle.Focus();
                }
            }
            else if (isDigits(txtEdition.Text) == true && int.Parse(txtEdition.Text) < 1 || isDigits(txtPrice.Text) == true && int.Parse(txtPrice.Text) < 1)
            {
                if (isDigits(txtEdition.Text) == true && int.Parse(txtEdition.Text) < 1)
                {
                    MessageBox.Show("Th edition can't be less than 1!", "Invalid Editon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEdition.Clear();
                    txtEdition.Focus();
                }
                if (isDigits(txtPrice.Text) == true && int.Parse(txtPrice.Text) < 1)
                {
                    MessageBox.Show("The price can't be less than R1 !", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Clear();
                    txtPrice.Focus();
                }
            }
            else
            {
                try
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

                    progressBar1.Visible = true;
                    this.timer1.Start();
                }
                catch (SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult update = MessageBox.Show("Are you sure you want to update this Book's Details?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    MessageBox.Show("Please select content inside the table!\n" + a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Details updated successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayAll("Select * From Books");
                txtISBN.Clear();
                txtTitle.Clear();
                txtEdition.Clear();
                txtPrice.Clear();
                txtISBN.Focus();
               
            }

        }
    }
}
