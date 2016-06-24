using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicLibrary.Models
{
    public class AlbumViewModel
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int AlbumID { get; set; }
        public int? ArtistID { get; set; }
        public IEnumerable<SongsViewModel> SongList { get; set; }
        public IEnumerable<SelectListItem> ArtistNames = new List<SelectListItem>();
        

        public void ToModel(album a)
        {
            ArtistID = a.artist_id;
            AlbumName = a.name;
            AlbumID = a.id;
        }

        public album FromModel()
        {
            return new album
            {
                artist_id = ArtistID,
                id = AlbumID,
                name = AlbumName
            };
        }
    }
}