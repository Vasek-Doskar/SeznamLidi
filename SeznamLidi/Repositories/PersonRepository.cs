using Microsoft.EntityFrameworkCore;
using SeznamLidi.Database;
using SeznamLidi.Interfaces;
using SeznamLidi.Models;

namespace SeznamLidi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges(); // TOTO provede uložení změn do databáze
        }

        public void Delete(int? id)
        {
            if (id == null)
                return;
            Person? person = GetById(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
            else throw new InvalidOperationException("Person not found");
        }

        public List<Person> GetAll()
        {
            return _context.Persons.AsNoTracking().ToList();
        }

        public Person GetById(int? id)
        {
            return _context.Persons.Find(id) ?? throw new InvalidOperationException("Person not found");
        }

        public void Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
