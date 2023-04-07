using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static ConsoleApp5.WeatherApiResponse;
using NPOI.SS.Formula.Functions;

namespace ConsoleApp5
{
    public class OpenWeatherMapHttpClient
    {
        private const string baseUrl = "https://api.openweathermap.org/data/2.5/";
        private readonly HttpClient client;

        // Creating a new instance of HttpClient and
        // setting its base address to the OpenWeatherMap API URL
        public OpenWeatherMapHttpClient(string apiKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            this.apiKey = apiKey;
        }

        private string apiKey { get; }

        public async Task<WeatherApiResponse> GetWeatherByCityName(string cityName)
        {
            try
            {
                // Sending a GET request to the OpenWeatherMap API
                // to get the weather for the specified city
                var response = await client.GetAsync($"weather?q={cityName}&appid={apiKey}&units=metric");
                // Reading the response content as a string
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // If the response status code is OK,
                    // deserializing the weather data from the response content
                    var weather = JsonConvert.DeserializeObject<WeatherData>(content);
                    var dataList = new List<WeatherData>();
                    dataList.Add(weather);

                    // Returning a new WeatherApiResponse object containing the message,
                    // the statuscode and the weather data
                    return new WeatherApiResponse
                    {
                        Message = "Everething is OK!",
                        StatusCode = (int)response.StatusCode,
                        Data = dataList
                    };
                }
                else
                {
                    // If the response status code isn't OK, returning a WeatherApiResponse object
                    // indicating an error occurred
                    return new WeatherApiResponse
                    {
                        Message = "Error has occurred!",
                        StatusCode = 500,
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                // If an exception is thrown, returning a WeatherApiResponse object
                // indicating an error occurred
                return new WeatherApiResponse
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    Data = null
                };
            }
        }

        public async Task<WeatherApiResponse> PostRequest(string url, object data)
        {
            try
            {
                // Serializing the data object as a JSON string
                var content = JsonConvert.SerializeObject(data);
                // Sending a POST request to the specified URL with the serialized data as the request body
                var response = await client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
                // Reading the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // If the response status code indicates success,
                    // deserializing the response data and set its properties
                    var responseData = JsonConvert.DeserializeObject<WeatherData>(responseContent);
                    responseData.Main = new MainData();
                    responseData.Wind = new WindData();
                    responseData.Sys = new SysData();

                    responseData.Main.Temperature = ((WeatherApiRequest)data).Temperature;
                    responseData.Main.Pressure = ((WeatherApiRequest)data).Pressure;
                    responseData.Main.Humidity = ((WeatherApiRequest)data).Humidity;
                    responseData.Wind.Speed = ((WeatherApiRequest)data).Speed;
                    responseData.Sys.Country = ((WeatherApiRequest)data).Country;

                    var dataList = new List<WeatherData>();
                    dataList.Add(responseData);

                    // Returning a new WeatherApiResponse object containing the message,
                    // the statuscode and the weather data
                    return new WeatherApiResponse
                    {
                        Message = "Everething is OK!",
                        StatusCode = (int)response.StatusCode,
                        Data = dataList
                    };
                }
                else
                {
                    // If the response status code indicates an error,
                    // returning a WeatherApiResponse object indicating an error occurred
                    return new WeatherApiResponse
                    {
                        Message = "Error has occurred!",
                        StatusCode = 500,
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                // If an exception is thrown, returning a WeatherApiResponse object
                // indicating an error occurred
                return new WeatherApiResponse
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    Data = null
                };
            }
        }
    }
}
