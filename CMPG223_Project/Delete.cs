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
// Nuwe Weergame
namespace CMPG223_Project
{
    public partial class Delete : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int id;
        public string constr;
        public Delete(string myConstr)
        {
            constr = myConstr;
            InitializeComponent();
        }

        public int getAmount(string sql)
        {
            int count = 0;
            conn = new SqlConnection(constr);
            conn.Open();
            comm = new SqlCommand(sql, conn);
            datread = comm.ExecuteReader();

            while (datread.Read())
            {
                count++;
            }
            conn.Close();
            return count;
        }
        public int getPrimaryKeyValue(string sql)
        {
            int primarykey;
            using (conn = new SqlConnection(constr))
            {
                conn.Open();
                comm = new SqlCommand(sql, conn);
                conn.Close();
                try
                {
                    conn.Open();
                    if(comm.ExecuteScalar() == null)
                    {
                        return -1;
                    }
                    else
                    {
                        primarykey = (int)comm.ExecuteScalar();
                        conn.Close();
                        return primarykey;
                    }
                
                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);

                    return -1;
                }

            }
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
                MessageBox.Show(error.Message);
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

        private void radBook_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAll("SELECT * FROM Books");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id2,loop;
            if (radBook.Checked)
            {
                DialogResult delete = MessageBox.Show("Are you sure you want to delete this Book?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (delete == DialogResult.Yes)
                {
                    // Validates if the user selected a  valid RowIndex
                    try
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            dataGridView1.CurrentRow.Selected = true;
                            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["BookId"].FormattedValue.ToString());
                            DeleteEntry("Delete FROM BookAuthors WHERE BookId = '" + id + "'");
                            DeleteEntry("Delete FROM BookAdverts WHERE BookId = '" + id + "'");
                            DeleteEntry("Delete FROM Books WHERE BookId = '" + id + "'");
                            MessageBox.Show("Book has been deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayAll("SELECT * FROM Books");
                        }
                    }
                    catch (System.ArgumentOutOfRangeException a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
            }
            
            if (radClient.Checked)
            {
                DialogResult delete = MessageBox.Show("Are you sure you want to delete this Client?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (delete == DialogResult.Yes)
                {
                    // Validates if the user selected a  valid RowIndex
                    try
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            dataGridView1.CurrentRow.Selected = true;
                            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ClientId"].FormattedValue.ToString());

                                loop = getAmount("SELECT * FROM BookAdverts WHERE ClientId = '" + id + "'");
                                if (loop > 0)
                                {
                                    for (int i = 0; i < loop; i++)
                                    {
                                        id2 = getPrimaryKeyValue("SELECT BookId from BookAdverts WHERE ClientId = '" + id + "'");
                                        DeleteEntry("Delete FROM BookAuthors WHERE BookId = '" + id2 + "'");
                                        DeleteEntry("Delete FROM BookAdverts WHERE BookId = '" + id2 + "'");
                                        DeleteEntry("Delete FROM Books WHERE BookId = '" + id2 + "'");
                                    }
                                }

                            DeleteEntry("Delete FROM Clients WHERE ClientId = '" + id + "'");
                            MessageBox.Show("Client has been deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayAll("SELECT * FROM Clients");
                        }
                    }
                    catch (System.ArgumentOutOfRangeException a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
            }
        }

        private void radClient_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAll("SELECT * FROM Clients");
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
    }

}
