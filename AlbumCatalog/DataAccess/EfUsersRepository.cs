using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EfUsersRepository : IRepository<Person>
    {
        public EfUsersRepository()
        {

        }

        public Person[] All()
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Users.ToArray();
            }
        }

        public Person GetByKey(int key)
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Users.FirstOrDefault(Person => Person.UserID == key);
            }
        }

        public void Add(Person person)
        {
            using (var ctx = new MediaDbContext())
            {
                ctx.Users.Add(person);
                ctx.SaveChanges();
            }
        }

        public void Delete(Person person)
        {
            using (var ctx = new MediaDbContext())
            {
                ctx.Users.Remove(person);
                ctx.SaveChanges();
            }
        }

        public int Count()
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Users.Count();
            }
        }
    }
}
