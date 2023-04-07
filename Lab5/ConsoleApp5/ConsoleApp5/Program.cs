using ConsoleApp5;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Initializing the OpenWeatherMapHttpClient with API key
        var client = new OpenWeatherMapHttpClient("3ae2e6e687c3cc067dc49375fc94f7b8");

        // Calling the Get method and print the weather description
        var getResponse = await client.GetWeatherByCityName("Mykolaiv");
        if (getResponse.StatusCode != 500)
        {
            Console.WriteLine($"Get method:\n{getResponse.Data[0].ToString()}");
        }
        else
        {
            Console.WriteLine($"Error: {getResponse.Message}");
        }

        //Creating an object with information about the city and weather
        var postData = new WeatherApiRequest
        {
            Name = "Kyiv",
            Temperature = 10.5
        };

        // Calling the Post method with a custom URL and JSON data
        var postResponse = await client.PostRequest("https://jsonplaceholder.typicode.com/posts", postData);
        if (postResponse.StatusCode != 500)
        {
            Console.WriteLine($"Post method:\n{postResponse.Data[0].ToString()}");
        }
        else
        {
            Console.WriteLine($"Error: {postResponse.Message}");
        }

        Console.ReadKey();
    }
}