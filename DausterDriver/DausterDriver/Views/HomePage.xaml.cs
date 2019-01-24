using DausterDriver.Utils;
using DausterDriver.ViewModels;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace DausterDriver.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        string GooglePlacesApiKey = Constants.googleKey;
        private HomePageViewModel viewModel;
        private static string jsonStyle = "[{'elementType': 'geometry', 'stylers': [{'color': '#1d2c4d'}] }," +
            "{'elementType': 'labels.text.fill','stylers': [{'color': '#8ec3b9'}]}," +
            "{'elementType': 'labels.text.stroke','stylers': [{'color': '#1a3646'}]}," +
            "{'featureType': 'administrative.country','elementType': 'geometry.stroke','stylers': [{'color': '#4b6878'}]}," +
            "{'featureType': 'administrative.land_parcel','elementType': 'labels.text.fill','stylers': [{'color': '#64779e'}]}," +
            "{'featureType': 'administrative.province','elementType': 'geometry.stroke','stylers': [{'color': '#4b6878'}]}," +
            "{'featureType': 'landscape.man_made','elementType': 'geometry.stroke','stylers': [{'color': '#334e87'}]}," +
            "{'featureType': 'landscape.natural','elementType': 'geometry','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'poi','elementType': 'geometry','stylers': [{'color': '#283d6a'}]}," +
            "{'featureType': 'poi','elementType': 'labels.text.fill','stylers': [{'color': '#6f9ba5'}]}," +
            "{'featureType': 'poi','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'poi.park','elementType': 'geometry.fill','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'poi.park','elementType': 'labels.text.fill','stylers': [{'color': '#3C7680'}]}," +
            "{'featureType': 'road','elementType': 'geometry','stylers': [{'color': '#304a7d'}]}," +
            "{'featureType': 'road','elementType': 'labels.text.fill','stylers': [{'color': '#98a5be'}]}," +
            "{'featureType': 'road','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'road.highway','elementType': 'geometry','stylers': [{'color': '#2c6675'}]}," +
            "{'featureType': 'road.highway','elementType': 'geometry.stroke','stylers': [{'color': '#255763'}]}," +
            "{'featureType': 'road.highway','elementType': 'labels.text.fill','stylers': [{'color': '#b0d5ce'}]}," +
            "{'featureType': 'road.highway','elementType': 'labels.text.stroke','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'transit','elementType': 'labels.text.fill','stylers': [{'color': '#98a5be'}]}," +
            "{'featureType': 'transit','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'transit.line','elementType': 'geometry.fill','stylers': [{'color': '#283d6a'}]}," +
            "{'featureType': 'transit.station','elementType': 'geometry','stylers': [{'color': '#3a4762'}]}," +
            "{'featureType': 'water','elementType': 'geometry','stylers': [{'color': '#0e1626'}]}," +
            "{'featureType': 'water','elementType': 'labels.text.fill','stylers': [{'color': '#4e6d70'}]}]";

        public HomePage ()
		{
			InitializeComponent();
            map.MapStyle = MapStyle.FromJson(jsonStyle);
            map.UiSettings.ZoomControlsEnabled = false;
            BindingContext = viewModel = new HomePageViewModel();
            viewModel.Navigation = this.Navigation;
            LocationEnabled();
        }

        private async void LocationEnabled()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Ubicación", "Es necesario utilizar su ubicación", "Aceptar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                map.MyLocationEnabled = true;
                map.UiSettings.MyLocationButtonEnabled = true;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(150)), true);
            }
            else if (status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Ubicación", "Se denego la ubucación, no es posible continuar.", "Aceptar");
            }
        }
    }
}