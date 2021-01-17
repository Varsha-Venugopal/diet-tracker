using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wpf_DietTracking
{
    public class Log : INotifyPropertyChanged
    {
        public int calConsumed_;
        public int calConsumed 
        {
            get { return calConsumed_; }
            set
            {
                calConsumed_ = value;
                OnPropertyChanged("calConsumed");
            }
        }
        public int calBurned_;
        public int calBurned 
        {
            get { return calBurned_; }
            set
            {
                calBurned_ = value;
                OnPropertyChanged("calBurned");
            }
        }
        public int calRemained_;
        public int calRemained 
        {
            get { return calRemained_; }
            set
            {
                calRemained_ = value;
                OnPropertyChanged("calRemained");
            }
        }

        public int calGoal_;
        public int calGoal
        {
            get { return calGoal_; }
            set
            {
                calGoal_ = value;
                OnPropertyChanged("calGoal");
            }
        }

        public DateTime logDate_;
        public DateTime logDate
        {
            get { return logDate_; }
            set
            {
                logDate_ = value;
                OnPropertyChanged("logDate");
            }
        }
        
        public ObservableCollection<FoodLog> foodLogs_;
        public ObservableCollection<FoodLog> foodLogs
        {
            get { return foodLogs_; }
            set
            {
                foodLogs_ = value;
                OnPropertyChanged("foodLogs");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<ActivityLog> activityLogs_;
        public ObservableCollection<ActivityLog> activityLogs
        {
            get { return activityLogs_; }
            set
            {
                activityLogs_ = value;
                OnPropertyChanged("activityLogs");
            }
        }

        public Log()
        {
            foodLogs = new ObservableCollection<FoodLog>();
            activityLogs = new ObservableCollection<ActivityLog>();
        }
    }

}