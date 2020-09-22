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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Login login = new Login();
            int Id = login.ClientIdValue;
            MessageBox.Show(Id.ToString());
        }

        private void viewAdvertsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void makeAdvertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MakeAdvert ma = new MakeAdvert();
            ma.MdiParent = this;
            ma.Show();
        }
    }
}
