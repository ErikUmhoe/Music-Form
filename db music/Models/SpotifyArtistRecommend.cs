using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Models
{
    public class SpotifyArtistRecommend
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class ExternalUrls
        {

            [JsonProperty("spotify")]
            public string Spotify;

        }

        public class Artist
        {

            [JsonProperty("external_urls")]
            public ExternalUrls ExternalUrls;

            [JsonProperty("href")]
            public string Href;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("uri")]
            public string Uri;

        }

        public class ExternalUrls2
        {

            [JsonProperty("spotify")]
            public string Spotify;

        }

        public class Image
        {

            [JsonProperty("height")]
            public int Height;

            [JsonProperty("url")]
            public string Url;

            [JsonProperty("width")]
            public int Width;

        }

        public class Album
        {

            [JsonProperty("album_type")]
            public string AlbumType;

            [JsonProperty("artists")]
            public List<Artist> Artists;

            [JsonProperty("external_urls")]
            public ExternalUrls2 ExternalUrls;

            [JsonProperty("href")]
            public string Href;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("images")]
            public List<Image> Images;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("release_date")]
            public string ReleaseDate;

            [JsonProperty("release_date_precision")]
            public string ReleaseDatePrecision;

            [JsonProperty("total_tracks")]
            public int TotalTracks;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("uri")]
            public string Uri;

        }

        public class ExternalUrls3
        {

            [JsonProperty("spotify")]
            public string Spotify;

        }

        public class Artist2
        {

            [JsonProperty("external_urls")]
            public ExternalUrls3 ExternalUrls;

            [JsonProperty("href")]
            public string Href;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("uri")]
            public string Uri;

        }

        public class ExternalIds
        {

            [JsonProperty("isrc")]
            public string Isrc;

        }

        public class ExternalUrls4
        {

            [JsonProperty("spotify")]
            public string Spotify;

        }

        public class ExternalUrls5
        {

            [JsonProperty("spotify")]
            public string Spotify;

        }

        public class LinkedFrom
        {

            [JsonProperty("external_urls")]
            public ExternalUrls5 ExternalUrls;

            [JsonProperty("href")]
            public string Href;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("uri")]
            public string Uri;

        }

        public class Track
        {

            [JsonProperty("album")]
            public Album Album;

            [JsonProperty("artists")]
            public List<Artist2> Artists;

            [JsonProperty("disc_number")]
            public int DiscNumber;

            [JsonProperty("duration_ms")]
            public int DurationMs;

            [JsonProperty("explicit")]
            public bool Explicit;

            [JsonProperty("external_ids")]
            public ExternalIds ExternalIds;

            [JsonProperty("external_urls")]
            public ExternalUrls4 ExternalUrls;

            [JsonProperty("href")]
            public string Href;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("is_local")]
            public bool IsLocal;

            [JsonProperty("is_playable")]
            public bool IsPlayable;

            [JsonProperty("linked_from")]
            public LinkedFrom LinkedFrom;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("popularity")]
            public int Popularity;

            [JsonProperty("preview_url")]
            public object PreviewUrl;

            [JsonProperty("track_number")]
            public int TrackNumber;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("uri")]
            public string Uri;

        }

        public class Seed
        {

            [JsonProperty("initialPoolSize")]
            public int InitialPoolSize;

            [JsonProperty("afterFilteringSize")]
            public int AfterFilteringSize;

            [JsonProperty("afterRelinkingSize")]
            public int AfterRelinkingSize;

            [JsonProperty("id")]
            public string Id;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("href")]
            public string Href;

        }

        public class Root
        {

            [JsonProperty("tracks")]
            public List<Track> Tracks;

            [JsonProperty("seeds")]
            public List<Seed> Seeds;

        }


    }
}