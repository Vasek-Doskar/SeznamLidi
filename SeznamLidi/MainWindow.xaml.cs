using SeznamLidi.Database;
using SeznamLidi.Interfaces;
using SeznamLidi.Managers;
using SeznamLidi.Models;
using SeznamLidi.Repositories;
using SeznamLidi.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace SeznamLidi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Person> Data { get; set; }
        public IPersonManager PersonManager {  get; set; }
        public MainWindow()
        {
            IPersonRepository personRepository = new PersonRepository(new PersonContext());
            PersonManager = new PersonManager(personRepository);
            Data = new ObservableCollection<Person>(PersonManager.GetAll());
            InitializeComponent();            
            LV.DataContext = Data;
        }


        void OnClick(object sender, RoutedEventArgs e) { }

        void AddClick(object sender, RoutedEventArgs e) 
        {
            CreateNewPersonWindow CNPW = new(PersonManager);
            CNPW.Closed += (s, e) => { Data.Add(CNPW.newPerson); };
            CNPW.ShowDialog();
        }
    }

    // TODO: CRUD, DI
}