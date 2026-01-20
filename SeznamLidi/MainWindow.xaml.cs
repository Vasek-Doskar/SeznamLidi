using Microsoft.Extensions.DependencyInjection;
using SeznamLidi.Database;
using SeznamLidi.Interfaces;
using SeznamLidi.Managers;
using SeznamLidi.Models;
using SeznamLidi.Repositories;
using SeznamLidi.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation.Peers;

namespace SeznamLidi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _provider;
        public ObservableCollection<Person> Data { get; set; }

        public IPersonManager PersonManager { get; set; }
        public MainWindow(IServiceProvider provider)
        {
            _provider = provider;

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

                UpdatePersonWindow updatePersonWindow = _provider.GetRequiredService<UpdatePersonWindow>();
                updatePersonWindow.Person = person;

                updatePersonWindow.Closed += (s, e) =>
                {
                    Data = new ObservableCollection<Person>(PersonManager.GetAll());
                    LV.DataContext = Data;
                };

                updatePersonWindow.Owner = this;

                updatePersonWindow.Show();
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
            //CreateNewPersonWindow CNPW = new(PersonManager);
            using var scope = _provider.CreateScope();
            CreateNewPersonWindow CNPW = scope.ServiceProvider.GetRequiredService<CreateNewPersonWindow>();
            CNPW.Owner = this;
            CNPW.Closed += (s, e) => { Data.Add(CNPW.newPerson); };
            CNPW.ShowDialog();
        }
    }

    // TODO: CRUD, DI
}