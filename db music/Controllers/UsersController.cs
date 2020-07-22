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
    public class UsersController : Controller
    {
        private testEntities db = new testEntities();

        // GET: Users
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Creation Date" ? "date_desc" : "Creation Date";
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IQueryable<User> dbUsers = from u in db.Users
                                       select u;
            if(!String.IsNullOrEmpty(searchString))
            {
                dbUsers = dbUsers.Where(x => x.Username.Contains(searchString));
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            //This logic is equivalent to adding an ORDER BY clause to the query
            switch (sortOrder)
            {
                //ORDER BY artist_name DESC
                case "name_desc":
                    return View(dbUsers.OrderByDescending(s => s.Username).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites
                case "Creation Date":
                    return View(dbUsers.OrderBy(s => s.Cdate).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites DESC
                case "date_desc":
                    return View(dbUsers.OrderByDescending(s => s.Cdate).ToPagedList(pageNumber, pageSize));
                //ORDER BY artist_name
                default:
                    return View(dbUsers.OrderBy(s => s.Username).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult ShowUserComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("ShowUserComments", user);
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
