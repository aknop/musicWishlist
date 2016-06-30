using System.Linq;
using System.ComponentModel.DataAnnotations;
using MusicLibrary.Models;

namespace MusicLibrary.Filter
{
    public class DuplicateAlbumAttribute : ValidationAttribute
    {
        private TrueEntities db = new TrueEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var album = value as AlbumViewModel;
            ValidationResult result = null;

            var albumNameList = db.albums.Where(a => (a.albumName == album.AlbumName) && (a.artist_id == album.ArtistID));
            bool duplicateGenre = albumNameList.Any();

            if (duplicateGenre)
            {
                result = new ValidationResult("This Album already exists!");
            }

            return result;
        }
    }
}