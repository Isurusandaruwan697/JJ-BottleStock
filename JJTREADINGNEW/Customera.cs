using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JJTREADINGNEW
{
    public partial class Customera : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
       

            public Customera(int UserID, String FirstName)
        {
            InitializeComponent();

            this.UserID = UserID;
            this.FirstName = FirstName;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                guna2TextBox1.Text = row.Cells["CustomerID"].Value.ToString();
                guna2TextBox2.Text = row.Cells["Name"].Value.ToString();
                guna2TextBox4.Text = row.Cells["Address"].Value.ToString();

                


            }
        }

        private void Customera_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            DataTable customerData = GetCustomerData();

            // Assuming you have a DataGridView named dataGridView1
            dataGridView1.DataSource = customerData;
        }
        private DataTable GetCustomerData()
        {
            string query = "SELECT * FROM customer";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            CustomerContact obj=new CustomerContact();
            obj.Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customera obj = new Customera(UserID, FirstName);
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
            

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int CustomerID = int.Parse(guna2TextBox1.Text);
                    string name = guna2TextBox2.Text;
                    DateTime regDate = DateTime.Now;
                    string address = guna2TextBox4.Text;
              

                    using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Reg_Date", regDate);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully!");

                            GetCustomerData();
                            guna2TextBox1.Text = "";
                            guna2TextBox2.Text = "";
                            guna2TextBox4.Text = "";
                            GetCustomerData();
                            DataTable customerData = GetCustomerData();

                            // Assuming you have a DataGridView named dataGridView1
                            dataGridView1.DataSource = customerData;

                        }
                        else
                        {
                            MessageBox.Show("Data insertion failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            // Get the supplier data from your form's controls
            string CustomerID = guna2TextBox1.Text;
            string Name = guna2TextBox2.Text;
            string Address = guna2TextBox4.Text;
            DateTime regDate = DateTime.Now; // Use the current date

            

            string updateQuery = "UPDATE Customer SET Name = @Name, Address = @Address WHERE CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Reg_Date", regDate);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    
                    // Execute the update query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier updated successfully!");
                        GetCustomerData();
                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2TextBox4.Text = "";
                        DataTable customerData = GetCustomerData();

                        // Assuming you have a DataGridView named dataGridView1
                        dataGridView1.DataSource = customerData;
                    }
                    else
                    {
                        MessageBox.Show("Customer update failed. Verify the CustomerID.");
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            try
            {
                int CustomerID = int.Parse(guna2TextBox1.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the delete query
                    string deleteQuery = "DELETE FROM Customer  WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show(" deleted successfully!");

                            // Optionally clear the text boxes and reload data into the DataGridView



                            guna2TextBox1.Text = "";
                            guna2TextBox2.Text = "";
                            guna2TextBox4.Text = "";
                            GetCustomerData();
                            DataTable customerData = GetCustomerData();

                            // Assuming you have a DataGridView named dataGridView1
                            dataGridView1.DataSource = customerData;
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted. Verify the supplierID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Category obj = new Category(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supplier obj = new Supplier(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stock obj = new stock(FirstName, UserID);
            obj.Show();
            this.Hide();
        }
    }
}
