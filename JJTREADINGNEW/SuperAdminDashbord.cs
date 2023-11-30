using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace JJTREADINGNEW
{
    public partial class SuperAdminDashbord : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
        private string UserName;
        public SuperAdminDashbord(int UserID, String FirstName,String UserName)
        {
            InitializeComponent();
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.UserName = UserName;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void SuperAdminDashbord_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            label2.Text = "Welcome Back " + UserName + "!";
            DateTime currentDateTime = DateTime.Now;

            // Set the label's Text property to display the current date and time
            label4.Text = currentDateTime.ToString();

            UpdateCustomerAndSupplierCounts();
            leaba();
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
            }


            LoadChartData();

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

            label11.Text = "Total : " + customerCount;
            label5.Text = "Total : " + supplierCount;
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

                    label6.Text = "Total :" + categoryCount;
                }
            }

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

        private void button6_Click(object sender, EventArgs e)
        {
            Prdeictions obj=new Prdeictions(FirstName,UserID);
            this.Hide();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loginhistorycs obj=new Loginhistorycs(FirstName, UserID);
            this.Hide();
            obj.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log obj = new Log(FirstName, UserID);
            this.Hide();
            obj.Show();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void LoadChartData()
        {
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to retrieve data from the Stock table
                string sqlQuery = "SELECT C.CategoryName, SUM(S.Quantity) AS TotalQuantity " +
                                  "FROM Stock S " +
                                  "JOIN Category C ON S.CategoryID = C.CategoryID " +
                                  "GROUP BY C.CategoryName";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Clear any existing data from the chart
                    chart1.Series.Clear();

                    // Add a new series to the chart
                    Series series = chart1.Series.Add("Categories");
                    series.ChartType = SeriesChartType.Doughnut;

                    // Add data points to the series
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string category = row["CategoryName"].ToString();
                        int quantity = Convert.ToInt32(row["TotalQuantity"]);

                        DataPoint dataPoint = new DataPoint(0, quantity);
                        dataPoint.AxisLabel = category;
                        series.Points.Add(dataPoint);
                    }

                    // Set chart properties (optional)
                    chart1.Titles.Add("Categories by Quantities");
                    chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
    


}
