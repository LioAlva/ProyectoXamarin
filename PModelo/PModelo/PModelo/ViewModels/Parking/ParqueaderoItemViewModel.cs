using PModelo.Models;
using System.ComponentModel;

namespace PModelo.ViewModels
{
    public class ParqueaderoItemViewModel:Parqueadero, INotifyPropertyChanged
    {



        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
