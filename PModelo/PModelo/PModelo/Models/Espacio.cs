using SQLite.Net.Attributes;

namespace PModelo.Models
{
    public class Espacio
    {
        [PrimaryKey]
        public  int Id_Espacio { get; set; }
        public  int? Id_Parqueadero { get; set; }
        public  string Descripcion { get; set; }
    }
}
