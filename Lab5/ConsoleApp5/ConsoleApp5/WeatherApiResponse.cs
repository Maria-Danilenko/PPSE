using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class WeatherApiResponse
    {
        [JsonProperty("cod")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("list")]
        public List<WeatherData> Data { get; set; }
    }
    public class WeatherData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        //Due to the specifics of the data returned from the API,
        //fields are nested classes with fields with information
        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        [JsonProperty("sys")]
        public SysData Sys { get; set; }

        //Override method for outputing data
        public override string ToString()
        {
            return $"\tCountry: {Sys.Country}\n" +
                   $"\tCity: {Name}\n" +
                   $"\tTemperature: {Main.Temperature}\n" +
                   $"\tPressure: {Main.Pressure}\n" +
                   $"\tHumidity: {Main.Humidity}\n" +
                   $"\tWind: {Wind.Speed}\n";
        }
    }
    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }
    public class WindData
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

    }
    public class SysData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
