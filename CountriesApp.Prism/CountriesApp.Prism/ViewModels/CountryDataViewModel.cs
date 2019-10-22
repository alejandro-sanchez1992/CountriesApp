using System;
using CountriesApp.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace CountriesApp.Prism.ViewModels
{
    public class CountryDataViewModel : BaseViewModel { 

        private Country _country;
        private readonly INavigationService _navigationService;
        private DelegateCommand _viewMapCommand;

        public Country Country
        {
            get => _country;
            set => SetValue(ref _country, value);
        }

        public CountryDataViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Country";
            _navigationService = navigationService;
        }

        public DelegateCommand ViewMapCommand => _viewMapCommand ?? (_viewMapCommand = new DelegateCommand(ViewMapCountry));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
            }
        }

        private async void ViewMapCountry()
        {
            var parameters = new NavigationParameters
            {
                { "country", this }
            };

            await _navigationService.NavigateAsync("CountryMapPage", parameters);
        }
    }
}
