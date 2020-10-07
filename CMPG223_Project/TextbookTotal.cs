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
    public partial class TextbookTotal : Form
    {
        public TextbookTotal()
        {
            InitializeComponent();
        }

        public class BookAdverts
        {
            public int ID { get; set; }

        }

        private void TextbookTotal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<BookAdverts> student = new List<BookAdverts>();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tinus\Desktop\CMPG223(v2)\CMPG223_Project\Textbooks.mdf;Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from BookAdverts", conn);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    student.Add(new BookAdverts()
                    {
                        ID = dr.GetInt32(dr.GetOrdinal("ClientId"))
                        
                    });

                }
                dr.Close();
            }
            catch (Exception exp)
            {

                throw;
            }
            finally
            {

                conn.Close();
            }
            
            

            MessageBox.Show(String.Join("; ", student).ToString());

        }
    }
}
