namespace ShopControlLibrary
{
    partial class ProductControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureObyv = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.labelStockQuantity = new System.Windows.Forms.Label();
            this.labelCategoryName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelUnit = new System.Windows.Forms.Label();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelNewPrice = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelDiscount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureObyv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureObyv
            // 
            this.pictureObyv.BackColor = System.Drawing.Color.White;
            this.pictureObyv.Location = new System.Drawing.Point(15, 15);
            this.pictureObyv.Name = "pictureObyv";
            this.pictureObyv.Size = new System.Drawing.Size(185, 180);
            this.pictureObyv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureObyv.TabIndex = 0;
            this.pictureObyv.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSupplier);
            this.groupBox1.Controls.Add(this.labelStockQuantity);
            this.groupBox1.Controls.Add(this.labelCategoryName);
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Controls.Add(this.labelUnit);
            this.groupBox1.Controls.Add(this.labelManufacturer);
            this.groupBox1.Controls.Add(this.labelPrice);
            this.groupBox1.Controls.Add(this.labelNewPrice);
            this.groupBox1.Location = new System.Drawing.Point(206, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelSupplier.Location = new System.Drawing.Point(6, 92);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(91, 19);
            this.labelSupplier.TabIndex = 6;
            this.labelSupplier.Text = "Поставщик: ";
            // 
            // labelStockQuantity
            // 
            this.labelStockQuantity.AutoSize = true;
            this.labelStockQuantity.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelStockQuantity.Location = new System.Drawing.Point(6, 152);
            this.labelStockQuantity.Name = "labelStockQuantity";
            this.labelStockQuantity.Size = new System.Drawing.Size(164, 19);
            this.labelStockQuantity.TabIndex = 5;
            this.labelStockQuantity.Text = "Количество на складе: ";
            // 
            // labelCategoryName
            // 
            this.labelCategoryName.AutoSize = true;
            this.labelCategoryName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.labelCategoryName.Location = new System.Drawing.Point(6, 16);
            this.labelCategoryName.Name = "labelCategoryName";
            this.labelCategoryName.Size = new System.Drawing.Size(311, 19);
            this.labelCategoryName.TabIndex = 0;
            this.labelCategoryName.Text = "Категории товара | Наименование товара";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelDescription.Location = new System.Drawing.Point(6, 36);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(134, 19);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Описание товара: ";
            // 
            // labelUnit
            // 
            this.labelUnit.AutoSize = true;
            this.labelUnit.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelUnit.Location = new System.Drawing.Point(6, 132);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(152, 19);
            this.labelUnit.TabIndex = 4;
            this.labelUnit.Text = "Единица измерения: ";
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.AutoSize = true;
            this.labelManufacturer.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelManufacturer.Location = new System.Drawing.Point(6, 72);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(116, 19);
            this.labelManufacturer.TabIndex = 2;
            this.labelManufacturer.Text = "Производитель:";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelPrice.Location = new System.Drawing.Point(6, 112);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(50, 19);
            this.labelPrice.TabIndex = 3;
            this.labelPrice.Text = "Цена: ";
            // 
            // labelNewPrice
            // 
            this.labelNewPrice.AutoSize = true;
            this.labelNewPrice.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.labelNewPrice.Location = new System.Drawing.Point(160, 112);
            this.labelNewPrice.Name = "labelNewPrice";
            this.labelNewPrice.Size = new System.Drawing.Size(0, 19);
            this.labelNewPrice.TabIndex = 7;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(531, 15);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(124, 180);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // labelDiscount
            // 
            this.labelDiscount.AutoSize = true;
            this.labelDiscount.Font = new System.Drawing.Font("Times New Roman", 20.25F);
            this.labelDiscount.Location = new System.Drawing.Point(580, 87);
            this.labelDiscount.Name = "labelDiscount";
            this.labelDiscount.Size = new System.Drawing.Size(28, 31);
            this.labelDiscount.TabIndex = 3;
            this.labelDiscount.Text = "0";
            // 
            // ProductControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelDiscount);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureObyv);
            this.Name = "ProductControl";
            this.Size = new System.Drawing.Size(678, 214);
            ((System.ComponentModel.ISupportInitialize)(this.pictureObyv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox pictureObyv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelManufacturer;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelCategoryName;
        private System.Windows.Forms.Label labelStockQuantity;
        private System.Windows.Forms.Label labelUnit;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelNewPrice; // объявление
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labelDiscount;
        private System.Windows.Forms.Label labelSupplier;
    }
}