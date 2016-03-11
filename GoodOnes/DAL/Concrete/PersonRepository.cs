using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GoodOnes.DAL.Abstract;
using GoodOnes.Entities;

namespace GoodOnes.DAL.Concrete
{
    public class PersonRepository : IPersonRepository
    {
        private GOContext db;

        public PersonRepository(GOContext dbcontext)
        {
            db = dbcontext;
        }

        public T Get<T>(string email) where T : Person
        {
            return Get<T>(email, Person.INCLUDES.NONE);
        }

        public T Get<T>(string email, Person.INCLUDES includes) where T : Person
        {
            IQueryable<T> q = db.People.OfType<T>();

            if (includes.HasFlag(Person.INCLUDES.ANSWERS))
                q = q.Include(p => p.Answers);

            return q.FirstOrDefault(p => p.Email.Equals(email));
        }

        public void Insert(Person person)
        {
            db.People.Add(person);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}