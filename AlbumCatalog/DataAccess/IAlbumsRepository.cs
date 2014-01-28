using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IAlbumsRepository : IRepository<Album>
    {
        new Album[] All();
        new Album GetByKey(int key);
        new void Add(Album album);
        new void Delete(Album album);
        int Count();
        Album[] Available();
        Album[] UnAvailable();
        void CheckOut(List<Album> albums);
        void CheckIn(List<Album> albums);
    }
}
