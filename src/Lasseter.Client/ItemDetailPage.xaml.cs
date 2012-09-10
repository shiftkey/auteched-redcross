using Lasseter.Client.Data;
using Lasseter.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Bing.Maps;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace Lasseter.Client
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : Lasseter.Client.Common.LayoutAwarePage
    {
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int itemId = (int)e.Parameter;
            var vm = new ItemDetailPageViewModel(itemId);
            this.ViewModel = vm;
            // translate boolean
            PersonOnDuty.Text = vm.Person.OnDuty ? "Yes" : "No";
            AddPin(vm.Person.Name, vm.Person.Latitude, vm.Person.Longitude);
            CentreZoomMap(vm.Person.Latitude, vm.Person.Longitude,12);
            //LasseterPerson person = LasseterDataSource.GetItem(itemId.ToString());
            //Person = person;
        }

        // TODO: Refactor to utility class
        void AddPin(string text, double latitude, double longitude)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Text = text;
            MapLayer.SetPosition(pushpin, new Location(latitude, longitude));
            detailedMap.Children.Add(pushpin);
        }


        void CentreZoomMap(double latitude, double longitude,double zoom)
        {
            detailedMap.Center = new Location(latitude, longitude);
            detailedMap.ZoomLevel = zoom;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
          //  var selectedItem = (LasseterPerson)this.flipView.SelectedItem;
           // pageState["SelectedItem"] = selectedItem.UniqueId;
        }
    }
}
