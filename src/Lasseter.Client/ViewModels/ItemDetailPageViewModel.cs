using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Lasseter.Client.DataModel;
using Lasseter.Client.Data;

namespace Lasseter.Client.ViewModels
{
    public class ItemDetailPageViewModel : INotifyPropertyChanged
    {
        private LasseterPerson _person;

        public LasseterPerson Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ItemDetailPageViewModel(int itemId)
        {
            //bring in the properties, add INotifyPropertyChanged
            LasseterPerson person = LasseterDataSource.GetItem(itemId.ToString());
            this.Person = person;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
