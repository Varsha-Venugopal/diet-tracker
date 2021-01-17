using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wpf_DietTracking
{
    public class User : INotifyPropertyChanged
    {
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public int weight_;
        public int weight 
        {
            get { return weight_; }
            set
            {
                weight_ = value;
                OnPropertyChanged("weight");
            }
        }
        public int height_;
        public int height
        {
            get { return height_; }
            set
            {
                height_ = value;
                OnPropertyChanged("height");
            }
        }
        public string activity_;
        public string activity
        {
            get { return activity_; }
            set
            {
                activity_ = value;
                OnPropertyChanged("activity");
            }
        }
        public int calIntake_;
        public int calIntake
        {
            get { return calIntake_; }
            set
            {
                calIntake_ = value;
                OnPropertyChanged("calIntake");
            }
        }

        public double bmi_;
        public double bmi
        {
            get { return bmi_; }
            set
            {
                bmi_ = value;
                OnPropertyChanged("bmi");
            }
        }

        public string ibw_;
        public string ibw
        {
            get { return ibw_; }
            set
            {
                ibw_ = value;
                OnPropertyChanged("ibw");
            }
        }
        public double bmr_;
        public double bmr
        {
            get { return bmr_; }
            set
            {
                bmr_ = value;
                OnPropertyChanged("bmr");
            }
        }

        public int calReqt_;
        public int calReqt
        {
            get { return calReqt_; }
            set
            {
                calReqt_ = value;
                OnPropertyChanged("calReqt");
            }
        }       

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<Log> logs_;
        public ObservableCollection<Log> logs 
        {
            get { return logs_; }
            set
            {
                logs_ = value;
                OnPropertyChanged("logs");
            }
        }
    }
}