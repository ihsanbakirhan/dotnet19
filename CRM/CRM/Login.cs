using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CRM
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SqlConnection connection = 
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE " +
                    "(UserName=@Handle AND Password=@Password) OR " +
                    "(Email=@Handle AND Password=@Password)");
                command.Parameters.Add(new SqlParameter("@Handle", handleTextBox.Text));
                command.Parameters.Add(new SqlParameter("@Password", passwordTextBox.Text));
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if(reader["Password"].ToString() == passwordTextBox.Text)
                    {
                        MessageBox.Show("Login Success!");
                    }
                    else
                    {
                        MessageBox.Show("Error in login information!");
                    }
                }
                else
                {
                    MessageBox.Show("Error in login information!");
                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();
        }
    }
}
