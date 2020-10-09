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
        public string constr;
        public Admin(string myConstr)
        {
            constr = myConstr;
            InitializeComponent();
        }

        private void viewAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyTextbooks mt = new MonthlyTextbooks(constr);
            mt.MdiParent = this;
            mt.Show();
        }


        private void booksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Books b = new Books(constr);
            b.MdiParent = this;
            b.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete d = new Delete(constr);
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

        private void topUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopUsers tu1 = new TopUsers(constr);
            tu1.MdiParent = this;
            tu1.Show();
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author au1 = new Author(constr);
            au1.MdiParent = this;
            au1.Show();
        }
    }
}
