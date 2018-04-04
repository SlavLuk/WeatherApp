using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoWeather
{
   public class WeatherProxy
    {
        private static string API_KEY = "APPID=cbc65cb5b205e9aee376cb91b5d42804";
        private static string URL = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&";
      

        //get weather based on location coords
        public async static Task<RootObject> GetWeatherByLocation(double lat,double lon,string tempUnit)
        {
            var http = new HttpClient();
    
            var response = await http.GetStringAsync(String.Format(URL, lat,lon,tempUnit) + API_KEY);

            var weatherObject = JsonConvert.DeserializeObject<RootObject>(response);

            return weatherObject;

        }
  
    }
    //classes used from weather service passed in json format
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
    
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int humidity { get; set; }
     
    }

    public class Wind
    {
        public double speed { get; set; }
      
    }

   

    public class Sys
    {
    
        public string country { get; set; }
 
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }   
        public Main main { get; set; }   
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
    
    }



}
