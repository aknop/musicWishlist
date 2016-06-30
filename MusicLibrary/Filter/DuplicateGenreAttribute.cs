using System.ComponentModel.DataAnnotations;
using System.Linq;
using MusicLibrary.Models;
namespace MusicLibrary.Filter
{
    public class DuplicateGenreAttribute : ValidationAttribute
    {
        private TrueEntities db = new TrueEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var genreName = value as GenreViewModel;
            ValidationResult result = null;

            var genreNameList = db.genres.Where(g => g.genreName == genreName.GenreName);
            bool duplicateGenre = genreNameList.Any();
            
            if (duplicateGenre)
            {
                result = new ValidationResult("This Genre already exists!");
            }
                     
            return result;
            
            //Json add-on
            //private readonly IJsonResponseFactory _jsonResponseFactory;

            //public DuplicateExceptionHandlerAttribute()
            //{
            //    _jsonResponseFactory = ServiceLocator.Current.GetInstance<IJsonResponseFactory>();
            //}
            
        }
    }
}