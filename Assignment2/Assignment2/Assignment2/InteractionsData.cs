using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Assignment2
{
    public class InteractionsData 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public String Comments { get; set; }
        public int ProductID { get; set; }
        public Boolean Purchased { get; set; }
    }
}