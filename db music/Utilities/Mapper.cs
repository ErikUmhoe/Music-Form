using db_music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_music.Utilities
{
    public class Mapper
    {
        private testEntities db = new testEntities();

        public static AlbumViewModel ToAlbumViewModel(db_music.Models.album album)
        {
            var vm = new AlbumViewModel
            {
                Title = album.album_title,
                Tracks = album.album_tracks,
                DateCreated = album.album_date_created,
                DateReleased = album.album_date_released,
                Type = album.album_type,
                Artist = album.artist_name,
                Producer = album.album_producer,
                NumComments = album.Comments.Count
            };
            var comments = new List<CommentViewModel>();
            
            foreach(var comment in album.Comments)
            {
                comments.Add(Mapper.ToCommentViewModel(comment));
            }
            vm.Comments = comments;
            vm.AvgRating = comments.Count() / (decimal)comments.Sum(x => x.Rating);
            return vm;
        }
        
        public static CommentViewModel ToCommentViewModel(db_music.Models.Comment comment)
        {
            return new CommentViewModel
            {
                Comment = comment.Comment1,
                AlbumId = comment.AlbumId,
                TrackId = comment.TrackId,
                ArtistId = comment.ArtistId,
                Id = comment.Id,
                Type = comment.Type,
                Rating = comment.Rating,
                CDate = comment.Cdate
            };
        }
        public static ArtistViewModel ToArtistviewModel(db_music.Models.artist artist)
        {
            var vm = new ArtistViewModel
            {
                Name = artist.artist_name,
                Id = artist.artist_id,
                Bio = artist.artist_bio,
                Wiki = artist.artist_wikipedia_page,
                Website = artist.artist_website,
                NumComments = artist.Comments.Count
            };
            var albums = new List<AlbumViewModel>();
            foreach(var album in artist.albums)
            {
                albums.Add(Mapper.ToAlbumViewModel(album));
            }
            var comments = new List<CommentViewModel>();
            foreach(var comment in artist.Comments)
            {
                comments.Add(Mapper.ToCommentViewModel(comment));
            }
            vm.Comments = comments;
            vm.Albums = albums;
            vm.NumAlbums = albums.Count;
            vm.AvgRating = comments.Count / (decimal)comments.Sum(x => x.Rating);
            return vm;
        }
    }
}