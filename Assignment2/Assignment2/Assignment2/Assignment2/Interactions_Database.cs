using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Assignment2
{
    public class Interactions_Database 
    {
        public SQLiteConnection database;
        String filePath;
        public Interactions_Database(string dbpath)
        {
            filePath = dbpath;
            database = new SQLiteConnection(filePath);
            database.CreateTable<InteractionsData>();
        }

        public List<InteractionsData> GetItems(int id)
        {
            return database.Table<InteractionsData>().Where(i => i.CustomerID == id).ToList(); 
        }

        public InteractionsData GetItem(int id)
        {
            return database.Table<InteractionsData>().Where(i => i.ID == id).FirstOrDefault();
        }

        public int DeleteItems(InteractionsData item)
        {
            return database.Delete(item);
        }

        public int SaveItem(InteractionsData item) 
        {
            return database.Insert(item);
        }

        public int UpdateItem(int ItemID, Boolean isPurchased) 
        { 
            var item = GetItem(ItemID);
            item.Purchased = isPurchased;
            return database.Update(item);
            
            
        }

        public void DropTable()
        {
            database.DropTable<InteractionsData>();
            database = new SQLiteConnection(filePath);
            database.CreateTable<InteractionsData>();
        }


    }
}