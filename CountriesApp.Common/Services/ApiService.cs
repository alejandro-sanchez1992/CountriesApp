namespace CountriesApp.Common.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Models;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Plugin.Connectivity;
    using System.Collections.Generic;
    using System;

    public class ApiService : IApiService
    {
        public async Task<Response<object>> CheckConnection(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(url);
            if (!isReachable)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response<object>
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<Response<object>> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var url = $"{servicePrefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response<object>
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
