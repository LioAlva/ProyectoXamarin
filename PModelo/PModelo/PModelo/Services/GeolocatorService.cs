using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PModelo.Services
{
    public class GeolocatorService
    {
        public string AdminArea { get; set; }
        public double Latitud { get; set; }
        public double Longitude { get; set; }
        public string Thoroughfare { get; set; }
        public string Locality { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string SubLocality { get; set; }
        public string SubThoroughfare { get; set; }
        public string SubAdminArea { get; set; }

        public async Task GetLocation()
        {
            try
            {
                string mm = string.Empty;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var location = await locator.GetPositionAsync();

                if (location == null)
                    return;

                var address = await locator.GetAddressesForPositionAsync(location);

                if (address != null || address.Count() > 0)
                {
                    var a = address.FirstOrDefault();

                    AdminArea = a.AdminArea ?? string.Empty;
                    Thoroughfare = a.Thoroughfare ?? string.Empty;
                    Locality = a.Locality ?? string.Empty;
                    CountryCode = a.CountryCode ?? string.Empty;
                    CountryName = a.CountryName ?? string.Empty;
                    PostalCode = a.PostalCode ?? string.Empty;
                    SubLocality = a.SubLocality ?? string.Empty;
                    SubThoroughfare = a.SubThoroughfare ?? string.Empty;
                    SubAdminArea = a.SubAdminArea ?? string.Empty;
                }

                Latitud = location.Latitude;
                Longitude = location.Longitude;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
