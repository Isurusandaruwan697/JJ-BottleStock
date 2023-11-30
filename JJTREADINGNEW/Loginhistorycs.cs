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
    public partial class Loginhistorycs : Form
    {

        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
        public Loginhistorycs(String FirstName, int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            this.FirstName = FirstName;
        }

        private void Loginhistorycs_Load(object sender, EventArgs e)
        {

            label1.Text = FirstName;
            DateTime currentDateTime = DateTime.Now;

            // Set the label's Text property to display the current date and time
            label4.Text = currentDateTime.ToString();
            LoadData();
            label2.Text = $"Total History : {GetLoginHistoryRowCount()}";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlDataAdapter to fetch data from the view
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM UserLoginHistoryView", connection);

                    // Create a DataTable to store the data
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the data from the view
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the dataGridView1
                    dataGridView1.DataSource = dataTable;

                    // Close the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private int GetLoginHistoryRowCount()
        {
            int rowCount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to execute a SELECT COUNT(*) query
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM LoginHistory", connection))
                    {
                        // Execute the query and get the result
                        rowCount = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // Close the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return rowCount;
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ListUser obj = new ListUser();
            obj.Show();
                
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddUser obj = new AddUser();
            obj.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Prdeictions obj = new Prdeictions(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log obj = new Log(FirstName,UserID);
            obj.Show();
            this.Hide();
        }
    }
}
