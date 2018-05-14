using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Models
{
    public class Parking
    {
        [PrimaryKey]
        public int Id_Parqueadero { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono_Fijo { get; set; }
        public string Telefono_Movil { get; set; }
        public string Capacidad { get; set; }
        public int? Id_Tipo_Parking { get; set; }
    }
}
