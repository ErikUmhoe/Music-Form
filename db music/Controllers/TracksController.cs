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
    public class TracksController : Controller
    {
        private testEntities db = new testEntities();

        [HttpPost]
        public ActionResult Index(string genre_title)
        {
            if (!String.IsNullOrEmpty(genre_title))
            {
                return RedirectToAction("Index", new { genre_title = genre_title });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Tracks
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string genre_title)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FavoritesSortParam = sortOrder == "Favorites" ? "favorites_desc" : "Favorites";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (genre_title == null)
            {
                genre_title = ViewBag.genre;
            }
            /*Main query to gett artists is SELECT TOP(20) FROM Artists 
            This implements pagination to reduce the size of the query when we hit the database, only keeping the first
            20 results that return back from the query built by the following search criteria and order by.
            With pagination, when a user moves to the next page they will load the next twenty results from the query.
            */
            IQueryable<Track> dbTracks = from a in db.Tracks
                                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                //This is equivalent to SELECT TOP(20) * FROM Tracks WHERE Tracks.track_title = searchString
                dbTracks = dbTracks.Where(x => x.track_title.Contains(searchString));
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            if (!String.IsNullOrEmpty(genre_title))
            {
                Genre genre = db.Genres.Where(x => x.genre_title == genre_title).First();
                //dbArtists = dbArtists.Where(x => x.TrackGenres.FirstOrDefault().GenreId == genre.genre_id);
                var tracks = genre.TrackGenres.Where(x => x.GenreId == genre.genre_id).Select(x => x.Track);
                ViewBag.genre = genre_title;
                if (!String.IsNullOrEmpty(searchString))
                {
                    tracks = tracks.Where(x => x.track_title.Contains(searchString));
                }
                  
                return View(tracks.ToPagedList(pageNumber, pageSize));
            }


            //This logic is equivalent to adding an ORDER BY clause to the query
            switch (sortOrder)
            {
                //ORDER BY artist_name DESC
                case "name_desc":
                    return View(dbTracks.OrderByDescending(s => s.track_title).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites
                case "Favorites":
                    return View(dbTracks.OrderBy(s => s.track_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites DESC
                case "favorites_desc":
                    return View(dbTracks.OrderByDescending(s => s.track_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY artist_name
                default:
                    return View(dbTracks.OrderBy(s => s.track_title).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            var album = track.Albums;
            var artist = track.Artists.First();
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "track_id,album_id,artist_id,license_title,track_bit_rate,track_comments,track_composer,track_date_created,track_date_recorded,track_disc_number,track_duration,track_explicit,track_explicit_notes,track_favorites,track_information,track_instrumental,track_interest,track_language_code,track_listens,track_lyricist,track_number,track_publisher,track_title,track_url")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Tracks.Add(track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(track);
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "track_id,album_id,artist_id,license_title,track_bit_rate,track_comments,track_composer,track_date_created,track_date_recorded,track_disc_number,track_duration,track_explicit,track_explicit_notes,track_favorites,track_information,track_instrumental,track_interest,track_language_code,track_listens,track_lyricist,track_number,track_publisher,track_title,track_url")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(track);
        }

        // GET: Tracks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Track track = db.Tracks.Find(id);
            db.Tracks.Remove(track);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddComment(string CommentText, string CommentRating, string Username, int track_id)
        {
            try
            {
                var newComment = db.Comments.Create();
                newComment.Text = CommentText;
                newComment.Rating = Int32.Parse(CommentRating);
                newComment.UserId = db.Users.Where(x => x.Username == Username).SingleOrDefault().Id;
                newComment.TrackId = track_id;
                newComment.Type = "Track";
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