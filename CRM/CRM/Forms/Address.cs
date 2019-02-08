using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM.Forms
{
    public partial class Address : Form
    {
        SqlConnection connection =
                   new SqlConnection(
                       ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        DataSet dt = new DataSet();
        public Address()
        {
            InitializeComponent();
        }

        private void Address_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand comm = new SqlCommand("Select country_name From countries where ((country_name is not null) and (country_name != '')) order by country_name", connection);
            SqlDataReader oku = comm.ExecuteReader();
            while (oku.Read())
            {
               CountrycomboBox.Items.Add(oku[0].ToString().Trim('"'));
            }
            connection.Close();
        }

        private void CountrycomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand comm2 = new SqlCommand("Select distinct(subdivision_1_name) From cities where (subdivision_1_name is not null and subdivision_1_name != '' )and country_name = '" + CountrycomboBox.Text + "' order by subdivision_1_name", connection);
            SqlDataReader oku = comm2.ExecuteReader();
            while (oku.Read())
            {
                CitycomboBox.Items.Add(oku[0].ToString().Trim('"'));
            }

        }
    }
}
