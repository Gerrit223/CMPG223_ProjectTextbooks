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
    public partial class Author : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int id;
        public string constr;
        public string First;
        public string Surname;

        public Author(string myConstr)
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


        public void DisplayAll(string sql)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string sql = "UPDATE Author SET AuthorName = @first, AuthorSurname = @last WHERE AuthorId = '" + id + "'";
                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@first", txtFirst.Text);
                comm.Parameters.AddWithValue("@last", txtLast.Text);
                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Author Details Updated", "Change Details");
                DisplayAll("Select * From Author");
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Author_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DisplayAll("Select * From Author");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validates if the user selected a  valid RowIndex
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["AuthorId"].FormattedValue.ToString());

                    First = getStringValue("SELECT AuthorName from Author WHERE AuthorId = '" + id + "' ");
                    Surname = getStringValue("SELECT AuthorSurname from Author WHERE AuthorId = '" + id + "' ");
                    txtFirst.Text = First;
                    txtLast.Text = Surname;
                }
            }
            catch (System.ArgumentOutOfRangeException a)
            {
                MessageBox.Show(a.Message);
            }

        }
    }
}
