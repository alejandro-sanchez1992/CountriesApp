using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace CountriesApp.MVVM.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Destroy()
        {

        }
    }
}
