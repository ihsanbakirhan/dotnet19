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
    public partial class CheckoutForm : Form
    {
        User user;
        ProductListForm productListForm;
        SqlConnection connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public CheckoutForm()
        {
            InitializeComponent();
        }
        public CheckoutForm(User user, ProductListForm productListForm)
        {
            InitializeComponent();
            this.user = user;
            this.productListForm = productListForm;
        }

        private void checkoutButton_Click(object sender, EventArgs e)
        {
            if (streetTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter street name");
                streetTextBox.Focus();
            }
            else if (zipTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter zip code");
                zipTextBox.Focus();
            }
            else if (phoneTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter telephone number");
                phoneTextBox.Focus();
            }
            else
            {
                sendForm();
            }
        }

        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            LoadCountries();
        }

        private void LoadCountries()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT country_iso_code, country_name from Countries " +
                    "WHERE country_name is not null " +
                    "ORDER BY country_name", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable countries = new DataTable();
                adapter.Fill(countries);
                
                countryComboBox.DataSource = countries;
                countryComboBox.DisplayMember = "country_name";
                countryComboBox.ValueMember = "country_iso_code";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(countryComboBox.SelectedIndex > 0)
            {
                LoadCities(countryComboBox.SelectedValue.ToString());
            }
        }
        private void LoadCities(String CountryCode)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT geoname_id, city_name FROM Cities " +
                    "WHERE country_iso_code=@CountryCode and city_name is not null " +
                    "ORDER BY city_name", connection);
                command.Parameters.AddWithValue("@CountryCode", CountryCode);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable cities = new DataTable();
                adapter.Fill(cities);
                cityComboBox.DataSource = cities;
                cityComboBox.DisplayMember = "city_name";
                cityComboBox.ValueMember = "geoname_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void sendForm()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CreateOrder", connection);
                command.Parameters.AddWithValue("@UserId", user.userId);
                command.Parameters.AddWithValue("@CityId", Convert.ToInt32(cityComboBox.SelectedValue));
                command.Parameters.AddWithValue("@Country", countryComboBox.SelectedValue);
                command.Parameters.AddWithValue("@Street", streetTextBox.Text.Trim());
                command.Parameters.AddWithValue("@Zip", zipTextBox.Text.Trim());
                command.Parameters.AddWithValue("@Telephone", phoneTextBox.Text.Trim());

                command.Parameters.Add("@OrderId", SqlDbType.BigInt);
                command.Parameters["@OrderId"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                MessageBox.Show("Order Saved : " + command.Parameters["@OrderId"].Value.ToString());
                productListForm.LoadShoppingCart();
                productListForm.LoadProducts();

                var orderDetail = new OrderDetailsForm(user, (Int64)command.Parameters["@OrderId"].Value);
                orderDetail.MdiParent = this.MdiParent;
                orderDetail.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
