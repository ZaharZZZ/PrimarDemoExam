using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ShopClassLibrary;
using ShopControlLibrary;

namespace ShopApp
{
    public partial class MainForm : Form
    {
        private User currentUser;

        // Конструктор для авторизованного пользователя или гостя (null)
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Отображение ФИО
            if (currentUser != null)
                lblUserName.Text = currentUser.FullName;
            else
                lblUserName.Text = "Гость";

            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                List<Product> products = DatabaseHelper.GetProducts();
                flowLayoutPanel1.Controls.Clear();

                foreach (Product product in products)
                {
                    var control = new ProductControl();
                    control.SetProduct(product);
                    flowLayoutPanel1.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Возврат на форму входа
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close(); // закрыть текущую форму
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если закрывается главная форма, но мы не хотим завершать приложение, когда открыта форма входа,
            // можно разрешить закрытие только если пользователь вышел (уже открыта LoginForm).
            // В данном случае просто разрешаем закрытие.
        }
    }
}