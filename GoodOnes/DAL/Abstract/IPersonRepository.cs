using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Entities;

namespace GoodOnes.DAL.Abstract
{
    public interface IPersonRepository
    {
        T Get<T>(string email) where T : Person;

        T Get<T>(string email, Person.INCLUDES includes) where T : Person;

        void Insert(Person person);

        void SaveChanges();
    }
}