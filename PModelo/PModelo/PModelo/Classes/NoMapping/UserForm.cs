using System;

namespace PModelo.Classes.NoMapping
{
    public class UserForm
    {
        public int Id_Usuario { get; set; }
        public int? Id_Persona { get; set; }
        public string Usuario_Name { get; set; }
        public string Contrasenia { get; set; }
        public string Perfil_Creacion { get; set; }
        public string Perfil_Modificacion { get; set; }
        public string Estado { get; set; }
        public PersonaForm Persona { get; set; }
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public DateTime? Fecha_Caducidad_Password { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int UserTypeId { get; set; }
    }
}
