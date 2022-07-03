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
using System.Threading;

namespace Weather_HWG
{
    public partial class MainPage : ContentPage
    {
        HttpClient Client = new HttpClient();
        private string Location { get; set; } = "Vladivostok";
        private double Lat { get; set; }
        private double Lon { get; set; }

        CancellationTokenSource cts;
        private async void GetCoord()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Lat = location.Latitude;
                    Lon = location.Longitude;
                    
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public MainPage()
        {
            InitializeComponent();

            //check_connection();

            APIHelper aPIHelper = new APIHelper();
            GetCoord();
            Get_response(Lat, Lon);
            
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

            async void Get_response(double lat = 0, double lon = 0)
            {
                string result = await aPIHelper.Get_response(Location, lat, lon);

                Weather_response weather_Responses = JsonConvert.DeserializeObject<Weather_response>(result);
                tempTxt.Text = Math.Round(weather_Responses.main.temp - 273.15,1).ToString();
                humidiryTxt.Text = weather_Responses.main.humidity.ToString()+ "💧";
                windTxt.Text = weather_Responses.wind.speed.ToString()+" m/s";
                pressureTxt.Text = weather_Responses.main.pressure.ToString()+ " hpa";
                cloudsTxt.Text = weather_Responses.clouds.all.ToString()+"%";
                cityTxt.Text = weather_Responses.name.ToString();
                mainTxt.Text = weather_Responses.weather[0].main.ToString();

                string data = weather_Responses.dt.ToString();
                //Console.WriteLine(data);
                var date = (new DateTime(1970, 1, 1)).AddSeconds(double.Parse(data));
                var correct_date = date.Date;
                //Другой способ
                //var dateAndTime = DateTime.Now;
                //var datee = dateAndTime.Date;

                dateTxt.Text = date.ToString("dd/MM/yyyy");

                string pics_url = "https://openweathermap.org/img/wn/";

                mainImg.Source = pics_url+weather_Responses.weather[0].icon.ToString()+".png";
                

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
