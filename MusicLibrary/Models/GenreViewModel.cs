using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicLibrary.Models
{
    public class GenreViewModel
    {
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