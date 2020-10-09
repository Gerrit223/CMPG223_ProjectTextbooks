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
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlDataReader datread;
        public SqlCommand cmd;
        public string insert , constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\IT2020\CMPG223\New\CMPG223_Project\TextbookDB.mdf;Integrated Security=True";
        public int clientId, bookId, authorId; //primary key values
        public MakeAdvert(int id)
        {
            clientId = id;
            InitializeComponent();

        }

        public int getPrimaryKeyValue(string sql)
        {
            string sqlStatement = sql;
            int primarykey;
            using (conn = new SqlConnection(constr))
            {
                conn.Open();
                cmd = new SqlCommand(sqlStatement, conn);
                conn.Close();
                try
                {
                    conn.Open();
                    primarykey = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return primarykey;                   
                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);
               
                    return -1;
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string isbn, title, edition, authorname, authorsurname;
            int price;

            isbn = txtISBN.Text;
            title = txtTitle.Text;
            edition = txtEdition.Text;
            price = int.Parse(txtPrice.Text);
            authorname = txtAuthorName.Text;
            authorsurname = txtAuthorSurname.Text;

            try
            {

                //INSERT INTO AUTHOR
                insert = "INSERT INTO Author VALUES(@AuthorName,@AuthorSurname)";
                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@AuthorName", authorname);
                cmd.Parameters.AddWithValue("@AuthorSurname", authorsurname);
                cmd.ExecuteNonQuery();
                conn.Close();
                string sqlAuthorId = "SELECT AuthorId FROM Author WHERE AuthorName = '" + authorname + "' AND AuthorSurName = '" + authorsurname + "' ";
                authorId = getPrimaryKeyValue(sqlAuthorId);

                //INSERT INTO BOOKS
                insert = "INSERT INTO Books VALUES(@ISBN,@Title,@Edition,@Price)";
                conn = new SqlConnection(constr);
                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Edition", edition);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.ExecuteNonQuery();
                conn.Close();

                string sqlBookId = "SELECT BookId FROM Books WHERE ISBN = '" + isbn + "' AND Title = '" + title + "' AND Edition = '" + edition + "' AND Price = '" + price + "' ";
                bookId = getPrimaryKeyValue(sqlBookId);

                //INSERT INTO BOOKADVERTS
                insert = "INSERT INTO BookAdverts VALUES(@ClientId,@BookId,@DateAdded)";
                conn = new SqlConnection(constr);
                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@ClientId", clientId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Today.ToShortDateString());
                cmd.ExecuteNonQuery();
                conn.Close();


                //INSERT INTO BOOKAUTHOR
                insert = "INSERT INTO BookAuthors VALUES(@BookId,@AuthorId)";
                conn = new SqlConnection(constr);
                conn.Open();
                cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@AuthorId", authorId);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Advertisement made!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtISBN.Clear();
                txtTitle.Clear();
                txtEdition.Clear();
                txtPrice.Clear();
                txtAuthorName.Clear();
                txtAuthorSurname.Clear();
                txtISBN.Focus();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void MakeAdvert_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
