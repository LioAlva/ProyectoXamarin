using SQLite.Net.Attributes;
using System;

namespace PModelo.Models
{
    public class Parqueadero
    {
        [PrimaryKey]    
        public  int Id_Parqueadero { get; set; }
        public  string Nombre { get; set; }
        public  double? Longitud { get; set; }
        public  double? Latitud { get; set; }
        public  string Direccion { get; set; }
        public  string Telefono_Fijo { get; set; }
        public  string Telefono_Movil { get; set; }
        public  string Plazas_Disponibles { get; set; }
        public  int? Plazas_Ocupadas { get; set; }
        public  int? Capacidad { get; set; }
        public  int? Id_Tipo_Parking { get; set; }
        public  DateTime? Fecha_Creacion { get; set; }
        public  string Estado { get; set; }
    }
}
