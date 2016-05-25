using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicLibrary.Models
{
    public class AlbumViewModel
    {
        public string AlbumName { get; set; }
        public IEnumerable<SongsViewModel> SongList { get; set; }
    }
}