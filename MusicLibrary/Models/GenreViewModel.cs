using System.ComponentModel.DataAnnotations;


namespace MusicLibrary.Models
{
    public class GenreViewModel
    {
        [StringLength(20)]
        [Required]
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