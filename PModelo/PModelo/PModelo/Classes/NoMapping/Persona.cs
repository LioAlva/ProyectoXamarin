using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Classes.NoMapping
{
    public class Persona
    {
        public  int Id_Persona { get; set; }
        public  string Nombre { get; set; }
        public  DateTime Fecha_Nacimiento { get; set; }
        public  string Email_Persona { get; set; }
        public  string Email_Institucional { get; set; }
        public  string Direccion { get; set; }
        public  string Picture { get; set; }
        public  string Apellido_Paterno { get; set; }
        public  string Apellido_Materno { get; set; }
        public  string DNI { get; set; }
        public  string Estado { get; set; }
    }
}
