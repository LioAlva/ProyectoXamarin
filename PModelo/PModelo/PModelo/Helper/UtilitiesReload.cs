using PModelo.Models;
using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PModelo.Helper
{
    public class UtilitiesReload
    {
        public static ObservableCollection<Parqueadero> ReloadParqueaderos(List<Parqueadero> listParqueaderos)
        {
            if (listParqueaderos != null) {
                ObservableCollection<Parqueadero> listPar = new ObservableCollection<Parqueadero>();
                if (listParqueaderos != null)
                {
                    foreach (var itemP in listParqueaderos)
                    {
                        listPar.Add(new Parqueadero
                        {
                            Capacidad = itemP.Capacidad,
                            Direccion = itemP.Direccion,
                            Fecha_Creacion = itemP.Fecha_Creacion,
                            Id_Parqueadero = itemP.Id_Parqueadero,
                            Latitud = itemP.Latitud,
                            Longitud = itemP.Longitud,
                            Nombre = itemP.Nombre,
                            Telefono_Fijo = itemP.Telefono_Fijo,
                            Telefono_Movil = itemP.Telefono_Movil,
                            Id_Tipo_Parking = itemP.Id_Tipo_Parking,
                            Estado = itemP.Estado,
                            Plazas_Disponibles = itemP.Plazas_Disponibles,
                            Plazas_Ocupadas = itemP.Plazas_Ocupadas
                        });
                    }
                }
                return listPar;
            }
            return null;
        }

        public static ObservableCollection<ParqueaderoItemViewModel> ReloadParqueaderosZS(List<Parqueadero> listParqueaderos)
        {
            if (listParqueaderos != null)
            {
                ObservableCollection<ParqueaderoItemViewModel> listPar = new ObservableCollection<ParqueaderoItemViewModel>();
                if (listParqueaderos != null)
                {
                    foreach (var itemP in listParqueaderos)
                    {
                        listPar.Add(new ParqueaderoItemViewModel
                        {
                            Capacidad = itemP.Capacidad,
                            Direccion = itemP.Direccion,
                            Fecha_Creacion = itemP.Fecha_Creacion,
                            Id_Parqueadero = itemP.Id_Parqueadero,
                            Latitud = itemP.Latitud,
                            Longitud = itemP.Longitud,
                            Nombre = itemP.Nombre,
                            Telefono_Fijo = itemP.Telefono_Fijo,
                            Telefono_Movil = itemP.Telefono_Movil,
                            Id_Tipo_Parking = itemP.Id_Tipo_Parking,
                            Estado = itemP.Estado,
                            Plazas_Disponibles = itemP.Plazas_Disponibles,
                            Plazas_Ocupadas = itemP.Plazas_Ocupadas,
                            Espacios=itemP.Espacios
                        });
                    }
                }
                return listPar;
            }
            return null;
        }
    }
}

//public static List<EspacioClass> ReloadEspacios(IList<Espacio> espacios)
//{
//    EspacioClass escl = new EspacioClass();
//    List<EspacioClass> listesp = new List<EspacioClass>();
//    if (espacios != null)
//    {
//        foreach (var item in espacios)
//        {
//            escl = new EspacioClass
//            {
//                Id_Espacio = item.Id_Espacio,
//                Id_Parqueadero = item.Id_Parqueadero,
//                Descripcion = item.Descripcion,
//                Estado = item.Estado,
//                Hora_Entrada = item.Hora_Entrada,
//                Hora_Salida = item.Hora_Salida,
//                Ocupado = item.Ocupado,
//                Tipo_Espacio = item.Tipo_Espacio
//            };
//            listesp.Add(escl);
//        }
//        return listesp;
//    }
//    return null;
//}
