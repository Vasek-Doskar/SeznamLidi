using Microsoft.Extensions.DependencyInjection;
using SeznamLidi.Interfaces;
using SeznamLidi.Models;
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
        private readonly IServiceProvider _provider;
        private readonly ICRUDWindowFactory _factory;
        public ObservableCollection<Person> Data { get; set; }

        public IPersonManager PersonManager { get; set; }
        public MainWindow(IServiceProvider provider, ICRUDWindowFactory factory)
        {
            _provider = provider;
            _factory = factory;

            var scope = _provider.CreateScope();
            PersonManager = scope.ServiceProvider.GetRequiredService<IPersonManager>();


            Data = new ObservableCollection<Person>(PersonManager.GetAll());
            InitializeComponent();
            LV.DataContext = Data;
        }


        void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            Person? person = LV.SelectedItem as Person;
            if (person != null)
            {
                UpdatePersonWindow updatePersonWindow = _factory.CreateUpdateWindow(person.Id);
                if (updatePersonWindow.ShowDialog() == true)
                {
                    int index = Data.IndexOf(person);
                    Data[index] = PersonManager.GetById(person.Id);
                }
            }
        }

        void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            Person? selected = LV.SelectedItem as Person;
            if (selected != null)
            {
                PersonManager.Delete(selected.Id);
                Data.Remove(selected);
            }
        }

        void AddClick(object sender, RoutedEventArgs e)
        {
            using var scope = _provider.CreateScope();
            CreateNewPersonWindow CNPW = scope.ServiceProvider.GetRequiredService<CreateNewPersonWindow>();
            CNPW.Owner = this;

            if (CNPW.ShowDialog() == true)
            {
                Data.Add(CNPW.newPerson);              
            }
        }
    }

    // TODO: CRUD, DI
}