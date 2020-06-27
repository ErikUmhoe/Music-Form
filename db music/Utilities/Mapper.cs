using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using db_music.Models;

namespace db_music.Utilities
{
    public class Mapper
    {
        private testEntities db = new testEntities();

        public static AlbumViewModel ToAlbumViewModel(db_music.Models.Album album)
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
            if(comments.Count > 0)
            {
                vm.AvgRating = comments.Count() / (decimal)comments.Sum(x => x.Rating);
            }

            return vm;
        }

        public static TrackViewModel ToTrackViewModel(db_music.Models.Track track)
        {
            var vm = new TrackViewModel
            {
                Id = track.track_id,
                AlbumTitle = track.album_title,
                ArtistName = track.artist_name,
                Tags = string.Join(", ", track.tags.Split(',')),
                NumComments = track.Comments.Count,
                AvgRating = track.Comments.Sum(x => x.Rating) / (decimal)track.Comments.Count,
                Cdate = track.track_date_created,
                RecordDate = track.track_date_recorded,
                ExplicitRating = track.track_explicit,
                Favorites = Int32.Parse(track.track_favorites),
                Info = track.track_information,
                LanguageCode = track.track_language_code,
                Listens = track.track_listens,
                TrackTitle = track.track_title,
                TrackNumber = track.track_number,
                TrackUrl = track.track_url,
                Composer = track.track_composer
            };
            foreach(var comment in track.Comments)
            {
                vm.Comments.Add(Mapper.ToCommentViewModel(comment));
            };
            vm.Genres = Mapper.ToGenreViewModel(track.Genres.ToList());
            return vm;
        }
        public static List<GenreViewModel> ToGenreViewModel(List<Genre> genreList)
        {
            List<GenreViewModel> vms = new List<GenreViewModel>();
            foreach(var genre in genreList)
            {
                vms.Add(new GenreViewModel
                {
                    Id = genre.genre_id,
                    Title = genre.genre_title
                });
            }
            return vms;

            
    
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
        public static ArtistViewModel ToArtistviewModel(db_music.Models.Artist artist)
        {
            Int32.TryParse(artist.artist_favorites, out var faves);
            var vm = new ArtistViewModel
            {
                Name = artist.artist_name,
                Id = artist.artist_id,
                Bio = artist.artist_bio,
                Wiki = artist.artist_wikipedia_page,
                Website = artist.artist_website,
                NumComments = artist.Comments.Count,
                Favorites = faves
            };
            var albums = new List<AlbumViewModel>();
            foreach (var album in artist.Albums)
            {
                albums.Add(Mapper.ToAlbumViewModel(album));
            }
            var comments = new List<CommentViewModel>();
            foreach (var comment in artist.Comments)
            {
                comments.Add(Mapper.ToCommentViewModel(comment));
            }
            vm.Comments = comments;
            vm.Albums = albums;
            vm.NumAlbums = albums.Count;
            if (comments.Count > 0)
            {

                vm.AvgRating = comments.Count / (decimal)comments.Sum(x => x.Rating);
            }
            return vm;
        }
    }
}