using System;
using System.Threading.Tasks;

namespace CountriesApp.Common.Services
{
    public interface IGeolocatorService
    {
        double Latitude { get; set; }

        double Longitude { get; set; }

        Task GetLocationAsync();
    }
}
