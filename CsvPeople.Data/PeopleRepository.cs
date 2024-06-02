using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvPeople.Data
{
    public class PeopleRepository
    {
        private string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            var ctx = new PersonDataContext(_connectionString);
            return ctx.People.ToList();
        }

        public void DeleteAll()
        {
            var ctx = new PersonDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM People");
        }

        public void AddPeople(List<Person> people)
        {
            var ctx = new PersonDataContext(_connectionString);
            ctx.People.AddRange(people);
            ctx.SaveChanges();
        }
    }
}
