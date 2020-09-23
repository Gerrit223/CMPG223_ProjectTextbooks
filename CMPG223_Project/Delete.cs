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
    public partial class Delete : Form
    {
        public SqlConnection conn;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommand comm;
        public SqlDataReader datread;
        public DataTable dt;
        public int ClientIdValue;
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\CMPG 223 Project\CMPG223_Project\Textbooks.mdf;Integrated Security = True";
        public Delete()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(constr);

                conn.Open();

                string sql = "Delete FROM Clients Where ClientId = @id";
                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@id", textBox1.Text);
                comm.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Shap");

                

            }
            catch
            {
                MessageBox.Show("Error");
            }

        }
    }
    
}
