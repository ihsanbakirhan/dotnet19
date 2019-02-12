using CRM.Objects;
using CRM.Forms;
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
    public partial class ProductEditForm : Form
    {
        Product product;
        User user;
        ProductsForm productForm;
        DataTable variants = new DataTable();
        SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public ProductEditForm()
        {
            InitializeComponent();
        }
        public ProductEditForm(Product product, User user, ProductsForm productForm)
        {
            InitializeComponent();
            this.product = product;
            this.user = user;
            this.productForm = productForm;
        }
        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            if (product != null)
            {
                productNameTextBox.Text = product.productName;
                descriptionTextBox.Text = product.description;
                LoadVariants();
            }
            LoadUnits();
            LoadSuppliers();
        }
        private void LoadUnits()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Units ORDER BY UnitName", connection);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                unitComboBox.DataSource = dt;
                unitComboBox.DisplayMember = "UnitName";
                unitComboBox.ValueMember = "UnitId";
                if (product != null)
                {
                    unitComboBox.SelectedValue = product.unitId;
                }
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
        private void LoadSuppliers()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Suppliers ORDER BY SupplierName", connection);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                supplierComboBox.DataSource = dt;
                supplierComboBox.DisplayMember = "SupplierName";
                supplierComboBox.ValueMember = "SupplierId";
                if (product != null)
                {
                    supplierComboBox.SelectedValue = product.supplierId;
                }
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
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (product == null)
            {
                //Insert Product
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductName", productNameTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@Description", descriptionTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@UnitId", unitComboBox.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@CreateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@SupplierId", supplierComboBox.SelectedValue));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Insert Successfull!");
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
            else
            {
                //Update Product
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Products SET ProductName=@ProductName, " +
                        "[Description]=@Description, UnitId=@UnitId, UpdateUser=@UpdateUser, " +
                        "SupplierId=@SupplierId, UpdateDate=getdate() WHERE ProductId=@ProductID", connection);
                    command.Parameters.Add(new SqlParameter("@ProductId", product.productId));
                    command.Parameters.Add(new SqlParameter("@ProductName", productNameTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@Description", descriptionTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@UnitId", unitComboBox.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@UpdateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@SupplierId", supplierComboBox.SelectedValue));
                    command.ExecuteNonQuery();
                    if (productForm != null)
                    {
                        productForm.loadProducts();
                    }
                    MessageBox.Show("Update Successfull!");

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
        public void LoadVariants()
        {
            variants.Clear();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ProductVariantId, VariantName, UnitPrice, Cost, Stock FROM ProductVariants WHERE ProductId=@ProductId ORDER BY VariantName", connection);
                command.Parameters.Add(new SqlParameter("@ProductId", product.productId));
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(variants);
                variantsDataGrid.DataSource = variants;
                variantsDataGrid.Columns["ProductVariantId"].Visible = false;
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

        private void variantsDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dr = variants.Rows[e.RowIndex];
            var variant = new Variant(Convert.ToInt64(dr["ProductVariantId"]),
                dr["VariantName"].ToString(), Convert.ToDecimal(dr["UnitPrice"]),
                Convert.ToDecimal(dr["Cost"]), Convert.ToInt16(dr["Stock"]));
            var variantEdit = new VariantEditForm(user, product, variant, this);
            variantEdit.MdiParent = this.MdiParent;
            variantEdit.Show();
        }

        private void newVariantButton_Click(object sender, EventArgs e)
        {
            var variantEdit = new VariantEditForm(user, product, null, this);
            variantEdit.MdiParent = this.MdiParent;
            variantEdit.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var status = MessageBox.Show("Are you sure to delete this product and all its variants?", "Delete", MessageBoxButtons.YesNo);
            if (status == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Products " +
                        "WHERE ProductId=@ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", product.productId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted!");
                    if (productForm != null)
                    {
                        productForm.loadProducts();
                    }
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
}
