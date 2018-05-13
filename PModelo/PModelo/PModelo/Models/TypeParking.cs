using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Models
{
    public class TypeParking
    {
        [PrimaryKey]
        public int TypeParkingId { get; set; }
        public string Description{ get; set; }
    }
}
