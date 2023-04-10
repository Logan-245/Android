using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Assignment2
{
    public class Product_Database
    {
        public SQLiteConnection database;

        String path;

        public Product_Database(string dbPath)
        {
            path = dbPath;
            database = new SQLiteConnection(dbPath);

            database.CreateTable<ProductsData>();

            if (database.Table<ProductsData>().Count() == 0) // if no records make one
            {
                ProductsData p = new ProductsData
                {
                    ProductName = "Wonder Jacket",
                    Price = 499.99,
                    Description = "A Wonderful jacket"
                };
                SaveItem(p);

                ProductsData pp = new ProductsData
                {
                    ProductName = "Wonder Hat",
                    Price = 124.99,
                    Description = "A wonderful hat"
                };
                SaveItem(pp);

                ProductsData ppp = new ProductsData
                {
                    ProductName = "Wonder Boots",
                    Price = 224.99,
                    Description = "A wonderful pair of high quality boots"
                };
                SaveItem(ppp);
            }
        }

        public int SaveItem(ProductsData p)
        {
           return database.Insert(p);
        }

        public List<ProductsData> GetItems()
        {
            return database.Table<ProductsData>().ToList(); 
        }

        public ProductsData GetItem(String name)
        {
            return database.Table<ProductsData>().Where(i => i.ProductName == name).FirstOrDefault(); 
        }

        public string  GetItemsForInteractions(int ProID)
        {


            
            if (ProID != 0 )
            {
                return database.Table<ProductsData>().Where(i => i.ID == ProID).FirstOrDefault().ProductName.ToString();
            }
            else
            {
                return "Product Not Found";
            }

            
            
        }


        public void DropTable()
        {
            database.DropTable<ProductsData>();
            database = new SQLiteConnection(path);
            database.CreateTable<ProductsData>();
            ProductsData p = new ProductsData
            {
                ProductName = "Wonder Jacket",
                Price = 499.99,
                Description = "A Wonderful jacket"
            };
            SaveItem(p);

            ProductsData pp = new ProductsData
            {
                ProductName = "Wonder Hat",
                Price = 124.99,
                Description = "A wonderful hat"
            };
            SaveItem(pp);

            ProductsData ppp = new ProductsData
            {
                ProductName = "Wonder Boots",
                Price = 224.99,
                Description = "A wonderful pair of high quality boots"
            };
            SaveItem(ppp);

        }


    }
   

}