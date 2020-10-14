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
    public partial class YourAdvert : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int clientId, bookId;
        public string constr;
        public YourAdvert(int id, string myConstr)
        {
            constr = myConstr;
            clientId = id;
            InitializeComponent();
        }

        private void DeleteEntry(String sqlDelete)
        {
            // Delete the entery that the user selected
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand(sqlDelete, conn);
                comm.Connection = conn;
                comm.ExecuteNonQuery();
                conn.Close();
            }
                catch (SqlException error)
            {
                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAll()
        {
            // Populates the datagrid
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                comm = new SqlCommand("SELECT b.*, ba.DateAdded from Books AS b, BookAdverts AS ba Where b.BookId = ba.BookId AND ba.ClientId = '" + clientId + "'", conn);
                comm.Connection = conn;
                ds = new DataSet();
                SqlDataAdapter ads = new SqlDataAdapter("SELECT b.*, ba.DateAdded from Books AS b, BookAdverts AS ba Where b.BookId = ba.BookId AND ba.ClientId = '" + clientId + "'", conn);
                ads.SelectCommand = comm;
                ads.Fill(ds, "Bookadverts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "BookAdverts";
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult delete = MessageBox.Show("Are you sure you want to delete this Advert?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                // Validates if the user selected a  valid RowIndex
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        dataGridView1.CurrentRow.Selected = true;
                        bookId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BookId"].FormattedValue.ToString());
                        DeleteEntry("Delete FROM BookAuthors WHERE BookId = '" + bookId + "'");
                        DeleteEntry("Delete FROM BookAdverts WHERE BookId = '" + bookId + "'");
                        DeleteEntry("Delete FROM Books WHERE BookId = '" + bookId + "'");
                        progressBar1.Visible = true;
                        this.timer1.Start();
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
                MessageBox.Show("Advert has been deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayAll();
            }
        }

        private void YourAdvert_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DisplayAll();
        }
    }
}
