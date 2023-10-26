using Guna.UI2.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class stock : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";
        private int UserID;
        private string FullName;
        public stock(String FullName, int UserID)
        {
            InitializeComponent();

            this.UserID = UserID;
            this.FullName = FullName;
           
        }
       
       

            private void stock_Load(object sender, EventArgs e)
        {
            label1.Text = FullName;
            LoadStockData();

            
           LoadStockLogDataWithNames();
         

        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    int StLogID = int.Parse(label3.Text);
                    string categoryName = guna2TextBox1.Text;
                    int quantityChange = int.Parse(guna2TextBox3.Text);
                    int supplierID = int.Parse(guna2TextBox5.Text);
                    string action = checkBox1.Checked ? "A" : "R"; // Determine the action based on the checkbox state

                    connection.Open();
                    string query = "UpdateStockLog";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StLogID", StLogID);
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@NewQuantity", quantityChange);
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                        cmd.Parameters.AddWithValue("@Action", action);

                        cmd.ExecuteNonQuery();
                    }

                    LoadStockData();
                    guna2TextBox1.Text = "";
                    guna2TextBox3.Text = "";
                    guna2TextBox5.Text = "";

                    MessageBox.Show("Update successful!");
                    LoadStockLogDataWithNames();
                }
                catch (FormatException ex)
                {
                    // Handle the exception, e.g., show an error message for invalid input format
                    MessageBox.Show("Error: Invalid input format. Please enter valid numbers.");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions, e.g., show a general error message
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }


        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError error in e.Errors)
            {
                if (error.Class == 0)
                {
                    string message = error.Message;
                    // Handle or store the PRINT message as needed
                    Console.WriteLine("PRINT message: " + message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            string categoryName = guna2TextBox1.Text;
            int quantityChange = int.Parse(guna2TextBox3.Text);
            int supplierID = int.Parse(guna2TextBox5.Text);

            // Define your SQL connection string

            try
            {
                // Create a SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to execute the stored procedure
                    using (SqlCommand cmd = new SqlCommand("InsertOrUpdateStockData", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@QuantityChange", quantityChange);
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);  // Pass the user ID

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();
                        LoadStockData();
                        LoadStockLogDataWithNames();
                        guna2TextBox1.Text = "";
                        guna2TextBox3.Text = "";
                        guna2TextBox5.Text = "";

                    }
                }

                // Provide feedback to the user, e.g., show a success message.
                MessageBox.Show("Stock data added successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

       


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


           

        }
        
            private void LoadStockData()
            {
                try
                {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT S.StockID, S.Quantity, S.CategoryID, C.CategoryName FROM Stock S JOIN Category C ON S.CategoryID = C.CategoryID;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);


                        // Clear any existing series in the chart
                        chart1.Series.Clear();

                        // Create a new series for the chart
                        Series series = new Series("StockQuantity");

                        // Set the chart type to bar chart (you can choose a different chart type)
                        series.ChartType = SeriesChartType.Column;

                        // Loop through the rows in the DataTable and add data points to the series
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string category = row["CategoryName"].ToString();
                            int quantity = Convert.ToInt32(row["Quantity"]);
                            series.Points.AddXY(category, quantity);
                        }

                        // Add the series to the chart
                        chart1.Series.Add(series);
                    }
                }
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            string stockAlertMessage = GetStockQuantityAlertMessage();

            //Display the alert message
            MessageBox.Show(stockAlertMessage, "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];


                label3.Text = selectedRow.Cells[0].Value.ToString();


                guna2TextBox1.Text = selectedRow.Cells[2].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells[4].Value.ToString();
                guna2TextBox5.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        private string GetStockQuantityAlertMessage()
        {
            string alertMessage = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT dbo.CheckStockQuantity() AS AlertMessage", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            alertMessage = reader["AlertMessage"].ToString();
                        }
                    }
                }
            }

            return alertMessage;
        }


        public class StockLogData
        {
            public int StLogID { get; set; }
            public DateTime Date { get; set; }
            public string CategoryName { get; set; }
            public int Quantity { get; set; }
            public string SupplierName { get; set; }
            public int QuantityChange { get; set; }
        }


        private void LoadStockLogDataWithNames()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM GetStockLogDataWithNames();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Clear the existing columns in the DataGridView
                        dataGridView1.Columns.Clear();

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Auto-generate the columns based on the DataTable
                        dataGridView1.AutoGenerateColumns = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                int StLogID = int.Parse(label3.Text);
                
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteStockLogAndReduceStock", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Provide the StLogIDToDelete as a parameter
                    cmd.Parameters.AddWithValue("@StLogID", StLogID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Stock Log deleted, and stock quantity reduced successfully.");
                        MessageBox.Show("Delete successful!");
                        LoadStockLogDataWithNames();
                        LoadStockData();
                        guna2TextBox1.Text = "";
                        guna2TextBox3.Text = "";
                        guna2TextBox5.Text = "";



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }


      
    }
}
