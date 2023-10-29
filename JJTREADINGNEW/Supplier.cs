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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using Guna.UI2.WinForms;

namespace JJTREADINGNEW
{
    public partial class Supplier : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;

       
            public Supplier(int UserID, String FullName)
        {
            InitializeComponent();


            this.UserID = UserID;
            this.FirstName = FirstName;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            DataTable supplierData = GetSupplierData();

            // Assuming you have a DataGridView named dataGridView1
            dataGridView1.DataSource = supplierData;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            SupplyContact obj = new SupplyContact();
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


                // Get the supplier data from your form's controls
                string name = guna2TextBox1.Text;
                string description = guna2TextBox3.Text;
                string email = guna2TextBox4.Text;
                DateTime regDate = DateTime.Now; // Use the current date
             

            // Create an SQL query to insert the supplier data
            string insertQuery = "INSERT INTO Supplier (Name, Description, Email, Reg_Date, UserID) VALUES (@Name, @Description, @Email, @RegDate, @UserID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@RegDate", regDate);
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        // Execute the insert query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier added successfully!");
                            ClearFormFields(); // A custom function to clear input fields
                        GetSupplierData();
                    }
                        else
                        {
                            MessageBox.Show("Supplier insertion failed.");
                        }
                    
                }
            }

        }

        private void ClearFormFields()
        {
            guna2TextBox1.Text = string.Empty;
            guna2TextBox3.Text = string.Empty;
            guna2TextBox4.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click_1(object sender, EventArgs e)
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Get the supplier data from your form's controls
            string name = guna2TextBox1.Text;
            string description = guna2TextBox3.Text;
            string email = guna2TextBox4.Text;
            DateTime regDate = DateTime.Now; // Use the current date

            int supplierID = int.Parse(label5.Text);

            string updateQuery = "UPDATE Supplier SET Name = @Name, Description = @Description, Email = @Email WHERE SupplierID = @SupplierID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                    // Execute the update query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier updated successfully!");
                        ClearFormFields();
                        GetSupplierData();
                    }
                    else
                    {
                        MessageBox.Show("Supplier update failed. Verify the SupplierID.");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                guna2TextBox1.Text = row.Cells["Name"].Value.ToString();
                guna2TextBox3.Text = row.Cells["Description"].Value.ToString();
                guna2TextBox4.Text = row.Cells["Email"].Value.ToString();

                label5.Text = row.Cells["SupplierID"].Value.ToString();


            }

        }

        private DataTable GetSupplierData()
        {
            string query = "SELECT * FROM Supplier";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            try
            {
                int SupplierID = int.Parse(label5.Text); 

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the delete query
                    string deleteQuery = "DELETE FROM Supplier  WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show(" deleted successfully!");

                            // Optionally clear the text boxes and reload data into the DataGridView
                         


                            guna2TextBox1.Text="";
                            guna2TextBox3.Text ="";
                            guna2TextBox4.Text = "";
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
            Category obj = new Category(UserID,FirstName);
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stock obj = new stock(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supplier obj = new Supplier(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customera obj = new Customera(UserID, FirstName);
            obj.Show();
            this.Hide();
        }
    }
    
}
