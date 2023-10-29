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

namespace JJTREADINGNEW
{
    public partial class SupplyContact : Form
    {
        public SupplyContact()
        {
            InitializeComponent();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the SupplierID and Mobile from your form's controls
                int supplierID = int.Parse(guna2TextBox4.Text);
                string mobile = guna2TextBox1.Text;

                // Define your connection string
                string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to insert data into the Supplier_Mobile table
                    string insertQuery = "INSERT INTO Supplier_Mobile (SupplierID, Mobile) VALUES (@SupplierID, @Mobile)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                        cmd.Parameters.AddWithValue("@Mobile", mobile);

                        // Execute the SQL command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Mobile record added successfully!");
                            guna2TextBox1.Text="";
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add mobile record.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Assuming you have textboxes named txtSupplierID and txtMobile
                guna2TextBox4.Text = selectedRow.Cells["SupplierID"].Value.ToString();
                guna2TextBox1.Text = selectedRow.Cells["Mobile"].Value.ToString();
            }
        }

        private DataTable LoadDataIntoDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
            string query = "SELECT * FROM Supplier_Mobile";

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

        private void SupplyContact_Load(object sender, EventArgs e)
        {
            

            DataTable supplierData = LoadDataIntoDataGridView();

            // Assuming you have a DataGridView named dataGridView1
            dataGridView1.DataSource = supplierData;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
               
          

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        // Call the LoadDataIntoDataGridView method to populate the DataGridView


    }
}
