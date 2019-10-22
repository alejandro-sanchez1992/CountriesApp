using System;
using System.Collections.Generic;
using CountriesApp.Common.Models;

namespace CountriesApp.Prism.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public List<Country> CountriesList
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public CountriesListViewModel Countries
        {
            get;
            set;
        }

        public CountryDataViewModel Country
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
