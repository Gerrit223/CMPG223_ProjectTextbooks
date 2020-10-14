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
        public string insert, constr;
        public int clientId, bookId, authorId; //primary key values
        public MakeAdvert(int id,string myConstr)
        {
            clientId = id;
            constr = myConstr;
            InitializeComponent();

        }
        public bool isDigits(string number) //Method om te kyk of cell nr. uit digits bestaan
        {
            foreach (char c in number)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(10);

            if (progressBar1.Value == 100)
            {
                this.timer1.Stop();
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                MessageBox.Show("Advertisement made!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtISBN.Clear();
                txtTitle.Clear();
                txtEdition.Clear();
                txtPrice.Clear();
                txtAuthorName.Clear();
                txtAuthorSurname.Clear();
                txtTitle.Focus();
            }
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
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string isbn, title, edition, authorname, authorsurname;
            int price;
            if (txtAuthorName.Text == "" || txtAuthorSurname.Text == "" || txtEdition.Text == "" || txtISBN.Text.Length != 17 || txtPrice.Text == "" || txtTitle.Text == "" || isDigits(txtEdition.Text) == false || isDigits(txtPrice.Text) == false)
            {
                if (txtEdition.Text == "" || isDigits(txtEdition.Text) == false)
                {
                    MessageBox.Show("Please enter a valid edition!", "Invalid Edition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEdition.Clear();
                    txtEdition.Focus();
                }
                if (txtISBN.Text.Length != 17)
                {
                    MessageBox.Show("Please enter a valid ISBN that consists of 17 charachters!", "Invalid ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtISBN.Clear();
                    txtISBN.Focus();
                }
                if (txtPrice.Text == "" || isDigits(txtPrice.Text) == false)
                {
                    MessageBox.Show("Please enter a valid price!", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Clear();
                    txtPrice.Focus();
                }
                if (txtTitle.Text == "")
                {
                    MessageBox.Show("Empty field!", "Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTitle.Focus();
                }
                if (txtAuthorName.Text == "")
                {
                    MessageBox.Show("Empty field!", "Author Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAuthorName.Focus();
                }
                if (txtAuthorSurname.Text == "")
                {
                    MessageBox.Show("Empty field!", "Author Surname", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAuthorSurname.Focus();
                }
            }
            else if (isDigits(txtEdition.Text) == true && int.Parse(txtEdition.Text) < 1 || isDigits(txtPrice.Text) == true && int.Parse(txtPrice.Text) < 1)
            {
                if (isDigits(txtEdition.Text) == true && int.Parse(txtEdition.Text) < 1)
                {
                    MessageBox.Show("Edition can't be less than 1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEdition.Clear();
                    txtEdition.Focus();
                }
                if (isDigits(txtPrice.Text) == true && int.Parse(txtPrice.Text) < 1)
                {
                    MessageBox.Show("Price can't be less than R1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Clear();
                    txtPrice.Focus();
                }
            }
            else
            {
                try
                {
                    isbn = txtISBN.Text;
                    title = txtTitle.Text;
                    edition = txtEdition.Text;
                    price = int.Parse(txtPrice.Text);
                    authorname = txtAuthorName.Text;
                    authorsurname = txtAuthorSurname.Text;

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

                    progressBar1.Visible = true;
                    this.timer1.Start();
                }
                catch (SqlException error)
                {
                    MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                MessageBox.Show("Please contact page advisor!\n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
