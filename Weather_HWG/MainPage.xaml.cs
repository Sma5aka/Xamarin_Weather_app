using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Weather_HWG
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            foo();
            foo();
            
        }
        async void foo()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                await DisplayAlert("Success", "INTERNET IS ON", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "INTANATA NET", "neOk");
            }
        }
    }
}
