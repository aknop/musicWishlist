using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicLibrary.Models
{
    public class SongsViewModel
    {
        [StringLength(40)]
        [Required(ErrorMessage="Name is required.")]
        public string TrackName { get; set; }
        [Required]
        [Range(1,99,ErrorMessage ="Please enter a valid track number.")]
        public int TrackNumber { get; set; }
        [StringLength(25)]
        public string ArtistName { get; set; }
        [StringLength(25)]
        public string AlbumName { get; set; }
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        public int SongID { get; set; }
        public bool importedArtist { get; set; }

        //Drop downs when you create a song
        public IEnumerable<SelectListItem> ArtistNames = new List<SelectListItem>();
        public IEnumerable<SelectListItem> AlbumNames = new List<SelectListItem>();
        public song FromModel()
        {
            return new song
            {
                track_number = TrackNumber,
                album_id = AlbumID,
                artist_id = ArtistID,
                name = TrackName,
                id = SongID
            };
        }
        
        public void ToModel (song s)
        {

            TrackNumber = s.track_number;
            ArtistID = s.artist_id;
            AlbumID = s.album_id;
            TrackName = s.name;
            SongID = s.id;
            AlbumName = s.album.albumName;
            ArtistName = s.artist.artistName;
        }
        
    }
}