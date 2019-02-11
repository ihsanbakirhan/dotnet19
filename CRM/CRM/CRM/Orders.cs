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

namespace CRM
{
    public partial class Orders : Form
    {
        SqlConnection connection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        User user;

        ProductListForm productListForm;
        public Orders()
        {
            InitializeComponent();
        }
        public Orders(User user, ProductListForm productListForm)
        {
            InitializeComponent();
            this.user = user;
            this.productListForm = productListForm;
        }
        private void Orders_Load(object sender, EventArgs e)
        {
            fillCountries();
        }
        public void fillCountries()
        {


            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select DISTINCT * From Countries where country_name is not null  ", connection);
                DataTable dataTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                CountryComboBox.DataSource = dataTable;
                CountryComboBox.DisplayMember = "country_name";
                CountryComboBox.ValueMember = "country_iso_code";
                connection.Close();
            }
            catch (Exception ex)
            {
                //connection.Close();
                MessageBox.Show("Something went wrong.Details : \n" + ex.Message);

            }
            finally
            {
                connection.Close();
            }
        }

        public void fillCities(String CountryIsoCode)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT * FROM Cities WHERE country_iso_code = @CountryIsoCode ORDER BY country_name", connection);
                command.Parameters.AddWithValue("@CountryIsoCode", CountryIsoCode);
                DataTable cityTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(cityTable);
                CityComboBox.DataSource = cityTable;
                CityComboBox.DisplayMember = "city_name";
                CityComboBox.ValueMember = "geoname_id";
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong " + ex.Message);
                // connection.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        private void CountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryComboBox.SelectedIndex > 0)
                fillCities(CountryComboBox.SelectedValue.ToString());
        }
        private void sendForm()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("CreateOrder", connection);
                cmd.Parameters.AddWithValue("@UserId", user.userId);
                cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(CityComboBox.SelectedValue));
                cmd.Parameters.AddWithValue("@Country", CountryComboBox.SelectedValue);
                cmd.Parameters.AddWithValue("@Street", textBoxStreet.Text.Trim());
                cmd.Parameters.AddWithValue("@Zip", textBoxZip.Text.Trim());
                cmd.Parameters.AddWithValue("@Telephone", textBoxTelephone.Text.Trim());

               // var orderId = new SqlParameter("@OrderId", SqlDbType.BigInt).Direction;
             //   orderId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OrderId", SqlDbType.BigInt);
                cmd.Parameters["@OrderId"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Order Saved:" + cmd.Parameters["@OrderId"].Value.ToString());
                productListForm.loadShoppingCart();
                productListForm.LoadProducts();

                var orderDetail = new OrderDetailsForm(user, (Int64)cmd.Parameters["@OrderId"].Value);
                orderDetail.MdiParent = this.MdiParent;
                orderDetail.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:"+ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            var btn = new OrderDetailsForm();
            btn.Show();
            if (textBoxStreet.Text.Length == 0)
            {
                MessageBox.Show("Enter street name...!");
                textBoxStreet.Focus();
            }
            else if (textBoxZip.Text.Length == 0)
            {
                MessageBox.Show("Enter Zip Code...!");
                textBoxZip.Focus();
            }
            else if (textBoxTelephone.Text.Length == 0)
            {
                MessageBox.Show("Enter Telephone...!");
                textBoxTelephone.Focus();
            }
        }
    }
}

