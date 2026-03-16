using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShopClassLibrary;

namespace ShopApp
{
    public partial class OrderEditForm : Form
    {
        private Order _order;
        private User _currentUser;
        private List<PickupPoint> _pickupPoints;
        private List<User> _customers; // для выбора клиента

        public OrderEditForm(Order order, User user)
        {
            InitializeComponent();
            _order = order;
            _currentUser = user;

            LoadPickupPoints();
            LoadCustomers();

            if (_order == null)
            {
                this.Text = "Новый заказ";
                dtpOrderDate.Value = DateTime.Now;
                chkNoDelivery.Checked = true;
                // Можно установить значения по умолчанию
            }
            else
            {
                // Заполняем поля
                txtOrderId.Text = _order.OrderId.ToString();
                cmbStatus.SelectedItem = _order.Status;
                cmbPickupPoint.SelectedValue = _order.PickupPointId;
                dtpOrderDate.Value = _order.OrderDate;
                if (_order.DeliveryDate.HasValue)
                {
                    dtpDeliveryDate.Value = _order.DeliveryDate.Value;
                    chkNoDelivery.Checked = false;
                }
                else
                {
                    chkNoDelivery.Checked = true;
                }
                txtPickupCode.Text = _order.PickupCode;
                cmbCustomer.SelectedValue = _order.CustomerId;
            }
        }

        private void LoadPickupPoints()
        {
            try
            {
                _pickupPoints = DatabaseHelper.GetPickupPoints();
                cmbPickupPoint.DataSource = _pickupPoints;
                cmbPickupPoint.DisplayMember = "Address";
                cmbPickupPoint.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пунктов выдачи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            // Загружаем всех пользователей (можно ограничить только клиентами)
            // Для простоты загрузим всех из таблицы users
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id, full_name FROM users ORDER BY full_name";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        _customers = new List<User>();
                        while (reader.Read())
                        {
                            _customers.Add(new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                FullName = reader["full_name"].ToString()
                            });
                        }
                    }
                }
                cmbCustomer.DataSource = _customers;
                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPickupPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите пункт выдачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPickupCode.Text))
            {
                MessageBox.Show("Введите код получения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Order order = new Order
            {
                OrderId = _order?.OrderId ?? 0,
                OrderDate = dtpOrderDate.Value,
                DeliveryDate = chkNoDelivery.Checked ? (DateTime?)null : dtpDeliveryDate.Value,
                PickupPointId = (int)cmbPickupPoint.SelectedValue,
                CustomerId = (int)cmbCustomer.SelectedValue,
                PickupCode = txtPickupCode.Text.Trim(),
                Status = cmbStatus.SelectedItem.ToString()
            };

            try
            {
                if (_order == null)
                {
                    DatabaseHelper.AddOrder(order);
                    MessageBox.Show("Заказ добавлен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DatabaseHelper.UpdateOrder(order);
                    MessageBox.Show("Заказ обновлён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}