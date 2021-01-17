using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_DietTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class W_aboutYou : Window
    {
        User user;
        public W_aboutYou()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user = new User();
            CoBx_gender.ItemsSource = App._genders;
            var act = new List<string> { "Inactive", "Slightly active", "Active", "Very active", "Extra active" };
            CoBx_activity.ItemsSource = act;
            
                
        }

        private void Btn_CalcResult_Click(object sender, RoutedEventArgs e)
        {
             
            int height, age, weight;
            float hm, mult;

            if (Tbox_wt.Text == "0" | Tbox_nam.Text == "" | DtPckr_dateDob.SelectedDate == null | CoBx_gender.SelectedItem == null | Tbox_hgt.Text == "0" | Tbox_cal.Text == "0" | CoBx_activity.SelectedItem == null)
            {
                MessageBox.Show("Please fill-in all the fields first!");
            }
            else
            {
                user.dob = (DateTime)DtPckr_dateDob.SelectedDate;     //age calculation
                age = DateTime.Now.Year - user.dob.Year;

                if ((user.dob.Month > DateTime.Now.Month) || (user.dob.Month == DateTime.Now.Month && user.dob.Day > DateTime.Now.Day))
                    age--;
                if (age < 18)
                {
                    MessageBox.Show("Sorry, this app is designed only for people over the age of 18! :(");
                }
                user.name = Tbox_nam.Text;    //name

                user.calIntake = Convert.ToInt32(Tbox_cal.Text);    // daily calories
                if (user.calIntake > 5000 | user.calIntake < 1000)
                    MessageBox.Show("It is not advisable to consume so much calories. Please enter a smaller amount!");

                user.gender = CoBx_gender.SelectedItem.ToString();
                
                 //Int32.TryParse(Tbox_wgt.Text, out xd);
                 weight = Convert.ToInt32(Tbox_wt.Text);
                 user.weight = weight;

                 //Int32.TryParse(Tbox_hgt.Text, out xs);
                 height = Convert.ToInt32(Tbox_hgt.Text);
                 user.height = height;

                 hm = (float)height / 100;
                 var bmi = weight / (hm * hm);
                 user.bmi = Math.Round(bmi, 2);   //BMI calculation


                 if (user.bmi > 18.5 && user.bmi < 25)   //IBW calculation
                     user.ibw = "Healthy";
                 else if (user.bmi < 18.5)
                     user.ibw = "Underweight";
                 else
                     user.ibw = "Overweight";

                 //BMR calculation

                 if (user.gender == "Male")
                 {
                     var bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                     user.bmr = Math.Round(bmr, 2);
                 }

                 else
                 {
                     var bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                     user.bmr = Math.Round(bmr, 2);
                 }
               

                user.activity = CoBx_activity.SelectedItem.ToString();   //Daily calorie reqt calculation
                
                switch (user.activity)
                {
                    case "Inactive":
                        mult = (float)1.2;
                        break;
                    case "Slightly active":
                        mult = (float)1.375;
                        break;
                    case "Active":
                        mult = (float)1.55;
                        break;
                    case "Very active":
                        mult = (float)1.725;
                        break;
                    default:
                    case "Extra active":
                        mult = (float)1.9;
                        break;
                }

                user.calReqt = Convert.ToInt32(user.bmr * mult);
                
                StPnl_Details.DataContext = user;   //Binding into UI
                App._user.Add(user);
               // Lbx_Result.ItemsSource = App._user;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            user.logs = App._logs;     //Put all logs within the user's observable collection
            
            Owner.Visibility = Visibility.Visible;
        }

    }
}