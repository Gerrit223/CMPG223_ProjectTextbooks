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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            // Opens the user manual pdf file
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            // Location of the user manual that can be changed upon installation
            axAcroPDF1.src = @"D:\Documents\IT2020\CMPG223\New\CMPG223_Project\USERMANUAL.pdf";
        }
    }
}
