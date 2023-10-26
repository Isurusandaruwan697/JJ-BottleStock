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


namespace JJTREADINGNEW
{
    public partial class Dashboardscreen : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";
        private int UserID;
        private string FullName;
        private string UserName;
        public Dashboardscreen(int UserID,String FullName,String UserName)
        {
            InitializeComponent();
      
            this.UserID = UserID;
            this.FullName = FullName;
            this.UserName = UserName;
        }

        public Dashboardscreen(int userID, string fullName)
        {
            UserID = userID;
            FullName = fullName;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Dashboardscreen_Load(object sender, EventArgs e)
        {
            label1.Text = FullName;
            label2.Text = "Welcome Back "+ UserName +"!";
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT CategoryName, Quantity FROM Stock INNER JOIN Category ON Stock.CategoryID = Category.CategoryID;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader["CategoryName"].ToString();
                            int quantity = Convert.ToInt32(reader["Quantity"]);

                            // Update labels based on category names
                            if (categoryName == "500ML")
                            {
                                label8.Text = "" + quantity;
                            }
                            else if (categoryName == "1L")
                            {
                                label3.Text = "" + quantity;
                            }
                            else if (categoryName == "5L")
                            {
                                label9.Text = "" + quantity;
                            }
                            else if (categoryName == "10L")
                            {
                                label10.Text = "" + quantity;
                            }
                        }
                    }
                }


                DateTime currentDateTime = DateTime.Now;

                // Set the label's Text property to display the current date and time
                label4.Text = currentDateTime.ToString();
            }

          


        }



        private DataTable GetStockPercentageData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.GetStockPercentageByCategory()";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            stock obj = new stock(FullName,UserID);
            obj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("RecordLogoutTime", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", UserID); // Provide the admin's ID
                    cmd.ExecuteNonQuery();
                }

                Login obj = new Login();
                this.Hide();
                obj.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            stock obj=new stock(FullName, UserID);  
            obj.Show(); 
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Category obj = new Category(UserID, FullName);
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }
    }
}
