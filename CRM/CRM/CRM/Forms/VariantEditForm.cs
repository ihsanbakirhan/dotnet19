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
                VariantNameTextBox.Text = variant.variantName;
                CostTextBox.Text = variant.cost.ToString();
                UnitPriceTextBox.Text = variant.unitPrice.ToString();
                StockTextBox.Text = variant.stock.ToString();
                stock = variant.stock;

            }
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            try
            {
                stock = Convert.ToInt16(StockTextBox.Text);
                if (stock > 0)
                {
                    stock--;
                    StockTextBox.Text = stock.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Stock must be numeric" + ex.Message);
            }
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            try
            {
                stock = Convert.ToInt16(StockTextBox.Text);
                stock++;
                StockTextBox.Text = stock.ToString();


            }
            catch (Exception ex)
            {

                MessageBox.Show("stock must be numeric" + ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SqlConnection connection =
                   new SqlConnection(
                       ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            if (variant == null)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertProductVariant", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductId", product.productId));
                    command.Parameters.Add(new SqlParameter("@VariantName", VariantNameTextBox.Text));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", Convert.ToDecimal(UnitPriceTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@Cost", Convert.ToDecimal(CostTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@CreateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@Stock", Convert.ToInt16(StockTextBox.Text)));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Insert Successfull!");
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
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE ProductVariants SET ProductId=@ProductId,VariantName=@VariantName, UnitPrice=@Unitprice, Cost=@Cost,UpdateUser=@UpdateUser,Stock=@Stock where ProductVariantId=@ProductVariantId", connection);
                    command.Parameters.Add(new SqlParameter("@ProductVariantId", variant.variantId));
                    command.Parameters.Add(new SqlParameter("@ProductId", product.productId));
                    command.Parameters.Add(new SqlParameter("@VariantName", VariantNameTextBox.Text));
                    command.Parameters.Add(new SqlParameter("@UnitPrice", Convert.ToDecimal(UnitPriceTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@Cost", Convert.ToDecimal(CostTextBox.Text)));
                    command.Parameters.Add(new SqlParameter("@UpdateUser", user.userId));
                    command.Parameters.Add(new SqlParameter("@Stock", Convert.ToInt16(StockTextBox.Text)));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Update Successfull!");


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
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection connection =
                   new SqlConnection(
                       ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

            {
                var status = MessageBox.Show("Are you sure to delete this variant?", "Delete", MessageBoxButtons.YesNo);
                if (status == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("DELETE FROM ProductVariants WHERE ProductVariantId=@ProductVariantId", connection);
                        command.Parameters.AddWithValue("@ProductVariantId", variant.variantId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Variant Deleted");
                        if (productEditForm != null)
                        {
                            productEditForm.LoadVariants();

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
}

