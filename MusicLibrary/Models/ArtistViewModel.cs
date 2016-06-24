using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicLibrary.Models
{
    public class ArtistViewModel
    {
        public string ArtistName { get; set; }
        public int ArtistID { get; set; }
        public IEnumerable<SongsViewModel> SongList { get; set; }

    }
}