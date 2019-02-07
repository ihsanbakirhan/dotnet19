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
        User user;
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
                ProductsdataGridView.DataSource = products;
                ProductsdataGridView.Columns["ProductId"].Visible = false;
                ProductsdataGridView.Columns["UnitId"].Visible = false;
                ProductsdataGridView.Columns["SupplierId"].Visible = false;
                ProductsdataGridView.Columns["ProductVariantId"].Visible = false;
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
