using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;
using Lasseter.Client.DataModel;
using Lasseter.Entities;
using Newtonsoft.Json;
using System.Threading.Tasks;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Lasseter.Client.Data
{
    /// <summary>
    /// Base class for <see cref="Person"/> and <see cref="PersonGroup"/> that
    /// defines properties common to both.
    /// </summary>
    /// 
    public static class LasseterData
    {
        public static async Task<IEnumerable<Entities.Person>> PopulateInitialData()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://10.43.4.252/MvcwebService/api/values");

            var people = JsonConvert.DeserializeObject<List<Entities.Person>>(response);
            AllPeople = new ObservableCollection<Person>(people);
            return AllPeople;
        }

        public static async Task<IEnumerable<Entities.Person>> PopulateSelectedData(String pcode)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://10.43.4.252/MvcwebService/api/values?PostCode=" + pcode);

            var people = JsonConvert.DeserializeObject<List<Entities.Person>>(response);
            AllPeople = new ObservableCollection<Person>(people);
            return AllPeople;
        }

        public static ObservableCollection<Entities.Person> AllPeople { get; set; }
        public static ObservableCollection<Entities.Person> SelectedPeople { get; set; }
    }
}
