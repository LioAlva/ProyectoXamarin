using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PModelo.Classes;
using PModelo.Classes.ApiGoogle;
using PModelo.Classes.NoMapping;
using PModelo.Models;
using PModelo.Services;
using PModelo.Util;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PModelo.ViewModels
{//Parking
    public class ParkItemViewModel: Parking, INotifyPropertyChanged
    {
        #region Attributes
        //public NetService netService;
        public DataService dataService;
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        public GeolocatorService geolocatorService;
        public SettingsService settingsService;
        //private bool isRunning;
        private bool isBusy;
        private bool isEnabled;
        SfBusyIndicator busyIndicator;
        private List<string> _color; 
        private List<TypeParking> _typeParkings { get; set; }
        private ImageSource _imageSource;



        MediaFile file;
        #endregion

        #region Properties
        public double Longitude;
        public double Latitud;

        public string AdminArea { get; set; }
        private string Thoroughfare { get; set; }
        private string Locality { get; set; }
        private string CountryCode { get; set; }
        private string CountryName { get; set; }
        private string PostalCode { get; set; }
        private string SubLocality { get; set; }
        private string SubThoroughfare { get; set; }
        private string SubAdminArea { get; set; }
        private string[] ArrayPostalCodes { get; set; }

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


        public List<TypeParking> TypeParkings
        {
            set
            {
                if (_typeParkings != value)
                {
                    _typeParkings = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TypeParkings"));
                }
            }
            get
            {
                return _typeParkings;
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
        public ParkItemViewModel()
        {
            busyIndicator = new SfBusyIndicator();
            ImageSource = "icon";
            dialogService = new DialogService();
            navigationService = new NavigationService();
            geolocatorService = new GeolocatorService();
            apiService = new ApiService();
            dataService = new DataService();
            settingsService = new SettingsService();
            //netService = new NetService();

            TypeParkings = new List<TypeParking>();
            LoadTypeUsers();


            LoadColor();

            IsBusy = false;
            IsEnabled = true;
        }

        private void LoadTypeUsers()
        {
            TypeParkings.Clear();
            TypeParkings.Add(new TypeParking
            {
                Descripcion = "Privado",
                Id_Tipo_Parking = 1,
            });
            TypeParkings.Add(new TypeParking
            {
                Descripcion = "Público",
                Id_Tipo_Parking = 2,
            });
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
            if (string.IsNullOrEmpty(Nombre))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar el nombre del parqueadero.");
                return;
            }
            //if (string.IsNullOrEmpty(Direccion))
            //{
            //    await dialogService.ShowMessage("Mensaje", "Debe ingresar la dirección del parqueadero.");
            //    return;
            //}
            if (string.IsNullOrEmpty(Telefono_Fijo))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar el teléfono fijo del parqueadero.");
                return;
            }
            if (!string.IsNullOrEmpty(Telefono_Movil)) {
                var digito = Telefono_Movil.Substring(0, 1);
                if (Convert.ToInt32(digito) != 9)
                {
                    await dialogService.ShowMessage("Mensaje", "Debe empezar el primero dígito del número móvil con el número 9");
                    return;
                }
            }
            
            if (string.IsNullOrEmpty(Capacidad))
            {
                await dialogService.ShowMessage("Mensaje", "Debe ingresar la cantidad de espacios del parqueadero.");
                return;
            }
            if (Id_Tipo_Parking == 0)
            {
                await dialogService.ShowMessage("Mensaje", "Seleccione un tipo de parqueadero");
                return;
            }

            #region Foto no se usa todavia
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

            #endregion

            var parkForm = new ParkForm
            {
                Nombre = Nombre,
                Direccion = Direccion,
                Telefono_Fijo = Telefono_Fijo,
                Telefono_Movil = Telefono_Movil,
                Capacidad =int.Parse(Capacidad),
                Id_Tipo_Parking = Id_Tipo_Parking ?? 0,
            };

            if (CrossConnectivity.Current.IsConnected)
            {
                var response = settingsService.IsPermited();

                if (!response)
                {
                    var respuesta = await dialogService.ShowMessageYesAndNot("Mensaje", "¿Desea activar el Gps para poder registrar el establecimiento de parqueo?");
                    if (!respuesta)
                    {
                        return;
                    }
                    else
                    {
                        IsBusy = true;
                        IsEnabled = !IsBusy;
                        var resp = settingsService.IsConnected();
                        if (!resp)
                        {
                            IsBusy = false;
                            IsEnabled = !IsBusy;
                            ImageSource = "icon.png";
                        }
                        else
                        {
                            var result = await dialogService.ShowMessageYesAndNot("Confimación", "Estas seguro de enviar la Alerta");
                            if (result)
                            {
                                Conectarse(parkForm);
                            }
                            else
                            {
                                IsBusy= false;
                                IsEnabled = !IsBusy;
                                ImageSource = "icon.png";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    var result = await dialogService.ShowMessageYesAndNot("Confimación", "¿Estás seguro de agregar este establecimiento de parqueo?");
                    if (result)
                    {
                        IsBusy= true;
                        IsEnabled = !IsBusy;
                        Conectarse(parkForm);
                    }
                    else
                    {
                        IsBusy= false;
                        IsEnabled = !IsBusy;
                        ImageSource = "icon.png";
                        return;
                    }
                }
            }
            else
            {
                IsBusy = false;
                IsEnabled = !IsBusy;
                ImageSource = "icon.png";
                await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de Datos por favor.");
                return;
            }
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        private async void Cancel()
        {
            await navigationService.Navigate("MainPage");
        }

        #endregion

        #region Methods
        private async void Conectarse(ParkForm parkForm)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await geolocatorService.GetLocation();
                Longitude = geolocatorService.Longitude;
                Latitud = geolocatorService.Latitud;

                AdminArea = geolocatorService.AdminArea;
                Thoroughfare = geolocatorService.Thoroughfare;
                Locality = geolocatorService.Locality;
                CountryCode = geolocatorService.CountryCode;
                CountryName = geolocatorService.CountryName;
                PostalCode = geolocatorService.PostalCode;
                SubLocality = geolocatorService.SubLocality;
                SubThoroughfare = geolocatorService.SubThoroughfare;
                SubAdminArea = geolocatorService.SubAdminArea;

                Response response = null;
                if (parkForm != null)
                {
                    if (Longitude != 0 && Latitud != 0)
                    {
                        parkForm.Longitud = Longitude;
                        parkForm.Latitud = Latitud;

                        if (string.IsNullOrEmpty(Direccion)) {
                            var cadena = "https://maps.googleapis.com/maps/api/geocode/";
                            var UrlServerMethod = "json?latlng=" + Latitud + "," + Longitude;
                            var request = await apiService.GetGoogleService<ResponseGoogle>(cadena, UrlServerMethod);

                            if (request != null)
                            {
                                if (request.status.Equals("OK"))
                                {
                                    if (request.results.Count() > 0)
                                    {
                                        string[] caracteres = request.results.FirstOrDefault().formatted_address.Split(',');
                                        Thoroughfare = caracteres[0];
                                    }
                                }
                            }
                            Direccion = Thoroughfare;
                        }

                        var currentUser = dataService.First<User>(false);
                        if (currentUser != null && currentUser.UserId > 0)
                        {
                            parkForm.UserTypeId = currentUser.UserTypeId;
                            parkForm.UserId = currentUser.UserId;
                            parkForm.Direccion = Direccion;
                        }
                        else {
                            isBusy = false;
                            IsEnabled = !isBusy;
                            await dialogService.ShowMessage("Mensaje", "Usuario no identifiacado, revise su configuración");
                            return;
                        }

                        var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
                        if (isReachable)
                        {          
                            var respuesta = await apiService.Post<ParkForm, Response>(Configuration.SERVER, "/api", "/Parking/RegisterParking", currentUser.TokenType, currentUser.AccessToken, parkForm);

                            if (respuesta != null)
                            {
                                if (respuesta.IsSuccess)
                                {
                                    var result = (Response)respuesta.Resullt;
                                    IsBusy = false;
                                    IsEnabled = !IsBusy;

                                    if (result.IsSuccess)
                                    {
                                        await dialogService.ShowMessage("Confirmación", result.Message);
                                        await navigationService.Navigate("MainPage");
                                        ClearFormParqueadero();
                                    }
                                    else
                                    {
                                        await dialogService.ShowMessage("Mensaje", result.Message);
                                        return;
                                    }
                                }
                                else
                                {
                                    IsBusy = false;
                                    IsEnabled = !IsBusy;
                                    await dialogService.ShowMessage("Mensaje", "Servicio no encontrado");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            IsBusy = false;
                            IsEnabled = !IsBusy;
                            await dialogService.ShowMessage("Mensaje", "Es necesario tener conexión a internet para poder registrarse");
                            return;
                        }
                     }
                     else
                     {
                        IsBusy = false;
                        IsEnabled = !IsBusy;
                        ImageSource = "icon.png";
                        await dialogService.ShowMessage("Mensaje", response.Message);
                        return;
                     }
                }
                else
                {
                    IsBusy = false;
                    IsEnabled = !IsBusy;
                    ImageSource = "icon.png";
                    await dialogService.ShowMessage("Mensaje", "No se pudo crear el parqueadero verifique la información agregada");
                    return;
                }
            }
            else
            {
                ImageSource = "icon.png";
                await dialogService.ShowMessage("Confimación", "Es necesario tener acceso a Internet, active el Wifi o su paquete de datos por favor.");
                return;
            }

        }

        private void ClearFormParqueadero()
        {
            Nombre = string.Empty;
            Direccion = string.Empty;
            Telefono_Fijo = string.Empty;
            Telefono_Movil = string.Empty;
            Capacidad = string.Empty;
            Id_Tipo_Parking = 0;
        }
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}





/******
 * 
 * 
 * PROCEDURE REVISAR
 
    create procedure [usp_my_procedure_name]
as
begin
    set nocount on;
    declare @trancount int;
    set @trancount = @@trancount;
    begin try
        if @trancount = 0
            begin transaction
        else
            save transaction usp_my_procedure_name;

        -- Do the actual work here

lbexit:
        if @trancount = 0
            commit;
    end try
    begin catch
        declare @error int, @message varchar(4000), @xstate int;
        select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE(), @xstate = XACT_STATE();
        if @xstate = -1
            rollback;
        if @xstate = 1 and @trancount = 0
            rollback
        if @xstate = 1 and @trancount > 0
            rollback transaction usp_my_procedure_name;

        raiserror ('usp_my_procedure_name: %d: %s', 16, 1, @error, @message) ;
    end catch
end
go


 * 
 * 
 * *******/
