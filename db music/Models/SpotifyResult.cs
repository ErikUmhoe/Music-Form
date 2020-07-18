using Newtonsoft.Json;
using System.Collections.Generic;

public class ExternalUrls
{
    [JsonProperty("spotify")]
    public string spotify { get; set; }

}

public class Followers
{
    [JsonProperty("href")]
    public object href { get; set; }
    [JsonProperty("total")]
    public int total { get; set; }

}

public class Image
{
    [JsonProperty("height")]
    public int height { get; set; }
    [JsonProperty("url")]
    public string url { get; set; }
    [JsonProperty("width")]
    public int width { get; set; }

}

public class Item
{
    [JsonProperty("external_urls")]
    public ExternalUrls external_urls { get; set; }
    [JsonProperty("followers")]
    public Followers followers { get; set; }
    [JsonProperty("genres")]
    public List<string> genres { get; set; }
    [JsonProperty("href")]
    public string href { get; set; }
    [JsonProperty("id")]
    public string id { get; set; }
    [JsonProperty("images")]
    public List<Image> images { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("popularity")]
    public int popularity { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("uri")]
    public string uri { get; set; }

}

public class Artists
{
    [JsonProperty("href")]
    public string href { get; set; }
    [JsonProperty("items")]
    public List<Item> artistList { get; set; }
    [JsonProperty("limit")]
    public int limit { get; set; }
    [JsonProperty("next")]
    public string next { get; set; }
    [JsonProperty("offset")]
    public int offset { get; set; }
    [JsonProperty("previous")]
    public string previous { get; set; }
    [JsonProperty("total")]
    public int total { get; set; }

}

public class SpotifyResult
{
    [JsonProperty("artists")]
    public Artists artists { get; set; }

}