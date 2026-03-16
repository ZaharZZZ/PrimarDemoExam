using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ShopClassLibrary
{
    public static class DatabaseHelper
    {
        private static string connectionString = "server=127.0.0.1;user=root;database=shop;password=vertrigo;";
        public static string ConnectionString => connectionString;
        // Получение всех товаров
        public static List<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT article, name, unit, price, supplier, manufacturer, category, 
                           discount, stock_quantity, description, photo 
                    FROM tovar";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Article = reader["article"].ToString(),
                            Name = reader["name"].ToString(),
                            Unit = reader["unit"].ToString(),
                            Price = Convert.ToDecimal(reader["price"]),
                            Supplier = reader["supplier"]?.ToString() ?? "",
                            Manufacture = reader["manufacturer"]?.ToString() ?? "",
                            Category = reader["category"]?.ToString() ?? "",
                            Discount = reader["discount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["discount"]),
                            Stock = reader["stock_quantity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock_quantity"]),
                            Description = reader["description"]?.ToString() ?? "",
                            Photo = reader["photo"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return products;
        }

        // Аутентификация пользователя
        public static User AuthenticateUser(string login, string password)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, role, full_name, login, password FROM users WHERE login = @login AND password = @password";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Role = reader["role"].ToString(),
                                FullName = reader["full_name"].ToString(),
                                Login = reader["login"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Получение списка уникальных поставщиков
        public static List<string> GetDistinctSuppliers()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT supplier FROM tovar WHERE supplier IS NOT NULL AND supplier != '' ORDER BY supplier";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader[0].ToString());
                }
            }
            return list;
        }

        // Получение списка уникальных категорий
        public static List<string> GetDistinctCategories()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT category FROM tovar WHERE category IS NOT NULL AND category != '' ORDER BY category";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader[0].ToString());
                }
            }
            return list;
        }

        // Получение списка уникальных производителей
        public static List<string> GetDistinctManufacturers()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT manufacturer FROM tovar WHERE manufacturer IS NOT NULL AND manufacturer != '' ORDER BY manufacturer";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader[0].ToString());
                }
            }
            return list;
        }

        // Проверка, есть ли товар в заказах
        public static bool IsProductInOrders(string article)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM order_items WHERE article = @article";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Добавление нового товара
        public static void AddProduct(Product product)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO tovar (article, name, unit, price, supplier, manufacturer, category, discount, stock_quantity, description, photo)
                                 VALUES (@article, @name, @unit, @price, @supplier, @manufacturer, @category, @discount, @stock, @description, @photo)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", product.Article);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@unit", product.Unit);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@supplier", string.IsNullOrEmpty(product.Supplier) ? DBNull.Value : (object)product.Supplier);
                    cmd.Parameters.AddWithValue("@manufacturer", string.IsNullOrEmpty(product.Manufacture) ? DBNull.Value : (object)product.Manufacture);
                    cmd.Parameters.AddWithValue("@category", string.IsNullOrEmpty(product.Category) ? DBNull.Value : (object)product.Category);
                    cmd.Parameters.AddWithValue("@discount", product.Discount);
                    cmd.Parameters.AddWithValue("@stock", product.Stock);
                    cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(product.Description) ? DBNull.Value : (object)product.Description);
                    cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(product.Photo) ? DBNull.Value : (object)product.Photo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Обновление существующего товара
        public static void UpdateProduct(Product product)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE tovar SET name=@name, unit=@unit, price=@price, supplier=@supplier, 
                                 manufacturer=@manufacturer, category=@category, discount=@discount, 
                                 stock_quantity=@stock, description=@description, photo=@photo 
                                 WHERE article=@article";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", product.Article);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@unit", product.Unit);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@supplier", string.IsNullOrEmpty(product.Supplier) ? DBNull.Value : (object)product.Supplier);
                    cmd.Parameters.AddWithValue("@manufacturer", string.IsNullOrEmpty(product.Manufacture) ? DBNull.Value : (object)product.Manufacture);
                    cmd.Parameters.AddWithValue("@category", string.IsNullOrEmpty(product.Category) ? DBNull.Value : (object)product.Category);
                    cmd.Parameters.AddWithValue("@discount", product.Discount);
                    cmd.Parameters.AddWithValue("@stock", product.Stock);
                    cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(product.Description) ? DBNull.Value : (object)product.Description);
                    cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(product.Photo) ? DBNull.Value : (object)product.Photo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Удаление товара
        public static void DeleteProduct(string article)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM tovar WHERE article = @article";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Order> GetOrders()
        {
            var orders = new List<Order>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT o.order_id, o.order_date, o.delivery_date, o.pickup_point_id, 
                   o.customer_name, o.pickup_code, o.status, p.address as pickup_address,
                   u.full_name as customer_fullname
            FROM orders o
            JOIN pickup_points p ON o.pickup_point_id = p.id
            JOIN users u ON o.customer_name = u.id
            ORDER BY o.order_date DESC";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderId = Convert.ToInt32(reader["order_id"]),
                            OrderDate = Convert.ToDateTime(reader["order_date"]),
                            DeliveryDate = reader["delivery_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["delivery_date"]),
                            PickupPointId = Convert.ToInt32(reader["pickup_point_id"]),
                            PickupAddress = reader["pickup_address"].ToString(),
                            CustomerId = Convert.ToInt32(reader["customer_name"]),
                            CustomerName = reader["customer_fullname"].ToString(),
                            PickupCode = reader["pickup_code"].ToString(),
                            Status = reader["status"].ToString()
                        });
                    }
                }
            }
            return orders;
        }

        public static Order GetOrder(int orderId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT o.order_id, o.order_date, o.delivery_date, o.pickup_point_id, 
                   o.customer_name, o.pickup_code, o.status, p.address as pickup_address,
                   u.full_name as customer_fullname
            FROM orders o
            JOIN pickup_points p ON o.pickup_point_id = p.id
            JOIN users u ON o.customer_name = u.id
            WHERE o.order_id = @orderId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Order
                            {
                                OrderId = Convert.ToInt32(reader["order_id"]),
                                OrderDate = Convert.ToDateTime(reader["order_date"]),
                                DeliveryDate = reader["delivery_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["delivery_date"]),
                                PickupPointId = Convert.ToInt32(reader["pickup_point_id"]),
                                PickupAddress = reader["pickup_address"].ToString(),
                                CustomerId = Convert.ToInt32(reader["customer_name"]),
                                CustomerName = reader["customer_fullname"].ToString(),
                                PickupCode = reader["pickup_code"].ToString(),
                                Status = reader["status"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<PickupPoint> GetPickupPoints()
        {
            var points = new List<PickupPoint>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, mail_index, address FROM pickup_points ORDER BY address";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        points.Add(new PickupPoint
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            MailIndex = Convert.ToInt32(reader["mail_index"]),
                            Address = reader["address"].ToString()
                        });
                    }
                }
            }
            return points;
        }

        public static void AddOrder(Order order)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO orders (order_date, delivery_date, pickup_point_id, customer_name, pickup_code, status)
                         VALUES (@order_date, @delivery_date, @pickup_point_id, @customer_name, @pickup_code, @status)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@delivery_date", order.DeliveryDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pickup_point_id", order.PickupPointId);
                    cmd.Parameters.AddWithValue("@customer_name", order.CustomerId);
                    cmd.Parameters.AddWithValue("@pickup_code", order.PickupCode);
                    cmd.Parameters.AddWithValue("@status", order.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateOrder(Order order)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE orders SET order_date=@order_date, delivery_date=@delivery_date,
                         pickup_point_id=@pickup_point_id, customer_name=@customer_name,
                         pickup_code=@pickup_code, status=@status WHERE order_id=@order_id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", order.OrderId);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@delivery_date", order.DeliveryDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@pickup_point_id", order.PickupPointId);
                    cmd.Parameters.AddWithValue("@customer_name", order.CustomerId);
                    cmd.Parameters.AddWithValue("@pickup_code", order.PickupCode);
                    cmd.Parameters.AddWithValue("@status", order.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteOrder(int orderId)
        {
            // Сначала удаляем связанные позиции (из-за внешнего ключа)
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Удалить позиции заказа
                        string deleteItems = "DELETE FROM order_items WHERE order_id = @order_id";
                        using (var cmd = new MySqlCommand(deleteItems, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.ExecuteNonQuery();
                        }
                        // Удалить сам заказ
                        string deleteOrder = "DELETE FROM orders WHERE order_id = @order_id";
                        using (var cmd = new MySqlCommand(deleteOrder, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }


    }
}