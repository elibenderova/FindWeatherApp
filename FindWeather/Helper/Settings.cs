using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FindWeather.Helper
{
    public static class Settings
    {
     
        private const string UseCityKey = "use_city";
        private static readonly bool UseCityDefault = true;


        private const string CityKey = "city";
        private static readonly string CityDefault = "Sofia";

        public static bool UseCity
        {
            get => Preferences.Get(UseCityKey, UseCityDefault);
            set => Preferences.Set(UseCityKey, value);
        }

        public static string City
        {
            get => Preferences.Get(CityKey, CityDefault);
            set => Preferences.Set(CityKey, value);
        }

    }
}
