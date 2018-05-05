using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PModelo.Models
{
    [Preserve(AllMembers = true)]
    public class BookDetails : INotifyPropertyChanged
    {
        public BookDetails()
        {
        }

        #region private variables

        private string bookID;
        private string customerName;
        private string price;
        private string bookName;
        private ImageSource image;

        #endregion

        #region Public Properties

        public string BookID
        {
            get { return bookID; }
            set
            {
                this.bookID = value;
                RaisePropertyChanged("BookID");
            }
        }

        public string CustomerName
        {
            get { return this.customerName; }
            set
            {
                this.customerName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string BookName
        {
            get { return bookName; }
            set
            {
                this.bookName = value;
                RaisePropertyChanged("BookName");
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                this.price = value;
                RaisePropertyChanged("Price");
            }
        }

        public ImageSource CustomerImage
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.RaisePropertyChanged("Image");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
