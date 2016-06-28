using System.ComponentModel.DataAnnotations;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Filter
{
    public class DuplicateGenreAttribute : ValidationAttribute
    {
        private TrueEntities db = new TrueEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var genreName = value as string;
            ValidationResult result = null;

            var genreNameList = db.genres.Where(g => g.genreName == genreName);
            bool duplicateGenre = genreNameList.Any();
            
            if (duplicateGenre)
            {
                result = new ValidationResult("This Genre already exists!");
            }
                     
            return result;
            
            //private readonly IJsonResponseFactory _jsonResponseFactory;

            //public DuplicateExceptionHandlerAttribute()
            //{
            //    _jsonResponseFactory = ServiceLocator.Current.GetInstance<IJsonResponseFactory>();
            //}
            
        }
    }
}