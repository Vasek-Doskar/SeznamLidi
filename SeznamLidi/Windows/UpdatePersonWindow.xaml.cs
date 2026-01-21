using Microsoft.Extensions.DependencyInjection;
using SeznamLidi.Interfaces;
using SeznamLidi.Models;
using System.Windows;

namespace SeznamLidi.Windows
{
    /// <summary>
    /// Interaction logic for UpdatePersonWindow.xaml
    /// </summary>
    public partial class UpdatePersonWindow : Window, IDisposable
    {
        private readonly IPersonManager _manager;
        private readonly IServiceScope _scope;
        
        public Person Person{get;set;}
        private readonly IServiceProvider _provider;
        public UpdatePersonWindow(IServiceProvider provider, int id)
        {
            _provider = provider;
            _scope = _provider.CreateScope();
            _manager = _scope.ServiceProvider.GetRequiredService<IPersonManager>();
            Person = _manager.GetById(id);
            DataContext = Person;
            InitializeComponent();
            
        }


 
        void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            if (Person != null)
            {
                _manager.Update(Person);
                DialogResult = true;
                Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Dispose();
        }

        public void Dispose()
        {
            _scope?.Dispose(); // uvolnění scope a všech jeho služeb
        }
    }
}
