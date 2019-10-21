using System;
using CountriesApp.Common.Models;
using CountriesApp.Prism.Views;
using Prism.Commands;
using Xamarin.Forms;

namespace CountriesApp.Prism.ViewModels
{
    public class CountryItemViewModel : Country
    {
        private DelegateCommand _selectCountryCommand;

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ?? (_selectCountryCommand = new DelegateCommand(SelectCountry));

        private async void SelectCountry()
        {
            MainViewModel.GetInstance().Country = new CountryViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountryItemPage());
        }
    }
}
