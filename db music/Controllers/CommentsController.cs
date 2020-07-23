using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using db_music.Models;
using PagedList;

namespace db_music.Controllers
{
    public class CommentsController : BaseController
    {
        private testEntities db = new testEntities();

        // GET: Comments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParam = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.RatingSortParam = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewBag.UsernameSortParam = sortOrder == "Username" ? "username_desc" : "Username";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IQueryable<Comment> dbComments = from c in db.Comments
                                           select c;
            if (!String.IsNullOrEmpty(searchString))
            {
        
                dbComments = dbComments.Where(x => x.User.Username.Contains(searchString));
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //This logic is equivalent to adding an ORDER BY clause to the query
            switch (sortOrder)
            {
                //ORDER BY username DESC
                case "username_desc":
                    return View(dbComments.OrderByDescending(s => s.User.Username).ToPagedList(pageNumber, pageSize));
                case "Username":
                    return View(dbComments.OrderBy(s => s.User.Username).ToPagedList(pageNumber, pageSize));
                    //ORDER BY type
                case "Type":
                    return View(dbComments.OrderBy(s => s.Type).ToPagedList(pageNumber, pageSize));
                //ORDER BY type DESC
                case "type_desc":
                    return View(dbComments.OrderByDescending(s => s.Type).ToPagedList(pageNumber, pageSize));
                //Order by cdate DESC
                case "date_desc":
                    return View(dbComments.OrderByDescending(s => s.Cdate).ToPagedList(pageNumber, pageSize));
                //order by rating desc
                case "rating_desc":
                    return View(dbComments.OrderByDescending(s => s.Rating).ToPagedList(pageNumber, pageSize));
                //Order by rating
                case "Rating":
                    return View(dbComments.OrderBy(s => s.Rating).ToPagedList(pageNumber, pageSize));
                    //ORDER BY cdate
                default:
                    return View(dbComments.OrderBy(s => s.Cdate).ToPagedList(pageNumber, pageSize));
            }
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
            ViewBag.RequestUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,AlbumId,TrackId,ArtistId,Text,Rating,Type,Cdate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                switch(comment.Type)
                {
                    case "Artist":
                        return RedirectToAction("Details", "Artists", new { id = comment.ArtistId });
                    case "Track":
                        return RedirectToAction("Details", "Tracks", new { id = comment.TrackId });
                    case "Album":
                        return RedirectToAction("Details", "Albums", new { id = comment.AlbumId });
                }
            }
            /*
             *Given the passed in attributes from comment, this will execute the follow SQL Code to update a comment:
             * USE [music]
                GO

                UPDATE [dbo].[Comments]
                   SET [UserId] = UserId
                      ,[AlbumId] = AlbumId
                      ,[TrackId] = TrackId
                      ,[ArtistId] =  ArtistId
                      ,[Text] =  Text
                      ,[Rating] = Rating
                      ,[Type] = Type
                      ,[Cdate] =  Cdate
                 WHERE Id = Id
                GO
             */
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
            ViewBag.RequestUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
             This executes the following SQL Command:
             USE [music]
            GO

            DELETE FROM [dbo].[Comments]
                    WHERE Id = Id
            GO
             */
            Comment comment = db.Comments.Find(id);
            var type = $"{comment.Type}s";
            int? rId = null;

            switch (type)
            {
                case "Artists":
                    rId = comment.ArtistId;
                    break;
                case "Tracks":
                    rId = comment.TrackId;
                    break;
                case "Albums":
                    rId = comment.AlbumId;
                    break;
            }
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", type, new { id = rId });
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
