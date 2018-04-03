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

        public async static Task<RootObjectForecast> GetWeatherByLocation(double lat, double lon, string tempUnit)
        {
            var http = new HttpClient();
            var response = await http.GetStringAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units={2}&", lat, lon, tempUnit) + API_KEY);
                                                                   
            var weatherObject = JsonConvert.DeserializeObject<RootObjectForecast>(response);


            return weatherObject;

        }
    }

    public class MainTempForecast
    {
       // public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
     //   public double pressure { get; set; }
     //   public double sea_level { get; set; }
      //  public double grnd_level { get; set; }
      //  public int humidity { get; set; }
      //  public double temp_kf { get; set; }
    }

    public class WeatherForecast
    {
      //  public int id { get; set; }
      //  public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    //public class CloudsForecast
    //{
    //    public int all { get; set; }
    //}

    //public class WindForecast
    //{
    //    public double speed { get; set; }
    //    public double deg { get; set; }
    //}

    //public class Rain
    //{
    //    public double? __invalid_name__3h { get; set; }
    //}

    //public class SysForecast
    //{
    //    public string pod { get; set; }
    //}

    public class ListForecast
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private string _temp;
       // public int dt { get; set; }
        public MainTempForecast main { get; set; }
        public List<WeatherForecast> weather { get; set; }
       // public CloudsForecast clouds { get; set; }
      //  public WindForecast wind { get; set; }
      //  public Rain rain { get; set; }
       // public SysForecast sys { get; set; }
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
            get { return DateTime.Parse(dt_txt).DayOfWeek.ToString(); }
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

    //public class CoordCityForecast
    //{
    //    public double lat { get; set; }
    //    public double lon { get; set; }
    //}

    public class City
    {
     //   public int id { get; set; }
        public string name { get; set; }
     //   public CoordCityForecast coord { get; set; }
         public string country { get; set; }
      //  public int population { get; set; }
    }

    public class RootObjectForecast
    {
     //   public string cod { get; set; }
    //    public double message { get; set; }
    //    public int cnt { get; set; }
        public List<ListForecast> list { get; set; }
        public City city { get; set; }
    }


}


