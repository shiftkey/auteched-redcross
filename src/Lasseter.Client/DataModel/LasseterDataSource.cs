using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class LasseterDataCommon : Lasseter.Client.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public LasseterDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(LasseterDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class Person : LasseterDataCommon
    {
        public Person(String uniqueId, String title, String subtitle, String imagePath, String description, String content, PersonGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private PersonGroup _group;
        public PersonGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class PersonGroup : LasseterDataCommon
    {
        public PersonGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex,Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<Person> _items = new ObservableCollection<Person>();
        public ObservableCollection<Person> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<Person> _topItem = new ObservableCollection<Person>();
        public ObservableCollection<Person> TopItems
        {
            get {return this._topItem; }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class LasseterDataSource
    {
        private static LasseterDataSource _sampleDataSource = new LasseterDataSource();
        private ObservableCollection<Entities.Person> _allGroups = new ObservableCollection<Entities.Person>();
        //private ObservableCollection<PersonGroup> _allGroups = new ObservableCollection<PersonGroup>();
        //public ObservableCollection<PersonGroup> AllGroups
        //{
        //    get { return this._allGroups; }
        //}

        public ObservableCollection<Entities.Person> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<LasseterPerson> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            List<LasseterPerson> persons = new List<LasseterPerson>();
            foreach (var person in _sampleDataSource.AllGroups)
            {
                LasseterPerson testPerson = ConvertPersonToLasseterPerson(person);
                persons.Add(testPerson);
            }
            //return _sampleDataSource.AllGroups;
            return persons;
        }

        //HACK: haven't yet figured out column mappings for data view.
        private static LasseterPerson ConvertPersonToLasseterPerson(Entities.Person person)
        {
            LasseterPerson testPerson = new LasseterPerson() { UniqueId=person.UniqueId, Title = person.Name, Subtitle = person.Latitude.ToString() };
            return testPerson;
        }

        //public static PersonGroup GetGroup(string uniqueId)
        //{
        //    // Simple linear search is acceptable for small data sets
        //    var matches = _sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
        //    if (matches.Count() == 1) return matches.First();
        //    return null;
        //}

        public static LasseterPerson GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = (from Entities.Person i in _sampleDataSource.AllGroups where i.UniqueId.ToString().Equals(uniqueId) select i).FirstOrDefault();
                
                //_sampleDataSource.AllGroups.Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches!= null) return ConvertPersonToLasseterPerson(matches);
            return null;
        }

        public static IEnumerable<PersonGroup> GetFilteredItems(string filterValue)
        {
            ObservableCollection<PersonGroup> filteredGroups = new ObservableCollection<PersonGroup>();
            var group1 = new PersonGroup("Group-1", "value1", "value1 subtitle", "", "test1");
            group1.Items.Add(new Person("Group1Item1", "item1value", "item1 subtitle", "", "item 1", "dklsafjdklsjfkdlas=", group1));
            group1.Items.Add(new Person("Group1Item2", "item2value", "item2 subtitle", "", "item 2", "fdgghfhjgghfd", group1));
            filteredGroups.Add(group1);
            return filteredGroups;
        }
        public LasseterDataSource() 
        {
            var person = new Entities.Person { UniqueId=1, Name = "Joe Bloggs", Latitude = 1234, Longitude = 4321 };
            this.AllGroups.Add(person);
            var person1 = new Entities.Person { UniqueId=2, Name = "Jim Bloggs", Latitude = 1234, Longitude = 4321 };
            this.AllGroups.Add(person1);
        }


    }
}
