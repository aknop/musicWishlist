using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicLibrary.Filter;
namespace MusicLibrary.Models
{
    public class ArtistViewModel
    {
        [StringLength(25)]
        [Required]
        [DuplicateArtist]
        public string ArtistName { get; set; }
        public int ArtistID { get; set; }
        public IEnumerable<SongsViewModel> SongList { get; set; }


        public void ToModel(artist a)
        {
            ArtistName = a.artistName;
            ArtistID = a.id;
        }

        public artist FromModel()
        {
            return new artist
            {
                artistName = ArtistName,
                id = ArtistID
            };
        }
    }
}