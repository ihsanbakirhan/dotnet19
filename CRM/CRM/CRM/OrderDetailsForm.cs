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
using System.Configuration;

namespace CRM
{
    public partial class OrderDetailsForm : Form
    {
        User user;
        Int64 orderId;
        SqlConnection connection =
              new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
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
                SqlCommand cmd = new SqlCommand("select o.OrderStatus,o.TotalPrice,o.Street,o.Zip,o.Telephone,u.UserName,c.city_name from Orders as o inner join Users as u on o.UserId = u.UserId  inner join Cities as c on o.CityId = c.geoname_id " + " inner join OrderStatus as os on o.OrderStatus=os.StatusId " +
                   "where OrderId = @OrderId", connection);
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserNameLabel.Text = reader["UserName"].ToString();
                    DateLabel.Text = ((DateTime)reader["CrateDate"]).ToShortDateString();
                    AddressLabel.Text = reader["Street"].ToString() + reader["city_name"].ToString();
                    TelephoneLabel.Text = reader["Telephone"].ToString();
                    TotalLabel.Text = reader["TotalPrice"].ToString();
                    StatusLabel.Text = reader["StatusName"].ToString();

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        private void LoadOrderDetails()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select p.ProductName,pv.VariantName,op.Quantity,op.TotalPrice from OrderProducts as op inner join ProductVariants as pv on pv.ProductVariantId=op.ProductVariantId inner join Products as p on p.ProductId = pv.ProductId where OrderId = @OrderId",connection);
                cmd.Parameters.AddWithValue("@OrderId",orderId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if(dt.Rows.Count != 0)
                {
                    dataGridViewProduct.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:"+ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }



    }
}
