using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace Assignment2
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            
            Title = "Settings";
            //Tool bar Code
            ToolbarItem SettingsItem = new ToolbarItem { Text = "Settings" };
            ToolbarItem ProductsItem = new ToolbarItem { Text = "Products" };
            this.ToolbarItems.Add(SettingsItem);
            this.ToolbarItems.Add(ProductsItem);
            SettingsItem.Clicked += (sender, e) => { Navigation.PushAsync(new SettingsPage()); };
            ProductsItem.Clicked += (sender, e) => { Navigation.PushAsync(new ProductsPage()); };
            Button ResetApp = new Button
            {
                Text = "Reset App",
            };


            ResetApp.Clicked += (s, e) =>
            {
                //Drops Customer table and populates the listview with empty data
                CustomerPage.database.DropTable();
                CustomerPage.Pdatabase.DropTable();
                CustomerPage.Idatabase.DropTable();
                List<string> list1 = new List<string>();
                CustomerPage.listView.ItemsSource = list1;
            };

            
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Click To Reset All Data NOTE: This is Perminant and cannot be undone" },
                    ResetApp
                }
            };
        }
    }
}