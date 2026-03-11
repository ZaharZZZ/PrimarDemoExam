using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ShopClassLibrary;

namespace ShopControlLibrary
{
    public partial class ProductControl : UserControl
    {
        private Product product;

        public ProductControl()
        {
            InitializeComponent();
        }

        public void SetProduct(Product product)
        {
            if (product == null)
            {
                ClearControls();
                return;
            }

            // Заполнение текстовых полей
            labelCategoryName.Text = product.Category + " | " + product.Name;
            labelDescription.Text = "Описание товара: " + product.Description;
            labelManufacturer.Text = "Производитель: " + product.Manufacture;
            labelSupplier.Text = "Поставщик: " + product.Supplier;
                        labelUnit.Text = "Единица измерения: " + product.Unit;
            labelStockQuantity.Text = "Количество на складе: " + product.Stock;
            labelDiscount.Text = product.Discount.ToString(); // можно добавить "%", если нужно

            decimal price = product.Price;
            int discount = product.Discount;

            if (discount > 0)
            {
                // Вычисляем цену со скидкой
                decimal newPrice = price * (100 - discount) / 100;

                // Старая цена – красная, зачеркнутая
                labelPrice.Text = "Цена: " + price.ToString("C");
                labelPrice.Font = new Font(labelPrice.Font, FontStyle.Strikeout);
                labelPrice.ForeColor = Color.Red;

                // Новая цена – черная
                labelNewPrice.Text = "  => " + newPrice.ToString("C"); // можно добавить стрелку или просто пробел
                labelNewPrice.ForeColor = Color.Black;
                labelNewPrice.Visible = true;
            }
            else
            {
                // Без скидки – обычная цена
                labelPrice.Text = "Цена: " + price.ToString("C");
                labelPrice.Font = new Font(labelPrice.Font, FontStyle.Regular);
                labelPrice.ForeColor = Color.Black;
                labelNewPrice.Visible = false;
            }

            // Установка фона в зависимости от наличия и скидки
            if (product.Stock == 0)
            {
                // Товара нет на складе – голубой фон
                this.BackColor = Color.LightBlue;
            }
            else if (discount > 15)
            {
                // Скидка >15% – фон #2E8B57
                this.BackColor = Color.FromArgb(0x2E, 0x8B, 0x57);
            }
            else
            {
                // Обычный фон
                this.BackColor = SystemColors.Control;
            }

            // Загрузка изображения
            LoadImage(product.Photo);
        }

        private void LoadImage(string photoFileName)
        {
            // 1. Пытаемся загрузить указанное в БД изображение
            if (!string.IsNullOrEmpty(photoFileName))
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", photoFileName);
                if (File.Exists(imagePath))
                {
                    try
                    {
                        pictureObyv.Image?.Dispose();
                        pictureObyv.Image = Image.FromFile(imagePath);
                        return; // Успешно загрузили – выходим
                    }
                    catch (Exception ex)
                    {
                        // Логируем ошибку, но продолжаем – попытаемся загрузить заглушку
                        System.Diagnostics.Debug.WriteLine($"Ошибка загрузки {imagePath}: {ex.Message}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Файл не найден: {imagePath}");
                }
            }

            // 2. Если файл не найден или photo пусто – загружаем заглушку picture.png
            string defaultImagePath = Path.Combine(Application.StartupPath, "Resources", "picture.png");
            if (File.Exists(defaultImagePath))
            {
                try
                {
                    pictureObyv.Image?.Dispose();
                    pictureObyv.Image = Image.FromFile(defaultImagePath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка загрузки заглушки {defaultImagePath}: {ex.Message}");
                    pictureObyv.Image = null;
                }
            }
            else
            {
                // Если даже заглушка отсутствует – просто очищаем PictureBox
                System.Diagnostics.Debug.WriteLine($"Файл заглушки не найден: {defaultImagePath}");
                pictureObyv.Image = null;
            }
        }

        private void ClearControls()
        {
            labelCategoryName.Text = "Категория | Наименование";
            labelDescription.Text = "Описание товара: ";
            labelManufacturer.Text = "Производитель: ";
            labelSupplier.Text = "Поставщик: ";
            labelPrice.Text = "Цена: ";
            labelUnit.Text = "Единица измерения: ";
            labelStockQuantity.Text = "Количество на складе: ";
            labelDiscount.Text = "0";
            labelNewPrice.Visible = false;
            this.BackColor = SystemColors.Control;
            pictureObyv.Image = null;
        }


    }
}