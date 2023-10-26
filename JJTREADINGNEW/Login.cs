using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace JJTREADINGNEW
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-386L026\\SQLEXPRESS;Initial Catalog=JJBottleStage;Integrated Security=True";
            string Username = guna2TextBox2.Text;
            string Password = guna2TextBox1.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT UserID, Role, FullName,UserName FROM Users WHERE UserName = @Username AND Password = @Password", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            int UserID = reader.GetInt32(0);
                            string role = reader.GetString(1);
                            string UserName = reader.GetString(1);
                            string FullName = reader.GetString(2); // Use 2 instead of 1 for FullName
                            reader.Close();

                            // Call the LogLogin stored procedure to store the login time
                            using (SqlCommand recordLoginTimeCmd = new SqlCommand("InsertLoginHistory", con))
                            {
                                recordLoginTimeCmd.CommandType = CommandType.StoredProcedure;
                                recordLoginTimeCmd.Parameters.AddWithValue("@UserID", UserID);
                                recordLoginTimeCmd.Parameters.AddWithValue("@LoginTime", DateTime.Now); // You can use DateTime.Now to record login time
                                recordLoginTimeCmd.ExecuteNonQuery();
                            }

                            if (role == "User")
                            {
                                Dashboardscreen obj = new Dashboardscreen(UserID, FullName,UserName);
                                this.Hide();
                                obj.Show();
                            }
                            else if (role == "Admin")
                            {
                                SuperAdminDashbord superAdminObj = new SuperAdminDashbord();
                                this.Hide();
                                superAdminObj.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
