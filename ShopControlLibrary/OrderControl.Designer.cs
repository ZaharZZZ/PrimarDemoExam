namespace ShopControlLibrary
{
    partial class OrderControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // groupBox1
            this.groupBox1.Controls.Add(this.lblOrderId);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.lblOrderDate);
            this.groupBox1.Controls.Add(this.lblDeliveryDate);
            this.groupBox1.Controls.Add(this.lblCustomer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;

            // lblOrderId
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderId.Location = new System.Drawing.Point(10, 20);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(0, 17);
            this.lblOrderId.TabIndex = 0;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblStatus.Location = new System.Drawing.Point(10, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 1;

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblAddress.Location = new System.Drawing.Point(10, 70);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(0, 16);
            this.lblAddress.TabIndex = 2;

            // lblOrderDate
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblOrderDate.Location = new System.Drawing.Point(10, 95);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(0, 16);
            this.lblOrderDate.TabIndex = 3;

            // lblDeliveryDate
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblDeliveryDate.Location = new System.Drawing.Point(10, 120);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(0, 16);
            this.lblDeliveryDate.TabIndex = 4;

            // lblCustomer
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblCustomer.Location = new System.Drawing.Point(150, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(0, 16);
            this.lblCustomer.TabIndex = 5;

            // OrderControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "OrderControl";
            this.Size = new System.Drawing.Size(300, 150);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.Label lblCustomer;
    }
}
