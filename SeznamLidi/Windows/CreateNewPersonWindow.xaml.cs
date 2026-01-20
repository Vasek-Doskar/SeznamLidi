using SeznamLidi.Interfaces;
using SeznamLidi.Models;
using System.Windows;

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
                DialogResult = true;
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
