using System;
using System.Threading.Tasks;
using CountriesApp.Common.Models;

namespace CountriesApp.Common.Services
{
    public interface IApiService
    {
        Task<Response<object>> CheckConnection(string url);


        Task<Response<object>> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller);
    }
}
