using SeznamLidi.Models;

namespace SeznamLidi.Interfaces
{
    public interface IPersonManager
    {
        public Person GetById(int? id);
        public List<Person> GetAll();
        public void Add(Person person);
        public void Update(Person person);
        public void Delete(int? id);
    }
}
