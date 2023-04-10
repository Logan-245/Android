using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Assignment2
{
    public class Customer_Database
    {
        public SQLiteConnection database;
        String filePath;
        public Customer_Database(string dbPath)
        {
            filePath= dbPath;
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Customer>();
        }

        public List<Customer> GetItems()
        {
            return database.Table<Customer>().ToList(); 
        }

        public Customer GetItem(int id)
        {
            return database.Table<Customer>().Where(i => i.ID == id).FirstOrDefault(); // return matching fuel purchase by id, if it exists
        }

        


        public int DeleteItems(Customer customer)
        {
            return database.Delete(customer);
        }

        public int SaveItem(Customer p)
        {
            return database.Insert(p);
        }

        public void DropTable()
        {
            database.DropTable<Customer>();
            database = new SQLiteConnection(filePath);
            database.CreateTable<Customer>();

        }





    }

    

}