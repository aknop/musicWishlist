using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MusicLibrary.Filter;

namespace MusicLibrary.Models
{
    [DuplicateAlbum]
    public class AlbumViewModel
    {
        [Required(ErrorMessage ="Album name is required!")]
        [StringLength(25)]
        public string AlbumName { get; set; }
        public int AlbumID { get; set; }
        [StringLength(25)]
        public string ArtistName { get; set; }
        public int ArtistID { get; set; }
        public string GenreName { get; set; }
        public int GenreID { get; set; }
        //Album Index songs
        public IEnumerable<SongsViewModel> SongList { get; set; }
        //drop downs used when adding an album
        public IEnumerable<SelectListItem> ArtistNames = new List<SelectListItem>();
        public IEnumerable<SelectListItem> GenreNames = new List<SelectListItem>();
        public bool importedArtist { get; set; }

        public void ToModel(album a)
        {
            ArtistID = a.artist_id;
            AlbumName = a.albumName;
            AlbumID = a.id;
            GenreID = a.genre_id;
        }

        public album FromModel()
        {
            return new album
            {
                artist_id = ArtistID,
                id = AlbumID,
                albumName = AlbumName,
                genre_id = GenreID,
            };
        }
    }
}