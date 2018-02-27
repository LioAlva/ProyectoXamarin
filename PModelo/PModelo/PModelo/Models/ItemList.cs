using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PModelo.Models
{
    public class ItemList
    {
        #region private variables
        protected int _orderID;
        protected string _customerID;
        protected string _freight;
        protected string _country;
        #endregion



        #region Public Properties

        public int OrderID
        {
            get { return _orderID; }
            set
            {
                this._orderID = value;
                RaisePropertyChanged("OrderID");
            }
        }

        public string CustomerID
        {
            get { return _customerID; }
            set
            {
                this._customerID = value;
                RaisePropertyChanged("CustomerID");
            }
        }

        public string Freight
        {
            get { return _freight; }
            set
            {
                this._freight = value;
                RaisePropertyChanged("Freight");
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                this._country = value;
                RaisePropertyChanged("Country");
            }
        }

        #endregion


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String Name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        #endregion
    }
}
