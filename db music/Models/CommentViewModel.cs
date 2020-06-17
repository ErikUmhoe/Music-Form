using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? AlbumId { get; set; }
        public int? TrackId { get; set; }
        public int? ArtistId { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
        public int Rating { get; set; }
        public DateTime CDate { get; set; }
        public CommentViewModel()
        {

        }
    }
}