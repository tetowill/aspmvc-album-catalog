using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EfAlbumsRepository : IAlbumsRepository
    {
        public EfAlbumsRepository()
        {

        }

        public Album[] All()
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Albums.ToArray();
            }
        }

        public Album GetByKey(int key)
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Albums.FirstOrDefault(Album => Album.AlbumId == key);
            }
        }

        public void Add(Album album)
        {
            using (var ctx = new MediaDbContext())
            {
                ctx.Albums.Add(album);
                ctx.SaveChanges();
            }
        }

        public void Delete(Album album)
        {
            using (var ctx = new MediaDbContext())
            {
                ctx.Albums.Remove(album);
                ctx.SaveChanges();
            }
        }

        public int Count()
        {
            using (var ctx = new MediaDbContext())
            {
                return ctx.Albums.Count();
            }
        }

        public void CheckOut(List<Album> albums)
        {
            using (var ctx = new MediaDbContext())
            {
                foreach (Album album in albums)
                {
                    album.IsCheckedOut = true;
                    ctx.Entry(album).State = EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }

        public void CheckIn(List<Album> albums)
        {
            using (var ctx = new MediaDbContext())
            {
                foreach (Album album in albums)
                {
                    album.IsCheckedOut = false;
                    ctx.Entry(album).State = EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }

        public Album[] Available()
        {
            using (var ctx = new MediaDbContext())
            {
                Album[] albumsA = ctx.Albums.ToArray();

                List<Album> albumsL = new List<Album>();
                
                foreach (Album album in albumsA)
                    if (!album.IsCheckedOut)
                        albumsL.Add(album);

                Album[] albums = albumsL.ToArray();

                return albums;
            }
        }

        public Album[] UnAvailable()
        {
            using (var ctx = new MediaDbContext())
            {
                Album[] albumsA = ctx.Albums.ToArray();

                List<Album> albumsL = new List<Album>();

                foreach (Album album in albumsA)
                    if (album.IsCheckedOut)
                        albumsL.Add(album);

                Album[] albums = albumsL.ToArray();

                return albums;
            }
        }

    }
}
