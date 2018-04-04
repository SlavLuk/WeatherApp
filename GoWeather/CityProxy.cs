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
    class CityProxy
    {
        private static string API_KEY = "APPID=cbc65cb5b205e9aee376cb91b5d42804";
        private static string URL = "http://api.openweathermap.org/data/2.5/find?q={0}&units={1}&cnt=10&type=accurate&";
       

        public async static Task<RootObjectCity> GetWeatherByCityName(string city,string tempUnit)
        {
            var http = new HttpClient();
            var response = await http.GetStringAsync(String.Format(URL, city, tempUnit) + API_KEY);

            var weatherObject = JsonConvert.DeserializeObject<RootObjectCity>(response);


            return weatherObject;
        }
    }

    public class CoordCity
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class MainTemp
    {

        public double temp_min { get; set; }
        public double temp_max { get; set; }
      
    }

  

    public class SysCountry
    {
        public string country { get; set; }
    }

   

    public class WeatherList
    {
     
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class ListCity
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private string _temp;

    
        public string name { get; set; }
        public CoordCity coord { get; set; }
        public MainTemp main { get; set; }
    
        public SysCountry sys { get; set; }
    
        public List<WeatherList> weather { get; set; }

        public string getName
        {
            get { return name + "," + sys.country; }
        }

        public string getTemp
        {
            get {
               

               if(localSettings.Values["temp"].Equals("metric"))
                {
                    return _temp = (int)main.temp_min + " \u2103" + " / " + (int)main.temp_max + " \u2103";
                }

                return _temp = (int)main.temp_min +" \u2109" + " / " + (int)main.temp_max + " \u2109";

            }
       
        }

        public string getDesc
        {
            get { return weather[0].description; }
        }
        public ImageSource getIcon
        {
            get {

                string icon = String.Format("http://openweathermap.org/img/w/{0}.png", weather[0].icon);

                return new BitmapImage(new Uri(icon, UriKind.Absolute));
            }     
        }
    }

    public class RootObjectCity
    {
 
        public List<ListCity> list { get; set; }
    }
}
