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
    public partial class ProductListForm : Form
    {
        User user;
        DataTable products = new DataTable();
        DataTable cart = new DataTable();
        SqlConnection connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public ProductListForm()
        {
            InitializeComponent();
        }
        public ProductListForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadShoppingCart();
        }
        public void LoadProducts()
        {
            products.Clear();
                
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT p.ProductId, s.SupplierName, p.ProductName, " +
                    "v.VariantName, p.[Description], p.UnitId, p.SupplierId, v.ProductVariantId, " +
                    "v.Stock, v.UnitPrice " +
                    "FROM Products AS p INNER JOIN Suppliers AS s on p.SupplierId=s.SupplierId " +
                    "INNER JOIN ProductVariants AS v on p.ProductId=v.ProductId " +
                    "ORDER BY p.ProductName", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(products);
                productsDataGridView.DataSource = products;
                productsDataGridView.Columns["ProductId"].Visible = false;
                productsDataGridView.Columns["UnitId"].Visible = false;
                productsDataGridView.Columns["SupplierId"].Visible = false;
                productsDataGridView.Columns["ProductVariantId"].Visible = false;
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
        public void LoadShoppingCart()
        {
            cart.Clear();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT p.ProductId,v.ProductVariantId, p.ProductName, " +
                    "v.VariantName, s.Quantity, v.UnitPrice, (v.UnitPrice * s.Quantity) as TotalPrice " +
                    "FROM Products AS p " +
                    "INNER JOIN ProductVariants AS v on p.ProductId = v.ProductId " +
                    "INNER JOIN ShoppingCarts AS s on s.ProductVariantId = v.ProductVariantId " +
                    "ORDER BY p.ProductName", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(cart);
                cartDataGridView.DataSource = cart;
                cartDataGridView.Columns["ProductId"].Visible = false;
                cartDataGridView.Columns["ProductVariantId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Decimal total = 0;
            foreach (DataRow dr in cart.Rows)
            {
                total += (Decimal)dr["TotalPrice"];
            }
            totalLabel.Text = "Total: " + total.ToString();
        }
        private void productsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dr = products.Rows[e.RowIndex];
                connection.Open();
                SqlCommand command = new SqlCommand("AddShoppingCart", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", user.userId);
                command.Parameters.AddWithValue("@ProductVariantId", Convert.ToInt64(dr["ProductVariantId"]));
                command.Parameters.AddWithValue("@Quantity", 1);
                command.ExecuteNonQuery();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadShoppingCart();
        }

        private void cartDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dr = cart.Rows[e.RowIndex];
                connection.Open();
                SqlCommand command = new SqlCommand("RemoveShoppingCart", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", user.userId);
                command.Parameters.AddWithValue("@ProductVariantId", Convert.ToInt64(dr["ProductVariantId"]));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadShoppingCart();
        }

        private void checkoutButton_Click(object sender, EventArgs e)
        {
            var checkout = new CheckoutForm(user, this);
            checkout.MdiParent = this.MdiParent;
            checkout.Show();
        }
    }
}
