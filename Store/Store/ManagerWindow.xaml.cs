using Store.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Store
{
    using Entity;

    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; } = Data.Products;
        public ObservableCollection<Category> Categories { set; get; } = Data.Categories;
        public ObservableCollection<Producer> Producers { set; get; } = Data.Producers;
        public ObservableCollection<Order> Orders { set; get; } = Data.Orders;
        public List<string> Commands { set; get; } = Data.Commands;

        public Product product;
        public bool ProductNew = false;

        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = this;

            Data.LoadFromDataBase();
        }


        private bool HaveChanges(Product product)
        {
            if (product == null)
                return false;
            if (ProductImagePathBox.Text != product.Image ||
                ProductNameBox.Text != product.Name ||
                (int)CategoryCombo.SelectedValue != product.category_id ||
                (int)ProducerCombo.SelectedValue != product.producer_id ||
                ProductPriceBox.Text != product.Price.ToString() ||
                ProductCountBox.Text != product.Count.ToString() ||
                ProductDescriptionBox.Text != product.Description)
                return true;
            return false;
        }
        private void ProductClearAllInfo()
        {
            ProductImagePathBox.Text = string.Empty;
            ProductImage.Source = new BitmapImage();

            ProductNameBox.Text = string.Empty;

            CategoryCombo.SelectedValue = null;
            ProducerCombo.SelectedValue = null;

            ProductPriceBox.Text = string.Empty;
            ProductCountBox.Text = string.Empty;
            ProductDescriptionBox.Text = string.Empty;

            product = null;
            ProductNew = false;
        }

        private void ProductEnebleForNone()
        {
            ProductInfo.IsEnabled = false;

            SaveProduct.IsEnabled = false;
            DeleteProduct.IsEnabled = false;
            CancelProduct.IsEnabled = false;
        }
        private void ProductEnebleForEdit()
        {
            ProductInfo.IsEnabled = true;

            SaveProduct.IsEnabled = true;
            DeleteProduct.IsEnabled = true;
            CancelProduct.IsEnabled = true;
        }
        private void ProductEnebleForNew()
        {
            ProductInfo.IsEnabled = true;

            SaveProduct.IsEnabled = true;
            DeleteProduct.IsEnabled = false;
            CancelProduct.IsEnabled = true;
        }

        private bool ProductCancel()
        {
            if (HaveChanges(product))
                if (MessageBox.Show("Cancle all changes?", "Cancel", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;

            ProductEnebleForNone();
            ProductClearAllInfo();
            return true;
        }

        private void Products_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(sender is ListViewItem item)
            {
                if (item.Content is Product product)
                {
                    if (!ProductCancel()) return;

                    ProductImagePathBox.Text = product.Image;
                    ProductImage.Source = new BitmapImage(new Uri($"/Image/{ProductImagePathBox.Text}", UriKind.Relative));

                    ProductNameBox.Text = product.Name;

                    CategoryCombo.SelectedValue = product.category_id;
                    ProducerCombo.SelectedValue = product.producer_id;

                    ProductPriceBox.Text = product.Price.ToString();
                    ProductCountBox.Text = product.Count.ToString();
                    ProductDescriptionBox.Text = product.Description.ToString();

                    this.product = product;
                    ProductEnebleForEdit();
                }
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
                if (MessageBox.Show("Clear all field?", "Clear", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

            ProductClearAllInfo();
            ProductEnebleForNew();

            ProductNew = true;
        }
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!ProductNew && !HaveChanges(product))
                return;
            if (MessageBox.Show("Save all changes?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            Product newProduct = new Product()
            {
                Id = product == null ? 0 : product.Id,
                Name = ProductNameBox.Text,
                category_id = (int)CategoryCombo.SelectedValue,
                producer_id = (int)ProducerCombo.SelectedValue,
                Price = (float)Convert.ToDouble(ProductPriceBox.Text),
                Count = Convert.ToInt32(ProductCountBox.Text),
                Description = ProductDescriptionBox.Text,
                Image = ProductImagePathBox.Text
            };
            
            if (ProductNew)
                Data.Insert("Product", newProduct);
            else
                Data.Update("Product", newProduct);

            ProductEnebleForNone();
            ProductClearAllInfo();
        }
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete product?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            Data.Delete("Product", product.Id.ToString());

            ProductEnebleForNone();
            ProductClearAllInfo();

            product = null;
            ProductNew = false;
        }
        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductCancel();
        }
    }
}
