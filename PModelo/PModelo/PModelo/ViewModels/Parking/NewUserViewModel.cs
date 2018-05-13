using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PModelo.Classes;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace PModelo.ViewModels
{
    public class NewUserViewModel : User, INotifyPropertyChanged
    {
        #region Attributes
        //public NetService netService;
        public DataService dataService;
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        public GeolocatorService geolocatorService;
        //private bool isRunning;
        private bool isBusy;
        private bool isEnabled;
        SfBusyIndicator busyIndicator;
        private List<string> _color;
        private List<TypeUser> _typeUsers { get; set; }
        private ImageSource _imageSource;


        MediaFile file;
        #endregion

        #region Properties

        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }


        public List<TypeUser> TypeUsers
        {
            set
            {
                if (_typeUsers != value)
                {
                    _typeUsers = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TypeUsers"));
                }
            }
            get
            {
                return _typeUsers;
            }
        }


        public List<string> Colors
        {
            set
            {
                if (_color != value)
                {
                    _color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Colors"));
                }
            }
            get
            {
                return _color;
            }
        }

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

        #endregion

        #region Contructor
        public NewUserViewModel()
        {
            ImageSource = "icon";
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            dataService = new DataService();
            //netService = new NetService();

            TypeUsers = new List<TypeUser>();
            LoadTypeUsers();


            LoadColor();

            isBusy = false;
            isEnabled = true;
        }

        private void LoadTypeUsers()
        {
            //var products = dataService.GetAllProducts(false);
            //Products.Clear();
            //foreach (var product in products)
            //{
                TypeUsers.Clear();
                TypeUsers.Add(new TypeUser
                {
                    Description = "Automovilista",
                    Price = 20,
                    UserTypeId = 2,
                });
                TypeUsers.Add(new TypeUser
                {
                    Description = "Administrador",
                    Price = 20,
                    UserTypeId = 4,
                });
            //}
        }



        private void LoadColor()
        {
            Colors = new List<string>();
            Colors.Add("Red");
            Colors.Add("Green");
            Colors.Add("Yellow");
            Colors.Add("Blue");
            Colors.Add("SkyBlue");
            Colors.Add("Orange");
            Colors.Add("Gray");
            Colors.Add("Pink");
        }
        #endregion

        #region Commands

        public ICommand ChangeImageCommand { get { return new RelayCommand(ChangeImage); } }

        private async void ChangeImage()
        {
            bool hasPermission = false;
            await CrossMedia.Current.Initialize();
            try
            {
              
                hasPermission = CrossMedia.Current.IsPickPhotoSupported;
            }
            catch (Exception genEx)
            {
                var Error = genEx;
            }

            if (!hasPermission)
            {
                 await dialogService.ShowMessage("Photos Not Supported", ":( Permission not granted to photos."); 
                return;
            }
           
           
            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == "Cancelar")
                {
                    file = null;
                    return;
                }

                if (source == "De la Cámara")
                {

                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                          
                        }
                    );
                }
                else
                {

                    file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    });
                }
            }
            else
            {

                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                });
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su nombre.");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su apellido paterno.");
                return;
            }
            if (string.IsNullOrEmpty(MotherLastName))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su apellido materno.");
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su número de teléfono.");
                return;
            }
            var digito = Phone.Substring(0, 1);
            if (Convert.ToInt32(digito) != 9)
            {
                await dialogService.ShowMessage("Mensaje", "Debe empezar el primero dígito del número telefónico con el número 9");
                return;
            }
            if (string.IsNullOrEmpty(DNI))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su número de DNI.");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar su email.");
                return;
            }
            if (!Utilities.IsValidEmail(Email))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar un Email  válido");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Mensaje", "Debes ingresar una Contraseña");
                return;
            }

            int numberCharter = Password.Length;
            if (numberCharter < 5)
            {
                await dialogService.ShowMessage("Mensaje", "Su contarseña debe tener como mínimo 5 caracteres.");
                return;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await dialogService.ShowMessage("Mensaje", "Debes ingresar la confirmación de la contraseña");
                return;
            }
            if (!PasswordConfirm.Equals(Password))
            {
                await dialogService.ShowMessage("Mensaje", "Las contraseñas no coinciden");
                return;
            }

            if (UserTypeId == 0)
            {
                await dialogService.ShowMessage("Mensaje", "Seleccione un tipo de usuario");
                return;
            }

            isBusy = true;
            IsEnabled = !isBusy;

            byte[] array = null;
            if (ImageSource != null)
            {
                Stream stream = null;
                if (file != null)
                {
                    stream = file.GetStream() ?? null;
                    if (stream != null)
                    {
                        array = Utilities.ReadFully(stream);
                    }
                }
            }

            var userForm = new UserForm
            {
                Nombre = FirstName,
                Apellido_Paterno = LastName,
                Apellido_Materno = MotherLastName,
                Telefono = Phone,
                DNI = DNI,
                Email = Email,
                Contrasenia = Password,
                Fecha_Nacimiento = DateTime.UtcNow.AddHours(-5),
                //Photo
                //Usuario_Name=Email,
                UserTypeId = UserTypeId,
            };

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (isReachable)
            {
                var response = await apiService.Post<UserForm, Response>(Configuration.SERVER,"/api", "/account/RegisterUser", "","",userForm,false);

                if (response != null)
                {
                    var result = (Response)response.Result;
                    isBusy = false;
                    IsEnabled = !isBusy;

                    if (result.IsSuccess)
                    {
                        await dialogService.ShowMessage("Confirmación", result.Message);
                        navigationService.SetMainPage("NewLoginPage");
                        //    var mainViewModel = MainViewModel.GetInstance();
                        //    mainViewModel.LoadNewUserWhite();
                    }
                    else
                    {
                        await dialogService.ShowMessage("Mensaje", result.Message);
                        return;
                    }
                }
            }
            else
            {
                isBusy = false;
                IsEnabled = !isBusy;
                await dialogService.ShowMessage("Mensaje", "Es necesario tener conexión a internet para poder registrarse");
                return;
            }
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        private void Cancel()
        {
            var user = new User();

            //navigationService.SetMainPage("LoginPage", user);
            navigationService.SetMainPage("NewLoginPage");

        }

        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


    }
}
