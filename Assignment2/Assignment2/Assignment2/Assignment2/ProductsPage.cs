using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace Assignment2
{
    public class ProductsPage : ContentPage
    {
        public static Product_Database database;

        public static ListView listView;

        
        public static Product_Database Database 
        {
            get
            {
                if (database == null)
                {
                    database = new Product_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("Product.db3")); 
                }
                return database;
            }
        }

        public ProductsPage()
        {
            database = Database;
            Title = "Products";

            //Tool bar Code
            ToolbarItem SettingsItem = new ToolbarItem { Text = "Settings" };
            ToolbarItem ProductsItem = new ToolbarItem { Text = "Products" };
            this.ToolbarItems.Add(SettingsItem);
            this.ToolbarItems.Add(ProductsItem);
            SettingsItem.Clicked += (sender, e) => { Navigation.PushAsync(new SettingsPage()); };
            ProductsItem.Clicked += (sender, e) => { Navigation.PushAsync(new ProductsPage()); };

            List<ProductsData> products = database.GetItems();


           
            listView = new ListView
            {
                ItemsSource = products,
                ItemTemplate = new DataTemplate(typeof(ProductCell))
            };

            Content = new StackLayout
            {
                Children = {
                    

                    listView,
                    }

            };




        }
    }

    public class ProductCell : ViewCell
    {
        public const int RowHeight = 55;

        public ProductCell()
        {
            Label ProductName = new Label { FontAttributes = FontAttributes.Bold };
            ProductName.SetBinding(Label.TextProperty, "ProductName");

            Label ProductDescription = new Label();
            ProductDescription.SetBinding(Label.TextProperty, "Description");

            Label ProductPrice = new Label();
            ProductPrice.SetBinding(Label.TextProperty, "Price");

            View = new StackLayout
            {
                Padding = new Thickness(5, 2),
                Orientation = StackOrientation.Vertical,
                Children =
                {
                new StackLayout
                                      {
                                          Orientation = StackOrientation.Horizontal,

                                          Children =
                                          {
                                              ProductName,
                                          }
                                      },
                new StackLayout
                                      {
                                          Orientation = StackOrientation.Horizontal,

                                          Children =
                                          {
                                              ProductDescription,
                                              ProductPrice

                                          }
                                      },
                }
            };
        }
    }
}