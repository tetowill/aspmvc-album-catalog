using AlbumCatalog.Models;
using AlbumCatalog.Utilities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace AlbumCatalog.Controllers
{
    public class AlbumsController : Controller
    {
        //
        // GET: /Albums/

        public ActionResult Index()
        {
            Album[] albums = Dal.Albums.All();
            return View("Index", albums);
        }

        [Authorize]
        public ActionResult Create()
        {
            var album = new Album();
            return View("Create", album);
        }

        [Authorize]
        public ActionResult Available()
        {
            Album[] albums = Dal.Albums.Available();
            return View("Available", albums);
        }

        [Authorize]
        public ActionResult UnAvailable()
        {
            Album[] albums = Dal.Albums.UnAvailable();
            return View("UnAvailable", albums);
        }

        [HttpPost]
        public ActionResult CheckOut(string ids)
        {
            if (ids == null)
                return Json(new { msg = "Please select an album to check out." });

            var albumIds = JsonConvert.DeserializeObject<int[]>(ids);
            
            List<Album> albums = new List<Album>();

            foreach (int id in albumIds)
            {
                var album = Dal.Albums.GetByKey(id);
                albums.Add(album);
            }

            Dal.Albums.CheckOut(albums);

            return Json(new { msg = "Albums have been checked out." });
        }

        [HttpPost]
        public ActionResult CheckIn(string ids)
        {
            if (ids == null)
                return Json(new { msg = "Please select an album to check in." });

            var albumIds = JsonConvert.DeserializeObject<int[]>(ids);

            List<Album> albums = new List<Album>();

            foreach (int id in albumIds)
            {
                var album = Dal.Albums.GetByKey(id);
                albums.Add(album);
            }            

            Dal.Albums.CheckIn(albums);

            return Json(new { msg = "Albums have been checked in." });
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "thumbnail")]Album album, HttpPostedFileBase thumbnail)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", album);
            }

            var fileName = Path.GetFileName(thumbnail.FileName);
            var inputStream = thumbnail.InputStream;
            var physicalPath = Server.MapPath("~/UploadedFiles/" + fileName);

            return CreateAlbum(album, fileName, inputStream, physicalPath);
        }

        internal ActionResult CreateAlbum(Album album, string fileName, Stream inputStream, string physicalPath)
        {
            using (var newFile = new FileStream(physicalPath, FileMode.Create))
            {
                inputStream.CopyTo(newFile);
            }
            using (var file = new FileStream(physicalPath, FileMode.Open))
            {
                var memStream = Thumbnailer.Create(file, 192);
                album.Thumbnail = StreamUtils.ReadFully(memStream, 192 * 192);
            }
            album.PictureFileName = fileName;
            album.IsCheckedOut = false;
            album.AlbumId = Dal.Albums.Count();
            Dal.Albums.Add(album);

            return RedirectToAction("Index");
        }

        public FileResult Picture(int AlbumId)
        {
            Album album = Dal.Albums.GetByKey(AlbumId);
            string path = Url.Content("~/UploadedFiles/" + album.PictureFileName);
            return File(path, "image/jpeg");
        }

        public FileResult Thumbnail(int AlbumId)
        {
            Album album = Dal.Albums.GetByKey(AlbumId);

            if (album.Thumbnail != null)
            {
                return File(album.Thumbnail, "image/jpeg");
            }
            else
            {
                var physicalPath = Server.MapPath("~/UploadedFiles/" + album.PictureFileName);

                using (var file = new FileStream(physicalPath, FileMode.Open))
                {
                    var memStream = Thumbnailer.Create(file, 192);
                    album.Thumbnail = StreamUtils.ReadFully(memStream, 192 * 192);
                }

                return File(album.Thumbnail, "image/jpeg");
            }
        }
    }
}
