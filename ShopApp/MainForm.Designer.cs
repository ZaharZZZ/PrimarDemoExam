namespace ShopApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelUser = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelTools = new System.Windows.Forms.Panel();
            this.btnOrders = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.cmbSupplierFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelUser.SuspendLayout();
            this.panelTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Chartreuse;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 138);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(822, 331);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelUser
            // 
            this.panelUser.BackColor = System.Drawing.Color.Chartreuse;
            this.panelUser.Controls.Add(this.lblUserName);
            this.panelUser.Controls.Add(this.btnLogout);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUser.Location = new System.Drawing.Point(0, 0);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(822, 40);
            this.panelUser.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(12, 10);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 17);
            this.lblUserName.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnLogout.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLogout.Location = new System.Drawing.Point(699, 6);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 31);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.Color.White;
            this.panelTools.Controls.Add(this.btnOrders);
            this.panelTools.Controls.Add(this.pictureBox1);
            this.panelTools.Controls.Add(this.btnAddProduct);
            this.panelTools.Controls.Add(this.cmbSort);
            this.panelTools.Controls.Add(this.cmbSupplierFilter);
            this.panelTools.Controls.Add(this.txtSearch);
            this.panelTools.Controls.Add(this.label1);
            this.panelTools.Controls.Add(this.label2);
            this.panelTools.Controls.Add(this.label3);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(0, 40);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(822, 98);
            this.panelTools.TabIndex = 2;
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(170, 45);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(108, 36);
            this.btnOrders.TabIndex = 8;
            this.btnOrders.Text = "Заказы";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(710, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnAddProduct.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddProduct.Location = new System.Drawing.Point(12, 45);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(138, 36);
            this.btnAddProduct.TabIndex = 6;
            this.btnAddProduct.Text = "Добавить товар";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Visible = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Без сортировки",
            "По количеству (возрастание)",
            "По количеству (убывание)"});
            this.cmbSort.Location = new System.Drawing.Point(452, 15);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(150, 21);
            this.cmbSort.TabIndex = 5;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cmbSupplierFilter
            // 
            this.cmbSupplierFilter.FormattingEnabled = true;
            this.cmbSupplierFilter.Location = new System.Drawing.Point(253, 14);
            this.cmbSupplierFilter.Name = "cmbSupplierFilter";
            this.cmbSupplierFilter.Size = new System.Drawing.Size(120, 21);
            this.cmbSupplierFilter.TabIndex = 4;
            this.cmbSupplierFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            this.cmbSupplierFilter.TextUpdate += new System.EventHandler(this.FilterChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(70, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поиск:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(176, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Поставщ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(379, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Сортир.:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 469);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.panelUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Каталог товаров";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.panelTools.ResumeLayout(false);
            this.panelTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.ComboBox cmbSupplierFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOrders;
    }
}