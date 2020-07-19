﻿using System;
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
            /*Main query to gett artists is SELECT TOP(20) FROM Artists 
            This implements pagination to reduce the size of the query when we hit the database, only keeping the first
            20 results that return back from the query built by the following search criteria and order by.
            With pagination, when a user moves to the next page they will load the next twenty results from the query.
            */
            IQueryable<Artist> dbArtists = from a in db.Artists
                            select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                //This is equivalent to SELECT TOP(20) * FROM Artists WHERE Artists.artist_name = searchString
                dbArtists = dbArtists.Where(x => x.artist_name.Contains(searchString));
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //This logic is equivalent to adding an ORDER BY clause to the query
            switch (sortOrder)
            {
                //ORDER BY artist_name DESC
                case "name_desc":
                    return View(dbArtists.OrderByDescending(s => s.artist_name).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites
                case "Favorites":
                    return View(dbArtists.OrderBy(s => s.artist_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY favorites DESC
                case "favorites_desc":
                    return View(dbArtists.OrderByDescending(s => s.artist_favorites).ToPagedList(pageNumber, pageSize));
                //ORDER BY artist_name
                default:  
                    return View(dbArtists.OrderBy(s => s.artist_name).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //This is equivalent to SELECT * FROM Artists WHERE artist_id = id
            Artist artist = db.Artists.Find(id);
            var tracks = artist.Tracks;
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
          
            return Redirect(Request.UrlReferrer.ToString());
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
                var artistSeed = artist.uri.Substring(artist.uri.LastIndexOf(':')+1);
                var token2 = AccountController.GetAccessToken();
                HttpClient client2 = new HttpClient();
                client2.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", token2);
                //client2.DefaultRequestHeaders
                //  .Accept
                //  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client2.DefaultRequestHeaders.Add("Content-Type", "application/json");

                searchString = "https://api.spotify.com/v1/recommendations?limit=10market=US&seed_artists=" + artistSeed;
                response = await client2.GetAsync(searchString);
                response.EnsureSuccessStatusCode();
                if(response != null)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                    SpotifyArtistRecommend.Root myDeserializedClass = JsonConvert.DeserializeObject<SpotifyArtistRecommend.Root>(jsonString);
                    List<SpotifyArtistRecommend.Track> recTracks = myDeserializedClass.Tracks;
                }
                return PartialView("Views/Shared/_RecommendedTrackTable.cshtml");
            }
            TempData["Msg"] = $"An error occurred getting recommendations.s";
            return Redirect(Request.UrlReferrer.PathAndQuery);
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
