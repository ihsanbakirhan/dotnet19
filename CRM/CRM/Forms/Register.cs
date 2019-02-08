using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            try
            {
                registerButton.Enabled = false;
                if(FormValidated())
                {
                    InsertUser();
                }
                else
                {
                    registerButton.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool FormValidated()
        {
            bool validated = true;
            if(userNameTextBox.Text.Length == 0)
            {
                validated = false;
                MessageBox.Show("Please enter your Username!");
                userNameTextBox.Focus();
            }
            else if (emailTextBox.Text.Length == 0)
            {
                validated = false;
                MessageBox.Show("Please enter your Email!");
                emailTextBox.Focus();
            }
            else if (passwordTextBox.Text.Length == 0)
            {
                validated = false;
                MessageBox.Show("Please enter your Password!");
                passwordTextBox.Focus();
            }
            else if (!IsValidEmail(emailTextBox.Text))
            {
                validated = false;
                MessageBox.Show("Wrong Email format!");
                emailTextBox.Focus();
            }
            else if (!ValidatePassword(passwordTextBox.Text, out string errorMessage))
            {
                validated = false;
                MessageBox.Show(errorMessage);
                passwordTextBox.Focus();
            }
            else if (passwordTextBox.Text != password2TextBox.Text)
            {
                validated = false;
                MessageBox.Show("Passwords are not matching!");
                password2TextBox.Focus();
            }
            else if (!HandleAvailable(out string errorMessage2))
            {
                validated = false;
                MessageBox.Show(errorMessage2);
            }
            return validated;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,12}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than 8 or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool HandleAvailable(out string ErrorMessage)
        {
            bool available = true;
            ErrorMessage = "";
            SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE " +
                    "(UserName=@UserName) OR " +
                    "(Email=@Email)");
                command.Parameters.Add(new SqlParameter("@UserName", userNameTextBox.Text));
                command.Parameters.Add(new SqlParameter("@Email", emailTextBox.Text));
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if(reader["UserName"].ToString().ToLower() == userNameTextBox.Text.ToLower())
                    {
                        available = false;
                        ErrorMessage = "Username is in use!";
                    }
                    else if (reader["Email"].ToString().ToLower() == emailTextBox.Text.ToLower())
                    {
                        available = false;
                        ErrorMessage = "Email is in use!";
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return available;
        }
        private void InsertUser()
        {
            SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("InsertUser");
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", userNameTextBox.Text));
                command.Parameters.Add(new SqlParameter("@Email", emailTextBox.Text));
                command.Parameters.Add(new SqlParameter("@Password", passwordTextBox.Text));
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                MessageBox.Show("Register Success!");
                this.Close();
            }
        }
    }
}
