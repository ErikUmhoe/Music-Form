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
    public class GenresController : Controller
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
            /*Main query to get genres is SELECT TOP(20) FROM Genres 
            This implements pagination to reduce the size of the query when we hit the database, only keeping the first
            20 results that return back from the query built by the following search criteria and order by.
            With pagination, when a user moves to the next page they will load the next twenty results from the query.
            */
            IQueryable<Genre> dbGenres = from a in db.Genres
                                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                //This is equivalent to SELECT TOP(20) * FROM Artists WHERE Artists.artist_name = searchString
                dbGenres = dbGenres.Where(x => x.genre_title.Contains(searchString));
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(dbGenres.OrderBy(s => s.genre_title).ToPagedList(pageNumber, pageSize));

        }
    }
}