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
    public partial class MakeAdvert : Form
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataAdapter adap;
        public DataSet ds;

        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG 223 Project\CMPG223_Project\Textbooks.mdf;Integrated Security = True";
        public MakeAdvert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string create = "INSERT INTO Books VALUES(@ISBN, @Title, @Edition, @Price)";
            conn = new SqlConnection(constr);
            conn.Open();
            cmd = new SqlCommand(create, conn);

            cmd.Parameters.AddWithValue("@ISBN",txtISBN.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@Edition", txtEdition.Text);
            cmd.Parameters.AddWithValue("@Price", int.Parse(txtPrice.Text));
            cmd.ExecuteNonQuery();

            MessageBox.Show("Created Succesfully");
            conn.Close();
            
            
        }
    }
}
