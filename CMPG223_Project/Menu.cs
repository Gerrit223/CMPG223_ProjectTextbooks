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
    public partial class Menu : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public string constr;
        public int Client;
        public Menu(int ClientIdValue, string myString)
        {
            InitializeComponent();
            Client = ClientIdValue;
            constr = myString;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void makeAdvertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MakeAdvert ma = new MakeAdvert(Client,constr);
            ma.MdiParent = this;
            ma.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login l1 = new Login();
            DialogResult logout = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(logout == DialogResult.Yes)
            {
                l1.ShowDialog();
                this.Close();
            }
        }

        private void viewAdvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAdvert va = new ViewAdvert(Client,constr);
            va.MdiParent = this;
            va.Show();
        }

        private void yourAdvertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YourAdvert ya = new YourAdvert(Client,constr);
            ya.MdiParent = this;

            // Counts the amount of records
            int count = 0;
            conn = new SqlConnection(constr);
            conn.Open();
            comm = new SqlCommand("SELECT * FROM BookAdverts WHERE ClientId = '" + Client + "'", conn);
            datread = comm.ExecuteReader();

            while (datread.Read())
            {
                count++;
            }
            conn.Close();

            if (count > 0)
            {
                MessageBox.Show("AVAILABLE LISTINGS: " + count, "LISTINGS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NO LISTINGS MADE!", "LISTINGS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ya.Show();
        }

        private void changeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeDetails dt = new ChangeDetails(Client,constr);
            dt.MdiParent = this;
            dt.Show();
        }
    }
}
