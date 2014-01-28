using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T>
    {
        T[] All();
        T GetByKey(int key);
        void Add(T item);
        void Delete(T item);
    }
}
