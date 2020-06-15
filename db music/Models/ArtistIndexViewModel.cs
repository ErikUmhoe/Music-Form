using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class ArtistIndexViewModel
    {
        public List<ArtistViewModel> Artists { get; set; }
        public ArtistIndexViewModel()
        {
            Artists = new List<ArtistViewModel>();
        }
    }
}