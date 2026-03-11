using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ShopClassLibrary
{
    public class DatabaseHelper
    {
        private static string connectionString = "server=127.0.0.1;user=root;database=shop;password=vertrigo;";

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
    }
}