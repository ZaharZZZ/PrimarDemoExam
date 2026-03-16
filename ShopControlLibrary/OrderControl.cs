using System;
using System.Windows.Forms;
using ShopClassLibrary;

namespace ShopControlLibrary
{
    public partial class OrderControl : UserControl
    {
        private Order _order;

        public OrderControl()
        {
            InitializeComponent();
        }

        public void SetOrder(Order order)
        {
            _order = order;
            lblOrderId.Text = $"Заказ № {order.OrderId}";
            lblStatus.Text = $"Статус: {order.Status}";
            lblAddress.Text = $"Адрес: {order.PickupAddress}";
            lblOrderDate.Text = $"Дата заказа: {order.OrderDate:dd.MM.yyyy}";
            lblDeliveryDate.Text = order.DeliveryDate.HasValue
                ? $"Доставка: {order.DeliveryDate:dd.MM.yyyy}"
                : "Доставка: не назначена";
            lblCustomer.Text = $"Клиент: {order.CustomerName}";
        }

        public Order GetOrder() => _order;
    }
}