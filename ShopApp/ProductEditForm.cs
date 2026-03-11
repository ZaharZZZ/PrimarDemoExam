using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ShopClassLibrary;
using MySql.Data.MySqlClient;

namespace ShopApp
{
    public partial class ProductEditForm : Form
    {
        private Product currentProduct; // null для нового товара
        private User currentUser;
        private string loadedImagePath; // временный путь к загруженному изображению (или null)
        private bool imageChanged = false;

        public ProductEditForm(Product product, User user)
        {
            InitializeComponent();
            currentProduct = product;
            currentUser = user;

            // Заголовок формы
            if (currentProduct == null)
                this.Text = "Добавление товара";
            else
                this.Text = "Редактирование товара";

            // Заполнение комбобоксов
            LoadComboBoxes();

            // Если редактирование, заполняем поля
            if (currentProduct != null)
            {
                txtArticle.Text = currentProduct.Article;
                txtArticle.ReadOnly = true; // артикул не меняем
                txtName.Text = currentProduct.Name;
                cmbCategory.Text = currentProduct.Category;
                txtDescription.Text = currentProduct.Description;
                cmbManufacturer.Text = currentProduct.Manufacture;
                txtSupplier.Text = currentProduct.Supplier;
                txtPrice.Text = currentProduct.Price.ToString("F2");
                txtUnit.Text = currentProduct.Unit;
                numStock.Value = currentProduct.Stock;
                numDiscount.Value = currentProduct.Discount;

                // Загружаем фото, если есть
                if (!string.IsNullOrEmpty(currentProduct.Photo))
                {
                    string photoPath = Path.Combine(Application.StartupPath, "Resources", currentProduct.Photo);
                    if (File.Exists(photoPath))
                    {
                        try
                        {
                            pbPhoto.Image = Image.FromFile(photoPath);
                        }
                        catch { }
                    }
                }
            }

            // Если фото не загрузилось (нет файла или товар новый), показываем заглушку из файла
            if (pbPhoto.Image == null)
            {
                string defaultImagePath = Path.Combine(Application.StartupPath, "Resources", "picture.png");
                if (File.Exists(defaultImagePath))
                {
                    try
                    {
                        pbPhoto.Image = Image.FromFile(defaultImagePath);
                    }
                    catch
                    {
                        pbPhoto.Image = null;
                    }
                }
            }

            // Кнопка удаления видна только для существующего товара и если пользователь администратор
            btnDelete.Visible = (currentProduct != null) && (currentUser.Role == "Администратор");
        }

        private void LoadComboBoxes()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.AddRange(DatabaseHelper.GetDistinctCategories().ToArray());
                cmbManufacturer.Items.Clear();
                cmbManufacturer.Items.AddRange(DatabaseHelper.GetDistinctManufacturers().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списков: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Загружаем исходное изображение
                        using (var img = Image.FromFile(ofd.FileName))
                        {
                            // Ресайз до 300x200 с сохранением пропорций
                            var resized = ResizeImage(img, 300, 200);
                            // Показываем в PictureBox
                            pbPhoto.Image?.Dispose();
                            pbPhoto.Image = resized;
                            imageChanged = true;
                            // Запоминаем, что изображение было загружено, но пока не сохраняем
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var ratio = Math.Min((double)maxWidth / image.Width, (double)maxHeight / image.Height);
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Артикул не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Наименование не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть неотрицательным числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numStock.Value < 0)
            {
                MessageBox.Show("Количество не может быть отрицательным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка уникальности артикула для нового товара
            if (currentProduct == null)
            {
                try
                {
                    if (!IsArticleUnique(txtArticle.Text.Trim()))
                    {
                        MessageBox.Show("Товар с таким артикулом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка проверки артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Подготовка объекта Product
            Product product = new Product
            {
                Article = txtArticle.Text.Trim(),
                Name = txtName.Text.Trim(),
                Unit = txtUnit.Text.Trim(),
                Price = price,
                Supplier = txtSupplier.Text.Trim(),
                Manufacture = cmbManufacturer.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Discount = (int)numDiscount.Value,
                Stock = (int)numStock.Value,
                Description = txtDescription.Text.Trim(),
                Photo = currentProduct?.Photo ?? "" // пока старое, потом обновим
            };

            // Обработка изображения
            if (imageChanged && pbPhoto.Image != null)
            {
                // Генерируем имя файла на основе артикула (заменяем недопустимые символы)
                string safeArticle = string.Join("_", product.Article.Split(Path.GetInvalidFileNameChars()));
                string fileName = safeArticle + ".jpg";
                string savePath = Path.Combine(Application.StartupPath, "Resources", fileName);

                try
                {
                    // Удаляем старое фото, если оно было и отличается
                    if (currentProduct != null && !string.IsNullOrEmpty(currentProduct.Photo))
                    {
                        string oldPath = Path.Combine(Application.StartupPath, "Resources", currentProduct.Photo);
                        if (File.Exists(oldPath) && oldPath != savePath)
                        {
                            File.Delete(oldPath);
                        }
                    }

                    // Сохраняем новое изображение
                    pbPhoto.Image.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    product.Photo = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // Если изображение не менялось, оставляем старое имя
                if (currentProduct != null)
                    product.Photo = currentProduct.Photo;
                else
                    product.Photo = ""; // для нового товара без фото
            }

            // Сохранение в БД
            try
            {
                if (currentProduct == null)
                {
                    DatabaseHelper.AddProduct(product);
                    MessageBox.Show("Товар успешно добавлен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DatabaseHelper.UpdateProduct(product);
                    MessageBox.Show("Товар успешно обновлён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения в БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsArticleUnique(string article)
        {
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tovar WHERE article = @article";
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 0;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentProduct == null) return;

            // Проверка, есть ли товар в заказах
            try
            {
                if (DatabaseHelper.IsProductInOrders(currentProduct.Article))
                {
                    MessageBox.Show("Нельзя удалить товар, который присутствует в заказах.", "Предупреждение",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки наличия в заказах: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show("Вы уверены, что хотите удалить товар?", "Подтверждение",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Удаляем изображение
                    if (!string.IsNullOrEmpty(currentProduct.Photo))
                    {
                        string photoPath = Path.Combine(Application.StartupPath, "Resources", currentProduct.Photo);
                        if (File.Exists(photoPath))
                            File.Delete(photoPath);
                    }

                    DatabaseHelper.DeleteProduct(currentProduct.Article);
                    MessageBox.Show("Товар удалён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProductEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Освобождаем ресурсы изображения
            if (pbPhoto.Image != null)
            {
                pbPhoto.Image.Dispose();
            }
        }
    }
}