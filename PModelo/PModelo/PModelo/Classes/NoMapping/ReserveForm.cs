using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Classes.NoMapping
{
    public class ReserveForm
    {
        public int Id_Reserva { get; set; }
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }
        public DateTime? Fecha_Reserva { get; set; }
        public int? Id_Parqueadero { get; set; }
        public int? Id_Cliente { get; set; }
        public int? Id_Scoring { get; set; }
        public string Estado { get; set; }
        public int? Id_Espacio { get; set; }
        public int UserTypeId { get; set; }
        public DateTime? Hora_Reserva { get; set; }
        public DateTime? Fecha_Hora_Inicio { get; set; }
        public DateTime? Fecha_Hora_Fin { get; set; }

    }
}
