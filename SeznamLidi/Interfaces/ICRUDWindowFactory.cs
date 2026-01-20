using SeznamLidi.Windows;

namespace SeznamLidi.Interfaces
{
    public interface ICRUDWindowFactory
    {
        
        public CreateNewPersonWindow CreateAddWindow();
        public UpdatePersonWindow CreateUpdateWindow(int id);
    }
}
