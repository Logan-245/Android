using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerPage : ContentPage
    {
        public CustomerPage()
        {
            InitializeComponent();
            btnAddCustomer.Clicked += (s, e) => { Navigation.PushAsync(new AddCustomerPage()); };

        }
    }
}