using FindWeather.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainView())
            {
                BarBackgroundColor = Color.LightSalmon,
                BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
