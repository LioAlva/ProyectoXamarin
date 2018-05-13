using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace PModelo.Models
{
    public class Persona
    {
        public Persona() { }
        [PrimaryKey, Column("Id_Persona")]
        public  int Id_Persona { get; set; }
        public  string Nombre { get; set; }
        public  DateTime? Fecha_Nacimiento { get; set; }
        public  string Email_Persona { get; set; }
        public  string Email_Institucional { get; set; }
        public  string Direccion { get; set; }
        public  string Picture { get; set; }
        public  string Apellido_Paterno { get; set; }
        public  string Apellido_Materno { get; set; }
        public  string DNI { get; set; }
        public  string Estado { get; set; }
        public  string Telefono { get; set; }


        //[ManyToOne]
        //public Persona Persona { get; set; }
        
        //[OneToOne]
        //public User User { get; set; }



        //[ForeignKey(typeof(Timeline)]
        //public int timelineId { get; set; }
        //[OneToOne(CascadeOperations = CascadeOperation.All)]
        //public Timeline timeline { get; set; }
    }

}
