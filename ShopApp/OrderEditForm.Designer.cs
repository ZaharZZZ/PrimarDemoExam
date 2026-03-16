using System.Windows.Forms;

namespace ShopApp
{
    partial class OrderEditForm
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
            this.lblOrderId = new System.Windows.Forms.Label();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblPickupPoint = new System.Windows.Forms.Label();
            this.cmbPickupPoint = new System.Windows.Forms.ComboBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.chkNoDelivery = new System.Windows.Forms.CheckBox();
            this.lblPickupCode = new System.Windows.Forms.Label();
            this.txtPickupCode = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox(); // для выбора пользователя
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Расположение элементов (упрощённо)
            this.lblOrderId.Text = "Номер заказа:";
            this.lblOrderId.Location = new System.Drawing.Point(20, 20);
            this.txtOrderId.Location = new System.Drawing.Point(120, 17);
            this.txtOrderId.ReadOnly = true;

            this.lblStatus.Text = "Статус:";
            this.lblStatus.Location = new System.Drawing.Point(20, 50);
            this.cmbStatus.Location = new System.Drawing.Point(120, 47);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new[] { "Новый", "В обработке", "Завершен", "Отменён" });

            this.lblPickupPoint.Text = "Пункт выдачи:";
            this.lblPickupPoint.Location = new System.Drawing.Point(20, 80);
            this.cmbPickupPoint.Location = new System.Drawing.Point(120, 77);
            this.cmbPickupPoint.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPickupPoint.DisplayMember = "Address";
            this.cmbPickupPoint.ValueMember = "Id";

            this.lblOrderDate.Text = "Дата заказа:";
            this.lblOrderDate.Location = new System.Drawing.Point(20, 110);
            this.dtpOrderDate.Location = new System.Drawing.Point(120, 107);
            this.dtpOrderDate.Format = DateTimePickerFormat.Short;

            this.lblDeliveryDate.Text = "Дата доставки:";
            this.lblDeliveryDate.Location = new System.Drawing.Point(20, 140);
            this.dtpDeliveryDate.Location = new System.Drawing.Point(120, 137);
            this.dtpDeliveryDate.Format = DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Enabled = false;

            this.chkNoDelivery.Text = "Не назначена";
            this.chkNoDelivery.Location = new System.Drawing.Point(250, 140);
            this.chkNoDelivery.Checked = true;
            this.chkNoDelivery.CheckedChanged += (s, e) => dtpDeliveryDate.Enabled = !chkNoDelivery.Checked;

            this.lblPickupCode.Text = "Код получения:";
            this.lblPickupCode.Location = new System.Drawing.Point(20, 170);
            this.txtPickupCode.Location = new System.Drawing.Point(120, 167);

            this.lblCustomer.Text = "Клиент:";
            this.lblCustomer.Location = new System.Drawing.Point(20, 200);
            this.cmbCustomer.Location = new System.Drawing.Point(120, 197);
            this.cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomer.DisplayMember = "FullName";
            this.cmbCustomer.ValueMember = "Id";

            this.btnSave.Text = "Сохранить";
            this.btnSave.Location = new System.Drawing.Point(120, 240);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(200, 240);
            this.btnCancel.Click += (s, e) => this.Close();

            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.AddRange(new Control[] {
                lblOrderId, txtOrderId,
                lblStatus, cmbStatus,
                lblPickupPoint, cmbPickupPoint,
                lblOrderDate, dtpOrderDate,
                lblDeliveryDate, dtpDeliveryDate, chkNoDelivery,
                lblPickupCode, txtPickupCode,
                lblCustomer, cmbCustomer,
                btnSave, btnCancel
            });
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.Text = "Редактирование заказа";
        }

        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblPickupPoint;
        private System.Windows.Forms.ComboBox cmbPickupPoint;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.CheckBox chkNoDelivery;
        private System.Windows.Forms.Label lblPickupCode;
        private System.Windows.Forms.TextBox txtPickupCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}