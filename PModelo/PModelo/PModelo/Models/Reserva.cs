using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Models
{
    public class Reserva
    {
        public  int Id_Reserva { get; set; }
        public  double? Longitud { get; set; }
        public  double? Latitud { get; set; }
        public  DateTime? Fecha_Reserva { get; set; }
        public  int? Id_Parqueadero { get; set; }
        public string Nombre_Parqueadero { get; set; }
        public string Nombre_Tipo_Parqueadero { get; set; }

        public int? Id_Cliente { get; set; }
        public  int? Id_Scoring { get; set; }
        public  string Estado { get; set; }
        public  int? Id_Espacio { get; set; }
        public string Nombre_Espacio { get; set; }

    }
}
