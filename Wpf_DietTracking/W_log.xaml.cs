using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class W_log : Window
    {
        Log newLog = new Log();
        double  sp, speed;
        ObservableCollection<FoodLog> _newFoodLog = new ObservableCollection<FoodLog>();
        ObservableCollection<ActivityLog> _newActivityLog = new ObservableCollection<ActivityLog>();
        int cal_con = 0, cal, cal_burn = 0;

        public W_log()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newLog.logDate = (DateTime)DtPckr_Logdate.SelectedDate;
            newLog.calGoal = Convert.ToInt32(Tbx_cal.Text);
            App._flogs = new ObservableCollection<FoodLog>();
            App._alogs = new ObservableCollection<ActivityLog>();
            CoBx_meal.ItemsSource = App._meals;
            Cobx_activity.ItemsSource = App._act;
        }


        //FOOD LOG
        private void Btn_AddItem_Click(object sender, RoutedEventArgs e)   //Add item in food Log
        {
            
            CoBx_meal.IsEnabled = true;        //to allow changes
            Tbx_item.IsEnabled = true;
            Tbx_qty.IsEnabled = true;
            FoodLog fl = new FoodLog { item = new Item { name = "edit"}, quantity = 0, meal = "Breakfast" };    //default values for food log()
            _newFoodLog.Add(fl);
            LVw_food.ItemsSource = _newFoodLog;
            LVw_food.ScrollIntoView(fl);
            LVw_food.SelectedItem = fl;
            
            App._flogs = _newFoodLog;
            //_newFoodLog.Clear();


        }

        private void Tbx_item_KeyDown(object sender, KeyEventArgs e)     //get calories from xml based on item entered
        {
            if (e.Key == Key.Return)
            {
                var foodItem = (sender as TextBox).Text.ToLower();
                Item items = (from i in App._items where i.name.ToLower().Contains(foodItem) select i).FirstOrDefault();
                if (items == null)
                    return;
                Tblk_calories.Text = items.calories.ToString();
            }
        }

        private void Tbx_qty_KeyDown(object sender, KeyEventArgs e)     //calories calc. according to the quantity
        {
            if (e.Key == Key.Return)
            {
                int qty = Convert.ToInt32((sender as TextBox).Text);
                cal = Convert.ToInt32(Tblk_calories.Text);
                Tblk_calories.Text = (cal * qty).ToString();

                Tblk_calc.Text = getCalConsumed().ToString();
                Tblk_calr.Text = getCalRemained().ToString();
            }
            
        }

        private int getCalConsumed()      //calculate calories consumed
        {
            
            cal_con += Convert.ToInt32(Tblk_calories.Text);
            newLog.calConsumed = cal_con;
            return cal_con;
        }

        private void Btn_DeleteItem_Click(object sender, RoutedEventArgs e)   // delete food log entry
        {
            var item = LVw_food.SelectedItem;

            if (item == null)
            {
                MessageBox.Show("Please select an item to be deleted from the log!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelete = item as FoodLog;  //object of type student
            var res = MessageBox.Show($"Are you sure you want to delete {toDelete.meal} {toDelete.item.name}?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res == MessageBoxResult.OK)
                App._flogs.Remove(toDelete);
        }


        //ACTIVITY LOG
        private void Btn_AddActivity_Click(object sender, RoutedEventArgs e)    //add new entry into activity log
        {
            Cobx_activity.IsEnabled = true;
            Tbx_speed.IsEnabled = true;
            ActivityLog al = new ActivityLog { activity = new Activity { actName = "Running", speed = 0} };
            _newActivityLog.Add(al);
            LVw_activity.ItemsSource = _newActivityLog;
            LVw_activity.ScrollIntoView(al);
            LVw_activity.SelectedItem = al;

            App._alogs = _newActivityLog;
            //_newActivityLog.Clear();
            

        }

        private void Cobx_activity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var activity = (sender as ComboBox).SelectedItem.ToString();
            foreach (var item in App._activities)
            {
                if (item.actName == activity)
                {
                    Tblk_activityCalories.Text = item.calories.ToString();
                    speed = item.speed;
                }
            }
        }

        private void Tbx_speed_KeyDown(object sender, KeyEventArgs e)      //calories corresponding to the speed
        {
            if (e.Key == Key.Return)
            {
                cal = Convert.ToInt32(Tblk_activityCalories.Text);
                sp = Convert.ToDouble(Tbx_speed.Text);
                Tblk_activityCalories.Text = Convert.ToInt32((cal * sp) / speed).ToString();

                Tblk_calb.Text = getCalBurned().ToString();
                Tblk_calr.Text = getCalRemained().ToString();
            }
            
        }

        private int getCalBurned()        //calculate calories burned
        {
            
            cal_burn += Convert.ToInt32(Tblk_activityCalories.Text);
            newLog.calBurned = cal_burn;
            return cal_burn;

        }

        

        private void Btn_DeleteActivity_Click(object sender, RoutedEventArgs e)   //delete an entry from activity log
        {
            var item = LVw_activity.SelectedItem;

            if (item == null)
            {
                MessageBox.Show("Please select an activity to be deleted from the log!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelete = item as ActivityLog;  //object of type student
            var res = MessageBox.Show($"Are you sure you want to delete {toDelete.activity.actName}?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res == MessageBoxResult.OK)
                App._alogs.Remove(toDelete);
        }

        private int getCalRemained()       //calculate calories remaining
        {
            newLog.calRemained = newLog.calGoal - newLog.calConsumed + newLog.calBurned;
            return newLog.calRemained;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(newLog != null)
            {
                newLog.foodLogs = App._flogs;
                newLog.activityLogs = App._alogs;
                App._logs.Add(newLog);
                App._alogs.Clear();
                App._flogs.Clear();
            }
            Owner.Visibility = Visibility.Visible;
        }





        
    }
}
