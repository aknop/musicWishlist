using System.ComponentModel.DataAnnotations;
using MusicLibrary.Filter;

namespace MusicLibrary.Models
{
    [DuplicateGenre]
    public class GenreViewModel
    {
        [StringLength(20)]
        [Required(ErrorMessage ="Please enter a valid Genre")]
        public string GenreName { get; set; }
        public int GenreID { get; set; }

        public genre FromModel()
        {
            return new genre
            {
                genreName = GenreName,
                id = GenreID
            };
        }
        public void ToModel(genre g)
        {
            GenreName = g.genreName;
            GenreID = g.id;
        }
    }
}