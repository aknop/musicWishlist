using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MusicLibrary.Filter
{
    public class DuplicateArtistAttribute : ValidationAttribute
    {
        private TrueEntities db = new TrueEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var artistName = value as string;
            ValidationResult result = null;

            var artistNameList = db.artists.Where(g => g.artistName == artistName);
            bool duplicateGenre = artistNameList.Any();

            if (duplicateGenre)
            {
                result = new ValidationResult("This Artist already exists!");
            }

            return result;
        }
    }
}