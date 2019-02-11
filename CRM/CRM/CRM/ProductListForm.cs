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
    public partial class ProductListForm : Form
    {
        User user;
        DataTable shoppingcarts = new DataTable();
        DataTable products = new DataTable();
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
        }
        public void LoadProducts()
        {
            products.Clear();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT p.ProductId,s.SupplierName,p.ProductName,v.VariantName,p.[Description],p.UnitId,p.SupplierId,v.ProductVariantId,v.Stock,v.UnitPrice from Products as p inner join Suppliers as s on p.SupplierId=s.SupplierId inner join ProductVariants as v on p.ProductId=v.ProductId order by p.ProductName", connection);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dr = products.Rows[e.RowIndex];
                connection.Open();
                SqlCommand command = new SqlCommand("AddShoppingCart", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", user.userId));
                command.Parameters.Add(new SqlParameter("@ProductVariantId", (Int64)dr["ProductVariantId"]));
                command.Parameters.Add(new SqlParameter("@Quantity", 1));
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
            loadShoppingCart();
        }
        public void loadShoppingCart()
        {
            shoppingcarts.Clear();
            SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {

                connection.Open();
                SqlCommand command = new SqlCommand("select v.ProductVariantId,p.ProductName,v.VariantName,s.Quantity,v.UnitPrice,(s.Quantity*v.UnitPrice) as TotalPrice from ShoppingCarts as s inner join ProductVariants as v on v.ProductVariantId=s.ProductVariantId inner join Products as p on p.ProductId=v.ProductId order by p.ProductName", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(shoppingcarts);
                ShoppingCartDataGrid.DataSource = shoppingcarts;
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
            total();
        }

        private void ShoppingCartDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dw = shoppingcarts.Rows[e.RowIndex];
                connection.Open();
                SqlCommand command = new SqlCommand("RemoveShoppingCart", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", user.userId));
                command.Parameters.Add(new SqlParameter("@ProductVariantId", (Int64)dw["ProductVariantId"]));
                command.ExecuteNonQuery();
                MessageBox.Show("Delete Succesfully!");




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }
            loadShoppingCart();
        }
        private void total()
        {
           decimal top = 0;
            for (int i = 0; i < ShoppingCartDataGrid.RowCount; i++)
            {
                top += Convert.ToDecimal(ShoppingCartDataGrid.Rows[i].Cells["TotalPrice"].Value);
            }
            totalLabel.Text = "Total Price :" + top.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var checkoutbutton = new Orders();
            checkoutbutton.Show();
        }
    }
}
