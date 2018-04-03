
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using System;
using Windows.UI;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public MainPage()
        {

           

            this.InitializeComponent();


            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

          LoadGeoCity();

          

        }

   

        public void SetLayout(RootObject result,string tempUnit)
        {

            string icon = String.Format("http://openweathermap.org/img/w/{0}.png", result.weather[0].icon);
           


            if (tempUnit.Equals("metric"))
            {
                tempResult.Text = ((int)result.main.temp).ToString() + " \u2103";
            }
            else
            {
                tempResult.Text = ((int)result.main.temp).ToString() + " \u2109";
            }

           
            countryResult.Text = result.name.ToString() + "," + result.sys.country;


            description.Text =  result.weather[0].description.ToString()[0].ToString().ToUpper()+ result.weather[0].description.ToString().Substring(1);
            humidity.Text = String.Format("Humidity: {0} %", result.main.humidity);
            windSpeed.Text = "Wind: "+result.wind.speed.ToString() + " meter/sec";
            iconImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
         

        }


        public async void LoadGeoCity()
        {
            var position = await LocationManager.GetPosition();


            var x = position.Coordinate.Point.Position.Latitude;
            var y = position.Coordinate.Point.Position.Longitude;

            string tempUnit = localSettings.Values["temp"].ToString();

            RootObject result = await WeatherProxy.GetWeatherByLocation(x, y, tempUnit);


            if (result != null)
            {
                progressRing.IsActive = false;

                SetLayout(result,tempUnit);

            }








        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

          

            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                //   RootObject result = await WeatherProxy.GetWeatherByCity(e.Parameter.ToString());

                string[] coords = e.Parameter.ToString().Split(' ');

                string tempUnit = localSettings.Values["temp"].ToString();

                RootObject result = await WeatherProxy.GetWeatherByLocation(double.Parse(coords[0]), double.Parse(coords[1]), tempUnit);

           

                SetLayout(result, tempUnit);
            }
            else
            {
                LoadGeoCity();
            }

            base.OnNavigatedTo(e);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Setting));
        }
    }
}
