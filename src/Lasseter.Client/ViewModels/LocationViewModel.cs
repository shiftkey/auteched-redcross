
using Lasseter.Client.Data;
using Lasseter.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasseter.Client.ViewModels
{
    class LocationViewModel : INotifyPropertyChanged
    {
        public LocationViewModel()
        {
            
        }

        //public async Task<bool> LoadLocations()
        //{
        //    People = new ObservableCollection<Entities.PostCode>(await LasseterData.PopulateInitialData());
        //    return true;
        //}

        public async Task<bool> LoadSelectedLocations(String PostCode)
        {
            PCode = new ObservableCollection<Entities.PostCode>(await LasseterData.LocationSelectedData(PostCode));
            return true;
        }

        public ObservableCollection<Entities.PostCode> PCode
        {
            get { return _pcode; }
            set
            {
                _pcode = value;
                OnPropertyChanged("PostCode");
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
        private ObservableCollection<Entities.PostCode> _pcode;
    }
}