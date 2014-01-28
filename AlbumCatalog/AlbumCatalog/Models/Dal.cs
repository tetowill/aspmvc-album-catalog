using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace AlbumCatalog.Models
{
    public static class Dal
    {
        public static IAlbumsRepository Albums { get; set; }
        public static EfUsersRepository Users { get; set; }

        static Dal()
        {
            Albums = new EfAlbumsRepository();
            Users = new EfUsersRepository();
        }
    }
}