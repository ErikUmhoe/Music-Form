//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace db_music.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> SongId { get; set; }
        public Nullable<int> AlbumId { get; set; }
        public Nullable<int> TrackId { get; set; }
        public Nullable<int> ArtistId { get; set; }
        public string Comment1 { get; set; }
        public string Rating { get; set; }
        public string Type { get; set; }
        public System.DateTime Cdate { get; set; }
    
        public virtual album album { get; set; }
        public virtual artist artist { get; set; }
        public virtual track track { get; set; }
        public virtual User User { get; set; }
    }
}