using Lasseter.Client.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasseter.Client.ViewModels
{
    public class PeopleViewModel: INotifyPropertyChanged
    {
        public PeopleViewModel()
        {
            LoadPeople();
        }

        public async void LoadPeople()
        {
            People = new ObservableCollection<Entities.Person>(await LasseterData.PopulateInitialData());
        }

        public ObservableCollection<Entities.Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged("People");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Entities.Person> _people;
    }
}
