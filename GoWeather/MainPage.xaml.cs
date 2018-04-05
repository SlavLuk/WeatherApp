
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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using System;
using Windows.UI;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private RootObject result;

        public MainPage()
        {

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            LoadGeoCity();

        }

   

        private void SetLayout(RootObject result,string tempUnit)
        {

            //check on temp and wind speed unit passed 
            if (tempUnit.Equals("metric"))
            {
                TempResult.Text = ((int)result.main.temp).ToString() + " \u2103";
                WindSpeed.Text = "Wind: " + result.wind.speed.ToString() + " meter/sec";
            }
            else
            {
                TempResult.Text = ((int)result.main.temp).ToString() + " \u2109";
                WindSpeed.Text = "Wind: " + result.wind.speed.ToString() + " miles / hour";
            }

            //set up results 
            CountryResult.Text = result.name.ToString() + "," + result.sys.country;

            Description.Text =  result.weather[0].description.ToString()[0].ToString().ToUpper()+ result.weather[0].description.ToString().Substring(1);

            Humidity.Text = String.Format("Humidity: {0} %", result.main.humidity);

            string icon = String.Format("http://openweathermap.org/img/w/{0}.png", result.weather[0].icon);

            IconImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            

        }


        private async void LoadGeoCity()
        {

            try
            {
                //get position coords of current location
                var position = await LocationManager.GetPosition();

                string tempUnit = "";
                var x = position.Coordinate.Point.Position.Latitude;
                var y = position.Coordinate.Point.Position.Longitude;

                //check if settings set up 
                if (localSettings.Values["temp"] == null)
                {

                    localSettings.Values["temp"] = "metric";
                    tempUnit = "metric";

                }
                else
                {
                    tempUnit = localSettings.Values["temp"].ToString();
                }


                //get weather by location coords
                result = await WeatherProxy.GetWeatherByLocation(x, y, tempUnit);


                if (result != null)
                {
                    ProgressRing.IsActive = false;

                    SetLayout(result, tempUnit);

                }
            }
            catch (Exception e)
            {
                //write exception message
                GetForecast.IsEnabled = false;
                SearchCity.IsEnabled = false;
                Error.Foreground = new SolidColorBrush(Colors.Red);
                Error.Text = e.Message.ToString();

            }
            

        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //check passed back parameter
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
             
                string[] coords = e.Parameter.ToString().Split(' ');

                string tempUnit = localSettings.Values["temp"].ToString();

                //get weather root object based on coords passed back from search page
                 result = await WeatherProxy.GetWeatherByLocation(double.Parse(coords[0]), double.Parse(coords[1]), tempUnit);

           

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

        private void Search_Click(object sender, RoutedEventArgs e) => Frame.Navigate(sourcePageType: typeof(Search));

        private  void GetForecast_Click(object sender, RoutedEventArgs e)
        {

            if (result == null)
            {

                GetForecast.IsEnabled = false;

            }

            GetForecast.IsEnabled = true;

            var coords = String.Format("{0} {1}", result.coord.lat, result.coord.lon);
            
           

            Frame.Navigate(typeof(Forecast),coords);
        }
    }
}
