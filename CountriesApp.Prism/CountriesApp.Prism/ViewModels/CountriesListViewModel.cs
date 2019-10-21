using System;
using CountriesApp.Common.Services;
using Xamarin.Forms;
using Prism.Commands;
using Prism.Navigation;
using CountriesApp.Common.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CountriesApp.Prism.ViewModels
{
    public class CountriesListViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private bool _isRunning;
        private bool _isRefreshing;
        private string _filter;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _searchCommand;
        private ObservableCollection<CountryItemViewModel> _countries;

        public CountriesListViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;

            Title = "Countries List";
            GetCountries();
        }

        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(GetCountries));

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));

        public Assembly SvgAssembly
        {
            get => typeof(App).GetTypeInfo().Assembly; 
        }

        public string CoolMaskSvgPath
        {
            get => "CountriesApp.Prism.Images.CoolMask.svg";
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetValue(ref _isRunning, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetValue(ref _isRefreshing, value);
        }

        public string Filter
        {
            get => _filter;
            set
            {
                SetValue(ref _filter, value);
                Search();
            }
        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetValue(ref _countries, value);
        }

        private async void GetCountries()
        {
            IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();

            var connection = await _apiService.CheckConnection(url);

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await _apiService.GetListAsync<Country>(
                url,
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            MainViewModel.GetInstance().CountriesList = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<CountryItemViewModel>(ToCountryItemViewModel());

            IsRefreshing = false;
        }

        private IEnumerable<CountryItemViewModel> ToCountryItemViewModel()
        {
            return MainViewModel.GetInstance().CountriesList.Select(l => new CountryItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                this.Countries = new ObservableCollection<CountryItemViewModel>(ToCountryItemViewModel());
            }
            else
            {
                this.Countries = new ObservableCollection<CountryItemViewModel>(
                    ToCountryItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
    }
}

