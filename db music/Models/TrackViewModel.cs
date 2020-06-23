using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class TrackViewModel
    {
        public List<CommentViewModel> Comments { get; set; }
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public string Tags { get; set; }
        public int NumComments { get; set; }
        public decimal AvgRating { get; set; }
        public DateTime? Cdate { get; set; }
        public DateTime? RecordDate { get; set; }
        public DateTime? Duration { get; set; }
        public string ExplicitRating { get; set; }
        public int Favorites { get; set; }
        public List<GenreViewModel> Genres { get; set; }
        public string Info { get; set; }
        public string LanguageCode { get; set; }
        public int? Listens { get; set; }
        public string TrackTitle { get; set; }
        public string TrackNumber { get; set; }
        public string TrackUrl { get; set; }
        public string Composer { get; set; }
        public TrackViewModel()
        {
            Genres = new List<GenreViewModel>();
            Comments = new List<CommentViewModel>();
        }
    }
}