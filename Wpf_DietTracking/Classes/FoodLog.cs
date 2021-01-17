using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_DietTracking
{
    public class FoodLog
    {
        public Item item { get; set; }
        public string meal { get; set; }

        public int quantity_;
        public int quantity
        {
            get { return quantity_; }
            set
            {
                quantity_ = value;
                OnPropertyChanged("quantity");
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
