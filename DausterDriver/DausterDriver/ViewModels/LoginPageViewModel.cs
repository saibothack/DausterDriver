using DausterDriver.Helpers;
using DausterDriver.Models;
using DausterDriver.Utils;
using DausterDriver.Views;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterDriver.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Properties
        Global global = new Global();
        public ImageSource imageSorceLogo { get; set; }
        public ImageSource imageSorceBackgrond { get; set; }

        private User _user = new User();

        public User oUser
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        public LoginPageViewModel()
        {
            imageSorceLogo = ImageSource.FromResource("DausterCustomer.Images.ic_logo.png");
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_login.jpg");

            RegisterCommand = new Command(redirectRegister);
            LoginCommand = new Command(loginProcess);

        }

        public async void loginProcess()
        {
            IsBusy = true;
            String sError = String.Empty;
            String sComa = String.Empty;

            try
            {
                if (oUser.email == null)
                {
                    sError += "El email es obligatorio";
                    sComa = ", ";
                }
                else
                {
                    if (!global.IsValidEmail(oUser.email))
                    {
                        sError += "Por favor ingrese un email valido.";
                        sComa = ", ";
                    }
                }

                if (oUser.password == null)
                {
                    sError += sComa + "La contraseña es obligatoria";
                    IsBusy = false;
                }

                if (String.IsNullOrEmpty(sError))
                {
                    UserLogin userCurrent = await App.oServiceManager.LoginAsync(oUser);

                    if (userCurrent.success)
                    {
                        Settings.IsLoggedIn = true;
                        Settings.AccessToken = userCurrent.token;
                        CurrentLocation();
                        App.Current.MainPage = new MasterDetailPage()
                        {
                            Master = new MasterPage() { Title = "Main Page" },
                            Detail = new NavigationPage(new HomePage())
                        };
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Notificación", userCurrent.error.ToString(), "Ok");
                    }
                }
                else
                {
                    Message = sError;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsBusy = false;
            }
        }

        public async void CurrentLocation() {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {

                    await App.Current.MainPage.DisplayAlert("Ubicación", "Es necesario utilizar su ubicación", "Aceptar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
                App.currentLocation();
            }
            else if (status != PermissionStatus.Unknown)
            {
                await App.Current.MainPage.DisplayAlert("Ubicación", "Se denego la ubucación, no es posible continuar.", "Aceptar");
            }
        }

        public async void redirectRegister()
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
