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
    public partial class TopUsers : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public string constr;
        public TopUsers(string myConstr)
        {
            constr = myConstr;
            InitializeComponent();
        }

        private void TopUsers_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int date = 0;        
            int choice = comboBox1.SelectedIndex;

            if (choice == -1)
            {
                MessageBox.Show("Choose a month!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Obtains the selected month
                switch (choice)
                {
                    case 0:
                        date = 1;
                        lblHeading.Text = "Top Users for January";
                        break;
                    case 1:
                        date = 2;
                        lblHeading.Text = "Top Users for February";
                        break;
                    case 2:
                        date = 3;
                        lblHeading.Text = "Top Users for March";
                        break;
                    case 3:
                        date = 4;
                        lblHeading.Text = "Top Users for April";
                        break;
                    case 4:
                        date = 5;
                        lblHeading.Text = "Top Users for May";
                        break;
                    case 5:
                        date = 6;
                        lblHeading.Text = "Top Users for June";
                        break;
                    case 6:
                        date = 7;
                        lblHeading.Text = "Top Users for July";
                        break;
                    case 7:
                        date = 8;
                        lblHeading.Text = "Top Users for August";
                        break;
                    case 8:
                        date = 9;
                        lblHeading.Text = "Top Users for September";
                        break;
                    case 9:
                        date = 10;
                        lblHeading.Text = "Top Users for October";
                        break;
                    case 10:
                        date = 11;
                        lblHeading.Text = "Top Users for November";
                        break;
                    case 11:
                        date = 12;
                        lblHeading.Text = "Top Users for December";
                        break;
                    default:
                        break;
                }
                try
                {
                    // Displays the report
                    conn = new SqlConnection(constr);
                    conn.Open();
                    comm = new SqlCommand("SELECT Clients.Name, Clients.Surname, COUNT(BookAdverts.BookAdvertId) AS TotalAdverts FROM BookAdverts LEFT JOIN Clients ON BookAdverts.ClientId=Clients.ClientId WHERE MONTH(BookAdverts.DateAdded) = '" + date + "' GROUP BY Name,Surname ORDER BY TotalAdverts DESC", conn);
                    comm.Connection = conn;
                    ds = new DataSet();
                    SqlDataAdapter ads = new SqlDataAdapter("SELECT Clients.Name,Clients.Surname, COUNT(BookAdverts.BookAdvertId) AS TotalAdverts FROM BookAdverts LEFT JOIN Clients ON BookAdverts.ClientId=Clients.ClientId WHERE MONTH(BookAdverts.DateAdded) = '" + date + "' GROUP BY Name,Surname ORDER BY TotalAdverts DESC", conn);
                    ads.SelectCommand = comm;
                    ads.Fill(ds, "Report");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Report";
                    lblDate.Text = "Report generated on" + DateTime.Now.ToString("f");
                    conn.Close();
                }
                catch (SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
