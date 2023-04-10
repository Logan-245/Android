using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Xamarin.Forms;

namespace Assignment2
{
    public class AddCustomerPage : ContentPage
    {
       
        public AddCustomerPage()
        {

            Title = "New Customer";
          

            //Tool bar Code
            ToolbarItem SettingsItem = new ToolbarItem { Text = "Settings" };
            ToolbarItem ProductsItem = new ToolbarItem { Text = "Products" };
            this.ToolbarItems.Add(SettingsItem);
            this.ToolbarItems.Add(ProductsItem);
            SettingsItem.Clicked += (sender, e) => { Navigation.PushAsync(new SettingsPage()); };
            ProductsItem.Clicked += (sender, e) => { Navigation.PushAsync(new ProductsPage()); };

            


            EntryCell eFName = new EntryCell { Label = "First Name:" };
            EntryCell eLName = new EntryCell { Label = "Last Name:" };
            EntryCell eAddress = new EntryCell { Label = "Address:" };
            EntryCell ePhone = new EntryCell { Label = "Phone:" };
            EntryCell eEmail = new EntryCell { Label = "Email:" };


            Button SaveCustomerButton = new Button
            {
                Text = "Save",
            };
            SaveCustomerButton.Clicked += (s, e) =>
            {
               Customer p = new Customer
                {
                    FirstName = eFName.Text,
                    LastName = eLName.Text,
                    Address = eAddress.Text,
                    Phone = ePhone.Text,
                    Email = eEmail.Text
                };
                CustomerPage.database.SaveItem(p);
                CustomerPage.listView.ItemsSource = CustomerPage.database.GetItems();
                eFName.Text = "";
                eLName.Text = "";
                eAddress.Text = "";
                ePhone.Text = "";
                eEmail.Text = "";
            };

            Content = new StackLayout
            {
                Children = {
                     new TableView { Intent = TableIntent.Form, Root = new TableRoot {new TableSection("Add New Customer") { eFName, eLName, eAddress,ePhone, eEmail } }},
                     SaveCustomerButton
                }
            };
        }


        
    }
}