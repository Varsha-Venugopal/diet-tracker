using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_DietTracking
{
    public class Item
    {
        public string name { get; set; }

        public int calories_;
        public int calories
        {
            get { return calories_; }
            set
            {
                calories_ = value;
                OnPropertyChanged("calories");
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
