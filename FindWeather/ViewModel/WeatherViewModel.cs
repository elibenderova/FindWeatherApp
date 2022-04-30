using FindWeather.Helper;
using FindWeather.Models;
using FindWeather.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using MvvmHelpers;
using Xamarin.Essentials;

namespace FindWeather.ViewModel
{
    class WeatherViewModel : BaseViewModel
    {
        WeatherService WeatherService { get; set; } = new WeatherService();

        string location = Settings.City;
        public string Location
        {
            get { return location; }

            set
            {
                SetProperty(ref location, value);
                Settings.City = value;
            }
        }

        bool useCurrentLocation;
        public bool UseCurrentLocation
        {
            get { return useCurrentLocation; }

            set
            {
                SetProperty(ref useCurrentLocation, value);
            }
        }

        string temp = string.Empty;
        public string Temp
        {
            get { return temp; }
            set { SetProperty(ref temp, value); }
        }

        string minTemp = string.Empty;
        public string MinTemp
        {
            get { return minTemp; }
            set { SetProperty(ref minTemp, value); }
        }

        string maxTemp = string.Empty;
        public string MaxTemp
        {
            get { return maxTemp; }
            set { SetProperty(ref maxTemp, value); }
        }

        string wind = string.Empty;
        public string Wind
        {
            get { return wind; }
            set { SetProperty(ref wind, value); ; }
        }

        string cloud = string.Empty;
        public string Cloud
        {
            get { return cloud; }
            set { SetProperty(ref cloud, value); ; }
        }


        WeatherForecastRoot forecast;
        public WeatherForecastRoot Forecast
        {
            get { return forecast; }
            set { forecast = value; OnPropertyChanged(); }
        }


        ICommand getWeather;
        public ICommand GetWeatherCommand =>
                getWeather ??
                (getWeather = new Command(async () => await ExecuteGetWeatherCommand()));

        private async Task ExecuteGetWeatherCommand()
        {
            if (IsBusy)
            {
               return;
            }
               
            IsBusy = true;
            try
            {
                WeatherRoot weatherRoot = null;

                if (UseCurrentLocation)
                {
                    var position = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                    weatherRoot = await WeatherService.GetWeather(position.Latitude, position.Longitude);
                    
               
                }
                else
                {
                    //Get weather by city
                    weatherRoot = await WeatherService.GetWeather(Location.Trim());
                }


                //Get forecast based on cityId
                Forecast = await WeatherService.GetForecast(weatherRoot.CityId);

                Location = weatherRoot?.Name;
                Temp = $"{weatherRoot?.MainWeather?.Temperature ?? 0}°C";
                MinTemp = $"{weatherRoot?.MainWeather?.MinTemperature ?? 0}°C";
                MaxTemp = $"{weatherRoot?.MainWeather?.MaxTemperature ?? 0}°C";
                Wind = $"{weatherRoot?.Wind?.Speed ?? 0}m/s";
                Cloud = $"{weatherRoot?.Clouds?.CloudinessPercent ?? 0}%";
                

                await TextToSpeech.SpeakAsync($"The temperature in {weatherRoot?.Name} is {Temp} at the moment");

            }
            catch (Exception ex)
            {
                Temp = "Can't Get Weather";
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
