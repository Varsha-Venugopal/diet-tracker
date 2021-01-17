using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_DietTracking
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class W_start : Window
    {
        public W_start()
        {
            InitializeComponent();
             
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // App._items.Add(new Item { FoodItem = "Lemons", Calories = 29, Quantity =  });
            // App._activities.Add(new Activity { Act_name = "Running", Calories = 624, Speed = 6.67 });
            bool newUser = Properties.Settings.Default.Usernew_;
            if (newUser | App._user.Count == 0)   //checking for first time user entry
            {
                newUser = false;
                Properties.Settings.Default.Usernew_ = newUser;
                Properties.Settings.Default.Save();
                var window = new W_aboutYou();
                window.Owner = this;
                Visibility = Visibility.Hidden;
                window.ShowDialog();
            }
            Lbx_Dates.ItemsSource = App._logs;
        }


        private void Tbx_track_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tbx_count.Text == "0")
                MessageBox.Show("No logs yet to track, tap 'New log' button first to begin! :)");
            else
            {
                var filter = (sender as TextBox).Text;  //get info from filter box
                var lst = from l in App._logs where l.logDate.Equals(filter) select l; //select all students from list
                Lbx_Dates.ItemsSource = lst; //filter as you type
            }
        }

        //button-click events
        private void Btn_AboutYou_Click(object sender, RoutedEventArgs e)
        {
            var win = new W_aboutYou();
            win.Owner = this;  //this window will be the owner
            win.StPnl_Details.DataContext = App._user.LastOrDefault();
            Visibility = Visibility.Hidden;
            // win.Show(); //can create more than 1 windows
            win.ShowDialog();         
        }

        private void Btn_NewLog_Click(object sender, RoutedEventArgs e)
        {
            var cr = App._user.LastOrDefault().calReqt;
            Log newLog = new Log { logDate = DateTime.Now, calBurned = 0, calConsumed = 0, calGoal = cr, calRemained = 0, foodLogs = { }, activityLogs = { } };
            App._logs.Add(newLog);
            Lbx_Dates.SelectedItem = newLog;
            Lbx_Dates.ScrollIntoView(newLog);
            if (Convert.ToInt32(Tbx_calGoal.Text) < 1000 | Convert.ToInt32(Tbx_calGoal.Text) > 4000)
            {
                MessageBox.Show("Enter calorie goal between range of 1000 - 4000!");
            }
        }

        private void Btn_UpdateLog_Click(object sender, RoutedEventArgs e)
        {
            
            var window = new W_log();
            window.Owner = this;
            window.StPnl_Log.DataContext = Lbx_Dates.SelectedItem as Log;
            Visibility = Visibility.Hidden;
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App._user.LastOrDefault().logs = App._logs;
        }
    }
}
