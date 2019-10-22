using System;
using CountriesApp.Prism.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using CountriesApp.Prism.Views;
using CountriesApp.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CountriesApp.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("/NavigationPage/CountriesListPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CountriesListPage, CountriesListViewModel>();
            containerRegistry.RegisterForNavigation<CountryItemPage, CountryItemViewModel>();
            containerRegistry.RegisterForNavigation<CountryDataPage, CountryDataViewModel>();
            containerRegistry.RegisterForNavigation<CountryMapPage, CountryMapViewModel>();
        }
    }
}

