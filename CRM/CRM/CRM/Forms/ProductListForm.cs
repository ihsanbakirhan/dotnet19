using CRM.Objects;
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
        DataTable products = new DataTable();
      
      
        public ProductListForm()
        {
            InitializeComponent();
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            loadProducts();
       
        }

        public void loadProducts()
        {
            products.Clear();
            SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT p.ProductId,s.SupplierName,p.ProductName,v.VariantName,p.[Description],p.UnitId,p.SupplierId,v.ProductVariantId,v.Stock,v.UnitPrice from Products as p inner join Suppliers as s on p.SupplierId=s.SupplierId inner join ProductVariants as v on p.ProductId=v.ProductId order by p.ProductName", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(products);
                produtcsDataGridView.DataSource = products;
                produtcsDataGridView.Columns["ProductId"].Visible = false;
                produtcsDataGridView.Columns["UnitId"].Visible = false;
                produtcsDataGridView.Columns["SupplierId"].Visible = false;
                produtcsDataGridView.Columns["ProductVariantId"].Visible = false;
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




        private void produtcsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
       
    }
}
