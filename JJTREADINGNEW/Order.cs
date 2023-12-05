using Guna.UI2.WinForms;
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
    public partial class Order : Form
    {
        string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage1;Integrated Security=True";
        private int UserID;
        private string FirstName;
    
            public Order(int UserID, String FirstName)
        {
            InitializeComponent();
                this.UserID = UserID;
                this.FirstName = FirstName;
            }

        private void Order_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            DateTime currentDateTime = DateTime.Now;

            // Set the label's Text property to display the current date and time
            label4.Text = currentDateTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stock obj = new stock(FirstName, UserID);
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customera obj = new Customera(UserID, FirstName);
            obj.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
              int CustomerID= int.Parse(guna2TextBox6.Text);
              string CategoryName = guna2TextBox4.Text;
              int Quantity = int.Parse(guna2TextBox5.Text);
            DateTime currentDate = DateTime.Now;


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertOrderAndOrderStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@Date", currentDate);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        guna2TextBox6.Text = "";
                        guna2TextBox4.Text = "";
                        guna2TextBox5.Text = "";

                        // You can add additional logic here after the insertion
                        MessageBox.Show("Order and OrderStock inserted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            OrderList obj=new OrderList();
            obj.Show();
           
        }
    }
}
