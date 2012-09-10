using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Lasseter.Client.DataModel;
using Lasseter.Client.Data;
using Lasseter.Entities;

namespace Lasseter.Client.ViewModels
{
    public class ItemDetailPageViewModel : INotifyPropertyChanged
    {
        private Person _person;

        public Person Person
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
            Entities.Person person = LasseterData.AllPeople.Where((x, r) => x.UniqueId == itemId).First();
            this.Person = person;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
