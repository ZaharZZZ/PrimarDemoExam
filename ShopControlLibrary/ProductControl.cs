using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ShopClassLibrary;

namespace ShopControlLibrary
{
    public partial class ProductControl : UserControl
    {
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

            // Основные поля
            labelCategoryName.Text = product.Category + " | " + product.Name;
            labelDescription.Text = "Описание товара: " + product.Description;
            labelManufacturer.Text = "Производитель: " + product.Manufacture;
            labelSupplier.Text = "Поставщик: " + product.Supplier;
            labelUnit.Text = "Единица измерения: " + product.Unit;
            labelStockQuantity.Text = "Количество на складе: " + product.Stock;
            labelDiscount.Text = product.Discount.ToString() + "%";

            // Обработка цены и скидки
            decimal price = product.Price;
            int discount = product.Discount;

            if (discount > 0)
            {
                decimal newPrice = price * (100 - discount) / 100;

                // Старая цена – красная, зачёркнутая
                labelPrice.Text = "Цена: " + price.ToString("C");
                labelPrice.Font = new Font(labelPrice.Font, FontStyle.Strikeout);
                labelPrice.ForeColor = Color.Red;

                // Новая цена – черная
                labelNewPrice.Text = "  ⇒ " + newPrice.ToString("C");
                labelNewPrice.ForeColor = Color.Black;
                labelNewPrice.Visible = true;
            }
            else
            {
                labelPrice.Text = "Цена: " + price.ToString("C");
                labelPrice.Font = new Font(labelPrice.Font, FontStyle.Regular);
                labelPrice.ForeColor = Color.Black;
                labelNewPrice.Visible = false;
            }

            // Подсветка фона всего контрола
            if (product.Stock == 0)
            {
                this.BackColor = Color.LightBlue;
            }
            else if (discount > 15)
            {
                this.BackColor = Color.FromArgb(0x2E, 0x8B, 0x57); // #2E8B57
                // Для контраста можно изменить цвет текста на белый, но оставим как есть
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }

            // Загрузка изображения
            LoadImage(product.Photo);
        }

        private void LoadImage(string photoFileName)
        {
            // Путь к папке Resources относительно .exe
            string resourcesFolder = Path.Combine(Application.StartupPath, "Resources");

            // Попытка загрузить изображение товара
            if (!string.IsNullOrEmpty(photoFileName))
            {
                string imagePath = Path.Combine(resourcesFolder, photoFileName);
                if (File.Exists(imagePath))
                {
                    try
                    {
                        pictureObyv.Image?.Dispose();
                        pictureObyv.Image = Image.FromFile(imagePath);
                        return;
                    }
                    catch
                    {
                        // Ошибка загрузки – игнорируем, перейдём к заглушке
                    }
                }
            }

            // Заглушка
            string defaultImagePath = Path.Combine(resourcesFolder, "picture.png");
            if (File.Exists(defaultImagePath))
            {
                try
                {
                    pictureObyv.Image?.Dispose();
                    pictureObyv.Image = Image.FromFile(defaultImagePath);
                }
                catch
                {
                    pictureObyv.Image = null;
                }
            }
            else
            {
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