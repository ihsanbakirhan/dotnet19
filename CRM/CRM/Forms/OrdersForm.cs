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
    public partial class OrdersForm : Form
    {
        User user;
        DataTable orders = new DataTable();
        SqlConnection connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public OrdersForm()
        {
            InitializeComponent();
        }
        public OrdersForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void ordersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var order = orders.Rows[e.RowIndex];
            var orderDetails = new OrderDetailsForm(user, (Int64)order["OrderId"]);
            orderDetails.MdiParent = this.MdiParent;
            orderDetails.Show();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }
        private void LoadOrders()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT o.OrderId, o.TotalPrice, o.Street, o.Zip, o.Telephone, " +
                    "o.CreateDate, u.UserName, c.city_name, os.StatusName " +
                    "FROM Orders AS o INNER JOIN Users AS u ON o.UserId = u.UserId " +
                    "INNER JOIN Cities AS c ON o.CityId = c.geoname_id " +
                    "INNER JOIN OrderStatus AS os ON o.OrderStatus = os.StatusId", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(orders);
                ordersDataGridView.DataSource = orders;
                ordersDataGridView.Columns["OrderId"].Visible = false;
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
