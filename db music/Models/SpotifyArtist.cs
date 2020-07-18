using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class SpotifyArtist
    {
        [JsonProperty("name")]
        public string ArtistName { get; set; }
        [JsonProperty("id")]
        public string ArtistId { get; set; }
        [JsonProperty("spotify")]
        public string SpotifyUrl { get; set; }
    }
}