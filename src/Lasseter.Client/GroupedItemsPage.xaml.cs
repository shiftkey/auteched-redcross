using Lasseter.Client.Data;
using Lasseter.Client.DataModel;
using Lasseter.Client.ViewModels;
using Lasseter.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace Lasseter.Client
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : Lasseter.Client.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = new PeopleViewModel();
            await ((PeopleViewModel)this.DataContext).LoadPeople();
            CentreZoomMap(-25.584999, 134.503998);
            SetupPins();
            //LasseterPerson person = LasseterDataSource.GetItem(itemId.ToString());
            //Person = person;
        }

        void SetupPins()
        {
            ObservableCollection<Entities.Person> people = ((PeopleViewModel)this.DataContext).People;
            foreach (Person person in people)
            {
                AddPin(person.Name, person.Latitude, person.Longitude);
            }
        }

        void AddPin(string text,double latitude, double longitude)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Text = text;
            MapLayer.SetPosition(pushpin, new Location(latitude, longitude));
            myMap.Children.Add(pushpin);
        }

        void CentreZoomMap(double latitude, double longitude)
        {
            myMap.Center = new Location(latitude,longitude);
            myMap.ZoomLevel = 4.5;
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
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((Person)e.ClickedItem).ID;
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            // TODO: find matching values from database
            //groupedItemsViewSource.Source = LasseterDataSource.GetFilteredItems(tbFind.Text);
        }
    }
}
