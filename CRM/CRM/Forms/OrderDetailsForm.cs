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
    public partial class OrderDetailsForm : Form
    {
        User user;
        Int64 orderId;
        SqlConnection connection =
            new SqlConnection(
                ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public OrderDetailsForm()
        {
            InitializeComponent();
        }
        public OrderDetailsForm(User user, Int64 orderId)
        {
            InitializeComponent();
            this.user = user;
            this.orderId = orderId;
        }

        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            LoadOrder();
        }
        private void LoadOrder()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT os.StatusName, o.TotalPrice, " +
                    "o.Street, o.Zip, o.Telephone, o.CreateDate, u.UserName, c.city_name " +
                    "FROM Orders AS o INNER JOIN Users AS u ON o.UserId = u.UserId " +
                    "INNER JOIN Cities AS c ON o.CityId = c.geoname_id " +
                    "INNER JOIN OrderStatus AS os ON o.OrderStatus=os.StatusId " +
                    "WHERE OrderId=@OrderId", connection);
                command.Parameters.AddWithValue("@OrderId", orderId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userNameLabel.Text = reader["UserName"].ToString();
                    dateLabel.Text = ((DateTime)reader["CreateDate"]).ToShortDateString();
                    addressLabel.Text = reader["Street"].ToString() + reader["city_name"].ToString() +
                        reader["Zip"].ToString();
                    telephoneLabel.Text = reader["Telephone"].ToString();
                    priceLabel.Text = reader["TotalPrice"].ToString();
                    statusLabel.Text = reader["StatusName"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadOrderDetails();
        }
        private void LoadOrderDetails()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT p.ProductName, pv.VariantName, " +
                    "op.Quantity, op.TotalPrice FROM OrderProducts AS op " +
                    "INNER JOIN ProductVariants AS pv ON pv.ProductVariantId = op.ProductVariantId " +
                    "INNER JOIN Products AS p ON p.ProductId = pv.ProductId " +
                    "WHERE OrderId=@OrderId", connection);
                command.Parameters.AddWithValue("@OrderId", orderId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable products = new DataTable();
                adapter.Fill(products);
                productsDataGridView.DataSource = products;
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
    }
}
