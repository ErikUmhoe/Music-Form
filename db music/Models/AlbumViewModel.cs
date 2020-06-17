using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class AlbumViewModel
    {
        public string Title { get; set; }
        public int Tracks { get; set; }
        public int Id { get; set; } 
        public DateTime? DateCreated { get; set; }
        public DateTime? DateReleased { get; set; }
        public string Type { get; set; }
        public string Artist { get; set; }
        public string Producer { get; set; }
        public int NumComments { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public decimal AvgRating { get; set; }
        public string Genres { get; set; }
    }
}