using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicLibrary.Models
{
    public class SongsViewModel
    {
        [StringLength(40)]
        [Required(ErrorMessage="Name is required.")]
        public string TrackName { get; set; }
        [Required]
        public int TrackNumber { get; set; }
        [StringLength(25)]
        public string ArtistName { get; set; }
        [StringLength(25)]
        public string AlbumName { get; set; }
        [StringLength(20)]
        public string GenreName { get; set; }
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        public int GenreID { get; set; }
        public int SongID { get; set; }

        public IEnumerable<SelectListItem> ArtistNames = new List<SelectListItem>();
        public song ToModel()
        {
            return new song
            {
                track_number = TrackNumber,
                album_id = AlbumID,
                artist_id = ArtistID,
                genre_id = GenreID,
                name = TrackName
            };
        }

        
    }
}