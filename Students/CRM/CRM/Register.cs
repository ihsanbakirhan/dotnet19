using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        SqlConnection cn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        private void button1_Click(object sender, EventArgs e)
        {
            string boslar = "";
            if (textBox0.Text.Trim() == "")
            {
                boslar += "UserName ";
            }
            if (textBox1.Text.Trim() == "")
            {
                boslar += "Email ";
            }
            if (textBox2.Text.Trim() == "")
            {
                boslar += "Password ";
            }
            if (textBox3.Text.Trim() == "")
            {
                boslar += "RePassword ";
            }
            if (boslar != "")
            {
                boslar += "boş geçilmemeli!\n";
            }
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"(@)(.+)$"))
            {
                boslar += "Emailini düzgün gir hele!\n";
            }
            if (!Regex.IsMatch(textBox2.Text, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
            {
                boslar += "En az 8 karakter gir ve bunlardan en az 1 büyük 1 küçük 1 rakam ve 1 özel karakteri içermeli!\n";
            }
            if (textBox2.Text != textBox3.Text)
            {
                boslar += "Parolalar uyuşmamakta!\n";
            }
            try
            {
                cn2.Open();
                SqlCommand command = new SqlCommand("Select * From USERS Where " +
                    "(UserName=@UN Or Email=@E)");
                command.Parameters.Add(new SqlParameter("@UN", textBox0.Text));
                command.Parameters.Add(new SqlParameter("@E", textBox1.Text));
                command.Connection = cn2;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    boslar += "Böyle bir kullanıcı zaten var!\n";
                }
                command.Dispose();
            }
            catch
            {
                boslar += ("Bağlantıda sıkıntı var!\n");
            }
            finally
            {
                cn2.Close();
            }
            if (boslar != "")
            {
                MessageBox.Show(boslar);
            }
            else
            {
                if (cn2.State != ConnectionState.Open)
                {
                    cn2.Open();
                }
                    SqlCommand cmd2 = new SqlCommand("insert into Users (UserName,Email,Password) values (" + textBox0.Text + "," + textBox1.Text + "," + textBox2.Text + ")");
                    cmd2.Connection = cn2;
                    cmd2.ExecuteNonQuery();
            }
        }
    }
}