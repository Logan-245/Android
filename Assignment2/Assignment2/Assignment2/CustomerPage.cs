using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Assignment2
{
    public class CustomerPage : ContentPage
    {

        public static Customer_Database database;
        public static Product_Database Pdatabase;
        public static Interactions_Database Idatabase;

        public static ListView listView;

        public static int ListItemindex;
        public static Customer_Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Customer_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("customer.db3"));
                }
                return database;
            }
        }
        public static Product_Database PDatabase
        {
            get
            {
                if (Pdatabase == null)
                {
                    Pdatabase = new Product_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("Product.db3"));
                }
                return Pdatabase;
            }
        }
        public static Interactions_Database IDatabase 
        {
            get
            {
                if (Idatabase == null)
                {
                    Idatabase = new Interactions_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("Interactions.db3"));
                }
                return Idatabase;
            }
        }


        public CustomerPage()
        {

           
            // initialize the db
            database = Database;
            Pdatabase = PDatabase;
            Idatabase = IDatabase;
                


            Title = "Customers";
            //Tool bar Code
            ToolbarItem SettingsItem = new ToolbarItem { Text = "Settings" };
            ToolbarItem ProductsItem = new ToolbarItem { Text = "Products" };
            this.ToolbarItems.Add(SettingsItem);
            this.ToolbarItems.Add(ProductsItem);
            SettingsItem.Clicked += (sender, e) => { Navigation.PushAsync(new SettingsPage()); };
            ProductsItem.Clicked += (sender, e) => { Navigation.PushAsync(new ProductsPage()); };

            List<Customer> CustomersList = database.GetItems();
            ObservableCollection<Customer> CustomersCollection = new ObservableCollection<Customer>(CustomersList);
           



            listView = new ListView
            {
                ItemsSource = CustomersCollection,
                ItemTemplate = new DataTemplate(typeof(CustomerCell)),

            };

            listView.ItemTapped += (s, e) =>
            {
                listView.SelectedItem = null;
                Navigation.PushAsync(new InteractionsPage(e.Item as Customer));
            };

            





/*
            listView = new ListView
            {
                // Source of data items.
                ItemsSource = Customers,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "FirstName");

                    Label LnameLabel = new Label();
                    LnameLabel.SetBinding(Label.TextProperty, "LastName");

                    Label PhoneLabel = new Label();
                    PhoneLabel.SetBinding(Label.TextProperty, "Phone");

                    Label IDLabel = new Label();
                    LnameLabel.SetBinding(Label.TextProperty, "ID");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        HorizontalOptions = LayoutOptions.Center,
                                        Children =
                                        {
                                            nameLabel,
                                            LnameLabel,
                                            PhoneLabel,
                                            IDLabel,

                                        }
                                        }
                                }
                        }
                    };
                })
            };
*/
            Button AddCustomerButton = new Button
            {
                Text = "Add Customer",
            };
            AddCustomerButton.Clicked += (sender, e) => { Navigation.PushAsync(new AddCustomerPage()); };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "" },

                    listView,
                    AddCustomerButton,}

            };

            
        }
    }

    public class CustomerCell : ViewCell
    {
        


            public CustomerCell()
            {
            Label nameLabel = new Label();
            nameLabel.SetBinding(Label.TextProperty, "FirstName");

            Label LnameLabel = new Label();
            LnameLabel.SetBinding(Label.TextProperty, "LastName");

            Label PhoneLabel = new Label();
            PhoneLabel.SetBinding(Label.TextProperty, "Phone");
            View = new StackLayout
            {
                Padding = new Thickness(0, 5),
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new StackLayout
                    {
                     Orientation = StackOrientation.Horizontal,
                     HorizontalOptions = LayoutOptions.Center,
                     Children ={nameLabel,LnameLabel,PhoneLabel,}
                    }
                }
            };




            MenuItem ItemDeleteButton = new MenuItem { Text = "Delete", IsDestructive = true };
            ItemDeleteButton.Clicked += (s, e) =>
            {
                
                if(this.BindingContext != null)
                {
                    ListView parent = (ListView)this.Parent;
                    //ObservableCollection<Customer> CustomersCollection = ObservableCollection<Customer>(parent.ItemsSource);
                    CustomerPage.database.DeleteItems((Customer)this.BindingContext);
                    //CustomersCollection.Remove((Customer)this.BindingContext as Customer);
                    CustomerPage.listView.ItemsSource = CustomerPage.database.GetItems();
                }
                else
                {
                    return;
                }
               //CustomerPage.listView.ItemsSource = CustomerPage.database.GetItems();
            };






            ContextActions.Add(ItemDeleteButton);
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged(); 
            if (this.BindingContext == null) return; 
            Customer item = this.BindingContext as Customer;
           
        }
    }


    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}