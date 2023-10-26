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

namespace JJTREADINGNEW
{
    public partial class Category : Form


    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";
        private int UserID;
        private string FullName;
        public Category(int UserID, String FullName)
        {
            InitializeComponent();

            this.UserID = UserID;
            this.FullName = FullName;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboardscreen obj = new Dashboardscreen(UserID, FullName);
            obj.Show();
            this.Hide();

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
            label1.Text = FullName;
            LoadDataIntoDataGridView();
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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName, Reg_Date, Description, UserID) VALUES (@CategoryName, @Reg_Date, @Description, @UserID)", connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@Reg_Date", registrationDate);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Category added successfully!");

                        guna2TextBox2.Text = "";
                        guna2TextBox3.Text = "";
                        LoadDataIntoDataGridView();
                    }
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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the update query
                    string updateQuery = "UPDATE Category SET CategoryName = @CategoryName, Description = @Description WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully!");

                            guna2TextBox2.Text = "";
                            guna2TextBox3.Text = "";
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("No records were updated. Verify the CategoryID.");
                        }
                    }
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
                            guna2TextBox3.Text = "";
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
