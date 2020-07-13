using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using db_music.Models;
using db_music.Utilities;

namespace db_music.Controllers
{
    public class CommentController : Controller
    {
        private testEntities db = new testEntities();

        // GET: Comments
        public ActionResult Index()
        {
            var index = new CommentViewModel();

            foreach (var comment in db.Comment)
            {
                var comment = new List<CommentViewModel>();
                foreach (var comment in comment)
                {
                    comment.Add(new CommentViewModel
                    {
                        Id = comment.comment_Id,
                        UserId = comment.user_Id,
                        AlbumId = comment.album_Id,
                        TrackId = comment.track_Id,
                        ArtistId = comment.artist_Id,
                        Comment = comment.comment,
                        Rating = comment.rating,
                        CDate = comment.date_created,
                       

                    });
                    var comments = new List<CommentViewModel>();
                    foreach (var comment in album.Comments)
                    {
                        comments.Add(Mapper.ToCommentViewModel(comment));
                    }
                }
                var vm = new CommentViewModel
                {
                    Id = comment.comment_Id,
                    UserId = comment.user_Id,
                    AlbumId = comment.album_Id,
                    TrackId = comment.track_Id,
                    ArtistId = comment.artist_Id,
                    Comment = comment.comment,
                    Rating = comment.rating,
                    CDate = comment.date_created,
                };
                foreach (var comment in artist.Comments)
                {
                    vm.Comments.Add(Mapper.ToCommentViewModel(comment));
                }
                index.Artists.Add(vm);
            }

            return View(index.Comment);
        }

        // GET: artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            var vm = new CommentViewModel
            {
                Id = comment.comment_Id,
                UserId = comment.user_Id,
                AlbumId = comment.album_Id,
                TrackId = comment.track_Id,
                ArtistId = comment.artist_Id,
                Comment = comment.comment,
                Rating = comment.rating,
                CDate = comment.date_created,
            };
            foreach (var comment in artist.Comments)
            {
                vm.Comments.Add(Mapper.ToCommentViewModel(comment));
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
        public ActionResult Create([Bind(Include = "Id, user_Id, Song_Id, Album_Id, Track_Id, Artist_Id, Comment, Rating, Type, Cdate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var vm = Mapper.ToCommentviewModel(comment);
            return View(vm);
        }

        // GET: artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(Comment);
        }

        // POST: artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, user_Id, Song_Id, Album_Id, Track_Id, Artist_Id, Comment, Rating, Type, Cdate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Artists.Remove(comment);
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
 
