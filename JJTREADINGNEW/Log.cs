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

namespace JJTREADINGNEW
{
    public partial class Log : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
        public Log(String FirstName, int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            this.FirstName = FirstName;


        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Log_Load(object sender, EventArgs e)
        {
            label2.Text = FirstName;
            DateTime currentDateTime = DateTime.Now;

            // Set the label's Text property to display the current date and time
            label4.Text = currentDateTime.ToString();
            LoadData();
        }

        private void button8_Click(object sender, EventArgs e)
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

        private void LoadData()
        {
            // Connection string for your database
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";

            // SQL query to retrieve data
            string query = @"
                SELECT
                    C.CategoryName,
                    S.Name AS SupplierName,
                    U.FirstName AS ModifiedBy,
                    AL.Action AS ModificationAction,
                    AL.ActionTime AS ModifiedDate,
                    SL.Quantity,
                    SL.Date AS StockLogDate
                FROM
                    Stock_Log SL
                INNER JOIN
                    Category C ON SL.CategoryID = C.CategoryID
                INNER JOIN
                    Supplier S ON SL.SupplierID = S.SupplierID
                INNER JOIN
                    Users U ON SL.UserID = U.UserID
                LEFT JOIN
                    AuditLog AL ON SL.StLogID = AL.LogID;
            ";

            // Create a DataTable to hold the result of the query
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SqlDataAdapter to execute the query and fill the DataTable
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            int rowCount = dataTable.Rows.Count;

            // Display the row count in a label
            label1.Text = $"Row count: {rowCount}";

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Loginhistorycs obj = new Loginhistorycs( FirstName, UserID);
            this.Hide();
            obj.Show();
        }
    }
}
