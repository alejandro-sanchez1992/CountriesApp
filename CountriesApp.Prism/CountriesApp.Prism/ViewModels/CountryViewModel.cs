using System;
using CountriesApp.Common.Models;
using Prism.Navigation;

namespace CountriesApp.Prism.ViewModels
{
    public class CountryViewModel 
    {
        private readonly Country _country;

        public Country Country { get; set; }

        public CountryViewModel(
            Country country)
        {
            _country = country;
        }
    }
}
