using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using db_music.Models;

namespace db_music.Controllers
{
    public class artistsController : Controller
    {
        private testEntities db = new testEntities();

        // GET: artists
        public ActionResult Index()
        {
            var index = new ArtistIndexViewModel();
            
            foreach(var artist in db.artists)
            {
                var vm = new ArtistViewModel
                {
                    Name = artist.artist_name,
                    Id = artist.artist_id,
                    Bio = artist.artist_bio,
                    Url = artist.artist_url,
                    Website = artist.artist_website,
                    Wiki = artist.artist_wikipedia_page
                };
                foreach(var comment in artist.Comments)
                {
                    vm.Comments.Add(
                        new CommentViewModel
                        {
                            Comment = comment.Comment1,
                            AlbumId = comment.AlbumId,
                            TrackId = comment.TrackId,
                            ArtistId = comment.ArtistId,
                            Id = comment.Id,
                            Type = comment.Type,
                            Rating = comment.Rating,
                            CDate = comment.Cdate
                        });
                }
                index.Artists.Add(vm);
            }
            
            return View(index);
        }

        // GET: artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artist artist = db.artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            var vm = new ArtistViewModel
            {
                Name = artist.artist_name,
                Id = artist.artist_id,
                Bio = artist.artist_bio,
                Url = artist.artist_url,
                Website = artist.artist_website,
                Wiki = artist.artist_wikipedia_page
            };
            foreach (var comment in artist.Comments)
            {
                vm.Comments.Add(
                    new CommentViewModel
                    {
                        Comment = comment.Comment1,
                        AlbumId = comment.AlbumId,
                        TrackId = comment.TrackId,
                        ArtistId = comment.ArtistId,
                        Id = comment.Id,
                        Type = comment.Type,
                        Rating = comment.Rating,
                        CDate = comment.Cdate
                    });
            }
            return View(vm);
        }

        // GET: artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artist_id,artist_active_year_begin,artist_active_year_end,artist_associated_labels,artist_bio,artist_comments,artist_contact,artist_date_created,artist_donation_url,artist_favorites,artist_flattr_name,artist_handle,artist_image_file,artist_images,artist_latitude,artist_location,artist_longitude,artist_members,artist_name,artist_paypal_name,artist_related_projects,artist_url,artist_website,artist_wikipedia_page,tags")] artist artist)
        {
            if (ModelState.IsValid)
            {
                db.artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artist artist = db.artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artist_id,artist_active_year_begin,artist_active_year_end,artist_associated_labels,artist_bio,artist_comments,artist_contact,artist_date_created,artist_donation_url,artist_favorites,artist_flattr_name,artist_handle,artist_image_file,artist_images,artist_latitude,artist_location,artist_longitude,artist_members,artist_name,artist_paypal_name,artist_related_projects,artist_url,artist_website,artist_wikipedia_page,tags")] artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artist artist = db.artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            artist artist = db.artists.Find(id);
            db.artists.Remove(artist);
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
