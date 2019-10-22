using System;
using Prism.Navigation;

namespace CountriesApp.Prism.ViewModels
{
    public class CountryMapViewModel : BaseViewModel
    {
        public CountryMapViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Map";
        }
    }
}
