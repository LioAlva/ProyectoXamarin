using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Classes.NoMapping
{
    public class ParkForm
    {
        public int UserId { get; set; }
        public int Id_Parqueadero { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono_Fijo { get; set; }
        public string Telefono_Movil { get; set; }
        public int Capacidad { get; set; }
        public int Id_Tipo_Parking { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public int UserTypeId { get; set; }
    }
}
