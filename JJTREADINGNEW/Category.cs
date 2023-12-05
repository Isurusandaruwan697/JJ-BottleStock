using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms.DataVisualization.Charting;

namespace JJTREADINGNEW
{
    public partial class Category : Form


    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
        public Category(int UserID, String FirstName)
        {
            InitializeComponent();

            this.UserID = UserID;
            this.FirstName = FirstName;



        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataIntoDataGridView();
        }
        private void LoadDataIntoDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve data from the Category table
                string query = "SELECT * FROM Category";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        private void Category_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            LoadDataIntoDataGridView();
            LoadChartData();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            try
            {
                string categoryName = guna2TextBox2.Text; // Get the category name from a text box
                DateTime registrationDate = DateTime.Now; // Use the current date for registration
                string description = guna2TextBox3.Text; // Get the description from another text box
                decimal unitPrice;

                // Convert the string to a decimal
                if (decimal.TryParse(guna2TextBox1.Text, out unitPrice))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName, Reg_Date, Description, UserID, UnitPrice) VALUES (@CategoryName, @Reg_Date, @Description, @UserID, @UnitPrice)", connection))
                        {
                            cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                            cmd.Parameters.AddWithValue("@Reg_Date", registrationDate);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice); // Add the UnitPrice parameter

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Category added successfully!");

                            guna2TextBox2.Text = "";
                            guna2TextBox3.Text = "";
                            guna2TextBox1.Text="";
                            LoadDataIntoDataGridView();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Unit Price. Please enter a valid decimal value.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

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

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {



            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                guna2TextBox2.Text = row.Cells["CategoryName"].Value.ToString();
                guna2TextBox3.Text = row.Cells["Description"].Value.ToString();
                guna2TextBox1.Text = row.Cells["UnitPrice"].Value.ToString();

                label3.Text = row.Cells["CategoryID"].Value.ToString();


            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryID = int.Parse(label3.Text); // Get the CategoryID to identify the category to update
                string categoryName = guna2TextBox2.Text; // Get the updated category name from a text box
                string description = guna2TextBox3.Text; // Get the updated description from another text box
                decimal unitPrice;

                // Convert the string to a decimal
                if (decimal.TryParse(guna2TextBox1.Text, out unitPrice))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Define the update query
                        string updateQuery = "UPDATE Category SET CategoryName = @CategoryName, Description = @Description, UnitPrice = @UnitPrice WHERE CategoryID = @CategoryID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice); // Add the UnitPrice parameter

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Category updated successfully!");

                                guna2TextBox2.Text = "";
                                guna2TextBox3.Text = "";
                                guna2TextBox1.Text = "";
                                LoadDataIntoDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No records were updated. Verify the CategoryID.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Unit Price. Please enter a valid decimal value.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryID = int.Parse(label3.Text); // Get the CategoryID to identify the category to delete

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the delete query
                    string deleteQuery = "DELETE FROM Category WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category deleted successfully!");

                            // Optionally clear the text boxes and reload data into the DataGridView
                            guna2TextBox2.Text = "";
                            guna2TextBox3.Text = " ";
                            guna2TextBox1.Text = "";
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted. Verify the CategoryID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            stock obj = new stock(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
         
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Category obj = new Category(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supplier obj=new Supplier(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customera obj = new Customera(UserID, FirstName);
            obj.Show();
            this.Hide();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
