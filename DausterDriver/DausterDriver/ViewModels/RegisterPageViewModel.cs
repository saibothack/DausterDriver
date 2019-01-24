using DausterDriver.Helpers;
using DausterDriver.Models;
using DausterDriver.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterDriver.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase, IAsyncInitialization
    {
        Global global = new Global();

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
        public ImageSource imageSorceBackgrond { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public string buttonText { get; set; }
        public bool isVisiblePassword { get; set; }

        private User _oUser;
        public User oUser
        {
            get { return _oUser; }
            set
            {
                SetProperty(ref _oUser, value);
            }
        }

        private string _nameError;
        public string nameError
        {
            get { return _nameError; }
            set
            {
                SetProperty(ref _nameError, value);
            }
        }

        private bool _bNameError;
        public bool bNameError
        {
            get { return _bNameError; }
            set { SetProperty(ref _bNameError, value); }
        }

        private string _surnamesError;
        public string surnamesError
        {
            get { return _surnamesError; }
            set
            {
                SetProperty(ref _surnamesError, value);
            }
        }

        private bool _bSurnamesError;
        public bool bSurnamesError
        {
            get { return _bSurnamesError; }
            set { SetProperty(ref _bSurnamesError, value); }
        }

        private string _birthdayError;
        public string birthdayError
        {
            get { return _birthdayError; }
            set
            {
                SetProperty(ref _birthdayError, value);
            }
        }

        private bool _bBirthdayError;
        public bool bBirthdayError
        {
            get { return _bBirthdayError; }
            set { SetProperty(ref _bBirthdayError, value); }
        }

        private string _emailError;
        public string emailError
        {
            get { return _emailError; }
            set
            {
                SetProperty(ref _emailError, value);
            }
        }

        private bool _bEmailError;
        public bool bEmailError
        {
            get { return _bEmailError; }
            set { SetProperty(ref _bEmailError, value); }
        }

        private string _phoneError;
        public string phoneError
        {
            get { return _phoneError; }
            set
            {
                SetProperty(ref _phoneError, value);
            }
        }

        private bool _bPhoneError;
        public bool bPhoneError
        {
            get { return _bPhoneError; }
            set { SetProperty(ref _bPhoneError, value); }
        }

        private string _passwordError;
        public string passwordError
        {
            get { return _passwordError; }
            set
            {
                SetProperty(ref _passwordError, value);
            }
        }

        private bool _bPasswordError;
        public bool bPasswordError
        {
            get { return _bPasswordError; }
            set { SetProperty(ref _bPasswordError, value); }
        }

        private string _passwordConfirmError;
        public string passwordConfirmError
        {
            get { return _passwordConfirmError; }
            set
            {
                SetProperty(ref _passwordConfirmError, value);
            }
        }

        private bool _bPasswordConfirmError;
        public bool bPasswordConfirmError
        {
            get { return _bPasswordConfirmError; }
            set { SetProperty(ref _bPasswordConfirmError, value); }
        }

        private string _vehicleError;
        public string vehicleError
        {
            get { return _vehicleError; }
            set
            {
                SetProperty(ref _vehicleError, value);
            }
        }

        private bool _bVehicleError;
        public bool bVehicleError
        {
            get { return _bVehicleError; }
            set { SetProperty(ref _bVehicleError, value); }
        }

        private string _currentWorkError;
        public string currentWorkError
        {
            get { return _currentWorkError; }
            set
            {
                SetProperty(ref _currentWorkError, value);
            }
        }

        private bool _bCurrentWorkError;
        public bool bCurrentWorkError
        {
            get { return _bCurrentWorkError; }
            set { SetProperty(ref _bCurrentWorkError, value); }
        }

        private string _dataPlanError;
        public string dataPlanError
        {
            get { return _dataPlanError; }
            set
            {
                SetProperty(ref _dataPlanError, value);
            }
        }

        private bool _bDataPlanError;
        public bool bDataPlanError
        {
            get { return _bDataPlanError; }
            set { SetProperty(ref _bDataPlanError, value); }
        }

        private List<Vehicle> _lsVehicle;

        public List<Vehicle> lsVehicle
        {
            get { return _lsVehicle; }
            set { SetProperty(ref _lsVehicle, value); }
        }
        #endregion

        public RegisterPageViewModel() {
            Initialization = InitializeAsync();
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_inicial.jpg");

            DateTime dateTime = DateTime.Now;
            MinimumDate = dateTime.AddYears(-80);
            MaximumDate = dateTime.AddYears(-18);
            oUser.birthday = dateTime.AddYears(-20);

            if (!Settings.IsLoggedIn)
            {
                buttonText = "Registrar";
                isVisiblePassword = true;
            }
            else
            {
                buttonText = "Modificar";
                isVisiblePassword = false;
            }

            //RegisterCommand = new Command(RegisterProcess);
        }

        private async Task InitializeAsync()
        {
            if (Settings.IsLoggedIn)
            {
                IsBusy = true;
                oUser = await App.oServiceManager.GetUser();
                lsVehicle = await App.oServiceManager.GetVehicle();
                IsBusy = false;
            }
        }
    }
}
