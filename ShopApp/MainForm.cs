using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopClassLibrary;
using ShopControlLibrary;

namespace ShopApp
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private List<Product> allProducts; // полный список из БД
        private bool isAdminOrManager; // для определения видимости инструментов

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Отображение ФИО
            if (currentUser != null)
                lblUserName.Text = currentUser.FullName;
            else
                lblUserName.Text = "Гость";

            // Определяем, имеет ли пользователь права менеджера/администратора
            isAdminOrManager = currentUser != null && (currentUser.Role == "Менеджер" || currentUser.Role == "Администратор");

            // Настройка видимости инструментов
            panelTools.Visible = isAdminOrManager; // полностью скрываем панель для гостей и обычных клиентов
            // Кнопка добавления товара только для администратора
            btnAddProduct.Visible = currentUser != null && currentUser.Role == "Администратор";

            // Загрузка поставщиков в фильтр
            LoadSuppliers();

            // Загрузка товаров
            LoadProducts();
        }

        // Загрузка списка поставщиков для фильтра
        private void LoadSuppliers()
        {
            try
            {
                var suppliers = DatabaseHelper.GetDistinctSuppliers();
                cmbSupplierFilter.Items.Clear();
                cmbSupplierFilter.Items.Add("Все поставщики");
                cmbSupplierFilter.SelectedIndex = 0;
                foreach (var s in suppliers)
                    cmbSupplierFilter.Items.Add(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка поставщиков: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Загрузка всех товаров из БД
        private void LoadProducts()
        {
            try
            {
                allProducts = DatabaseHelper.GetProducts();
                ApplyFilterAndSort();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Применение текущих фильтров, поиска и сортировки
        private void ApplyFilterAndSort()
        {
            if (allProducts == null) return;

            var filtered = allProducts.AsEnumerable();

            // Поиск по текстовым полям
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(p =>
                    (p.Article?.ToLower().Contains(searchText) ?? false) ||
                    (p.Name?.ToLower().Contains(searchText) ?? false) ||
                    (p.Description?.ToLower().Contains(searchText) ?? false) ||
                    (p.Category?.ToLower().Contains(searchText) ?? false) ||
                    (p.Manufacture?.ToLower().Contains(searchText) ?? false) ||
                    (p.Supplier?.ToLower().Contains(searchText) ?? false)
                );
            }

            // Фильтр по поставщику
            string selectedSupplier = cmbSupplierFilter.Text;
            if (!string.IsNullOrEmpty(selectedSupplier) && selectedSupplier != "Все поставщики")
            {
                filtered = filtered.Where(p => p.Supplier == selectedSupplier);
            }

            // Сортировка
            switch (cmbSort.SelectedIndex)
            {
                case 1: // по возрастанию количества
                    filtered = filtered.OrderBy(p => p.Stock);
                    break;
                case 2: // по убыванию количества
                    filtered = filtered.OrderByDescending(p => p.Stock);
                    break;
                default: // без сортировки (по артикулу для стабильности)
                    filtered = filtered.OrderBy(p => p.Article);
                    break;
            }

            // Отображение
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in filtered)
            {
                var control = new ProductControl();
                control.SetProduct(product);

                // Добавляем обработчик клика для администратора
                if (currentUser != null && currentUser.Role == "Администратор")
                {
                    control.Click += (s, e) => EditProduct(product);
                    // Также можно обрабатывать клик на дочерних элементах, но для простоты ограничимся контролом
                    foreach (Control c in control.Controls)
                    {
                        c.Click += (s, e) => EditProduct(product);
                    }
                }

                flowLayoutPanel1.Controls.Add(control);
            }

            // Если ничего не найдено, можно показать сообщение
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Товары не найдены",
                    AutoSize = true,
                    Font = new Font("Microsoft Sans Serif", 12),
                    Location = new Point(10, 10)
                };
                flowLayoutPanel1.Controls.Add(lblEmpty);
            }
        }

        // Обработчик изменения фильтров
        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        // Редактирование товара (открытие формы)
        private void EditProduct(Product product)
        {
            // Проверяем, не открыта ли уже форма редактирования
            foreach (Form f in Application.OpenForms)
            {
                if (f is ProductEditForm)
                {
                    f.BringToFront();
                    return; // уже открыта
                }
            }

            using (var editForm = new ProductEditForm(product, currentUser))
            {
                editForm.FormClosed += (s, args) => LoadProducts(); // перезагружаем после закрытия
                editForm.ShowDialog();
            }
        }

        // Добавление нового товара
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Проверяем, не открыта ли уже форма редактирования
            foreach (Form f in Application.OpenForms)
            {
                if (f is ProductEditForm)
                {
                    f.BringToFront();
                    return;
                }
            }

            using (var editForm = new ProductEditForm(null, currentUser))
            {
                editForm.FormClosed += (s, args) => LoadProducts();
                editForm.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            // Проверяем, авторизован ли пользователь (кнопка видна только для авторизованных)
            if (currentUser == null) return;

            using (var ordersForm = new OrdersForm(currentUser))
            {
                ordersForm.ShowDialog();
            }
        }
    }
}