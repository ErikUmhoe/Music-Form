using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class ArtistViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public string Bio { get; set; }
        public string Url { get; set; }
        public string Wiki { get; set; }
        public string Website { get; set; }
        public ArtistViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
    }
}