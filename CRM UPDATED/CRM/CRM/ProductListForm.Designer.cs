namespace CRM.Forms
{
    partial class ProductListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProductsdataGridView = new System.Windows.Forms.DataGridView();
            this.CartdataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsdataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CartdataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductsdataGridView
            // 
            this.ProductsdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsdataGridView.Location = new System.Drawing.Point(-1, 1);
            this.ProductsdataGridView.Name = "ProductsdataGridView";
            this.ProductsdataGridView.Size = new System.Drawing.Size(414, 449);
            this.ProductsdataGridView.TabIndex = 0;
            // 
            // CartdataGridView2
            // 
            this.CartdataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CartdataGridView2.Location = new System.Drawing.Point(419, 1);
            this.CartdataGridView2.Name = "CartdataGridView2";
            this.CartdataGridView2.Size = new System.Drawing.Size(369, 386);
            this.CartdataGridView2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(434, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total :";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(659, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "CheckOut";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ProductListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CartdataGridView2);
            this.Controls.Add(this.ProductsdataGridView);
            this.Name = "ProductListForm";
            this.Text = "ProductListForm";
            this.Load += new System.EventHandler(this.ProductListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductsdataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CartdataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProductsdataGridView;
        private System.Windows.Forms.DataGridView CartdataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}