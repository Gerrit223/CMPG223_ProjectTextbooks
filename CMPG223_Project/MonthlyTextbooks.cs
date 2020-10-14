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
using System.Globalization;

namespace CMPG223_Project
{
    public partial class MonthlyTextbooks : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public string constr;
        public MonthlyTextbooks(string myConstr)
        {
            constr = myConstr;
            InitializeComponent();
        }

        private void MonthlyTextbooks_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int date = 0;
            int total = 0;
            

           int choice = comboBox1.SelectedIndex;
            if (choice == -1)
            {
                MessageBox.Show("Choose a month!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (choice)
                {
                    case 0:
                        date = 1;
                        lblHeading.Text = "Report for Adverts made in January";
                        break;
                    case 1:
                        date = 2;
                        lblHeading.Text = "Report for Adverts made in February";
                        break;
                    case 2:
                        date = 3;
                        lblHeading.Text = "Report for Adverts made in March";
                        break;
                    case 3:
                        date = 4;
                        lblHeading.Text = "Report for Adverts made in April";
                        break;
                    case 4:
                        date = 5;
                        lblHeading.Text = "Report for Adverts made in May";
                        break;
                    case 5:
                        date = 6;
                        lblHeading.Text = "Report for Adverts made in June";
                        break;
                    case 6:
                        date = 7;
                        lblHeading.Text = "Report for Adverts made in July";
                        break;
                    case 7:
                        date = 8;
                        lblHeading.Text = "Report for Adverts made in August";
                        break;
                    case 8:
                        date = 9;
                        lblHeading.Text = "Report for Adverts made in September";
                        break;
                    case 9:
                        date = 10;
                        lblHeading.Text = "Report for Adverts made in October";
                        break;
                    case 10:
                        date = 11;
                        lblHeading.Text = "Report for Adverts made in November";
                        break;
                    case 11:
                        date = 12;
                        lblHeading.Text = "Report for Adverts made in December";
                        break;
                    default:
                        break;
                }

                try
                {
                    conn = new SqlConnection(constr);
                    conn.Open();
                    comm = new SqlCommand("SELECT c.Name, c.Surname, b.Title, ba.DateAdded From Books as b, BookAdverts as ba, Clients as c Where c.ClientId = ba.ClientId AND b.BookId = ba.BookId AND MONTH(ba.DateAdded) = '" + date + "' ", conn);
                    comm.Connection = conn;
                    ds = new DataSet();
                    SqlDataAdapter ads = new SqlDataAdapter("SELECT c.Name, c.Surname, b.Title, ba.DateAdded From Books as b, BookAdverts as ba, Clients as c Where c.ClientId = ba.ClientId AND b.BookId = ba.BookId AND MONTH(ba.DateAdded) = '" + date + "' ", conn);
                    ads.SelectCommand = comm;
                    ads.Fill(ds, "Report");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Report";
                    conn.Close();
                    total = dataGridView1.RowCount;
                    lblTotal.Text = "Total Adverts: " + total;
                    lblDate.Text = "Report requested on: " + DateTime.Now.ToString("f");

                }
                catch (SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
