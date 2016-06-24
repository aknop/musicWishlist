using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Models
{
    public class AlbumViewModel
    {
        [Required]
        [StringLength(25)]
        public string AlbumName { get; set; }
        [StringLength(25)]
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