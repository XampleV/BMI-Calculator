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
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FileLocation = @"C:\Users\alshoubaki_mohammad\AppData\data.json";
        public static string json;
        public static dynamic users;
        public ObservableCollection<Users> myList { get; set; }
        //List<Users> users = new List<Users>();

        public MainWindow()
        {
            InitializeComponent();
            CheckFile();
            myList = new ObservableCollection<Users>();
            mainDataGrid.ItemsSource = myList;
            LoadScores();
        }

        public void CheckFile()
        {
            if (!File.Exists(FileLocation))
            {
                string defaultJson = "{'database':[]}";
                Root desHasToMadeFirst = JsonConvert.DeserializeObject<Root>(defaultJson);
                string newJsonFileToBeWritten = JsonConvert.SerializeObject(desHasToMadeFirst, Formatting.Indented);
                using (StreamWriter file = File.CreateText(FileLocation))
                {
                    file.Write(newJsonFileToBeWritten);
                    file.Close();
                    
                }
            }
        }
        public void LoadScores()
        {
            using (StreamReader r = new StreamReader(FileLocation))
            {
                json = r.ReadToEnd();
                r.Close();

            }
            users = JsonConvert.DeserializeObject<Root>(json);
            foreach(var i in users.database)
            {
                myList.Insert(0, new Users
                {
                    lastName = i.lastName,
                    firstName = i.firstName,
                    phoneNum = i.phoneNum,
                    height = i.height,
                    weight = i.weight,
                    BMI = i.BMI,
                    status = i.status
                });
            }
        }
        public void AddUser(string lastname, string firstname, string number, string height, string weight, string bmi, string status)
        { 
            // how do i make it less ugly.
            string tempJson = "{'lastName':'" + lastname+"','firstName':'"+firstname+"','phoneNum':'"+number+"','height':'"+height+"','weight':'"+weight+"','bmi':'"+bmi+"','status':'"+status+"'}";
            Database response = JsonConvert.DeserializeObject<Database>(tempJson);
            users.database.Add(response);

            string jsonToBeWritten = JsonConvert.SerializeObject(users, Formatting.Indented);
            using (StreamWriter file = File.CreateText(FileLocation))
            {
                file.Write(jsonToBeWritten);
                file.Close();
            }
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
            AddUser(lastNameBox.Text,  firstNameBox.Text, phoneNumBox.Text, heightBox.Text, weightBox.Text, BMINum.ToString(), title);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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
    // TODO: I need to figure out a better way to do this json. 
    public class Database
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phoneNum { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string BMI { get; set; }
        public string status { get; set; }
    }

    public class Root
    {
        public List<Database> database { get; set; }
    }
}



