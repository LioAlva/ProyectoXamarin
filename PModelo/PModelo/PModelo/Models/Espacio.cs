using SQLite.Net.Attributes;
using System;

namespace PModelo.Models
{
    public class Espacio
    {
        [PrimaryKey]
        public  int Id_Espacio { get; set; }
        public  int? Id_Parqueadero { get; set; }
        public  string Descripcion { get; set; }
        public  DateTime? Hora_Entrada { get; set; }
        public  DateTime? Hora_Salida { get; set; }
        public  string Estado { get; set; }
        public  string Ocupado { get; set; }
        public  int Tipo_Espacio { get; set; }
    }
}
