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
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
        private string UserName;
        public Dashboardscreen(int UserID,String FirstName, String UserName)
        {
            InitializeComponent();
      
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.UserName = UserName;
        }

        public Dashboardscreen(int userID, string FirstName)
        {
            UserID = userID;
            FirstName = FirstName;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Dashboardscreen_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            label2.Text = "Welcome Back "+ UserName +"!";
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";

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

                UpdateTotalQuantityLabel();
                UpdateCustomerAndSupplierCounts();

                DateTime currentDateTime = DateTime.Now;

                // Set the label's Text property to display the current date and time
                label4.Text = currentDateTime.ToString();

                leaba();
            }

          


        }

        private void leaba()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define a SQL query to count the number of categories
                string countQuery = "SELECT COUNT(*) FROM Category";

                using (SqlCommand countCmd = new SqlCommand(countQuery, connection))
                {
                    int categoryCount = (int)countCmd.ExecuteScalar(); // ExecuteScalar to get the count

                    label5.Text ="Total :"+ categoryCount;
                }
            }

        }

        private void UpdateTotalQuantityLabel()
        {
            // Assuming you have a Label control named 'labelTotalQuantity' on your form
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SUM(Quantity) AS TotalQuantity FROM Stock";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int totalQuantity = Convert.ToInt32(result);
                        label6.Text = "Total Stock:\n " + totalQuantity;
                    }
                    else
                    {
                        label6.Text = "No data found in the Stock table.";
                    }
                }
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
            stock obj = new stock(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";

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
           
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Category obj = new Category(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Category obj = new Category(UserID, FirstName);
            obj.Show();
            this.Hide();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Customera obj = new Customera(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supplier obj = new Supplier(UserID,FirstName);
             obj.Show();
            this.Hide();

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        // Method to get the total number of customers
        private int GetCustomerCount()
        {
            int customerCount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Customer";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    customerCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return customerCount;
        }

        // Method to get the total number of suppliers
        private int GetSupplierCount()
        {
            int supplierCount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Supplier";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    supplierCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return supplierCount;
        }


        // Assuming you have Label controls named labelCustomerCount and labelSupplierCount
        private void UpdateCustomerAndSupplierCounts()
        {
            int customerCount = GetCustomerCount();
            int supplierCount = GetSupplierCount();

            label7.Text = "Total : " + customerCount;
            label11.Text = "Total : " + supplierCount;
        }

       

    }
}
