using SeznamLidi.Interfaces;
using SeznamLidi.Models;
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
using System.Windows.Shapes;

namespace SeznamLidi.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewPersonWindow.xaml
    /// </summary>
    public partial class CreateNewPersonWindow : Window
    {
        IPersonManager _manager;
        public Person newPerson { get; private set; }
        public CreateNewPersonWindow(IPersonManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FN.Text;
                string lastName = LN.Text;
                int age = int.Parse(Age.Text);
                newPerson = new Person { Age = age ,FirstName=firstName,LastName=lastName};
                _manager.Add(newPerson);
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
