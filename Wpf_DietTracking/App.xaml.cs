using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_DietTracking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<User> _user;
        public static ObservableCollection<Log> _logs;
        public static ObservableCollection<Item> _items;
        public static ObservableCollection<FoodLog> _flogs;
        public static ObservableCollection<Activity> _activities;
        public static ObservableCollection<ActivityLog> _alogs;

        public static List<string> _genders = new List<string> { "Male", "Female" };
        public static List<string> _meals = new List<string> { "Breakfast", "Lunch", "Snacks", "Dinner" };
        public static List<string> _act = new List<string> { "Walking", "Running", "Jogging", "Bicycling" };

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<User>>(_user, "UserDetail.xml");
            MyStorage.WriteXml<ObservableCollection<Log>>(_logs, "LogDetail.xml");
            //MyStorage.WriteXml<ObservableCollection<Item>>(_items, "ItemList.xml");                    //Item-Calorie value XML file
            //MyStorage.WriteXml<ObservableCollection<Activity>>(_activities, "ActivityList.xml");       //Activity-Calorie value XML file
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _user = MyStorage.ReadXml<ObservableCollection<User>>("UserDetail.xml");
            if (_user == null)
                _user = new ObservableCollection<User>();

            _logs = MyStorage.ReadXml<ObservableCollection<Log>>("LogDetail.xml");
            if (_logs == null)
                _logs = new ObservableCollection<Log>();

            _items = MyStorage.ReadXml<ObservableCollection<Item>>("ItemList.xml");
            // if (_items == null)
            //   _items = new ObservableCollection<Item>();

            _activities = MyStorage.ReadXml<ObservableCollection<Activity>>("ActivityList.xml");
            // if (_activities == null)
            //   _activities = new ObservableCollection<Activity>();
        }
    }
}
