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
    public class CommentsController : Controller
    {
        private testEntities db = new testEntities();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Album).Include(c => c.Artist).Include(c => c.Track).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "album_id", "album_engineer");
            ViewBag.ArtistId = new SelectList(db.Artists, "artist_id", "artist_active_year_end");
            ViewBag.TrackId = new SelectList(db.Tracks, "track_id", "license_title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,SongId,AlbumId,TrackId,ArtistId,Text,Rating,Type,Cdate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "album_id", "album_engineer", comment.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "artist_id", "artist_active_year_end", comment.ArtistId);
            ViewBag.TrackId = new SelectList(db.Tracks, "track_id", "license_title", comment.TrackId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "album_id", "album_engineer", comment.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "artist_id", "artist_active_year_end", comment.ArtistId);
            ViewBag.TrackId = new SelectList(db.Tracks, "track_id", "license_title", comment.TrackId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,SongId,AlbumId,TrackId,ArtistId,Text,Rating,Type,Cdate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "album_id", "album_engineer", comment.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "artist_id", "artist_active_year_end", comment.ArtistId);
            ViewBag.TrackId = new SelectList(db.Tracks, "track_id", "license_title", comment.TrackId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
