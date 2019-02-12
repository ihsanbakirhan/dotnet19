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
    public partial class VariantEditForm : Form
    {
        User user;
        Product product;
        Variant variant;
        ProductEditForm productEditForm;
        Int16 stock = 0;
        SqlConnection connection =
                new SqlConnection(
                    ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        public VariantEditForm()
        {
            InitializeComponent();
        }
        public VariantEditForm(User user, Product product, Variant variant, ProductEditForm productEditForm)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;
            this.variant = variant;
            this.productEditForm = productEditForm;
        }
        private void VariantEditForm_Load(object sender, EventArgs e)
        {
            if (variant != null)
            {
                variantNameTextBox.Text = variant.variantName;
                costTextBox.Text = variant.cost.ToString();
                unitPriceTextBox.Text = variant.unitPrice.ToString();
                stockTextBox.Text = variant.stock.ToString();
                stock = variant.stock;
            }
        }
        private void minusButton_Click(object sender, EventArgs e)
        {
            try
            {
                stock = Convert.ToInt16(stockTextBox.Text);
                if (stock > 0)
                {
                    stock--;
                    stockTextBox.Text = stock.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Stock must be numeric");
            }
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            try
            {
                stock = Convert.ToInt16(stockTextBox.Text);
                stock++;
                stockTextBox.Text = stock.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stock must be numeric");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (variant == null)
            {
                //Insert Variant
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertProductVariant", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductId", product.productId));
                    command.Parameters.Add(new SqlParameter("@VariantName", variantNameTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", Convert.ToDecimal(unitPriceTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@Cost", Convert.ToDecimal(costTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@CreateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@Stock", Convert.ToInt16(stockTextBox.Text)));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Insert Successfull!");
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
            else
            {
                //Update Variant
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE ProductVariants SET VariantName=@VariantName, " +
                        "UnitPrice=@UnitPrice, Cost=@Cost, Stock=@Stock, UpdateUser=@UpdateUser, " +
                        "UpdateDate=getdate() WHERE ProductVariantId=@ProductVariantId", connection);
                    command.Parameters.Add(new SqlParameter("@ProductVariantId", variant.variantId));
                    command.Parameters.Add(new SqlParameter("@VariantName", variantNameTextBox.Text.Trim()));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", Convert.ToDecimal(unitPriceTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@Cost", Convert.ToDecimal(costTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@UpdateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@Stock", Convert.ToInt16(stockTextBox.Text)));
                    command.ExecuteNonQuery();
                    
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
            if (productEditForm != null)
            {
                productEditForm.LoadVariants();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var status = MessageBox.Show("Are you sure to delete this variant?", "Delete", MessageBoxButtons.YesNo);
            if (status == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM ProductVariants " +
                        "WHERE ProductVariantId=@ProductVariantId", connection);
                    command.Parameters.AddWithValue("@ProductVariantId", variant.variantId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Variant Deleted!");
                    if (productEditForm != null)
                    {
                        productEditForm.LoadVariants();
                    }
                    this.Close();
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
}
