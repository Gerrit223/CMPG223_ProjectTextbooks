using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPG223_Project
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void viewAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyTextbooks mt = new MonthlyTextbooks();
            mt.MdiParent = this;
            mt.Show();
        }

        private void totalTextbooksForTheMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextbookTotal tt = new TextbookTotal();
            tt.MdiParent = this;
            tt.Show();
        }

        private void booksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Books b = new Books();
            b.MdiParent = this;
            b.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete d = new Delete();
            d.MdiParent = this;
            d.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login l1 = new Login();
            DialogResult logout = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (logout == DialogResult.Yes)
            {
                l1.ShowDialog();
                this.Close();
            }
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientsUpdate cu = new ClientsUpdate();
            cu.MdiParent = this;
            cu.Show();
        }
    }
}
