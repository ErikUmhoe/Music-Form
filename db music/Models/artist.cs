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
    
    public partial class Artist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artist()
        {
            this.Albums = new HashSet<Album>();
            this.Comments = new HashSet<Comment>();
        }
    
        public int artist_id { get; set; }
        public Nullable<int> artist_active_year_begin { get; set; }
        public string artist_active_year_end { get; set; }
        public string artist_associated_labels { get; set; }
        public string artist_bio { get; set; }
        public string artist_comments { get; set; }
        public string artist_contact { get; set; }
        public Nullable<System.DateTime> artist_date_created { get; set; }
        public string artist_donation_url { get; set; }
        public string artist_favorites { get; set; }
        public string artist_flattr_name { get; set; }
        public string artist_handle { get; set; }
        public string artist_image_file { get; set; }
        public string artist_images { get; set; }
        public Nullable<double> artist_latitude { get; set; }
        public string artist_location { get; set; }
        public Nullable<double> artist_longitude { get; set; }
        public string artist_members { get; set; }
        public string artist_name { get; set; }
        public string artist_paypal_name { get; set; }
        public string artist_related_projects { get; set; }
        public string artist_url { get; set; }
        public string artist_website { get; set; }
        public string artist_wikipedia_page { get; set; }
        public string tags { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
