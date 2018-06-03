using PModelo.Models;
using System.ComponentModel;

namespace PModelo.ViewModels
{
    public class ParqueaderoItemViewModel:Parqueadero, INotifyPropertyChanged
    {

        public string NombreTipoParqueadero{ get; set; }

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
