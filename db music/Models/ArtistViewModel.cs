using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class ArtistViewModel
    {
        [DisplayName("Artist Name")]
        public string Name { get; set; }
        public int Id { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        [DisplayName("Number of comments")]
        public int NumComments { get; set; }
        [DisplayName("Artist Bio")]
        public string Bio { get; set; }
        [DisplayName("Artist URL")]
        public string ArtistUrl { get; set; }
        [DisplayName("Artist Wiki")]
        public string Wiki { get; set; }
        [DisplayName("Artist Website")]
        public string Website { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
        [DisplayName("Number of Albums")]
        public int NumAlbums { get; set; }
        [DisplayName("Average Rating")]
        public decimal AvgRating { get; set; }
        [DisplayName("Favorites")]
        public int Favorites { get; set; }
        public ArtistViewModel()
        {
            Comments = new List<CommentViewModel>();
            Albums = new List<AlbumViewModel>();
        }
    }
}