using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Store
{
    using Entity;
    using System.Windows;

    static class Data
    {
        // Временный ID который уходит в минус для новых товаров
        // Создание нового товара
        // Изменение в старом товаре

        public const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\qultv\source\repos\Store\Store\Database.mdf;Integrated Security=True";

        public static Guid UserID { set; get; }


        public static ObservableCollection<Product> Products { set; get; } = new ObservableCollection<Product>();
        public static ObservableCollection<Category> Categories { set; get; } = new ObservableCollection<Category>();
        public static ObservableCollection<Producer> Producers { set; get; } = new ObservableCollection<Producer>();
        public static ObservableCollection<Order> Orders { set; get; } = new ObservableCollection<Order>();

        public static List<string> Commands = new List<string>();

        // Load Data From Data Base
        public static void LoadFromDataBase()
        {
            Commands.Clear();
            Products.Clear();
            Categories.Clear();
            Producers.Clear();
            Orders.Clear();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command;
            SqlDataReader reader;

            // Products
            command = new SqlCommand("SELECT id, name, category_id, producer_id , price, count, description, image FROM Product", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Products.Add(new Product()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    category_id = reader.GetInt32(2),
                    producer_id = reader.GetInt32(3),
                    Price = (float)reader.GetDouble(4),
                    Count = reader.GetInt32(5),
                    Description = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Image = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            }
            reader.Close();


            // Category
            command = new SqlCommand("SELECT id, name FROM Category", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Categories.Add(new Category()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            reader.Close();


            // Producer
            command = new SqlCommand("SELECT id, name, address FROM Producer", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Producers.Add(new Producer()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2)
                });
            }
            reader.Close();


            // Orders
            command = new SqlCommand("SELECT * FROM Orders", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Orders.Add(new Order()
                {
                    Id = reader.GetInt32(0),
                    User_id = reader.GetGuid(1),
                    product_id = reader.GetInt32(2),
                    Count = reader.GetInt32(3),
                    Message = reader.GetString(4)
                });
            }
            reader.Close();

            connection.Close();


            Product.TempIdForNew = Products.Max(x => x.Id);
            Producer.TempIdForNew = Producers.Max(x => x.Id);
            Category.TempIdForNew = Categories.Max(x => x.Id);
            Order.TempIdForNew = Orders.Max(x => x.Id);
        }

        // Delete Row from Table By ID
        public static void Delete(string Table, string id)
        {
            Products.Remove(Products.Where(x => x.Id.ToString() == id).First());
            Commands.Add($"DELETE FROM {Table} WHERE id = {id}");
        }


        #region For Products
        public static List<string> Seters (Product original, Product changed)
        {
            List<string> values = new List<string>();

            if (changed.Name != original.Name) values.Add($"name = '{changed.Name}'");
            if (changed.category_id != original.category_id) values.Add($"category_id = {changed.category_id}");
            if (changed.producer_id != original.producer_id) values.Add($"producer_id = {changed.producer_id}");
            if (changed.Price != original.Price) values.Add($"price = {changed.Price}");
            if (changed.Count != original.Count) values.Add($"count = {changed.Count}");
            if (changed.Description != original.Description) values.Add($"description = '{changed.Description}'");
            if (changed.Image != original.Image) values.Add($"image = '{changed.Image}'");

            return values;
        }
        internal static void Insert(string Table, Product product)
        {
            Product.TempIdForNew++;
            product.Id = Product.TempIdForNew;

            Products.Add(product);
            Commands.Add($"INSERT INTO {Table} (id, name, category_id, producer_id, price, count, description, image) VALUES ({product.Id}, {product.Name}, {product.category_id}, {product.producer_id}, {product.Price}, {product.Count}, {product.Description}, {product.Image})");
        }
        internal static void Update(string Table, Product changed)
        {
            Product original = Products.First(x => x.Id == changed.Id);
            if (original == null) return;

            List<string> values = Seters(original, changed);

            string str = "";
            if (values.Count > 0)
                str += values[0];
            for (int i = 1; i < values.Count; i++)
                str += $" , {values[i]}";

            for (int i = 0; i < Products.Count; i++)
                if (Products[i].Id == original.Id)
                    Products[i] = changed;

            Commands.Add($"UPDATE {Table} SET {str} WHERE id = {original.Id}");
        }
        #endregion

        
    }
}
