﻿using Syncfusion.SfBusyIndicator.XForms;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using System.ComponentModel;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using PModelo.Pages;
using System.Threading.Tasks;

namespace PModelo.ViewModels
{
    public class LoginViewModel:  INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private DataService dataService;

        private NavigationService navigationService;

        private string email;

        private string password;

        private bool isRunning;

        private bool isBusy;

        private bool isEnabled;

        private bool isRemembered;

        SfBusyIndicator busyIndicator;
        #endregion

        #region Properties


        public SfBusyIndicator BusyIndicator
        {
            set
            {
                if (busyIndicator != value)
                {
                    busyIndicator = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BusyIndicator"));
                }
            }
            get
            {
                return busyIndicator;
            }
        }


        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsBusy
        {
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
            get
            {
                return isBusy;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            busyIndicator = new SfBusyIndicator();

            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();

            IsRemembered = true;
            //IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand LoginFacebookCommand { get { return new RelayCommand(LoginFacebook); } }

        private  async void LoginFacebook()
        {
           await navigationService.Navigate("ListaRojaPage");
        }

        public ICommand LoginCommand { get { return new RelayCommand(LoginX); } }

        private async  void LoginX()
        {
            await navigationService.Navigate("WelcomePage");
        }

        public ICommand RegisteredCommand { get { return new RelayCommand(Registered); } }

        private  void Registered()
        {
            navigationService.SetMainPage("RegisterUser");
        }

        public ICommand NewLoginCommand { get { return new RelayCommand(NewLogin); } }

        private async void NewLogin()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "Ingrese correo electrónico.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Ingrese su contraseña.");
                return;
            }

            IsBusy = true;

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsBusy = false;
                await dialogService.ShowMessage("Error", "Revise su configuración de Internet.");
                return;
            }

            //var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            //if (!isReachable)
            //{
            //    IsBusy = false;
            //    await dialogService.ShowMessage("Error", "Revise su configuración de Internet.");
            //    return;
            //}

            var token = await apiService.GetToken(Configuration.SERVER,Email,Password);

            if (token == null)
            {
                IsBusy = false;
                await dialogService.ShowMessage("Error", "Usuario o password incorrecto.");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                IsBusy = false;
                await dialogService.ShowMessage("Error", token.ErrorDescription);
                Password = null;
                return;
            }

            var response = await apiService.GetUserByEmail(Configuration.SERVER,"api", "/account/GetUserByEmail", token.TokenType, token.AccessToken, token.UserName);

            if (!response.IsSuccess)
            {
                IsBusy = false;
                await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter.");
                return;
            }

            var user = (User)response.Resullt;
            user.AccessToken = token.AccessToken;
            user.TokenType = token.TokenType;
            user.TokenExpires = token.Expires;
            user.IsRemembered = IsRemembered;
            user.Password = Password;
            if (user.UserTypeId == 4)
            {
                dataService.DeleteAllAndInsert(user.Persona);
            }
            //var data = dataService.Get<Persona>(false);
            dataService.DeleteAllAndInsert(user);

            //dataService.InsertOrUpdate(user.FavoriteTeam);
            //dataService.InsertOrUpdate(user.UserType);
            //App.CurrentUser

            //var dataas = dataService.Get<User>(false);

            var mainViewModel = MainViewModel.GetInstance();

            switch (user.UserTypeId)
            {
                case 2:
                    mainViewModel.LoadUser(user);
                    mainViewModel.SetCurrentUser(user);
                    mainViewModel.LoadMenu(user);
                    ClearForm();
                    App.CurrentUser = user;
                    navigationService.SetMainPage("MasterPage");
                    ; break;
                case 4:
                    mainViewModel.LoadMenu(user);
                    mainViewModel.LoadUser(user);
                    mainViewModel.ListParkingForUser();
                    ClearForm();
                    App.CurrentUser = user;
                    navigationService.SetMainPage("MasterAdminPage");
                    ; break;
                default:
                    break;
            }

          
   
        }

        private void ClearForm()
        {
            Email = string.Empty;
            Password = string.Empty;
            IsBusy = false;
        }
        #endregion

    }
}
