using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;


namespace Weather_HWG
{
    public partial class MainPage : ContentPage
    {
        HttpClient Client = new HttpClient();
        private string Location = "Moscow";
        public MainPage()
        {
            InitializeComponent();

            check_connection();

            APIHelper aPIHelper = new APIHelper();
            Get_response();

            async void check_connection()
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

            async void Get_response()
            {
                var result = await aPIHelper.Get_response();

                List<Weather_response> weather_Responses = JsonConvert.DeserializeObject<List<Weather_response>>(result);
                coord.Text = weather_Responses[0].coords.lat.ToString();

                //добавить вывод на экран
            }

            /*refresh.Clicked += async (sender, e) => {

                APIHelper textTask = new APIHelper();

                var result = await textTask.Get_response();

                here.Text = result;
                
                newlay.ResolveLayoutChanges();
            };*/
        }
    }
}
