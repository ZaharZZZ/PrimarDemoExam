using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ShopClassLibrary;
using ShopControlLibrary;

namespace ShopApp
{
    public partial class OrdersForm : Form
    {
        private User currentUser;
        private List<Order> allOrders;
        private OrderControl selectedControl;

        public OrdersForm(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Настройка доступности кнопок в зависимости от роли
            bool canEdit = currentUser != null && (currentUser.Role == "Администратор" || currentUser.Role == "Менеджер");
            btnAdd.Visible = canEdit;
            btnDelete.Visible = canEdit;

            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                allOrders = DatabaseHelper.GetOrders();
                flowLayoutPanel1.Controls.Clear();

                foreach (var order in allOrders)
                {
                    var control = new OrderControl();
                    control.SetOrder(order);
                    control.Margin = new Padding(5);
                    control.Click += (s, e) => SelectControl(control);
                    control.DoubleClick += (s, e) => EditOrder(control.GetOrder());
                    // Добавляем обработчик клика на все дочерние элементы для выделения
                    foreach (Control c in control.Controls)
                    {
                        c.Click += (s, e) => SelectControl(control);
                    }
                    flowLayoutPanel1.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectControl(OrderControl control)
        {
            // Снимаем выделение с предыдущего
            if (selectedControl != null)
                selectedControl.BackColor = Color.White;
            selectedControl = control;
            selectedControl.BackColor = Color.LightGray; // визуальное выделение
        }

        private void EditOrder(Order order)
        {
            if (!btnAdd.Visible) // если нет прав, не открываем
                return;

            using (var editForm = new OrderEditForm(order, currentUser))
            {
                editForm.FormClosed += (s, args) => LoadOrders();
                editForm.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new OrderEditForm(null, currentUser))
            {
                editForm.FormClosed += (s, args) => LoadOrders();
                editForm.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedControl == null)
            {
                MessageBox.Show("Выберите заказ для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var order = selectedControl.GetOrder();
            var confirm = MessageBox.Show($"Удалить заказ № {order.OrderId}?", "Подтверждение",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.DeleteOrder(order.OrderId);
                    LoadOrders();
                    selectedControl = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}