using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MediaContextInitializer : DropCreateDatabaseAlways<MediaDbContext> // Use DropCreateDatabaseAlways to force update seed, then change back to DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(MediaDbContext context)
        {
            var Albums = new List<Album>
            {
                new Album() { 
                    Title = "Wild Flowers", 
                    Artist = "Tom Petty",
                    ReleaseDate = "11/01/1994",
                    AlbumId = 1,
                    PictureFileName = "WildFlowersCover.jpg",
                    Rating = 8,
                    IsCheckedOut = false
                },
                new Album() { 
                    Title = "Dysfunction",
                    Artist = "Staind",
                    ReleaseDate = "04/13/1999",
                    AlbumId = 2,
                    PictureFileName = "DysfunctionCover.jpg",
                    Rating = 9,
                    IsCheckedOut = false
                },
                new Album() { 
                    Title = "The Fake Sound of Progress",
                    Artist = "Lostprophets",
                    ReleaseDate = "12/04/2001",
                    AlbumId = 3,
                    PictureFileName = "TheFakeSoundOfProgressCover.jpg",
                    Rating = 7,
                    IsCheckedOut = false
                }
            };

            foreach (var album in Albums)
                context.Albums.Add(album);

            context.SaveChanges();

            //var Users = new List<Person>
            //{
            //    new Person() {
            //        FullName = "Will Hutchinson",
            //        Email = "willhutch81@gmail.com",
            //        PhoneNumber = "619-123-4567",
            //        UserID = 1
            //    }
            //};

            //foreach (var person in Users)
            //    context.Users.Add(person);
            //context.SaveChanges();
        }
    }
}
