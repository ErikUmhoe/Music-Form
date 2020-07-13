using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using db_music.Models;
using Newtonsoft.Json;
using PagedList;

namespace db_music.Controllers
{
    public class ArtistsController : BaseController
    {
        private testEntities db = new testEntities();
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
            var dbArtists = from a in db.Artists
                            select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                dbArtists = dbArtists.Where(x => x.artist_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    dbArtists = dbArtists.OrderByDescending(s => s.artist_name);
                    break;
                case "Favorites":
                    dbArtists = dbArtists.OrderBy(s => s.artist_favorites);
                    break;
                case "favorites_desc":
                    dbArtists = dbArtists.OrderByDescending(s => s.artist_favorites);
                    break;
                default:  // Name ascending 
                    dbArtists = dbArtists.OrderBy(s => s.artist_name);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(dbArtists.ToPagedList(pageNumber, 5));
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }
        [HttpPost]
        public ActionResult AddComment(string CommentText, string CommentRating, string Username, int artist_id)
        {
            try
            {
                var newComment = db.Comments.Create();
                newComment.Text = CommentText;
                newComment.Rating = Int32.Parse(CommentRating);
                newComment.UserId = db.Users.Where(x => x.Username == Username).SingleOrDefault().Id;
                newComment.ArtistId = artist_id;
                newComment.Type = "Artist";
                newComment.Cdate = DateTime.Now;
                db.Comments.Add(newComment);
                db.SaveChanges();
            }
            catch(DbEntityValidationException dbEx)
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
          
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> ArtistRecommend(string artist_name)
        {
           
            HttpClient client = new HttpClient();
            var token = AccountController.GetAccessToken();
            client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", token);
            var searchString = "https://api.spotify.com/v1/search?q=" + artist_name + "&type=artist";
            HttpResponseMessage response = await client.GetAsync(searchString);
            response.EnsureSuccessStatusCode();
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObj = JsonConvert.DeserializeObject<SpotifyResult>(jsonString);
                if(!jsonObj.artists.artistList.Any(x => x.name.Equals(artist_name)))
                {
                   TempData["Msg"] = $"No artist with name {artist_name} found on Spotify";
                    return Redirect(Request.UrlReferrer.PathAndQuery);
                }
                var artist = jsonObj.artists.artistList.Where(x => x.name.Equals(artist_name)).First();
                System.Diagnostics.Debug.WriteLine("ArtistId: " + artist.id );
                System.Diagnostics.Debug.WriteLine("ArtistUrl: " + artist.external_urls.spotify.ToString());
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artist_id,artist_active_year_begin,artist_active_year_end,artist_bio,artist_comments,artist_contact,artist_date_created,artist_favorites,artist_handle,artist_location,artist_name,artist_url,artist_website,artist_wikipedia_page")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artist_id,artist_active_year_begin,artist_active_year_end,artist_bio,artist_comments,artist_contact,artist_date_created,artist_favorites,artist_handle,artist_location,artist_name,artist_url,artist_website,artist_wikipedia_page")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
            db.SaveChanges();
            return RedirectToAction("Index");
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
