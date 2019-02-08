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

namespace CRM
{
    public partial class ProductsForm : Form
    {
        User user;
        DataTable ds = new DataTable();
        public ProductsForm()
        {
            InitializeComponent();
        }
        public ProductsForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            loadProducts();
        }

        private void productsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dr = ds.Rows[e.RowIndex];
            var product = new Product(Convert.ToInt64(dr["ProductId"]), dr["ProductName"].ToString(), dr["Description"].ToString(), Convert.ToInt16(dr["UnitId"]), Convert.ToInt16(dr["SupplierId"]));
            var productEdit = new ProductEditForm(product, user, this);
            productEdit.MdiParent = this.MdiParent;
            productEdit.Show();
        }
        public void loadProducts()
        {
            ds.Clear();
            SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products ORDER BY ProductName", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                productsGrid.DataSource = ds;
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
