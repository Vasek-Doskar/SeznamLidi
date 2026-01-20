using Microsoft.Extensions.DependencyInjection;
using SeznamLidi.Interfaces;
using SeznamLidi.Windows;

namespace SeznamLidi.Managers
{
    public class CRUDWindowFactory : ICRUDWindowFactory
    {
        private readonly IServiceProvider _provider;

        public CRUDWindowFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public CreateNewPersonWindow CreateAddWindow()
        {
            return _provider.GetRequiredService<CreateNewPersonWindow>();
        }

 

        public UpdatePersonWindow CreateUpdateWindow(int id)
        {
            return ActivatorUtilities.CreateInstance<UpdatePersonWindow>(_provider, id);
        }
    }
}
