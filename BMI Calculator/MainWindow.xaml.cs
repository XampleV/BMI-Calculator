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
using System.Collections.ObjectModel;

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Users> myList { get; set; }
        //List<Users> users = new List<Users>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            myList = new ObservableCollection<Users>();
            mainDataGrid.ItemsSource = myList;


            //testc();
        }
        public void testc()
        {
            this.DataContext = this;

            myList.Add(new Users { lastName = "Doe", firstName = "Doe", phoneNum = " 123456789", height = "74", weight = "240", BMI = "25", status = "test" });
            myList.Add(new Users { lastName = "Doeeee", firstName = "Doe", phoneNum = " 123456789", height = "74", weight = "240", BMI = "25", status = "test" });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double userHeight = Convert.ToDouble(heightBox.Text);
            double userWeight = Convert.ToDouble(weightBox.Text);

            double BMINum = (userWeight / userHeight / userHeight * 703);

            string message;
            string title;

            if (BMINum >= 1 && BMINum <= 18.5)
            {
                message = "You're Underweight.";
                title = "Underweight";
            }
            else if (BMINum >= 18.5 && BMINum <= 24.9)
            {
                message = "You're Normal; Healthy weight.";
                title = "Normal";

            }
            else if (BMINum >= 25.0 && BMINum <= 29.9)
            {
                message = "You're overweight.";
                title = "Overweight";
            }
            else if (BMINum >= 30)
            {
                message = "You're Obese.";
                title = "Obese";
            }
            else
            {
                message = "What...?";
                title = "Unknown";
            }
            BMINum = Convert.ToInt32(BMINum);
            xMessage.Text = message;
            xBmiResults.Text = BMINum.ToString();
            myList.Insert(0, new Users
            {
                lastName = lastNameBox.Text,
                firstName = firstNameBox.Text,
                phoneNum = phoneNumBox.Text,
                height = heightBox.Text,
                weight = weightBox.Text,
                BMI = BMINum.ToString(),
                status = title
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lastNameBox.Text = "";
            firstNameBox.Text = "";
            phoneNumBox.Text = "";
            heightBox.Text = "";
            weightBox.Text = "";
            xMessage.Text = "";
            xBmiResults.Text = "BMI Results";
        }
    }
    public class Users
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phoneNum { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string BMI { get; set; }
        public string status { get; set; }
    }

}



