using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeznamLidi.Database;
using SeznamLidi.Interfaces;
using SeznamLidi.Managers;
using SeznamLidi.Repositories;
using SeznamLidi.Windows;
using System.Windows;

namespace SeznamLidi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<PersonContext>(options =>
            options.UseSqlite("Data Source=seznamlidi.db"), ServiceLifetime.Scoped);
            // registrace služeb do DI
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICRUDWindowFactory, CRUDWindowFactory>();

            services.AddTransient<MainWindow>();
            services.AddTransient<CreateNewPersonWindow>();
            services.AddTransient<UpdatePersonWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            SeznamLidi.MainWindow main = _serviceProvider.GetService<MainWindow>();
            main.Show();
        }
    }

}
