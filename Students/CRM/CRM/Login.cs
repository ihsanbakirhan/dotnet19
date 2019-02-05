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
using System.Configuration;

namespace CRM
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            try
            {
                cn.Open();
                //MessageBox.Show("Database connection success");
                SqlCommand command = new SqlCommand("Select * From USERS Where " +
                    "(UserName=@Handle And Password=@Password) Or" +
                    "(Email=@Handle and Password=@Password)");
                command.Parameters.Add(new SqlParameter("@Handle", tbHandle.Text));
                command.Parameters.Add(new SqlParameter("@Password", tbPassword.Text));
                command.Connection = cn;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("login Success!!!");
                }
                else
                {
                    MessageBox.Show("Okumadı hata!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: "+ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rgstr = new Register();
            rgstr.Show();
        }
    }
}
