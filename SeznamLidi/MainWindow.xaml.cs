using SeznamLidi.Database;
using SeznamLidi.Interfaces;
using SeznamLidi.Managers;
using SeznamLidi.Models;
using SeznamLidi.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace SeznamLidi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IPersonManager PersonManager {  get; set; }
        public MainWindow()
        {
            IPersonRepository personRepository = new PersonRepository(new PersonContext());
            PersonManager = new PersonManager(personRepository);
            InitializeComponent();
            LV.DataContext = new ObservableCollection<Person>(PersonManager.GetAll());
        }


        void OnClick(object sender, RoutedEventArgs e) { }
    }

    // TODO: CRUD, DI
}