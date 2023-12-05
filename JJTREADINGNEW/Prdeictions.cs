using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace JJTREADINGNEW
{
    public partial class Prdeictions : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
       
        public Prdeictions(String FirstName, int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            this.FirstName = FirstName;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Prdeictions_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            DateTime currentDateTime = DateTime.Now;

            // Set the label's Text property to display the current date and time
            label4.Text = currentDateTime.ToString();
            LoadChartData();
            LoadStockData();


            }


        private void LoadStockData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Replace the query with your logic to get stock data
                    string query = "SELECT c.CategoryName, s.Quantity, s.Quantity * 100.0 / SUM(s.Quantity) OVER () AS StockPercentage " +
                                   "FROM Category c " +
                                   "JOIN Stock s ON c.CategoryID = s.CategoryID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Clear existing series and points
                            chart2.Series.Clear();

                            // Add a series to the chart
                            Series series = chart2.Series.Add("StockPercentage");
                            series.ChartType = SeriesChartType.Pie;

                            // Add data points to the series
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string categoryName = row["CategoryName"].ToString();
                                double stockPercentage = Convert.ToDouble(row["StockPercentage"]);

                                // Add data points for each category
                                series.Points.AddXY(categoryName, stockPercentage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log obj = new Log(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loginhistorycs obj = new Loginhistorycs(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click_1(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }
    }
}
