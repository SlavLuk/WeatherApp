using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GoWeather
{
    class ForecastProxy
    {

        private static string API_KEY = "APPID=cbc65cb5b205e9aee376cb91b5d42804";
        private static string URL = "http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units={2}&";

        public async static Task<RootObjectForecast> GetWeatherByLocation(double lat, double lon, string tempUnit)
        {
            var http = new HttpClient();
            var response = await http.GetStringAsync(String.Format(URL, lat, lon, tempUnit) + API_KEY);
                                                                   
            var weatherObject = JsonConvert.DeserializeObject<RootObjectForecast>(response);


            return weatherObject;

        }
    }

    public class MainTempForecast
    {
       
        public double temp_min { get; set; }
        public double temp_max { get; set; }

    }

    public class WeatherForecast
    {
     
        public string description { get; set; }
        public string icon { get; set; }
    }



    public class ListForecast
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private string _temp;
 
        public MainTempForecast main { get; set; }
        public List<WeatherForecast> weather { get; set; }
    
        public string dt_txt { get; set; }
     
        public string getDesc
        {
            get
            {
                return weather[0].description.ToString();
            }
        }

        public string getTemp
        {
            get
            {              

                if (localSettings.Values["temp"].Equals("metric"))
                {
                    return _temp = (int)main.temp_min + " \u2103" + " / " + (int)main.temp_max + " \u2103";
                }

                     return _temp = (int)main.temp_min + " \u2109" + " / " + (int)main.temp_max + " \u2109";

            }

        }

        public string getDateName
        {
            get {
                return DateTime.Parse(dt_txt).DayOfWeek.ToString();

                }
        }

        public ImageSource getIcon
        {
            get
            {
                string icon = String.Format("http://openweathermap.org/img/w/{0}.png", weather[0].icon);

                return new BitmapImage(new Uri(icon, UriKind.Absolute));
            }
        }
    }


    public class City
    {
    
        public string name { get; set; }
        public string country { get; set; }
     
    }

    public class RootObjectForecast
    {
        public List<ListForecast> list { get; set; }
        public City city { get; set; }
    }


}


