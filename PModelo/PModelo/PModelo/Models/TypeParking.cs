using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Models
{
    public class TypeParking
    {
        [PrimaryKey]
        public int Id_Tipo_Parking { get; set; }
        public string Descripcion { get; set; }
    }
}
