using ConsoleApp5;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{

    //private readonly HttpClient _httpClient;
    //private const string ApiUrl = "http://numbersapi.com/random/trivia";

    //public ApiClient()
    //{
    //    _httpClient = new HttpClient();
    //}

    //public async Task<ResponseModel<string>> GetAsync()
    //{
    //    try
    //    {
    //        var response = await _httpClient.GetAsync(ApiUrl);
    //        response.EnsureSuccessStatusCode();
    //        var content = await response.Content.ReadAsStringAsync();
    //        var values = content.Split(",");


    //        //var response = await _httpClient.GetAsync(ApiUrl);
    //        //response.EnsureSuccessStatusCode();
    //        //var data = await response.Content.ReadAsStringAsync();
    //        return new ResponseModel<string>
    //        {
    //            Data = values.ToList(),
    //        StatusCode = HttpStatusCode.OK
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex);
    //        return new ResponseModel<string>
    //        {
    //            Message = "Error retrieving data",
    //            StatusCode = HttpStatusCode.InternalServerError
    //        };
    //    }
    //}
    //public async Task<string> Post(string url, string data)
    //{
    //    using (var httpClient = new HttpClient())
    //    {
    //        var content = new StringContent(data, Encoding.UTF8, "application/json");
    //        var response = await httpClient.PostAsync(url, content);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var responseContent = await response.Content.ReadAsStringAsync();
    //            return responseContent;
    //        }
    //        else
    //        {
    //            throw new Exception($"Request failed with status code {response.StatusCode}.");
    //        }
    //    }
    //}

    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ApiResponseModel<NumberFactModel>> Get()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://numbersapi.com/random/trivia?json");

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseModel<NumberFactModel>
                    {
                        StatusCode = response.StatusCode,
                        Message = "An error occurred while calling the API."
                    };
                }

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NumberFactModel>(content);

                return new ApiResponseModel<NumberFactModel>
                {
                    StatusCode = response.StatusCode,
                    Message = "Success",
                    Data = new List<NumberFactModel> { data }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<NumberFactModel>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponseModel<NumberFactModel>> Post(string number)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("json", ""),
                new KeyValuePair<string, string>("number", number)
            });

                var response = await _httpClient.PostAsync("http://numbersapi.com", content);

                //if (!response.IsSuccessStatusCode)
                //{
                //    return new ApiResponseModel<NumberFactModel>
                //    {
                //        StatusCode = response.StatusCode,
                //        Message = "An error occurred while calling the API."
                //    };
                //}

                var responseContent = await response.Content.ReadAsStringAsync();
                var data = new NumberFactModel
                {
                    Number = number,
                    Text = responseContent.Trim()
                };

                return new ApiResponseModel<NumberFactModel>
                {
                    StatusCode = response.StatusCode,
                    Message = "Success",
                    Data = new List<NumberFactModel> { data }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<NumberFactModel>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}
