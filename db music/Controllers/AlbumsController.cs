using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using db_music.Models;
using PagedList;

namespace db_music.Controllers
{
    public class AlbumsController : Controller
    {
        private testEntities db = new testEntities();

        // GET: Tracks
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FavoritesSortParam = String.IsNullOrEmpty(sortOrder) ? "favorites_desc" : "";
            ViewBag.RatingSortParam = String.IsNullOrEmpty(sortOrder) ? "rating_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            /*Main query to gett artists is SELECT TOP(20) FROM Artists 
            This implements pagination to reduce the size of the query when we hit the database, only keeping the first
            20 results that return back from the query built by the following search criteria and order by.
            With pagination, when a user moves to the next page they will load the next twenty results from the query.
            */
            IQueryable<Album> dbAlbums = from a in db.Albums
                                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                //This is equivalent to SELECT TOP(20) * FROM Artists WHERE Artists.artist_name = searchString
                dbAlbums = dbAlbums.Where(x => x.album_title.Contains(searchString));
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            //This logic is equivalent to adding an ORDER BY clause to the query
            switch (sortOrder)
            {
                //ORDER BY artist_name DESC
                case "name_desc":
                    return View(dbAlbums.OrderByDescending(s => s.album_title).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites
                case "Favorites":
                    return View(dbAlbums.OrderBy(s => s.album_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites DESC
                case "favorites_desc":
                    return View(dbAlbums.OrderByDescending(s => s.album_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY artist_name
                default:
                    return View(dbAlbums.OrderBy(s => s.album_title).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        public ActionResult AddComment(string CommentText, string CommentRating, string Username, int album_id)
        {
            try
            {
                var newComment = db.Comments.Create();
                newComment.Text = CommentText;
                newComment.Rating = Int32.Parse(CommentRating);
                newComment.UserId = db.Users.Where(x => x.Username == Username).SingleOrDefault().Id;
                newComment.AlbumId = album_id;
                newComment.Type = "Album";
                newComment.Cdate = DateTime.Now;
                db.Comments.Add(newComment);
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}