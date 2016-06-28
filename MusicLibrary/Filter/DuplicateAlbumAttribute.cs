using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Filter
{
    public class DuplicateAlbumAttribute : ValidationAttribute
    {
        private TrueEntities db = new TrueEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var albumName = value as string;
            ValidationResult result = null;

            var genreNameList = db.genres.Where(g => g.genreName == albumName);
            bool duplicateGenre = genreNameList.Any();

            if (duplicateGenre)
            {
                result = new ValidationResult("This Genre already exists!");
            }

            return result;
        }
    }
}