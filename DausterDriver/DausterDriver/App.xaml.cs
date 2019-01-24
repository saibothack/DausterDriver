//using Com.OneSignal;
using DausterDriver.Helpers;
using DausterDriver.Services;
using DausterDriver.Utils;
using DausterDriver.Views;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DausterDriver
{
    public partial class App : Application
    {
        public static ServiceManager oServiceManager { get; private set; }
        public static System.Timers.Timer oTimer { get; private set; }
        public App()
        {
            InitializeComponent();
            //OneSignal.Current.StartInit(Constants.ONESIGNAL)
            //      .EndInit();

            oServiceManager = new ServiceManager(new RestService());

            if (Settings.IsLoggedIn)
            {
                currentLocation();
                MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Main Page" },
                    Detail = new NavigationPage(new HomePage())
                };
            }
            else {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void currentLocation() {
            oTimer = new System.Timers.Timer();
            oTimer.Interval = 5000;
            oTimer.Elapsed += EventoIntervalo;
            oTimer.Enabled = true;
        }

        public async static void currentLocationEnd()
        {
            oTimer = new System.Timers.Timer();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            Dictionary<string, object> coordenadas = new Dictionary<string, object>();
            coordenadas["latitude"] = position.Latitude;
            coordenadas["longitude"] = position.Longitude;
            coordenadas["status"] = false;

            int IsSuccessStatusCode = await oServiceManager.CurrentLocationAsync(coordenadas);
        }

        private async static void EventoIntervalo(Object source, System.Timers.ElapsedEventArgs e)
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            Dictionary<string, object> coordenadas = new Dictionary<string, object>();
            coordenadas["latitude"] = position.Latitude;
            coordenadas["longitude"] = position.Longitude;
            coordenadas["status"] = true;

            int IsSuccessStatusCode = await oServiceManager.CurrentLocationAsync(coordenadas);

        }
    }
}
