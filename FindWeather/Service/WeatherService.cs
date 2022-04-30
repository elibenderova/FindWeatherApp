using FindWeather.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace FindWeather.Service
{
    class WeatherService
    {
        const string API_KEY = "de7757630b3ecad16a8d1249b3de39f7";
        const string WEATHER_COORDINATES_URI = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid=" + API_KEY;
        const string WEATHER_CITY_URI = "http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=" + API_KEY;
        const string FORECAST_URI = "http://api.openweathermap.org/data/2.5/forecast?id={0}&units=metric&appid=" + API_KEY;

        public async Task<WeatherRoot> GetWeather(double latitude, double longitude)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WEATHER_COORDINATES_URI, latitude, longitude);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }
                   
                return DeserializeObject<WeatherRoot>(json);
            }
        }

        public async Task<WeatherRoot> GetWeather(string city)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WEATHER_CITY_URI, city);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }

                return DeserializeObject<WeatherRoot>(json);
            }

        }

        public async Task<WeatherForecastRoot> GetForecast(int id)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(FORECAST_URI, id);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }
                    
                return DeserializeObject<WeatherForecastRoot>(json);
            }
        }
    }
}
