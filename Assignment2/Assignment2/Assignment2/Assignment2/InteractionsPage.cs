using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment2
{
    public class InteractionsPage : ContentPage
    {
        public static Interactions_Database interactionsdatabase;
        public static Product_Database productsdatabase;
        public static Customer_Database customerdatabase;
        public static ListView listView;
        public static string CName;
        public static Switch Purchased;
        public static int CID;

        public static List<ProductsData> products;


        public static Interactions_Database InteractionDatabase 
        {
            get
            {
                if (interactionsdatabase == null) 
                {
                    interactionsdatabase = new Interactions_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("Interactions.db3")); 
                }
                return interactionsdatabase;
            }
        }

        public static Product_Database ProductDatabase 
        {
            get
            {
                if (productsdatabase == null) 
                {
                    productsdatabase = new Product_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("Product.db3")); 
                }
                return productsdatabase;
            }
        }

        public static Customer_Database CustomerDatabase
        {
            get
            {
                if (customerdatabase == null) // not connected
                {
                    customerdatabase = new Customer_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("customer.db3")); // using the platform specific file helper, figure out the path
                }
                return customerdatabase;
            }
        }
        int Sel;

        public InteractionsPage(Customer customer)
        {
            productsdatabase = ProductDatabase;
            interactionsdatabase = InteractionDatabase;
            customerdatabase = CustomerDatabase;
            Title = "Interactions";
            CName = customer.FirstName + " " + customer.LastName;
            CID = customer.ID;



            List<InteractionsData> InteractionList = interactionsdatabase.GetItems(CID);
            ObservableCollection<InteractionsData> InteractionCollection = new ObservableCollection<InteractionsData>(InteractionList);

            products = productsdatabase.GetItems();
            ObservableCollection<ProductsData> ProductCollection = new ObservableCollection<ProductsData>(products);




            listView = new ListView
            {
                ItemsSource = InteractionCollection,
                ItemTemplate = new DataTemplate(typeof(InteractionCell)),
                RowHeight = 80,
                HeightRequest = 1000,


            };


            






            
            DatePicker dp = new DatePicker { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            Label d = new Label { Text = "Date:", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.CenterAndExpand };
            StackLayout sDate = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { d, dp } };
            ViewCell vDate = new ViewCell
            { View = sDate, };

            EntryCell eComments = new EntryCell { Label = "Comments:" };

            Picker pp = new Picker { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, ItemsSource = products, ItemDisplayBinding = new Binding("ProductName") };
            Label p = new Label { Text = "Product:", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.CenterAndExpand };
            StackLayout sProduct = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { p, pp } };
            ViewCell vProduct = new ViewCell
            { View = sProduct, };

            Switch ss = new Switch { IsToggled = false };
            Label sss = new Label { Text = "Purchased?:", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.CenterAndExpand };
            StackLayout sSwitch = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { sss, ss } };
            ViewCell vSwitch = new ViewCell
            { View = sSwitch, };

            Button AddInteractionButton = new Button
            {
                Text = "Add",
            };


            pp.SelectedIndexChanged += (s, e) =>
            {
                try
                {
                    Sel = productsdatabase.GetItem((pp.SelectedItem as ProductsData).ProductName).ID;
                }
                catch (Exception ex) { }
            };



            AddInteractionButton.Clicked += (s, e) =>
            {
                InteractionsData i = new InteractionsData
                {
                    CustomerID = Convert.ToInt32(customer.ID),
                    Date = Convert.ToDateTime(dp.Date),
                    Comments = eComments.Text,
                    ProductID = Sel,
                    Purchased = ss.IsToggled,
                };
                interactionsdatabase.SaveItem(i);
                listView.ItemsSource = interactionsdatabase.GetItems(customer.ID);
                dp.Date = DateTime.Now;
                eComments.Text = "";
                ss.IsToggled = false;
                pp.SelectedItem = null;
            };
            listView.ItemsSource = interactionsdatabase.GetItems(customer.ID);







            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {


                   listView,


                    new TableView
                   { 
                       Intent = TableIntent.Form,
                       Root = new TableRoot
                       {
                           new TableSection("Add Interaction")
                           {
                               vDate, eComments, vProduct, vSwitch,
                           }
                       }
                   },
                    AddInteractionButton

                },



            };
        }

        public class InteractionCell : ViewCell
        {
            public InteractionCell()
            {
                
                Label TestName = new Label();
                Label ProuductName = new Label();
                string productName;
                int productId;



                TestName.SetBinding(Label.TextProperty, "ProductID");
                productId = Convert.ToInt32(TestName.Text);
                productName = productsdatabase.GetItemsForInteractions(1);
                ProuductName.Text = productName;

                Label CustomertName = new Label { Text = CName };
                Label ISPURCHESED = new Label { Text = "Purchased?" };
                Label Date = new Label();
                Date.SetBinding(Label.TextProperty, new Binding("Date", stringFormat: "{0:dddd, MMMM dd, yyyy}"));
                Label Comment = new Label();
                Comment.SetBinding(Label.TextProperty, "Comments");



                
                
                Purchased = new Switch();
                Purchased.SetBinding( Switch.IsToggledProperty, "Purchased");

                Purchased.IsEnabled = false;


                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    

                    Children =
                    {
                        new StackLayout
                        {
                        Orientation = StackOrientation.Horizontal,
                      

                        Children =
                            {
                                CustomertName
                            }


                        },
                        new StackLayout
                        {
                        Orientation = StackOrientation.Horizontal,
                        

                        Children =
                            {
                                Date, Comment
                            }


                        },
                        new StackLayout
                        {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,



                        Children =
                            {
                                ProuductName, ISPURCHESED, Purchased,
                            }

                        },



                    }


                };
               






                SwipeItem deleteSwipeItem = new SwipeItem
                {
                    Text = "Delete",
                    BackgroundColor = Color.Red,
                };
                
                deleteSwipeItem.Invoked += (s, e) =>
                {
                    if (this.BindingContext is InteractionsData interactionsData)
                    {
                        
                        InteractionsPage.interactionsdatabase.DeleteItems((interactionsData));
                        
                        InteractionsPage.listView.ItemsSource = InteractionsPage.interactionsdatabase.GetItems(CID);
                    }
                    else
                    {
                        return;
                    }
                };

                SwipeItems swipeItems = new SwipeItems
                {
                    deleteSwipeItem,
                };

                SwipeView swipeView = new SwipeView
                {
                    RightItems = swipeItems,
                    Content = stackLayout,
                };
                
                View = swipeView;




            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged(); 
                if (this.BindingContext == null) return; 
                InteractionsData item = this.BindingContext as InteractionsData;


            }
        }


        
    }
}
